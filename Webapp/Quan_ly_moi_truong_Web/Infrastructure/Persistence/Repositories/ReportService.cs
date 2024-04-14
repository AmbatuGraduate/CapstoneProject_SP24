using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Domain.Entities.Report;
using Domain.Enums;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ReportService : IReportService
    {
        private readonly Func<GoogleCredential, GmailService> _gmailServiceFactory;
        private readonly WebDbContext context;

        public ReportService(Func<GoogleCredential, GmailService> gmailServiceFactory, WebDbContext webDbContext)
        {
            _gmailServiceFactory = gmailServiceFactory;
            context = webDbContext;
        }

        // add report to db
        public async Task AddReport(Reports report)
        {
            await context.Reports.AddAsync(report);
            await context.SaveChangesAsync();
        }

        // create report
        public async Task<ReportFormat> CreateReport(ReportFormat reportFormat)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(reportFormat.AccessToken);
                var service = _gmailServiceFactory(credential);

                // report format
                var emailBody = new StringBuilder();
                /*                emailBody.AppendLine("Issuer Email: " + reportFormat.IssuerEmail);
                                emailBody.AppendLine("Report Subject: " + reportFormat.ReportSubject);*/
                emailBody.Append("Report ID: " + reportFormat.Id + "\n");
                emailBody.Append("");
                emailBody.AppendLine(reportFormat.ReportBody);
                emailBody.Append("");
                emailBody.AppendLine("Issue Location: " + reportFormat.IssueLocation);
                emailBody.Append("");
                emailBody.AppendLine("Expected Resolution Date: " + reportFormat.ExpectedResolutionDate);
                emailBody.Append("");
                emailBody.AppendLine("Report Impact: " + reportFormat.ReportImpact);
               

                // Create report
                var email = new MailMessage
                {
                    From = new MailAddress(reportFormat.IssuerEmail),
                    Subject = $"[Report] {reportFormat.ReportSubject}",
                    Body = emailBody.ToString(),
                };
                email.To.Add("ambatuadmin@vesinhdanang.xyz");
                // add images
                if (reportFormat.ReportImages != null)
                {
                    foreach (var reportImage in reportFormat.ReportImages)
                    {
                        string base64Data = reportImage.Split(",")[1];
                        string mimeType = reportImage.Split(",")[0].Split(";")[0].Split(":")[1];
                        string extension = mimeType.Split("/")[1];

                        byte[] imageBytes = Convert.FromBase64String(base64Data);
                        var attachment = new Attachment(new MemoryStream(imageBytes), $"image.{extension}", mimeType);
                        email.Attachments.Add(attachment);
                    }
                }

                // send report
                ReportFormat r = await SendEmail(email, reportFormat.AccessToken);

                // add to db

                r.IssuerEmail = reportFormat.IssuerEmail;
                return r;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Notification to mail
        public async Task<ReportFormat> CreateNotification(ReportFormat reportFormat, string address)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(reportFormat.AccessToken);
                var service = _gmailServiceFactory(credential);

                // report format
                var emailBody = new StringBuilder();
                /*                emailBody.AppendLine("Issuer Email: " + reportFormat.IssuerEmail);
                                emailBody.AppendLine("Report Subject: " + reportFormat.ReportSubject);*/
                emailBody.Append("Report ID: " + reportFormat.Id + "\n");
                emailBody.Append("");
                emailBody.AppendLine(reportFormat.ReportBody);

                // Create report
                var email = new MailMessage
                {
                    From = new MailAddress(reportFormat.IssuerEmail),
                    Subject = $"{reportFormat.ReportSubject}",
                    Body = emailBody.ToString(),
                };
                email.CC.Add(address);
                ReportFormat r = await SendEmail(email, reportFormat.AccessToken);

                // add to db

                r.IssuerEmail = reportFormat.IssuerEmail;
                return r;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // get report by id
        public async Task<ReportFormat> GetReportById(string accessToken, string id)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);
            var service = _gmailServiceFactory(credential);

            var messages = await GetMessages(service);
            var messageDetails = await GetMessageDetails(service, messages);

            ReportFormat reportFormat = null;

            foreach (var messageDetail in messageDetails)
            {
                var body = GetBodyFromMessageDetail(messageDetail);
                if (body == null)
                {
                    continue;
                }

                var reportID = GetReportIdFromBody(body);

                if (ReportExist(reportID) && reportID == id)
                {
                    var reportDb = context.Reports.FirstOrDefault(e => e.ReportId == id);

                    reportFormat = CreateReportFormat(reportID, messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value, messageDetail, body, reportDb);

                    // Add images to the report format
                    await AddImageToReportFormat(service, messageDetail, reportFormat);

                    break;
                }
            }

            return reportFormat;
        }

        // get all reports id from db
        public List<Reports> GetAllReports()
        {
            return context.Reports.ToList();
        }

        // get all report
        public async Task<List<ReportFormat>> GetReportFormats(string accessToken)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);
            var service = _gmailServiceFactory(credential);

            var messages = await GetMessages(service);
            var messageDetails = await GetMessageDetails(service, messages);

            var reportFormats = new List<ReportFormat>();

            foreach (var messageDetail in messageDetails)
            {
                var body = GetBodyFromMessageDetail(messageDetail);
                if (body == null)
                {
                    continue;
                }

                var reportID = GetReportIdFromBody(body);

                if (reportID != null)
                {
                    var reportDb = await context.Reports.FirstOrDefaultAsync(e => e.ReportId == reportID);

                    if (reportDb != null)
                    {
                        var reportFormat = CreateReportFormat(reportID, messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value, messageDetail, body, reportDb);

                        await AddImageToReportFormat(service, messageDetail, reportFormat);

                        reportFormats.Add(reportFormat);
                    }
                }
            }

            return reportFormats;
        }

        // delete report
        public async Task<Reports> DeleteReport(string id)
        {
            // delete report from db
            var report = await context.Reports.FirstOrDefaultAsync(e => e.ReportId == id);
            context.Reports.Remove(report);
            await context.SaveChangesAsync();

            return report;
        }   

        public async Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _gmailServiceFactory(credential);

                var response = await GetMessages(service);

                var reportFormats = new List<ReportFormat>();

                List<Reports> list = GetReportsByUser(gmail);

                if (list.Count == 0)
                {
                    return reportFormats;
                }

                var messageDetails = await GetMessageDetails(service, response);

                foreach (var messageDetail in messageDetails)
                {
                    var body = GetBodyFromMessageDetail(messageDetail);
                    if (body == null)
                    {
                        continue;
                    }

                    var reportID = GetReportIdFromBody(body);

                    var reportDb = context.Reports.FirstOrDefault(e => e.ReportId == reportID);

                    if (reportDb != null && reportDb.IssuerGmail == gmail)
                    {
                        var reportFormat = CreateReportFormat(reportID, gmail, messageDetail, body, reportDb);
                        await AddImageToReportFormat(service, messageDetail, reportFormat);

                        reportFormats.Add(reportFormat);
                    }
                }

                return reportFormats;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<ReportFormat> ReponseReport(string accessToken, string reportID, string response, ReportStatus reportStatus)
        {
            // get request report
            var report = context.Reports.FirstOrDefault(e => e.ReportId == reportID);

            report.ResponseId = response;
            report.ActualResolutionDate = DateTime.Now;
            report.Status = reportStatus;

            // update in db
            context.Reports.Update(report);
            context.SaveChanges();

            // notify user
            // ...

            return Task.FromResult(new ReportFormat { Id = reportID, ReportResponse = response, IssuerEmail = report.IssuerGmail });
        }

        // ---------------------------------- Helper Methods ----------------------------------

        // send email
        private async Task<ReportFormat> SendEmail(MailMessage email, string accessToken)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _gmailServiceFactory(credential);

                var mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(email);
                var raw = Convert.ToBase64String(Encoding.UTF8.GetBytes(mimeMessage.ToString()))
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

                var message = new Message { Raw = raw };

                var request = service.Users.Messages.Send(message, "me");
                var response = await request.ExecuteAsync();
                ReportFormat returnId = new ReportFormat
                {
                    Id = response.Id
                };

                return returnId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // check if report exist in db
        private bool ReportExist(string id)
        {
            return context.Reports.Any(e => e.ReportId == id);
        }

        // get all reports by user email from db
        private List<Reports> GetReportsByUser(string gmail)
        {
            return context.Reports.Where(e => e.IssuerGmail == gmail).ToList();
        }

        private async Task<IList<Message>> GetMessages(GmailService service)
        {
            var request = service.Users.Messages.List("me");
            request.Q = "subject:Report";
            var response = await request.ExecuteAsync();
            return response.Messages;
        }

        // get message details
        private static async Task<Message[]> GetMessageDetails(GmailService service, IList<Message> messages)
        {
            var messageDetailsTasks = messages.Select(message =>
                service.Users.Messages.Get("me", message.Id).ExecuteAsync());

            return await Task.WhenAll(messageDetailsTasks);
        }

        // get body from message detail
        private static string GetBodyFromMessageDetail(Message messageDetail)
        {
            string data = messageDetail.Payload.Body.Data;
            if (data == null && messageDetail.Payload.Parts != null)
            {
                foreach (var part in messageDetail.Payload.Parts)
                {
                    if (part.MimeType == "text/plain")
                    {
                        data = part.Body.Data;
                        break;
                    }
                }
            }

            if (data == null)
            {
                return null;
            }

            var base64Url = data;
            var base64 = base64Url.Replace('-', '+').Replace('_', '/');
            var bodyBytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bodyBytes);
        }

        // get report id from body
        private static string GetReportIdFromBody(string body)
        {
            var reportIDMatch = Regex.Match(body, @"Report ID: (.*?)(\r\n|\n)");
            var reportID = reportIDMatch.Success ? reportIDMatch.Groups[1].Value.Trim() : null;
            return reportID;
        }

        // create report format
        private static ReportFormat CreateReportFormat(string reportID, string gmail, Message messageDetail, string body, Reports reportDb)
        {
            return new ReportFormat
            {
                Id = reportID,
                IssuerEmail = gmail,
                ReportSubject = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value,
                ReportBody = body,
                IssueLocation = Regex.Match(body, @"Issue Location: (.*?)(\r\n|\n)").Groups[1].Value.Trim(),
                ReportStatus = reportDb.Status.ToString(),
                ReportImpact = reportDb.ReportImpact,
                ExpectedResolutionDate = reportDb.ExpectedResolutionDate,
                ActualResolutionDate = reportDb.ActualResolutionDate,
                ReportResponse = reportDb.ResponseId
            };
        }

        // add image to report format
        private static async Task AddImageToReportFormat(GmailService service, Message messageDetail, ReportFormat reportFormat)
        {
            if (messageDetail.Payload.Parts == null)
            {
                return;
            }

            reportFormat.ReportImages = new List<string>(); 

            foreach (var part in messageDetail.Payload.Parts)
            {
                if (part.MimeType.StartsWith("image/")) 
                {
                    string imageBase64;
                    if (part.Body.Data != null)
                    {
                        var imageData = part.Body.Data;
                        imageBase64 = imageData.Replace('-', '+').Replace('_', '/');
                    }
                    else
                    {
                        var attachPart = service.Users.Messages.Attachments.Get("me", messageDetail.Id, part.Body.AttachmentId);
                        var attachPartResponse = await attachPart.ExecuteAsync();
                        imageBase64 = attachPartResponse.Data.Replace('-', '+').Replace('_', '/');
                    }

                    // create a data url
                    var photoUrl = $"data:{part.MimeType};base64,{imageBase64}";

                    reportFormat.ReportImages.Add(photoUrl);
                }
            }
        }

    }
}
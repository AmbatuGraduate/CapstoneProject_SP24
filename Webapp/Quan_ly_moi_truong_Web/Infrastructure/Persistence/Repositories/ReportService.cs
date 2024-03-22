
using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Domain.Entities.Report;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Org.BouncyCastle.Tls;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

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
        public void AddReport(Reports report)
        {
            context.Reports.Add(report);
            context.SaveChanges();
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

            // get email details
            var request = service.Users.Messages.List("me");
            request.Q = "subject:Report";
            var response = await request.ExecuteAsync();

            ReportFormat reportFormat = new();

            foreach (var message in response.Messages)
            {

                // get the details of the message
                var messageRequest = service.Users.Messages.Get("me", message.Id);
                var messageDetail = await messageRequest.ExecuteAsync();


                // extract id from body
                var base64Url = messageDetail.Payload.Body.Data;
                var base64 = base64Url.Replace('-', '+').Replace('_', '/');
                var bodyBytes = Convert.FromBase64String(base64);
                var body = Encoding.UTF8.GetString(bodyBytes);

                var reportIDMatch = Regex.Match(body, @"Report ID: (.*)");
                var reportID = reportIDMatch.Success ? reportIDMatch.Groups[1].Value.Trim() : null;

                // check if report id is in db
                if (ReportExist(reportID))
                {
                    // get report from db
                    var reportDb = context.Reports.FirstOrDefault(e => e.ReportId == reportID);

                    // report format
                    reportFormat = new ReportFormat
                    {
                        Id = reportID,
                        IssuerEmail = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value,
                        ReportSubject = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value,
                        ReportBody = body,
                        ReportStatus = reportDb.Status.ToString(),
                        ReportImpact = reportDb.ReportImpact,
                        ExpectedResolutionDate = reportDb.ExpectedResolutionDate,
                        ReportResponse = reportDb.ResponseId
                    };
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

            // Create a request to get report (subject)
            var request = service.Users.Messages.List("me");
            request.Q = "subject:Report";
            // get the response
            var response = await request.ExecuteAsync();

            var reportFormats = new List<ReportFormat>();

            foreach (var message in response.Messages)
            {

                // get the details of the message
                var messageRequest = service.Users.Messages.Get("me", message.Id);
                var messageDetail = await messageRequest.ExecuteAsync();

                // extract id from body
                var base64Url = messageDetail.Payload.Body.Data;
                var base64 = base64Url.Replace('-', '+').Replace('_', '/');
                var bodyBytes = Convert.FromBase64String(base64);
                var body = Encoding.UTF8.GetString(bodyBytes);

                var reportIDMatch = Regex.Match(body, @"Report ID: (.*)");
                var reportID = reportIDMatch.Success ? reportIDMatch.Groups[1].Value.Trim() : null;

                // check if report id is in db
                if (reportID != null && ReportExist(reportID))
                {
                    // get report from db
                    var reportDb = context.Reports.FirstOrDefault(e => e.ReportId == reportID);

                    // report format
                    var reportFormat = new ReportFormat
                    {
                        Id = reportID,
                        IssuerEmail = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value,
                        ReportSubject = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value,
                        ReportBody = body,
                        ReportStatus = reportDb.Status.ToString(),
                        ReportImpact = reportDb.ReportImpact,
                        ExpectedResolutionDate = reportDb.ExpectedResolutionDate,
                        ReportResponse = reportDb.ResponseId
                    };

                    reportFormats.Add(reportFormat);
                }
            }

            return reportFormats;
        }

        public async Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);
            var service = _gmailServiceFactory(credential);

            // Create a request to get report (subject)
            var request = service.Users.Messages.List("me");
            request.Q = "subject:Report";
            // get the response
            var response = await request.ExecuteAsync();

            var reportFormats = new List<ReportFormat>();

            // get matching email
            List<Reports> list = GetReportsByUser(gmail);
            var listIndex = 0;

            foreach (var message in response.Messages)
            {

                // get the details of the message
                var messageRequest = service.Users.Messages.Get("me", message.Id);
                var messageDetail = await messageRequest.ExecuteAsync();

                // extract id from body
                var base64Url = messageDetail.Payload.Body.Data;
                var base64 = base64Url.Replace('-', '+').Replace('_', '/');
                var bodyBytes = Convert.FromBase64String(base64);
                var body = Encoding.UTF8.GetString(bodyBytes);

                var reportID = list[listIndex].ReportId;
                // get report from db
                var reportDb = context.Reports.FirstOrDefault(e => e.ReportId == reportID);

                // report format
                var issuerEmail = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value;

                if (issuerEmail == gmail)
                {
                    var reportFormat = new ReportFormat
                    {
                        Id = reportID,
                        IssuerEmail = issuerEmail,
                        ReportSubject = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value,
                        ReportBody = body,
                        ReportStatus = reportDb.Status.ToString(),
                        ReportImpact = reportDb.ReportImpact,
                        ExpectedResolutionDate = reportDb.ExpectedResolutionDate,
                        ReportResponse = reportDb.ResponseId
                    };

                    reportFormats.Add(reportFormat);
                    listIndex++;
                } else
                {
                    continue;
                }

                if (listIndex == list.Count)
                {
                    break;
                }
               
            }

            return await Task.FromResult(reportFormats);
        }

        public Task<ReportFormat> ReponseReport(ReportFormat reportFormat)
        {
            throw new NotImplementedException();
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
    }
}
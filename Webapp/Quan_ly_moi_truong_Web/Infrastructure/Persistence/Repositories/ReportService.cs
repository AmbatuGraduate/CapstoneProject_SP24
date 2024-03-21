
using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Domain.Entities.Report;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Org.BouncyCastle.Tls;
using System.Net.Mail;
using System.Text;

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
                emailBody.AppendLine("Issuer Email: " + reportFormat.IssuerEmail);
                emailBody.AppendLine("Report Subject: " + reportFormat.ReportSubject);
                emailBody.AppendLine("Report Body: " + reportFormat.ReportBody);

                // Create report
                var email = new MailMessage
                {
                    From = new MailAddress(reportFormat.IssuerEmail),
                    Subject = $"[AmbatuReport] {reportFormat.ReportSubject} ({reportFormat.ReportStatus})",
                    Body = reportFormat.ReportBody
                };
                email.To.Add("ambatuadmin@vesinhdanang.xyz");
                ReportFormat r = await SendEmail(email, reportFormat.AccessToken);
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
            var request = service.Users.Messages.Get("me", id);
            var response = await request.ExecuteAsync();
            var reportFormat = new ReportFormat
            {
                Id = response.Id,

                IssuerEmail = response.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value,
                ReportSubject = response.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value,
                ReportBody = response.Snippet,
            };
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

            // get the response
            var response = await request.ExecuteAsync();

            var reportFormats = new List<ReportFormat>();

            foreach (var message in response.Messages)
            {
                // check if report id is in db
/*                if (ReportExist(message.Id))
                {*/
                    // Get the details of the message
                    var messageRequest = service.Users.Messages.Get("me", message.Id);
                    var messageDetail = await messageRequest.ExecuteAsync();

                    var reportFormat = new ReportFormat
                    {
                        Id = messageDetail.Id,
                        IssuerEmail = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value,
                        ReportSubject = messageDetail.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value,
                        ReportBody = messageDetail.Snippet,
                    };

                    reportFormats.Add(reportFormat);
/*                }
*/            }

            return reportFormats;
        }

        public async Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail)
        {
/*            // get all reports by user email from db
            var credential = GoogleCredential.FromAccessToken(accessToken);
            var service = _gmailServiceFactory(credential);*/

            List<Reports> list = GetReportsByUser(gmail);
            var reportFormats = new List<ReportFormat>();

            foreach (var report in list)
            {
                var reportFormat = await GetReportById(accessToken, report.ReportId);

                reportFormats.Add(reportFormat);
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
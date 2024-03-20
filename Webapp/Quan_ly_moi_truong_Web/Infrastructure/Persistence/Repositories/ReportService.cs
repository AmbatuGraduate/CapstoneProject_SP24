
using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
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

        public ReportService(Func<GoogleCredential, GmailService> gmailServiceFactory)
        {
            _gmailServiceFactory = gmailServiceFactory;
        }

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
                    Body = emailBody.ToString()
                };
                email.To.Add("ambatuadmin@vesinhdanang.xyz");
                ReportFormat r = await SendEmail(email, reportFormat.AccessToken);

                return r;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ReportFormat> GetReportById(string accessToken, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReportFormat>> GetReportFormats(string accessToken)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);
            var service = _gmailServiceFactory(credential);

            // Create a request to get report (subject)
            var request = service.Users.Messages.List("me");
            request.Q = "subject:AmbatuReport";

            // get the response
            var response = await request.ExecuteAsync();

            var reportFormats = new List<ReportFormat>();

            foreach (var message in response.Messages)
            {
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
            }

            return reportFormats;
        }

        public Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail)
        {
            throw new NotImplementedException();
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
                await request.ExecuteAsync();

                return new ReportFormat();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
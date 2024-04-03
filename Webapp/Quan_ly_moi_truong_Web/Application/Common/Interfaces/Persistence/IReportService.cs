using Application.Report.Common;
using Domain.Entities.Report;
using Domain.Enums;

namespace Application.Common.Interfaces.Persistence
{
    public interface IReportService
    {
        // google
        Task<ReportFormat> CreateReport(ReportFormat reportFormat);                                                       // create report

        Task<List<ReportFormat>> GetReportFormats(string accessToken);                                                    // list

        Task<ReportFormat> ReponseReport(string accessToken, string reportID, string response, ReportStatus reportSatus); // response report

        Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail);                                      // list by user

        Task<ReportFormat> GetReportById(string accessToken, string id);                                                  // get by id

        // db
        Task AddReport(Reports report);

        List<Reports> GetAllReports();
    }
}
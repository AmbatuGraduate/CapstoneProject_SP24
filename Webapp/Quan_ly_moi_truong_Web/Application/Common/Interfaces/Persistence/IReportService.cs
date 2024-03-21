

using Application.Report.Common;
using Domain.Entities.Report;

namespace Application.Common.Interfaces.Persistence
{
    public interface IReportService
    {

        // google
        Task<ReportFormat> CreateReport(ReportFormat reportFormat); // create report
        Task<List<ReportFormat>> GetReportFormats(string accessToken); // list
        Task<ReportFormat> ReponseReport(ReportFormat reportFormat);
        Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail); // list by user
        Task<ReportFormat> GetReportById(string accessToken, string id); // get by id

        // db
        void AddReport(Reports report);
        List<Reports> GetAllReports();
    }
}


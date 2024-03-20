

using Application.Report.Common;

namespace Application.Common.Interfaces.Persistence
{
    public interface IReportService
    {
        Task<ReportFormat> CreateReport(ReportFormat reportFormat);
        Task<List<ReportFormat>> GetReportFormats(string accessToken);
        Task<ReportFormat> ReponseReport(ReportFormat reportFormat);
        Task<List<ReportFormat>> GetReportsByUser(string accessToken, string gmail);
        Task<ReportFormat> GetReportById(string accessToken, string id);
    }
}


using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListLateReport
{
    public record ListLateReportQuery() : IRequest<ErrorOr<List<ReportResult>>>;
}
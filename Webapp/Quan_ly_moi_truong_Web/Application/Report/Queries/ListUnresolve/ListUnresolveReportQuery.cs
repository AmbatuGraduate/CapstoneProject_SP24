using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListUnresolve
{
    public record ListUnresolveReportQuery() : IRequest<ErrorOr<List<ReportResult>>>;
}
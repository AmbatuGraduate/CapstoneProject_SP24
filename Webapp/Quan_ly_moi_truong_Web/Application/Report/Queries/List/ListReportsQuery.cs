

using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.List
{
    public record ListReportsQuery(string accessToken) : IRequest<ErrorOr<List<ReportFormatRecord>>>;
}
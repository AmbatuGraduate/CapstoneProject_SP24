using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListByUser
{
    public record ListByEmailQuery(string accessToken, string email) : IRequest<ErrorOr<List<ReportFormatRecord>>>;
}
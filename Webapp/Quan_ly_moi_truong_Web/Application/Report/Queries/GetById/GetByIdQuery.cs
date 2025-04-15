using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.GetById
{
    public record GetByIdQuery(string accessToken, string id) : IRequest<ErrorOr<ReportFormatRecord>>;
}
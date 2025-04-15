using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListFromDb
{
    public record ListFromDbQuery : IRequest<ErrorOr<List<ReportResult>>>;
}
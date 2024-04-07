

using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Delete
{
    public record DeleteReportCommand(string ReportID) : IRequest<ErrorOr<Unit>>;
}

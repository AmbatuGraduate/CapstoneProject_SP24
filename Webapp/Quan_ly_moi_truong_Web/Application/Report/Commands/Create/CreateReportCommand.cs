

using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Create
{
    public record CreateReportCommand(
        string AccessToken,
        string IssuerEmail,
        string ReportSubject,
        string ReportBody

     ) : IRequest<ErrorOr<ReportFormatRecord>>;
}
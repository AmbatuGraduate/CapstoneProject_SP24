

using Application.Report.Common;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Response
{
    public record ReponseReportCommand
        (
            string AccessToken, 
            string ReportID,
            string Response,
            ReportStatus Status
            
        ) : IRequest<ErrorOr<ReportFormatRecord>>;
}

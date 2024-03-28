using Application.Report.Common;
using Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Commands.CreateNotification
{
    public record CreateNotificationCommand(
        string AccessToken,
        string IssuerEmail,
        DateTime ExpectedResolutionDate,
        ReportImpact ReportImpact
     ) : IRequest<ErrorOr<ReportFormatRecord>>;
}

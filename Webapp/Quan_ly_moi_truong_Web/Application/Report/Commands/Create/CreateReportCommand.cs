﻿using Application.Report.Common;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Create
{
    public record CreateReportCommand(
        string AccessToken,
        string IssuerEmail,
        string ReportSubject,
        string ReportBody,
        List<string>? ReportImages,
        DateTime ExpectedResolutionDate,
        ReportImpact ReportImpact

     ) : IRequest<ErrorOr<ReportFormatRecord>>;
}
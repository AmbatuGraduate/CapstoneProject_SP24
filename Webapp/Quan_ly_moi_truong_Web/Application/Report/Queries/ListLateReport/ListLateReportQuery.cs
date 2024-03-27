using Application.Report.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Queries.ListLateReport
{
    public record ListLateReportQuery(string accessToken) : IRequest<ErrorOr<List<ReportFormatRecord>>>;
}
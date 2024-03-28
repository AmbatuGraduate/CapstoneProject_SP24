using Application.Report.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Queries.ListUnresolve
{
    public record ListUnresolveReportQuery() : IRequest<ErrorOr<List<ReportResult>>>;
}

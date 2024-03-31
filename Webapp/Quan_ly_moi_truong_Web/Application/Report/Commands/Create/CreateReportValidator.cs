using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Commands.Create
{
    public class CreateReportValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
            RuleFor(x => x.IssuerEmail).NotEmpty();
            RuleFor(x => x.ReportBody).NotEmpty();
            RuleFor(x => x.ExpectedResolutionDate).NotEmpty();
            RuleFor(x => x.ReportImpact).NotEmpty();
            RuleFor(x => x.ReportSubject).NotEmpty();
        }
    }
}

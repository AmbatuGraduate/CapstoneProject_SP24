using FluentValidation;

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
            RuleFor(x => x.ReportSubject).NotEmpty();
            RuleFor(x => x.IssueLocation).NotEmpty();
        }
    }
}
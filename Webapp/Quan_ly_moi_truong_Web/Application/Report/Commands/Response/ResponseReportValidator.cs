using FluentValidation;

namespace Application.Report.Commands.Response
{
    public class ResponseReportValidator : AbstractValidator<ReponseReportCommand>
    {
        public ResponseReportValidator()
        {
            RuleFor(x => x.ReportID).NotEmpty();
            RuleFor(x => x.Response).NotEmpty();
        }
    }

}

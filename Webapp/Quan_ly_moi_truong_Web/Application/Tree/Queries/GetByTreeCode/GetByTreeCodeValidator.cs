using FluentValidation;

namespace Application.Tree.Queries.GetByTreeCode
{
    public class GetByTreeCodeValidator : AbstractValidator<GetByTreeCodeQuery>
    {
        public GetByTreeCodeValidator()
        {
            RuleFor(x => x.TreeCode).NotEmpty();
        }
    }
}
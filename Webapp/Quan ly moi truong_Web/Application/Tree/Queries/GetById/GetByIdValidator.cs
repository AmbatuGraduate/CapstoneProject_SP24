using FluentValidation;

namespace Application.Tree.Queries.GetById
{
    public class GetByIdValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdValidator()
        {
            RuleFor(x => x.TreeId).NotNull().NotEmpty();
        }
    }
}
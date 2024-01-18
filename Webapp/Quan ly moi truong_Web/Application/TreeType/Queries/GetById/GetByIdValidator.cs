using FluentValidation;

namespace Application.TreeType.Queries.GetById
{
    public class GetByIdValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdValidator()
        {
            RuleFor(x => x.TreeTypeId).NotEmpty();
        }
    }
}
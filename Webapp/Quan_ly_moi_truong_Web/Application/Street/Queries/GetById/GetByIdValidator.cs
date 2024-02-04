using FluentValidation;


namespace Application.Street.Queries.GetById
{
    public class GetByIdValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdValidator()
        {
            RuleFor(x => x.StreetId).NotNull().NotEmpty();
        }
    }
}

using FluentValidation;

namespace Application.Street.Commands.Delete
{
    public class DeleteStreetValidator : AbstractValidator<DeleteStreetCommand>
    {
        public DeleteStreetValidator()
        {
            RuleFor(x => x.StreetId).NotEmpty().NotNull();
        }
    }
}

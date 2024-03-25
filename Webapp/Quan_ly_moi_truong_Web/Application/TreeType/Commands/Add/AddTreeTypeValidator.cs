using FluentValidation;

namespace Application.TreeType.Commands.Add
{
    public class AddTreeTypeValidator : AbstractValidator<AddTreeTypeCommand>
    {
        public AddTreeTypeValidator()
        {
            RuleFor(x => x.TreeTypeName).NotEmpty();
        }
    }
}
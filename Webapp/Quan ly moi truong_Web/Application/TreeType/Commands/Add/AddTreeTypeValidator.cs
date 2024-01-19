using FluentValidation;

namespace Application.TreeType.Commands.Add
{
    public class AddTreeTypeValidator : AbstractValidator<AddTreeTypeCommand>
    {
        public AddTreeTypeValidator()
        {
            RuleFor(x => x.TreeTypeName).NotEmpty();
            RuleFor(x => x.UpdateBy).NotEmpty();
            RuleFor(x => x.CreateBy).NotEmpty();
        }
    }
}
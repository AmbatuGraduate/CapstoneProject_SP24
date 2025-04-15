using FluentValidation;

namespace Application.TreeType.Commands.Update
{
    public class UpdateTreeTypeValidator : AbstractValidator<UpdateTreeTypeCommand>
    {
        public UpdateTreeTypeValidator()
        {
            RuleFor(x => x.TreeTypeId).NotEmpty();
            RuleFor(x => x.TreeTypeName).NotEmpty();
        }
    }
}
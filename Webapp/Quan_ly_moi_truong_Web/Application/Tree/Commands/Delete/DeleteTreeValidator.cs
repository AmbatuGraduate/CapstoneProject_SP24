using FluentValidation;

namespace Application.Tree.Commands.Delete
{
    public class DeleteTreeValidator : AbstractValidator<DeleteTreeCommand>
    {
        public DeleteTreeValidator()
        {
            RuleFor(x => x.TreeCode).NotEmpty().NotNull();
        }
    }
}
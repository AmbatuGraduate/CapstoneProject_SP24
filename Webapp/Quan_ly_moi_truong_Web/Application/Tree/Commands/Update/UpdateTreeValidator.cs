using Application.Tree.Commands.Add;
using FluentValidation;

namespace Application.Tree.Commands.Update
{
    public class UpdateTreeValidator : AbstractValidator<AddTreeCommand>
    {
        public UpdateTreeValidator()
        {
            RuleFor(x => x.TreeCode).NotEmpty();
            RuleFor(x => x.TreeLocation).NotEmpty();
            RuleFor(x => x.BodyDiameter).NotEmpty();
            RuleFor(x => x.LeafLength).NotEmpty();
            RuleFor(x => x.PlantTime).NotEmpty();
            RuleFor(x => x.IntervalCutTime).NotEmpty();
        }
    }
}
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Add
{
    public class AddTreeValidator : AbstractValidator<AddTreeCommand>
    {
        public AddTreeValidator()
        {
            RuleFor(x => x.id).NotEmpty();
            RuleFor(x => x.bodyDiameter).NotEmpty();
            RuleFor(x => x.leafLength).NotEmpty();
            RuleFor(x => x.plantTime).NotEmpty();
            RuleFor(x => x.cutTime).NotEmpty();
            RuleFor(x => x.intervalCutTime).NotEmpty();

        }
    }
}

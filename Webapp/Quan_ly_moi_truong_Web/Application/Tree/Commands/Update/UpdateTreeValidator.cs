using Application.Tree.Commands.Add;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Update
{
    public class UpdateTreeValidator : AbstractValidator<AddTreeCommand>
    {
        public UpdateTreeValidator()
        {
            RuleFor(x => x.TreeCode).NotEmpty();
            RuleFor(x => x.StreetId).NotEmpty();
            RuleFor(x => x.BodyDiameter).NotEmpty();
            RuleFor(x => x.LeafLength).NotEmpty();
            RuleFor(x => x.PlantTime).NotEmpty();
            RuleFor(x => x.IntervalCutTime).NotEmpty();
        }
    }
}

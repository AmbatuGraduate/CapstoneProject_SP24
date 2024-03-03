using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Commands.Add
{
    public class AddCultivarValidator : AbstractValidator<AddCultivarCommand>
    {
        public AddCultivarValidator()
        {
            RuleFor(x => x.CultivarName).NotEmpty();
            RuleFor(x => x.CreateBy).NotEmpty();
            RuleFor(x => x.UpdateBy).NotEmpty();
        }
    }
}

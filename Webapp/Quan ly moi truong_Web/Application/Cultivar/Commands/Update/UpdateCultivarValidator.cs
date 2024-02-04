using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Commands.Update
{
    public class UpdateCultivarValidator : AbstractValidator<UpdateCultivarCommand>
    {
        public UpdateCultivarValidator()
        {
            RuleFor(x => x.CultivarName).NotEmpty();
            RuleFor(x => x.UpdateBy).NotEmpty();
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Delete
{
    public class DeleteTreeValidator : AbstractValidator<DeleteTreeCommand>
    {
        public DeleteTreeValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}

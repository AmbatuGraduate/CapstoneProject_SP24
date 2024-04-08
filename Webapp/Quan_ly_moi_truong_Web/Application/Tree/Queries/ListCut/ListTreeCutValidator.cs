using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Queries.ListCut
{
    public class ListTreeCutValidator : AbstractValidator<ListTreeCutQuery>
    {
        public ListTreeCutValidator()
        {
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}

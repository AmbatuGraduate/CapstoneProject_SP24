using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Queries.GetByTreeCode
{
    public class GetByTreeCodeValidator : AbstractValidator<GetByTreeCodeQuery>
    {
        public GetByTreeCodeValidator()
        {
            RuleFor(x => x.TreeCode).NotEmpty();
        }
    }
}

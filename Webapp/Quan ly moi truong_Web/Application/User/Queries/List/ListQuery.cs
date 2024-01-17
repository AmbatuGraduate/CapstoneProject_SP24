
using Application.User.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.List
{
    public record ListQuery() : IRequest<ErrorOr<List<UserResult>>>;
        
}

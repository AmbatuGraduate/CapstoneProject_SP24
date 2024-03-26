using Application.User.Common.Group;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetGroup
{
    public record GetGroupByGroupEmailQuery(string accessToken, string groupEmail) : IRequest<ErrorOr<GroupResult>>;
}

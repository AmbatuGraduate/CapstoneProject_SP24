using Application.Group.Common;
using Application.User.Common.List;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Queries.GetAllMembersOfGroup
{
    public record GetAllMembersOfGroupQuery(string accessToken, string groupEmail) : IRequest<ErrorOr<List<GoogleUser>>>;
}

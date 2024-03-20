using Application.User.Common.Group;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetAllGroupsByUserEmail
{
    public record GetAllGroupsByUserEmailQuery(string accessToken, string userEmail) : IRequest<ErrorOr<List<GroupResult>>>;
}

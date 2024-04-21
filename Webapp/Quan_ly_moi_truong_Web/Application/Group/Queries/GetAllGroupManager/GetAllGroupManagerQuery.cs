using Application.User.Common.List;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Queries.GetAllGroupManager
{
    public record GetAllGroupManagerQuery(string accessToken) : IRequest<ErrorOr<List<GoogleUser>>>;
}

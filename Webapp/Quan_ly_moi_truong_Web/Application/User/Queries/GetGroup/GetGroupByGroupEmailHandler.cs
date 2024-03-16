using Application.Common.Interfaces.Persistence;
using Application.User.Common.Group;
using Application.User.Common.List;
using Application.User.Queries.GetGroup;
using Application.User.Queries.List;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetGroup
{
    public class GetGroupByGroupEmailHandler
    : IRequestHandler<GetGroupByGroupEmailQuery, ErrorOr<GroupResult>>
    {
        private readonly IUserRepository userRepository;

        public GetGroupByGroupEmailHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(GetGroupByGroupEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var groupResult = await userRepository.GetGoogleGroupByEmail(request.accessToken, request.groupEmail);
            return groupResult;
        }
    }
}

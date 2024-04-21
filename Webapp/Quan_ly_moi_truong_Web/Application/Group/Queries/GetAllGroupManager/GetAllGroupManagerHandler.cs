using Application.Common.Interfaces.Persistence;
using Application.Group.Queries.GetAllMembersOfGroup;
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
    public class GetAllGroupManagerHandler : IRequestHandler<GetAllGroupManagerQuery, ErrorOr<List<GoogleUser>>>
    {
        private readonly IGroupRepository groupRepository;

        public GetAllGroupManagerHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<List<GoogleUser>>> Handle(GetAllGroupManagerQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var usersResult = await groupRepository.GetAllGroupManager(request.accessToken);
            return usersResult;
        }
    }
}

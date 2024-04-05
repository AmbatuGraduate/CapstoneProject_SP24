using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using Application.Group.Queries.GetAllGroupsByUserEmail;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Queries.GetAllGroups
{
    public class GetAllGroupsHandler : IRequestHandler<GetAllGroupsQuery, ErrorOr<List<GroupResult>>>
    {
        private readonly IGroupRepository groupRepository;

        public GetAllGroupsHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<List<GroupResult>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var groupsResult = groupRepository.GetAllGroups();
            if(groupsResult != null)
            {
                return groupsResult.Select(group => new GroupResult
                {
                    Id = group.DepartmentId,
                    Name = group.DepartmentName,
                    Email = group.DepartmentEmail,
                    Description = group.Description,
                    AdminCreated = group.AdminCreated,
                    DirectMembersCount = group.DirectMembersCount,
                }).ToList();
            }
            return new List<GroupResult>();
        }
    }
}

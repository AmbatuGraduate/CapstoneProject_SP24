using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using Domain.Entities.Deparment;
using ErrorOr;
using MediatR;

namespace Application.Group.Commands.Add
{
    public class AddGroupHandler : IRequestHandler<AddGroupCommand, ErrorOr<GroupResult>>
    {
        private readonly IGroupRepository groupRepository;

        public AddGroupHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(AddGroupCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            await Task.CompletedTask;
            var addedGroup = await groupRepository.AddGoogleGroup(request.accessToken, request.group);
            if (addedGroup != null)
            {
                var addedGroupDB = new Departments()
                {
                    DepartmentId = addedGroup.Id,
                    DepartmentName = addedGroup.Name,
                    DepartmentEmail = addedGroup.Email,
                    Description = addedGroup.Description,
                    AdminCreated = addedGroup.AdminCreated,
                    DirectMembersCount = addedGroup.DirectMembersCount
                };
                result = groupRepository.AddGroupDB(addedGroupDB);
            }
            return result ? addedGroup : new GroupResult();
        }
    }
}
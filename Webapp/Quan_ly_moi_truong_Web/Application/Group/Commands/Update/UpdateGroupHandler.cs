using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using Domain.Entities.Deparment;
using ErrorOr;
using MediatR;

namespace Application.Group.Commands.Update
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, ErrorOr<GroupResult>>
    {
        private readonly IGroupRepository groupRepository;

        public UpdateGroupHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            await Task.CompletedTask;
            var updatedGroup = await groupRepository.UpdateGoogleGroup(request.accessToken, request.group);
            if (updatedGroup != null)
            {
                var updatedGroupDB = new Departments()
                {
                    DepartmentId = updatedGroup.Id,
                    DepartmentName = updatedGroup.Name,
                    DepartmentEmail = updatedGroup.Email,
                    Description = updatedGroup.Description,
                    AdminCreated = updatedGroup.AdminCreated,
                    DirectMembersCount = updatedGroup.DirectMembersCount
                };
                result = groupRepository.UpdateGroupDB(updatedGroupDB);
            }
            return result ? updatedGroup : new GroupResult();
        }
    }
}
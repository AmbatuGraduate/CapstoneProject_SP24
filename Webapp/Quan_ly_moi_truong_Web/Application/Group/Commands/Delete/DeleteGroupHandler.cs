using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace Application.Group.Commands.Delete
{
    public class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, ErrorOr<bool>>
    {
        private readonly IGroupRepository groupRepository;

        public DeleteGroupHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            await Task.CompletedTask;
            var addedGroup = await groupRepository.DeleteGoogleGroup(request.accessToken, request.groupEmail);

            result = groupRepository.DeleteGroupDB(request.groupEmail);

            return result;
        }
    }
}
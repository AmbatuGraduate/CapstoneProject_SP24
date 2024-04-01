using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using ErrorOr;
using MediatR;

namespace Application.Group.Queries.GetAllGroupsByUserEmail
{
    public class GetAllGroupsByUserEmailHandler : IRequestHandler<GetAllGroupsByUserEmailQuery, ErrorOr<List<GroupResult>>>
    {
        private readonly IGroupRepository groupRepository;

        public GetAllGroupsByUserEmailHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<List<GroupResult>>> Handle(GetAllGroupsByUserEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var groupsResult = await groupRepository.GetAllGoogleGroupByUserEmail(request.accessToken, request.userEmail);
            return groupsResult;
        }
    }
}
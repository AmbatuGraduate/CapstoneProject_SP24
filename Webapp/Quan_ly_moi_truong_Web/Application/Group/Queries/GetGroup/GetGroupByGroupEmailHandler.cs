using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using ErrorOr;
using MediatR;

namespace Application.Group.Queries.GetGroup
{
    public class GetGroupByGroupEmailHandler
    : IRequestHandler<GetGroupByGroupEmailQuery, ErrorOr<GroupResult>>
    {
        private readonly IGroupRepository groupRepository;

        public GetGroupByGroupEmailHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(GetGroupByGroupEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var groupResult = await groupRepository.GetGoogleGroupByEmail(request.accessToken, request.groupEmail);
            return groupResult;
        }
    }
}
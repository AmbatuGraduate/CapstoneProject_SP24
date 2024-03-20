using Application.Common.Interfaces.Persistence;
using Application.User.Common.Group;
using Application.User.Queries.GetGroup;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetAllGroupsByUserEmail
{
    public class GetAllGroupsByUserEmailHandler : IRequestHandler<GetAllGroupsByUserEmailQuery, ErrorOr<List<GroupResult>>>
    {
        private readonly IUserRepository userRepository;

        public GetAllGroupsByUserEmailHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<List<GroupResult>>> Handle(GetAllGroupsByUserEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var groupsResult = await userRepository.GetAllGoogleGroupByUserEmail(request.accessToken, request.userEmail);
            return groupsResult;
        }

    }
}

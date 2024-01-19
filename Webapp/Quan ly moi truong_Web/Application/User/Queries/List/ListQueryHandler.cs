using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using ErrorOr;
using MediatR;

namespace Application.User.Queries.List
{
    public class ListQueryHandler
        : IRequestHandler<ListQuery, ErrorOr<List<UserResult>>>
    {
        private readonly IUserRepository userRepository;

        public ListQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<List<UserResult>>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<UserResult> userResults = new List<UserResult>();
            var list = userRepository.GetAll();

            foreach (var ls in list)
            {
                userResults.Add(new UserResult(ls));
            }

            return userResults;
        }
    }
}
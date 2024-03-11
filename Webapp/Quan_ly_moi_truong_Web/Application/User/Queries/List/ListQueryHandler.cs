using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using ErrorOr;
using MediatR;

namespace Application.User.Queries.List
{
    public class ListQueryHandler
        : IRequestHandler<ListQueryGoogle, ErrorOr<List<GoogleUserRecord>>>
    {
        private readonly IUserRepository userRepository;

        public ListQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<List<GoogleUserRecord>>> Handle(ListQueryGoogle request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<GoogleUserRecord> userResults = new List<GoogleUserRecord>();
            var list = await userRepository.GetGoogleUsers(request.accessToken);

            if (list != null)
            {
                foreach (var user in list)
                {
                    userResults.Add(new GoogleUserRecord(user));
                }
            }

            return userResults;
        }
    }
}
using Application.Common.Interfaces.Persistence;
using Application.User.Common.List;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.User.Queries.GetByEmail
{
    public class GetByEmailHandler : IRequestHandler<GetByEmailQuery, ErrorOr<GoogleUserRecord>>
    {
        private readonly IUserRepository userRepository;

        public GetByEmailHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<GoogleUserRecord>> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var user = userRepository.GetGoogleUserByEmail(request.accessToken, request.UserEmail);

            if (user == null)
            {
                return Errors.GetUserById.getUserFail;
            }

            return new GoogleUserRecord(user.Result);
        }
    }
}
using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Entities.User;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login
{
    public class LoginQueryHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // Check if user already exist
            //if (userRepository.GetUserByPhone(query.Phone) is not Users user)
            //    return Errors.Authentication.InvalidCredentials;

            // Valid the password is correct
            //if (user.Password != query.Password)
                return new[] { Errors.Authentication.InvalidCredentials };

            // Create JWT token
        //    var token = jwtTokenGenerator.GenerateToken(user);

        //    return new AuthenticationResult(
        //                user,
        //                token
        //            );
        }
    }
}
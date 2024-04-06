using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.GoogleAuthentication.Common;
using Domain.Common.Errors;
using ErrorOr;
using Google.Apis.Auth;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleRefresh
{
    public class GoogleRefreshHandler :
                IRequestHandler<GoogleRefreshQuery, ErrorOr<GoogleAuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IGroupRepository groupRepository;

        public GoogleRefreshHandler(IJwtTokenGenerator jwtTokenGenerator,
            IAuthenticationService authenticationService,
            IUserRefreshTokenRepository userRefreshTokenRepository,
            IRoleRepository roleRepository,
            IGroupRepository groupRepository,
            IUserRepository userRepository)
        {
            this.authenticationService = authenticationService;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRefreshTokenRepository = userRefreshTokenRepository;

            this.roleRepository = roleRepository;
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<GoogleAuthenticationResult>> Handle(GoogleRefreshQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //Check jwt is not null
            if (request.jwt != null)
            {
                var userId = jwtTokenGenerator.DecodeTokenToGetUserId(request.jwt);
                var jwt_expire = jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(claim => claim.Type == "exp").Value;
                var refresh_tkn = userRefreshTokenRepository.GetRefreshRokenByUserId(userId);

                //Check refresh token is exist or not
                if (refresh_tkn != null)
                {
                    if (DateTime.Now.CompareTo(new DateTime(refresh_tkn.Expire)) <= 0)
                    {
                        if (DateTimeOffset.FromUnixTimeSeconds((long)Convert.ToDouble(jwt_expire)).CompareTo(DateTime.Now) <= 0)
                        {
                            System.Diagnostics.Debug.WriteLine("REFRESH : " + "NEW TOKEN");

                            var tokenData = await authenticationService.RefreshTokenWithGoogle(refresh_tkn.RefreshToken);

                            // get new payload
                            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.id_token);

                            //generate new jwt token authen
                            var user = userRepository.GetById(payload.Subject);

                            DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).LocalDateTime;

                            var userRole = roleRepository.GetRole(user.RoleId).RoleName;
                            var userDepartment = groupRepository.GetGroupDbById(user.DepartmentId).DepartmentName;

                            var token = jwtTokenGenerator.GenerateToken(payload.Subject, userRole, userDepartment, tokenData.access_token, date);

                            return new GoogleAuthenticationResult(payload.Subject, payload.Name, payload.Picture, date, payload.Email, token,userRole, userDepartment);
                        }
                    }
                }
            }

            return new[] { Errors.Authentication.ExpireRefreshToken };
        }
    }
}
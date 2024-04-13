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
                var expire_refresh = refresh_tkn.Expire / TimeSpan.TicksPerSecond;
                //Check refresh token is exist or not
                if (refresh_tkn != null)
                {
                    var now = (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalSeconds;
                    var expireDate = (int)(DateTimeOffset.FromUnixTimeSeconds((long)Convert.ToDouble(jwt_expire)).LocalDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalSeconds;

                    Console.WriteLine(now + " - " + expireDate + " - " + expire_refresh);

                    // Check refresh token is exist or not
                    if(now < refresh_tkn.Expire)
                    {
                        // Check if jwt is expire
                        if (now > (long)Convert.ToDouble(jwt_expire))
                        {

                            var tokenData = await authenticationService.RefreshTokenWithGoogle(refresh_tkn.RefreshToken);

                            // get new payload
                            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.id_token);

                            //generate new jwt token authen
                            var user = userRepository.GetById(payload.Subject);
                            var userRole = roleRepository.GetRole(user.RoleId).RoleName;
                            var getDepartment = groupRepository.GetGroupDbById(user.DepartmentId);
                            var userDepartment = (userRole != "Admin") ? getDepartment.DepartmentName : "Admin";
                            var userDepartmentEmail = (userRole != "Admin") ? getDepartment.DepartmentName : "Admin@vesinhdanang.xyz";

                            DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).LocalDateTime;

                            var token = jwtTokenGenerator.GenerateToken(payload.Subject, userRole, userDepartment, tokenData.access_token, date);

                            return new GoogleAuthenticationResult(payload.Subject, payload.Name, payload.Picture, date, payload.Email, token, userRole, userDepartment, userDepartmentEmail);

                        }
                    }
                }
            }

            return new[] { Errors.Authentication.ExpireRefreshToken };
        }
    }
}
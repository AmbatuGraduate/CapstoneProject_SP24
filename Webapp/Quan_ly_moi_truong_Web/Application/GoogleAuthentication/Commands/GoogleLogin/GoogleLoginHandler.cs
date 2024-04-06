using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.GoogleAuthentication.Common;
using Domain.Common.Errors;
using Domain.Entities.UserRefreshToken;
using ErrorOr;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.GoogleAuthentication.Commands.GoogleLogin
{
    public class GoogleLoginHandler :
        IRequestHandler<GoogleLoginCommand, ErrorOr<GoogleAuthenticationResult>>
    {

        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserRepository userRepository;
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IGroupRepository groupRepository;

        public GoogleLoginHandler(IJwtTokenGenerator jwtTokenGenerator, 
            IAuthenticationService authenticationService, 
            IUserRepository userRepository, 
            IUserRefreshTokenRepository userRefreshTokenRepository,
            IRoleRepository roleRepository,
            IGroupRepository groupRepository)
        {
            this.authenticationService = authenticationService;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRepository = userRepository;
            this.userRefreshTokenRepository = userRefreshTokenRepository;
            this.roleRepository = roleRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<GoogleAuthenticationResult>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tokenData = await authenticationService.AuthenticateWithGoogle(request.authCode);

            if (tokenData != null)
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.id_token);

                // kiểm tra xem có lấy được payload không
                if (payload != null)
                {

                    var user = userRepository.GetById(payload.Subject);

                    //Check if user is exist in DB
                    if (user == null)
                        return Errors.Authentication.InvalidCredentials;

                    //Kiểm tra hạn của id token
                    DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).LocalDateTime;
                    if (date.CompareTo(DateTime.Now) == 1 && payload.Issuer.Contains("accounts.google.com"))
                    {
                        var userRole = roleRepository.GetRole(user.RoleId).RoleName;
                        var userDepartment = groupRepository.GetGroupDbById(user.DepartmentId).DepartmentName;

                        var token = jwtTokenGenerator.GenerateToken(payload.Subject, userRole, userDepartment ,tokenData.access_token, date);

                        System.Diagnostics.Debug.WriteLine("tkn " + token);

                        var refreshToken = new UserRefreshTokens
                        {
                            UserRefreshTokenId = new Guid(),
                            UserId = payload.Subject,
                            RefreshToken = tokenData.refresh_token,
                            Expire = DateTime.Now.AddMonths(6).Ticks,
                            CreateAt = DateTime.Now
                        };

                        //Save refresh token to DB
                        userRefreshTokenRepository.AddRefreshRoken(refreshToken);

                        return new GoogleAuthenticationResult(payload.Subject, payload.Name, payload.Picture, date, payload.Email, token, userRole, userDepartment);
                    }
                }
            }

            return new[] { Errors.Authentication.InvalidCredentials };
        }
    }
}
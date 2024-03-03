using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Contract.Authentication;
using Contracts.Authentication;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Application.Session.Common;
using Application.Common.Interfaces.Authentication;

namespace API.Controllers
{

    // Update At: 17/02/2024
    // Update By: Dang Nguyen Khanh Vu
    // Changes:
    // - Thêm Google sign-in => lấy id token từ FE và kiểm tra id token đó valid không
    // - Thêm biến googelApiSettings => để thay vì ghi rõ ra các thông tin cần bảo mật
    // - Refresh vẫn còn bị lỗi và không rõ nguyên nhân nên id token sẽ tồn tại được 1 tiếng

    [Route("api/auth")]
    [AllowAnonymous]

    public class AuthenticationController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        //Chứa thông tin về google api
        private readonly GoogleApiSettings googelApiSettings;
        private readonly ISessionService _sessionService;



        public AuthenticationController(IMediator mediator, IMapper mapper, IOptions<GoogleApiSettings> googelApiSettings, ISessionService sessionService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.googelApiSettings = googelApiSettings.Value;
            _sessionService = sessionService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = mapper.Map<LoginQuery>(request);

            ErrorOr<AuthenticationResult> authResult = await mediator.Send(query);

            if (authResult.IsError
                && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }

            return authResult.Match(
                    authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }


        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] GoogleAuthRequest request, [FromServices] AuthenticationService authenticationService)
        {
            var idToken = await authenticationService.AuthenticateWithGoogle(request.AuthCode);



            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

            // kiểm tra xem có lấy được payload không
            if (payload != null)
            {
                // Chuyển đổi thời gian lấy từ payload dưới dạng giây chuyển qua datetime
                DateTime date = new DateTime(1974, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds((long)payload.ExpirationTimeSeconds);

                // - Kiểm tra payload còn hạn không
                // - Issuer (iss) thông thường sẽ là accounts.google.com or là https://accounts.google.com
                // - Audience (aud) thì sẽ là client Id 
                // => thông tin cụ thể về việc validate id token https://developers.google.com/identity/openid-connect/openid-connect#validatinganidtoken
                if (date.CompareTo(DateTime.Now) == 0 && payload.Issuer.Contains("accounts.google.com") && payload.Audience.ToString() == googelApiSettings.ClientId)

                    // Nếu như hợp lệ thì sẽ set cookie cho nó
                    // Ở đây phải set Secure và HttpOnly true để tránh việc có thể dùng JS lấy

                    Response.Cookies.Append("token_v2", idToken, new CookieOptions()
                    {
                        IsEssential = true,
                        Expires = new DateTime(1974, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds((long)payload.ExpirationTimeSeconds),
                        Secure = true,
                        HttpOnly = true,
                        Domain = "localhost",
                        SameSite = SameSiteMode.None
                    });
                System.Diagnostics.Debug.WriteLine("access: " + _sessionService.getAccessToken());

                return Ok(_sessionService.getAccessToken());
            }

            return BadRequest("Invalid ID token");
        }

        [HttpPost("googlelogout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token_v2");
            return Ok("User is logged out");
        }


        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh([FromBody] GoogleAuthRequest request)
        {

            //var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            //{

            //    ClientSecrets = new ClientSecrets
            //    {
            //        ClientId = "1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com",
            //        ClientSecret = "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"
            //    },
            //    Scopes = new[] { "https://localhost:7024/api/auth/google" }
            //});

            //var token = await flow.ExchangeCodeForTokenAsync("", request.AuthCode, "https://localhost:7024/api/auth/CallBack", CancellationToken.None);

            return Ok();
        }
    }

}
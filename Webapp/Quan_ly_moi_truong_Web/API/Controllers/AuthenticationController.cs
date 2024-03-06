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
using Application.Common.Interfaces.Authentication;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleLogin;
using Application.GoogleAuthentication.Queries.GoogleRefresh;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{

    // Update At: 17/02/2024
    // Update By: Dang Nguyen Khanh Vu
    // Changes:
    // - Thêm Google sign-in => lấy id token từ FE và kiểm tra id token đó valid không
    // - Thêm biến googelApiSettings => để thay vì ghi rõ ra các thông tin cần bảo mật
    // - Refresh vẫn còn bị lỗi và không rõ nguyên nhân nên id token sẽ tồn tại được 1 tiếng

    [EnableCors("AllowAllHeaders")]
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

        // [HttpPost("login")]
        // public async Task<IActionResult> Login(LoginRequest request)
        // {
        //     var query = mapper.Map<LoginQuery>(request);

        //     ErrorOr<AuthenticationResult> authResult = await mediator.Send(query);

        //     if (authResult.IsError
        //         && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        //     {
        //         return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        //     }

        //     return authResult.Match(
        //             authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
        //             errors => Problem(errors)
        //         );
        // }


        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] GoogleAuthRequest request)
        {
            var query = mapper.Map<GoogleLoginQuery>(request);

            ErrorOr<GoogleAuthenticationResult> authResult = await mediator.Send(query);

            if (authResult.IsError
                && authResult.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

            Response.Cookies.Append("u_tkn", authResult.Value.token, new CookieOptions()
            {
                IsEssential = true,
                Expires = authResult.Value.expire_in.AddDays(1),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });

            //Refresh nên được set ở 1 nơi an toàn
            Response.Cookies.Append("refresh_tkn", authResult.Value.refresh_tkn, new CookieOptions()
            {
                IsEssential = true,
                Expires = authResult.Value.expire_in.AddMonths(6),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });

            return authResult.Match(
                    authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }

        [HttpPost("googlelogout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("u_tkn");
            return Ok("User is logged out");
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh()
        {
            var tokenAuth = Request.Cookies["u_tkn"];
            var refresh_tkn = Request.Cookies["refresh_tkn"];

            var query = mapper.Map<GoogleRefreshQuery>((refresh_tkn, tokenAuth));

            ErrorOr<GoogleRefreshResult> authResult = await mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.ExpireRefreshToken)
                return Problem(statusCode: StatusCodes.Status404NotFound, title: authResult.FirstError.Description);

            Response.Cookies.Append("u_tkn", authResult.Value.token, new CookieOptions()
            {
                IsEssential = true,
                Expires = authResult.Value.expire_in.AddDays(1),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });

            return authResult.Match(
                    authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }
    }

}
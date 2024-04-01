
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
using Application.GoogleAuthentication.Queries.GoogleRefresh;
using Microsoft.AspNetCore.Cors;
using Application.GoogleAuthentication.Queries.GoogleRefreshMobile;
using Application.GoogleAuthentication.Commands.GoogleLogin;
using Microsoft.Net.Http.Headers;

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
        private readonly ISessionService _sessionService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthenticationController(IMediator mediator, IMapper mapper, ISessionService sessionService, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _sessionService = sessionService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] GoogleAuthRequest request)
        {
            var query = mapper.Map<GoogleLoginCommand>(request);

            ErrorOr<GoogleAuthenticationResult> authResult = await mediator.Send(query);

            if (authResult.IsError
                && authResult.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("u_tkn", authResult.Value.token, new CookieOptions()
            {
                IsEssential = true,
                Expires = authResult.Value.expire_in.AddDays(1),
                Secure = true,
                HttpOnly = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
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
            if( !_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("u_tkn", out var token))
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, title: "Cookie is null");
            }

            var query = mapper.Map<GoogleRefreshQuery>(token);

            ErrorOr<GoogleAuthenticationResult> authResult = await mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.ExpireRefreshToken)
                return Problem(statusCode: StatusCodes.Status404NotFound, title: authResult.FirstError.Description);


            _httpContextAccessor.HttpContext.Response.Cookies.Append("u_tkn", authResult.Value.token, new CookieOptions()
            {
                IsEssential = true,
                Expires = authResult.Value.expire_in.AddDays(1),
                Secure = true,
                HttpOnly = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
            });


            return authResult.Match(
                    authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }

        [HttpGet("RefreshMobile")]
        public async Task<IActionResult> RefreshMobile()
        {
            // declare accesstoken
            string refreshToken;

            var authHeader = Request.Headers["Authorization"];
            if (String.IsNullOrEmpty(authHeader))
            {
                return BadRequest("Authorization header is missing");
            }
            refreshToken = authHeader.ToString().Replace("Bearer ", "");

            var refreshQuery = new GoogleRefreshQueryMobile(refreshToken);

            System.Diagnostics.Debug.WriteLine("controller level: " + refreshQuery.refresh_tk);

            ErrorOr<GoogleRefreshResultMobile> authResult = await mediator.Send(refreshQuery);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.ExpireRefreshToken)
                return Problem(statusCode: StatusCodes.Status404NotFound, title: authResult.FirstError.Description);
            return Ok(authResult);
        }
    }

}
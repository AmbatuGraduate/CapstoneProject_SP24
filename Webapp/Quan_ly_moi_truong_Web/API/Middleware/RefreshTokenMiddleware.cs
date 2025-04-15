using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleRefresh;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;


namespace API.Middleware
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate next;

        public RefreshTokenMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMediator mediator)
        {


            if (!context.Request.Cookies.TryGetValue("u_tkn", out var token))
            {
                await next(context);
                return;
            }

            ErrorOr<GoogleAuthenticationResult> authResult = await mediator.Send(new GoogleRefreshQuery(token));

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.ExpireRefreshToken)
            {
                await next(context);
                return;
            }

            context.Response.Cookies.Append("u_tkn", authResult.Value.token, new CookieOptions()
            {
                IsEssential = true,
                Expires = authResult.Value.expire_in.AddDays(1),
                Secure = true,
                HttpOnly = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
            });

            await next(context);
        }
    }
}

using Application.GoogleAuthentication.Commands.GoogleLogin;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleRefresh;
using Contract.Authentication;
using Mapster;

namespace API.Mapping
{
    /// <summary>
    /// Config mapping if some field is different
    /// </summary>
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GoogleAuthRequest, GoogleLoginCommand>()
                  .Map(dest => dest.authCode, src => src.AuthCode);

            config.NewConfig<string, GoogleRefreshQuery>()
                  .MapWith(dest => new GoogleRefreshQuery(dest));

            config.NewConfig<GoogleAuthenticationResult, AuthenticationResponse>()
                  .MapWith(dest => new AuthenticationResponse(dest.name, dest.avatar, dest.email, dest.role, dest.department, dest.departmentEmail,dest.expire_in));



            config.NewConfig<GoogleRefreshResultMobile, AccessTokenResMobile>()
                    .Map(dest => dest.expires_in, src => src.expire_in)
                    .Map(dest => dest.token, src => src.token);
        }
    }
}
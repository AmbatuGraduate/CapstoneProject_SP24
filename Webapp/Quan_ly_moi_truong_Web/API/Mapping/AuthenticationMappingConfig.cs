
using Application.GoogleAuthentication.Commands.GoogleLogin;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleRefresh;
using Contract.Authentication;
using Contracts.Authentication;
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
                  .Map(dest => dest.jwt, src => src);


            config.NewConfig<GoogleAuthenticationResult, AuthenticationResponse>()
                  .Map(dest => dest.Name, src => src.name)
                  .Map(dest => dest.Image, src => src.avatar);

            config.NewConfig<GoogleRefreshResult, AuthenticationResponse>()
                  .Map(dest => dest.Name, src => src.name)
                  .Map(dest => dest.Image, src => src.avatar);

            config.NewConfig<GoogleRefreshResultMobile, AccessTokenResMobile>()
                    .Map(dest => dest.expires_in, src => src.expire_in)
                    .Map(dest => dest.token, src => src.token);
        }
    }
}
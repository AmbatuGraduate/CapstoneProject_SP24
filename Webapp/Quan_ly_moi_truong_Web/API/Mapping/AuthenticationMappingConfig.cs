using Application.Authentication.Queries.Login;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleLogin;
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
            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<GoogleAuthRequest, GoogleLoginQuery>()
                  .Map(dest => dest.authCode, src => src.AuthCode);

            config.NewConfig<(string, string), GoogleRefreshQuery>()
                  .MapWith(dest => new GoogleRefreshQuery(dest.Item1, dest.Item2));

            //config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            //    .Map(dest => dest, src => src.User)
            //    .Map(dest => dest.Phone, src => src.User.PhoneNumber);

            config.NewConfig<GoogleAuthenticationResult, AuthenticationResponse>()
                  .Map(dest => dest.Name, src => src.name)
                  .Map(dest => dest.Image, src => src.avatar);

            config.NewConfig<GoogleRefreshResult, AuthenticationResponse>()
                  .Map(dest => dest.Name, src => src.name)
                  .Map(dest => dest.Image, src => src.avatar);
        }
    }
}
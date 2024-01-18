using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
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

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User)
                .Map(dest => dest.Phone, src => src.User.PhoneNumber);
        }
    }
}
using Application.User.Commands.Add;
using Application.User.Commands.Udpate;
using Application.User.Common;
using Contract.User;
using Mapster;

namespace API.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, UpdateUserRequest), UpdateUserCommand>()
                .Map(dest => dest.Id, src => src.Item1)
                .Map(dest => dest, src => src.Item2);

            config.NewConfig<AddUserRequest, AddUserCommand>();

            config.NewConfig<UserResult, UserResponse>()
                .Map(dest => dest, src => src.user)
                .Map(dest => dest.Phone, src => src.user.PhoneNumber)
                .Map(dest => dest.Status, src => src.user.LockoutEnabled);

            config.NewConfig<GoogleUserRecord, GoogleUserResponse>()
                .Map(dest => dest.Email, src => src.googleUser.Email)
                .Map(dest => dest.Name, src => src.googleUser.Name)
                .Map(dest => dest.Picture, src => src.googleUser.Picture);
        }
    }
}
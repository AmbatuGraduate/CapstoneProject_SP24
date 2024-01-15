
using Application.User.Commands.Add;
using Application.User.Commands.Udpate;
using Application.User.Common;
using Contract.Authentication;
using Contract.User;
using Contracts.Authentication;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}

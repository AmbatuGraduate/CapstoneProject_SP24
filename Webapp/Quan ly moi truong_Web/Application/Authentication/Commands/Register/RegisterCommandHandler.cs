using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Domain.Entities;
using Domain.Common.Errors;
using Application.Authentication.Common;

namespace Application.Authentication.Commands.Register
{

    public class RegisterCommandHandler :
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IUserRepository userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Check if user already exist
            if (userRepository.GetUserByPhone(command.Phone) is not null)
            {
                return Errors.User.DuplicateUser;
            }


            // Create user (generate unique id) & persist to DB
            Users user = new Users
            {
                Name = command.Name,
                Address = command.Address,
                PhoneNumber = command.Phone,
                Password = command.Password,
                Role = command.Role,
                Image = command.Image
            };

            userRepository.Add(user);

            // Create JWT token
            var token = jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                        user,
                        token
                    );
        }
    }
}

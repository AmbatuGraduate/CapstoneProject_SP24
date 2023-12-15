using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.Add
{
    public class AddUserCommandHandler :
        IRequestHandler<AddUserCommand, ErrorOr<UserResult>>
    {

        private readonly IUserRepository userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<UserResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Check if user already exist
            if (userRepository.GetUserByPhone(request.Phone) is not null)
            {
                return Domain.Common.Errors.Errors.User.DuplicateUser;
            }


            // Create user (generate unique id) & persist to DB
            Users user = new Users
            {
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.Phone,
                Password = request.Password,
                Role = "Employee",
                Image = request.Image
            };

            userRepository.Add(user);

            return new UserResult(user);
        }
    }
}

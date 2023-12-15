using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.Udpate
{
    public class UpdateUserCommandHandler :
        IRequestHandler<UpdateUserCommand, ErrorOr<UserResult>>
    {

        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<UserResult>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Checking if user is null or not 
            if (userRepository.GetById(request.Id) is null)
            {
                return Errors.UpdateUser.UpdateUserFail;
            }

            //Update user
            Users user = new Users
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.Phone,
                Password = request.Password,
                Role = request.Role,
                Image = request.Image,
                LockoutEnabled = request.Status
            };

            userRepository.Update(user);

            return new UserResult(user);
            
        }
    }
}

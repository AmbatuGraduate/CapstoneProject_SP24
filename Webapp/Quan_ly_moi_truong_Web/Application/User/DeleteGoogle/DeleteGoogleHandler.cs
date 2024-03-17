using Application.Common.Interfaces.Persistence;
using Application.User.Commands.UpdateGoogle;
using Application.User.Common.Delele;
using Application.User.Common.UpdateUser;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.DeleteGoogle
{
    public class DeleteGoogleHandler : IRequestHandler<DeleteGoogleCommand, ErrorOr<DeleteGoogleUserRecord>>
    {
        private readonly IUserRepository userRepository;
        public DeleteGoogleHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<DeleteGoogleUserRecord>> Handle(DeleteGoogleCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var userResult = await userRepository.DeleteGoogleUser(request.accessToken, request.userEmail);

            return new DeleteGoogleUserRecord(userResult);
            // add to db
            // ...
        }
    }
}

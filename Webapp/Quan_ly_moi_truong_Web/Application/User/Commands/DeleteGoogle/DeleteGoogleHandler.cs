using Application.Common.Interfaces.Persistence;
using Application.User.Common.Delele;
using ErrorOr;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.User.Commands.DeleteGoogle
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
            var currentUserGroupEmail = userRepository.GetCurrentDepartmentOfUser(request.userEmail);

            var userResult = await userRepository.DeleteGoogleUser(request.accessToken, request.userEmail);

            if (userResult && !currentUserGroupEmail.IsNullOrEmpty())
            {
                await userRepository.RemoveUserFromGoogleGroup(request.accessToken, request.userEmail, currentUserGroupEmail);
                await userRepository.RemoveUserFromDBGroup(currentUserGroupEmail);
            }

            return new DeleteGoogleUserRecord(userResult);
        }
    }
}
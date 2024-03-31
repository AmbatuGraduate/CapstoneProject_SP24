using Application.Common.Interfaces.Persistence;
using Application.User.Common.Delele;
using ErrorOr;
using MediatR;

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

            var userResult = await userRepository.DeleteGoogleUser(request.accessToken, request.userEmail);

            return new DeleteGoogleUserRecord(userResult);
            // add to db
            // ...
        }
    }
}
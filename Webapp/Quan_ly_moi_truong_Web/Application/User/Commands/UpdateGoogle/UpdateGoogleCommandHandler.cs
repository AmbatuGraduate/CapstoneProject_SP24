

using Application.Common.Interfaces.Persistence;
using Application.User.Common.Add;
using Application.User.Common.UpdateUser;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.UpdateGoogle
{
    public class UpdateGoogleCommandHandler : IRequestHandler<UpdateGoogleCommand, ErrorOr<UpdateGoogleUserRecord>>
    {
        private readonly IUserRepository userRepository;

        public UpdateGoogleCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<UpdateGoogleUserRecord>> Handle(UpdateGoogleCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            UpdateGoogleUser updateGoogleUser = new UpdateGoogleUser
            {
                AccessToken = request.accessToken,
                Email = request.Email,
                Name = request.Name,
                FamilyName = request.FamilyName,
                Password = request.Password
            };

            var userResult = await userRepository.UpdateGoogleUser(updateGoogleUser);

            return new UpdateGoogleUserRecord(userResult);
            // add to db
            // ...
        }
    }
}

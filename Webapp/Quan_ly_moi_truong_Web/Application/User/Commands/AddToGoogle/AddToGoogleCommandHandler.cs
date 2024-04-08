using Application.Common.Interfaces.Persistence;
using Application.User.Common.Add;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.AddToGoogle
{
    public class AddToGoogleCommandHandler : IRequestHandler<AddToGoogleCommand, ErrorOr<AddGoogleUserRecord>>
    {
        private readonly IUserRepository userRepository;

        public AddToGoogleCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<AddGoogleUserRecord>> Handle(AddToGoogleCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            AddGoogleUser AddGoogleUser = new AddGoogleUser
            {
                AccessToken = request.accessToken,
                Email = request.Email,
                Name = request.Name,
                FamilyName = request.FamilyName,
                Password = request.Password,
                PhoneNumber = request.phone,
                Address = request.address,
                BirthDate = request.birthDate,
                DepartmentEmail = request.departmentEmail,
            };

            var userResult = await userRepository.AddGoogleUser(AddGoogleUser);
            return new AddGoogleUserRecord(userResult);
            // add to db
            // ...
        }
    }
}
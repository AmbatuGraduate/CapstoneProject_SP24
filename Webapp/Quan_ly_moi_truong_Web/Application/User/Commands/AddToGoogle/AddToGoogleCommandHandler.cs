using Application.Common.Interfaces.Persistence;
using Application.User.Common.Add;
using ErrorOr;
using MediatR;
using Microsoft.IdentityModel.Tokens;

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
                DepartmentEmail = request.departmentEmail,
                UserRole = request.userRole
            };

            var userResult = await userRepository.AddGoogleUser(AddGoogleUser);
            int directMembersCount = 0;
            if (userResult != null && !request.departmentEmail.IsNullOrEmpty())
            {
                directMembersCount = await userRepository.AddUserToGoogleGroup(AddGoogleUser);
            }
            if(directMembersCount > 0 && !request.departmentEmail.IsNullOrEmpty())
            {
                await userRepository.AddUserToDBGroup(request.departmentEmail, directMembersCount);
            }
            return new AddGoogleUserRecord(userResult);
        }
    }
}
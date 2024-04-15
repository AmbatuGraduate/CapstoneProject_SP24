using Application.Common.Interfaces.Persistence;
using Application.User.Common.UpdateUser;
using ErrorOr;
using MediatR;
using Microsoft.IdentityModel.Tokens;

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
            var currentUserGroupEmail = userRepository.GetCurrentDepartmentOfUser(request.Email);
            UpdateGoogleUser updateGoogleUser = new UpdateGoogleUser
            {
                AccessToken = request.accessToken,
                Email = request.Email,
                Name = request.Name,
                FamilyName = request.FamilyName,
                Password = request.Password,
                PhoneNumber = request.phone,
                Address = request.address,
                DepartmentEmail = request.departmentEmail,
                UserRole = request.userRole,
            };

            var userResult = await userRepository.UpdateGoogleUser(updateGoogleUser);

            if(userResult != null && !request.departmentEmail.IsNullOrEmpty() && !currentUserGroupEmail.IsNullOrEmpty() && !currentUserGroupEmail.Equals(request.departmentEmail) ) 
            {
                await userRepository.UpdateUserToGoogleGroup(updateGoogleUser, currentUserGroupEmail);
                await userRepository.UpdateUserToDBGroup(request.departmentEmail, currentUserGroupEmail);
            }

            return new UpdateGoogleUserRecord(userResult);
        }
    }
}
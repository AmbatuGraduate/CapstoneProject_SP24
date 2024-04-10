using Application.User.Common.UpdateUser;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.UpdateGoogle
{
    public record UpdateGoogleCommand
    (
        string accessToken,
        string Name,
        string FamilyName,
        string Email,
        string Password,
        string phone,
        string address,
        string departmentEmail
     ) : IRequest<ErrorOr<UpdateGoogleUserRecord>>;
}
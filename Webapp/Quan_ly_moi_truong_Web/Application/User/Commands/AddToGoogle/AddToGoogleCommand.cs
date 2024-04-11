using Application.User.Common.Add;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.AddToGoogle
{
    public record AddToGoogleCommand
    (
               string accessToken, string Name, string FamilyName, string Email,
                      string Password, string phone, string address, string departmentEmail, int userRole
        ) : IRequest<ErrorOr<AddGoogleUserRecord>>;
}
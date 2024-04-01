using Application.User.Common.Delele;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.DeleteGoogle
{
    public record DeleteGoogleCommand
    (
      string accessToken,
      string userEmail
    ) : IRequest<ErrorOr<DeleteGoogleUserRecord>>;
}
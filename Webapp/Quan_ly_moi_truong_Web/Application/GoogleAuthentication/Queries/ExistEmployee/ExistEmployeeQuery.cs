using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.ExistEmployee
{
    public record ExistEmployeeQuery(string Email) : IRequest<ErrorOr<bool>>;
}

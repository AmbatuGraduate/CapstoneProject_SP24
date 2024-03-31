using Application.User.Common.List;
using ErrorOr;
using MediatR;

namespace Application.User.Queries.GetByEmail
{
    public record GetByEmailQuery(string accessToken, string UserEmail) : IRequest<ErrorOr<GoogleUserRecord>>;
}
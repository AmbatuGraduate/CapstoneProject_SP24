using Application.User.Common;
using ErrorOr;
using MediatR;


namespace Application.User.Queries.List
{
    public record ListQueryGoogle(string accessToken) : IRequest<ErrorOr<List<GoogleUserRecord>>>;

}

using Domain.Entities.User;

namespace Application.User.Common
{
    public record UserResult(Users user);
    public record UserEventResult(Users user, string FullName);
}
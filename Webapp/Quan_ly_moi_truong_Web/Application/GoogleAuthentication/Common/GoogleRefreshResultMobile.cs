
namespace Application.GoogleAuthentication.Common
{
    public record GoogleRefreshResultMobile
    (
        long expire_in,
        string token
    );
}

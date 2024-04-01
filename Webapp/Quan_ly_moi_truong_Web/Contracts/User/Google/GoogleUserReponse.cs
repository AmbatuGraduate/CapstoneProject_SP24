namespace Contract.User.Google
{
    public record GoogleUserResponse
    (
         string Id,
         string Email,
         string Name,
         string Picture,
         string Department,
         string PhoneNumber,
         string Role
    );
}
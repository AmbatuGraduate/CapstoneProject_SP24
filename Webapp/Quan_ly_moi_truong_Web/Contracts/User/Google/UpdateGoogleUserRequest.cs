namespace Contract.User.Google
{
    public record UpdateGoogleUserRequest
    (
        string AccessToken,
        string Name,
        string FamilyName,
        string Email,
        string Password
        );
}

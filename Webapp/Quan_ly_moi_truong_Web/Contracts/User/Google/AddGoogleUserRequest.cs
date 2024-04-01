namespace Contract.User.Google
{
    public record AddGoogleUserRequest
(
    string Name,
    string FamilyName,
    string Email,
    string Password
    );
}
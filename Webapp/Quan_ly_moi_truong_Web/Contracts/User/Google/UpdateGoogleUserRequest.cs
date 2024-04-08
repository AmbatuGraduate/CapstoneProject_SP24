namespace Contract.User.Google
{
    public record UpdateGoogleUserRequest
    (
        string Name,
        string FamilyName,
        string Email,
        string Password,
        string phone,
        string address,
        DateTime birthDate,
        string departmentEmail
        );
}
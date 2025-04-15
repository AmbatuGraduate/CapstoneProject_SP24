namespace Contract.User.Google
{
    public record AddGoogleUserRequest
(
    string Name,
    string FamilyName,
    string Email,
    string Password,
    string phone,
    string address,
    string departmentEmail, 
    int userRole
    );
}
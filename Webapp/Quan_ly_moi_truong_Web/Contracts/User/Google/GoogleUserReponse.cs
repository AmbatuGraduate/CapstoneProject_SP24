namespace Contract.User.Google
{
    public record GoogleUserResponse
    (
         string Id,
         string Email,
         string Name,
         string Picture,
         string Department,
         string DepartmentEmail,
         string PhoneNumber,
         string Role,
         string address
    );
}
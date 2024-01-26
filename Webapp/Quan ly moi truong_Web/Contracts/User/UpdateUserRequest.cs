namespace Contract.User
{
    public record UpdateUserRequest
    (
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image,
        bool status
    );
}
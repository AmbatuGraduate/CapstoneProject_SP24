namespace Contract.User
{
    public record AddUserRequest
    (
        string Name, string Address,
        string Phone, string Password,
        string Image
    );
}
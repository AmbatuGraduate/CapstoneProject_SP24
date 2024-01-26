namespace Contract.User
{
    public record UserResponse
    (
        Guid id,
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image,
        bool Status
    );
}
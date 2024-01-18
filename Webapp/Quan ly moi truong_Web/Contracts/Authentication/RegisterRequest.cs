namespace Contracts.Authentication
{
    public record RegisterRequest
    (
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image
    );
}
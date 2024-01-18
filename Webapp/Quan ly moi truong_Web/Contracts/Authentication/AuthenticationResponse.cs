namespace Contract.Authentication
{
    public record AuthenticationResponse
    (
        Guid id,
        string Name, string Address,
        string Phone,
        string Role, string Image,
        string Token
    );
}
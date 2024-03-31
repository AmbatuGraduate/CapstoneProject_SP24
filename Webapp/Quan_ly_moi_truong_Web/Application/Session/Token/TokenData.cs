namespace Application.Session.Token
{
    public record TokenData
    (
        string access_token,
        long expires_in,
        string refresh_token,
        string scope,
        string token_type,
        string id_token
    );
}
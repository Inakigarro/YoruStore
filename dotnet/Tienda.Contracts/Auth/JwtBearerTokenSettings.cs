namespace Tienda.Contracts.Auth;

public class JwtBearerTokenSettings
{
    public string SecretKey { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpiryTimeinSecconds { get; set; }
}

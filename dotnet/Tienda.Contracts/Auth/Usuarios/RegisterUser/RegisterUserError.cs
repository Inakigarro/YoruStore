namespace Tienda.Contracts.Auth.Usuarios.RegisterUser;

public record RegisterUserError
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
}

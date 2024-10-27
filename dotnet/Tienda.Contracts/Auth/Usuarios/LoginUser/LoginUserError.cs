namespace Tienda.Contracts.Auth.Usuarios.LoginUser;

public record LoginUserError
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

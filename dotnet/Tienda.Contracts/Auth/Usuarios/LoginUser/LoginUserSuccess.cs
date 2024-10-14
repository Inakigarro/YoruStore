namespace Tienda.Contracts.Auth.Usuarios.LoginUser;

public record LoginUserSuccess
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

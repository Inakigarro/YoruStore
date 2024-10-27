namespace Tienda.Contracts.Auth.Usuarios.RegisterUser;

public record RegisterUserSuccess
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;

namespace Tienda.Contracts.Auth.Usuarios.LoginUser;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;

namespace Tienda.Contracts.Auth.Usuarios.RegisterUser;

public class RegisterUserRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;
}

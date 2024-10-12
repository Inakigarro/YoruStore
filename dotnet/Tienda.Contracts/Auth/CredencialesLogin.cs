using System.ComponentModel.DataAnnotations;

namespace Tienda.Contracts.Auth;

public class CredencialesLogin
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}

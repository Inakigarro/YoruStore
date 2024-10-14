using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tienda.Contracts.Auth;
using Tienda.Contracts.Auth.Usuarios;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(
    IOptions<JwtBearerTokenSettings> jwtSettings,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager): ControllerBase
{
    private readonly IOptions<JwtBearerTokenSettings> _jwtSettings = jwtSettings;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody]CrearUser userDetails)
    {
        // Si se envia una request vacia, BadRequest.
        if (userDetails == null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Registro de usuario fallido.",
            });
        }

        // Creo el usuario a registrar.
        var identityUser = new IdentityUser()
        {
            UserName = userDetails.UserName,
            Email = userDetails.Email,
        };
        var result = await _userManager.CreateAsync(identityUser, userDetails.Password);

        // Si falla la creacion, recolecto los posibles errores y devuelvo BadRequest.
        if (!result.Succeeded)
        {
            var dictionary = new Dictionary<string, string>();
            foreach(var error in result.Errors)
            {
                dictionary.Add(error.Code, error.Description);
            }
            
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Registro de usuario fallido.",
                Errors = dictionary 
            });
        }

        // Si la creacion es exitosa, Ok.
        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Usuario registrado exitosamente.",
        });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody]CredencialesLogin credenciales)
    {
        IdentityUser identityUser;

        // Si las credenciales son nulas o el usuario no es validado correctamente, BadRequest.
        if (credenciales is null || (identityUser = await ValidateUser(credenciales)) is null)
        {
            return BadRequest(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Login Failed."
            });
        }

        // Genero el token y devuelvo Ok.
        var token = await GenerateToken(identityUser);
        return Ok(new { Token = token, Message = "Has iniciado sesion" });
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole(string nombreRol)
    {
        // Busco el rol por su nombre. Si ya existe, devuelvo Ok.
        var role = await _roleManager.FindByNameAsync(nombreRol);

        if (role is not null)
        {
            return Ok(role);
        }

        // Si el rol no existe, lo creo y devuelvo Ok.
        role = new IdentityRole(nombreRol);
        await _roleManager.CreateAsync(role);
        return Ok(role);
    }

    [HttpPut]
    [Route("AssignRole")]
    public async Task<IActionResult> AssignRole(string nombreRol, string userName)
    {
        // Buscamos el usuario a asignar.
        var user = await _userManager.FindByNameAsync(userName)
            ?? throw new ArgumentNullException(nameof(userName), "No existe un usuario con el nombre proveido.");

        // Si el rol existe, asignamos el usuario a ese rol.
        var result = await _roleManager.RoleExistsAsync(nombreRol)
            ? await _userManager.AddToRoleAsync(user, nombreRol)
            : throw new ArgumentNullException(nameof(nombreRol), "No existe un rol con el nombre proveido.");

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Te has desconectado."
        });
    }
    
    private async Task<IdentityUser?> ValidateUser(CredencialesLogin credenciales)
    {
        // Busco el usuario. Si no es nulo y la contraseña es la correcta, devuelvo el usuario.
        var identityUser = await _userManager.FindByNameAsync(credenciales.UserName);
        if (identityUser is not null)
        {
            var result = _userManager.PasswordHasher.VerifyHashedPassword(
                identityUser, identityUser.PasswordHash!, credenciales.Password);
            return result == PasswordVerificationResult.Failed ? null : identityUser;
        }

        // Sino, devuelvo nulo.
        return null;
    }

    private async Task<string> GenerateToken(IdentityUser user)
    {
        // Inicializo el token handler y obtengo la clave de encriptacion.
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.SecretKey);

        // En base a los datos del usuario y algunos valores de configuracion.
        // Armo la base del token a utilizar.
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
            }),
            Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.Value.ExpiryTimeinSecconds),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _jwtSettings.Value.Audience,
            Issuer = _jwtSettings.Value.Issuer,
        };

        // Busco los roles del usuario para agregar al token.
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            tokenDescriptor.Subject.AddClaim(new(ClaimTypes.Role, role));
        }

        // Creo el token y lo devuelvo.
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

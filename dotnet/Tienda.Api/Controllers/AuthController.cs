using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tienda.Contracts.Auth;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(
    IOptions<JwtBearerTokenSettings> jwtSettings,
    UserManager<IdentityUser> userManager): ControllerBase
{
    private readonly IOptions<JwtBearerTokenSettings> _jwtSettings = jwtSettings;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody]CrearUser userDetails)
    {
        if (userDetails == null)
        {
            return new BadRequestObjectResult(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Registro de usuario fallido.",
            });
        }

        var identityUser = new IdentityUser()
        {
            UserName = userDetails.UserName,
            Email = userDetails.Email,
        };
        var result = await _userManager.CreateAsync(identityUser, userDetails.Password);
        if (!result.Succeeded)
        {
            var dictionary = new Dictionary<string, string>();
            foreach(var error in result.Errors)
            {
                dictionary.Add(error.Code, error.Description);
            }
            
            return new BadRequestObjectResult(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Registro de usuario fallido.",
                Errors = dictionary 
            });
        }

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

        if (credenciales is null || (identityUser = await ValidateUser(credenciales)) is null)
        {
            return new BadRequestObjectResult(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Login Failed."
            });
        }

        var token = GenerateToken(identityUser);
        return Ok(new { Token = token, Message = "Has iniciado sesion" });
    }

    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Te has desconectado."
        });
    }
    
    private async Task<IdentityUser> ValidateUser(CredencialesLogin credenciales)
    {
        var identityUser = await _userManager.FindByNameAsync(credenciales.UserName);
        if (identityUser is not null)
        {
            var result = _userManager.PasswordHasher.VerifyHashedPassword(
                identityUser, identityUser.PasswordHash!, credenciales.Password);
            return result == PasswordVerificationResult.Failed ? null : identityUser;
        }

        return null;
    }

    private string GenerateToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString())
            }),
            Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.Value.ExpiryTimeinSecconds),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _jwtSettings.Value.Audience,
            Issuer = _jwtSettings.Value.Issuer,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

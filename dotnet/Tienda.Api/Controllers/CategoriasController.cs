using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda.Contracts.Auth.Roles;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Services;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController(
    ICategoriasService categoriaService,
    ILogger<CategoriasController> logger) : ControllerBase
{
    private readonly ICategoriasService _categoriaService = categoriaService;
    private readonly ILogger<CategoriasController> _logger = logger;

    [HttpPost]
    [Route("CrearCategoria")]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = AuthRoles.Admin)]
    public async Task<ActionResult> CrearCategoria(CrearCategoriaDto nuevaCategoria, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"Creando una categoria con el nombre: {nuevaCategoria.Nombre}");
            var categoriaCreada = await this._categoriaService.CreateAsync(nuevaCategoria, cancellationToken);
            return Ok(categoriaCreada);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la creacion. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpPut]
    [Route("ActualizarCategoria")]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = AuthRoles.Admin)]
    public async Task<ActionResult> ActualizarCategoria(ActualizarCategoriaDto actualizarCategoria, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"Actualizando una categoria con el nombre: {actualizarCategoria.Nombre}");
            var categoria = await this._categoriaService.UpdateAsync(actualizarCategoria, cancellationToken);
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la actualizacion de la categoria. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpGet]
    [Route("ObtenerCategoriaPorId")]
    public async Task<ActionResult> ObtenerCategoriaPorId(Guid categoriaId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"Obteniendo la categoria correspondiente al Id: {categoriaId}");
            var categoria = await this._categoriaService.GetAsync(categoriaId, cancellationToken);
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la busqueda de la categoria con Id: {categoriaId}. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpGet]
    [Route("ObtenerCategorias")]
    public async Task<ActionResult> ObtenerCategorias(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Obteniendo todas las categorias");
            var categorias = await this._categoriaService.GetAllAsync(cancellationToken);
            return Ok(categorias);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la busqueda de las categorias: {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpDelete]
    [Route("BorrarCategoriaPorId")]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = AuthRoles.Admin)]
    public async Task<ActionResult> BorrarCategoriaPorId(Guid categoriaId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogWarning($"Borrando categoria correspondiente al Id: {categoriaId}");
            var categoria = await this._categoriaService.DeleteAsync(categoriaId, cancellationToken);
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la eliminacion de la categoria con Id: {categoriaId}. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }
}

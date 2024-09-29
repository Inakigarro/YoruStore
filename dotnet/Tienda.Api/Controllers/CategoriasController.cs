using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> CrearCategoria(CrearCategoriaDto nuevaCategoria)
    {
        try
        {
            _logger.LogInformation($"Creando una categoria con el nombre: {nuevaCategoria.Nombre}");
            var categoriaCreada = await this._categoriaService.Create(nuevaCategoria);
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
    public async Task<ActionResult> ActualizarCategoria(ActualizarCategoriaDto actualizarCategoria)
    {
        try
        {
            _logger.LogInformation($"Actualizando una categoria con el nombre: {actualizarCategoria.Nombre}");
            var categoria = await this._categoriaService.Update(actualizarCategoria);
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
    public async Task<ActionResult> ObtenerCategoriaPorId(Guid categoriaId)
    {
        try
        {
            _logger.LogInformation($"Obteniendo la categoria correspondiente al Id: {categoriaId}");
            var categoria = await this._categoriaService.Get(categoriaId);
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
    public async Task<ActionResult> ObtenerCategorias()
    {
        try
        {
            _logger.LogInformation("Obteniendo todas las categorias");
            var categorias = await this._categoriaService.GetAll();
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
    public async Task<ActionResult> BorrarCategoriaPorId(Guid categoriaId)
    {
        try
        {
            _logger.LogWarning($"Borrando categoria correspondiente al Id: {categoriaId}");
            var categoria = await this._categoriaService.Delete(categoriaId);
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

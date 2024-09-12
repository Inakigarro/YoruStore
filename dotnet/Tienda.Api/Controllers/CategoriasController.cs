using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Services;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriasService _categoriaService;
    private readonly ILogger<CategoriasController> _logger;
    private readonly IMapper _mapper;

    public CategoriasController(
        ICategoriasService categoriaService,
        ILogger<CategoriasController> logger,
        IMapper mapper)
    {
        _categoriaService = categoriaService;
        _logger = logger;
        _mapper = mapper;
    }

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

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tienda.Contracts.Items;
using Tienda.Contracts.Services;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController(
    ICategoriasService categoriasService,
    IItemsService itemsService,
    ILogger<ItemsController> logger,
    IMapper mapper) : ControllerBase
{
    private readonly ICategoriasService _categoriasService = categoriasService;
    private readonly IItemsService _itemsService = itemsService;
    private readonly ILogger<ItemsController> _logger = logger;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Route("AgregarItem")]
    public async Task<IActionResult> AgregarItem(CrearItemDto crearItem, Guid categoriaId)
    {
        try
        {
            var item = await this._itemsService.Create(crearItem, categoriaId);
            return Ok(item);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la creacion del Item. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpPut]
    [Route("ModificarItem")]
    public async Task<ActionResult> ModificarItem(ActualizarItemDto item)
    {
        try
        {
            _logger.LogInformation($"Actualizando el item con id: {item.Id}");
            var itemActualizado = await this._itemsService.Update(item);
            return Ok(itemActualizado);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la modificacion del Item. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpGet]
    [Route("ObtenerItemPorId")]
    public async Task<ActionResult> ObtenerItemPorId(Guid itemId)
    {
        try
        {
            _logger.LogInformation($"Obteniendo el item con id: {itemId}");
            var item = await this._itemsService.Get(itemId);
            return Ok(item);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la busqueda del Item. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }

    [HttpGet]
    [Route("ObtenerItems")]
    public async Task<ActionResult> ObtenerItems()
    {
        try
        {
            _logger.LogInformation("Obteniendo todos los items.");
            var categorias = await this._itemsService.GetAll();
            return Ok(categorias);
        }
        catch (Exception ex)
        {
            string error = $"Ocurrio un error durante la busqueda de todos los items. {ex.Message} - {ex.StackTrace}";
            _logger.LogError(error);
            return BadRequest(error);
        }
    }
}

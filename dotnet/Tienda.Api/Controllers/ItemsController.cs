using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tienda.Contracts.Items;
using Tienda.Contracts.Services;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ICategoriasService _categoriasService;
    private readonly IItemsService _itemsService;
    private readonly ILogger<ItemsController> _logger;
    private readonly IMapper _mapper;

    public ItemsController(
        ICategoriasService categoriasService,
        IItemsService itemsService,
        ILogger<ItemsController> logger,
        IMapper mapper)
    {
        _categoriasService = categoriasService;
        _itemsService = itemsService;
        _logger = logger;
        _mapper = mapper;
    }

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

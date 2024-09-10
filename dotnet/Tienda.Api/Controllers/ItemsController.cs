using Microsoft.AspNetCore.Mvc;
using Tienda.Domain;

namespace Tienda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetItems")]
    public IEnumerable<Item> Get()
    {
        Categoria categoria1 = new Categoria(Guid.NewGuid());
        categoria1.SetNombre("Medias");
        var item1 = new Item(Guid.NewGuid());
        item1.SetTitulo("Medias negras vlack");
        item1.SetDescripcion("Tremendas meidas negras para hockey");
        item1.SetPrecio(1000);
        var list = new List<Item>();
        list.Add(item1);

        return list;
    }
}

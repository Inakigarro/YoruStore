using AutoMapper;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;

namespace Tienda.Infrastructure.Services;

public class ItemsService : IItemsService
{
    private readonly IItemsRepository _itemsRepository;
    private readonly ICategoriasRepository _categoriesRepository;
    private readonly ILogger<ItemsService> _logger;
    private readonly IMapper _mapper;

    public ItemsService(
        IItemsRepository itemsRepository,
        ICategoriasRepository categoriesRepository,
        ILogger<ItemsService> logger,
        IMapper mapper)
    {
        _itemsRepository = itemsRepository;
        _categoriesRepository = categoriesRepository;
        _logger = logger;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<ItemDto> Create(CrearItemDto nuevoItem, Guid categoriaId)
    {
        _logger.LogInformation($"Creando un nuevo item con el titulo: {nuevoItem.Titulo}");
        var item = await this._itemsRepository.Add(nuevoItem);
        var categoria = await this._categoriesRepository.Get(categoriaId);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }
        categoria.AddItem(item);
        ActualizarCategoriaDto categoriaDto = this._mapper.Map<ActualizarCategoriaDto>(categoria);
        await this._categoriesRepository.Update(categoriaDto);
        return this._mapper.Map<ItemDto>(item);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> Update(ActualizarItemDto item)
    {
        var itemExistente = await this._itemsRepository.Get(item.Id);
        if (itemExistente is null)
        {
            throw new InvalidOperationException($"No existe un item con el Id: {item.Id}");
        }

        _logger.LogInformation($"Actualizando el item: {itemExistente}, con la informacion: {item}");
        var itemActualizado = await this._itemsRepository.Update(item);
        return this._mapper.Map<ItemDto>(itemActualizado);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> Delete(Guid itemId)
    {
        var item = await this._itemsRepository.Get(itemId);
        if (item is null)
        {
            throw new InvalidOperationException($"No existe un item con el Id: {itemId}");
        }
        _logger.LogInformation($"Eliminando el item correspondiente al Id: {itemId}");
        var itemEliminado = await this._itemsRepository.Delete(itemId);
        return this._mapper.Map<ItemDto>(itemEliminado);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> Get(Guid itemId)
    {
        var item = await this._itemsRepository.Get(itemId);
        if (item is null)
        {
            throw new InvalidOperationException($"No existe un item con el Id: {itemId}");
        }

        return this._mapper.Map<ItemDto>(item);
    }

    public async Task<IEnumerable<ItemDto>> GetAll()
    {
        var items = await this._itemsRepository.GetAll();
        return this._mapper.Map<IEnumerable<ItemDto>>(items);
    }
}

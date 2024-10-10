using AutoMapper;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;

namespace Tienda.Infrastructure.Services;

public class ItemsService(
    IItemsRepository itemsRepository,
    ICategoriasRepository categoriesRepository,
    ILogger<ItemsService> logger,
    IMapper mapper) : IItemsService
{
    private readonly IItemsRepository _itemsRepository = itemsRepository;
    private readonly ICategoriasRepository _categoriesRepository = categoriesRepository;
    private readonly ILogger<ItemsService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc/>
    public async Task<ItemDto> CreateAsync(CrearItemDto nuevoItem, CancellationToken cancellationToken, Guid? categoriaId = default, string? categoriaNombre = default)
    {
        _logger.LogInformation($"Creando un nuevo item con el titulo: {nuevoItem.Titulo}");
        var categoria = categoriaId != default && categoriaId != Guid.Empty
            ? await this._categoriesRepository.GetByIdAsync(categoriaId.Value, cancellationToken)
            : categoriaNombre != default && !string.IsNullOrWhiteSpace(categoriaNombre)
                ? await this._categoriesRepository.GetByNombreAsync(categoriaNombre, cancellationToken)
                : throw new ArgumentNullException("Se necesita un Id o un Nombre para buscar la categoria en la que se agregara el item.");

        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        var item = await this._itemsRepository.AddAsync(nuevoItem, cancellationToken);
        categoria.AddItem(item);
        ActualizarCategoriaDto categoriaDto = this._mapper.Map<ActualizarCategoriaDto>(categoria);
        await this._categoriesRepository.UpdateAsync(categoriaDto, cancellationToken);
        await this._itemsRepository.SaveChangesAsync(cancellationToken);
        return this._mapper.Map<ItemDto>(item);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> UpdateAsync(ActualizarItemDto item, CancellationToken cancellationToken)
    {
        var itemExistente = await this._itemsRepository.GetAsync(item.Id, cancellationToken);
        if (itemExistente is null)
        {
            throw new InvalidOperationException($"No existe un item con el Id: {item.Id}");
        }

        _logger.LogInformation($"Actualizando el item: {itemExistente}, con la informacion: {item}");
        var itemActualizado = await this._itemsRepository.UpdateAsync(item, cancellationToken);
        return this._mapper.Map<ItemDto>(itemActualizado);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> DeleteAsync(Guid itemId, CancellationToken cancellationToken)
    {
        var item = await this._itemsRepository.GetAsync(itemId, cancellationToken);
        if (item is null)
        {
            throw new InvalidOperationException($"No existe un item con el Id: {itemId}");
        }
        _logger.LogInformation($"Eliminando el item correspondiente al Id: {itemId}");
        var itemEliminado = await this._itemsRepository.DeleteAsync(itemId, cancellationToken);
        return this._mapper.Map<ItemDto>(itemEliminado);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> GetAsync(Guid itemId, CancellationToken cancellationToken)
    {
        var item = await this._itemsRepository.GetAsync(itemId, cancellationToken);
        if (item is null)
        {
            throw new InvalidOperationException($"No existe un item con el Id: {itemId}");
        }

        return this._mapper.Map<ItemDto>(item);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ItemDto>> GetByFilterAsync(Guid categoriaId, string filter, CancellationToken cancellationToken)
    {
        var items = await this._itemsRepository.GetByFilterAsync(categoriaId, filter, cancellationToken);
        return this._mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<IEnumerable<ItemDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await this._itemsRepository.GetAllAsync(cancellationToken);
        return this._mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<IEnumerable<ItemDto>> GetByCategoriaId(Guid categoriaId, int skip, int take, CancellationToken cancellationToken)
    {
        var items = await this._itemsRepository.GetAllByCategoriaIdAsync(categoriaId, skip, take, cancellationToken);
        return this._mapper.Map<IEnumerable<ItemDto>>(items);
    }
}

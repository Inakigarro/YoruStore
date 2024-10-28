using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;
using Tienda.Domain;
using Tienda.Utilities.Attributes;

namespace Tienda.Infrastructure.Services;

[Scoped]
public class ItemsService(
    IItemsRepository itemsRepository,
    ICategoriasRepository categoriesRepository,
    ILogger<ItemsService> logger,
    IMapper mapper) : IItemsService
{
    /// <inheritdoc/>
    public async Task<ItemDto> CreateAsync(CrearItemDto nuevoItem, CancellationToken cancellationToken, Guid? categoriaId = default, string? categoriaNombre = default)
    {
        logger.LogInformation($"Creando un nuevo item con el titulo: {nuevoItem.Titulo}");
        
        // Obtengo la categoria a la que se agregara el item.
        var categoria = categoriaId != default && categoriaId != Guid.Empty
            ? await categoriesRepository.GetAsync(categoriaId.Value, cancellationToken)
            : categoriaNombre != default && !string.IsNullOrWhiteSpace(categoriaNombre)
                ? await categoriesRepository.GetByNombreAsync(categoriaNombre, cancellationToken)
                : throw new ArgumentNullException("Se necesita un Id o un Nombre para buscar la categoria en la que se agregara el item.");

        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        var item = new Item();
        item.SetTitulo(nuevoItem.Titulo);
        item.SetDescripcion(nuevoItem.Descripcion);
        item.SetPrecio(nuevoItem.Precio);
        await itemsRepository.AddAsync(item, cancellationToken);
        
        categoria.AddItem(item);
        ActualizarCategoriaDto categoriaDto = mapper.Map<ActualizarCategoriaDto>(categoria);
        await categoriesRepository.UpdateAsync(categoriaDto, cancellationToken);
        await itemsRepository.SaveAsync(cancellationToken);
        return mapper.Map<ItemDto>(item);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> UpdateAsync(ActualizarItemDto item, CancellationToken cancellationToken)
    {
        var itemExistente = await itemsRepository.GetAsync(item.Id, cancellationToken)
            ?? throw new InvalidOperationException($"No existe un item con el Id: {item.Id}");

        logger.LogInformation($"Actualizando el item: {itemExistente}, con la informacion: {item}");
        itemExistente.SetTitulo(item.Titulo);
        itemExistente.SetDescripcion(item.Descripcion);
        itemExistente.SetPrecio(item.Precio);
        
        itemsRepository.Update(itemExistente);
        await itemsRepository.SaveAsync(cancellationToken);
        
        return mapper.Map<ItemDto>(itemExistente);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> DeleteAsync(Guid itemId, CancellationToken cancellationToken)
    {
        var item = await itemsRepository.GetAsync(itemId, cancellationToken)
            ?? throw new InvalidOperationException($"No existe un item con el Id: {itemId}");
        
        logger.LogInformation($"Eliminando el item correspondiente al Id: {itemId}");
        
        await itemsRepository.Delete(itemId, cancellationToken);
        return mapper.Map<ItemDto>(item);
    }

    /// <inheritdoc/>
    public async Task<ItemDto> GetAsync(Guid itemId, CancellationToken cancellationToken)
    {
        var item = await itemsRepository.GetAsync(itemId, cancellationToken)
            ?? throw new InvalidOperationException($"No existe un item con el Id: {itemId}");

        return mapper.Map<ItemDto>(item);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ItemDto>> GetByFilterAsync(Guid categoriaId, string filter, CancellationToken cancellationToken)
    {
        var items = await itemsRepository.GetByFilterAsync(categoriaId, filter, cancellationToken);
        return mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<IEnumerable<ItemDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await itemsRepository.GetAll()
            .ToListAsync(cancellationToken);
        return mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<IEnumerable<ItemDto>> GetByCategoriaId(Guid categoriaId, int skip, int take, CancellationToken cancellationToken)
    {
        var items = await itemsRepository.GetAllByCategoriaIdAsync(categoriaId, skip, take, cancellationToken);
        return mapper.Map<IEnumerable<ItemDto>>(items);
    }
}

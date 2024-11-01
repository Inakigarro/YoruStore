using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;
using Tienda.Domain;

namespace Tienda.Infrastructure.Services;

public class CategoriasServices(
    ICategoriasRepository categoriasRepository,
    IItemsRepository itemsRepository,
    ILogger<CategoriasServices> logger,
    IMapper mapper) : ICategoriasService
{
    /// <inheritdoc/>
    public async Task<CategoriaDto> CreateAsync(CrearCategoriaDto nuevaCategoria, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Creando una nueva categoria con el nombre: {nuevaCategoria.Nombre}");
        Categoria categoria = new();
        categoria.SetNombre(nuevaCategoria.Nombre);
        await categoriasRepository.AddAsync(categoria, cancellationToken);
        await categoriasRepository.SaveAsync(cancellationToken);
        return mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> UpdateAsync(ActualizarCategoriaDto categoria, CancellationToken cancellationToken)
    {
        // Obtengo la categoria.
        var categoriaExistente = await categoriasRepository.GetAsync(categoria.Id, cancellationToken)
            ?? throw new InvalidOperationException($"No existe una categoria con el Id: {categoria.Id}");

        logger.LogInformation($"Actualizando la categoria: {categoriaExistente}, con la informacion: {categoria}");
        
        // Actualizo los datos de la categoria.
        categoriaExistente.SetNombre(categoria.Nombre);
        var itemsBorrados = await ObtenerItemsABorrar(categoriaExistente.Items.Select(x => x.Id), categoria.Items,
            cancellationToken);
        foreach (var item in itemsBorrados)
        {
            categoriaExistente.RemoveItem(item);
        }

        var itemsNuevos = await ObtenerItemsAGuardar(categoriaExistente.Items.Select(x => x.Id), categoria.Items,
            cancellationToken);
        foreach (var item in itemsNuevos)
        {
            categoriaExistente.AddItem(item);
        }
        
        // Guardo cambios.
        categoriasRepository.Update(categoriaExistente);
        await categoriasRepository.SaveAsync(cancellationToken);
        return mapper.Map<CategoriaDto>(categoriaExistente);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> DeleteAsync(Guid categoriaId, CancellationToken cancellationToken)
    {
        var categoria = await categoriasRepository.GetAsync(categoriaId, cancellationToken)
            ?? throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        logger.LogInformation($"Eliminando la categoria correspondiente al Id: {categoriaId}");
        await categoriasRepository.Delete(categoriaId, cancellationToken);
        await categoriasRepository.SaveAsync(cancellationToken);
        return mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> GetAsync(Guid categoriaId, CancellationToken cancellationToken)
    {
        var categoria = await categoriasRepository.GetAsync(categoriaId, cancellationToken)
            ?? throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");

        return mapper.Map<CategoriaDto>(categoria);
    }

    public async Task<CategoriaDto> GetByNameAsync(string nombre, CancellationToken cancellationToken)
    {
        var categoria = await categoriasRepository.GetByNombreAsync(nombre, cancellationToken)
            ?? throw new InvalidOperationException($"No existe una categoria con el nombre: {nombre}");

        return mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoriaDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categorias = await categoriasRepository
            .GetAll()
            .ToListAsync(cancellationToken);

        return mapper.Map<IEnumerable<CategoriaDto>>(categorias);
    }

    private async Task<IEnumerable<Item>> ObtenerItemsABorrar(IEnumerable<Guid> itemsExistentes,
        IEnumerable<Guid> itemsActualizados, CancellationToken cancellationToken)
    {
        var itemIdsBorrados = itemsExistentes.Where(item => !itemsActualizados.Contains(item));
        var itemsBorrados = await itemsRepository.GetAll()
            .Where(item => itemIdsBorrados.Contains(item.Id))
            .ToListAsync(cancellationToken);
        return itemsBorrados;
    }

    private async Task<IEnumerable<Item>> ObtenerItemsAGuardar(IEnumerable<Guid> itemsExistentes,
        IEnumerable<Guid> itemsActualizados, CancellationToken cancellationToken)
    {
        var itemIdsNuevos = itemsActualizados.Where(item => !itemsExistentes.Contains(item));
        var itemsNuevos = await itemsRepository.GetAll()
            .Where(item => itemIdsNuevos.Contains(item.Id))
            .ToListAsync(cancellationToken);
        return itemsNuevos;
    }
}

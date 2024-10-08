using Tienda.Contracts.Items;

namespace Tienda.Contracts.Services;

public interface IItemsService
{
    /// <summary>
    /// Crea un nuevo item.
    /// </summary>
    /// <param name="nuevoItem">El item a crear.</param>
    /// <param name="categoriaId">El id de la categoria a agregar el item.</param>
    /// <returns>Un dto que representa el item completamente creado.</returns>
    Task<ItemDto> CreateAsync(CrearItemDto nuevoItem, Guid categoriaId, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza un item existente.
    /// </summary>
    /// <param name="item">El item a modificar.</param>
    /// <returns>Un dto que representa el item completamente actualizado.</returns>
    Task<ItemDto> UpdateAsync(ActualizarItemDto item, CancellationToken cancellationToken);

    /// <summary>
    /// Elimina el item correspondiente al Id.
    /// </summary>
    /// <param name="itemId">El Id del item a borrar.</param>
    /// <returns>Un dto que representa el item borrado.</returns>
    Task<ItemDto> DeleteAsync(Guid itemId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un Item por su Id.
    /// </summary>
    /// <param name="itemId">El Id del item a buscar.</param>
    /// <returns>Un dto que representa el item encontrado.</returns>
    Task<ItemDto> GetAsync(Guid itemId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los items que cumplan con filtro especificado.
    /// </summary>
    /// <param name="categoriaId">El id de la categoria cuyos items se filtraran.</param>
    /// <param name="filter">El filtro a aplicar.</param>
    /// <returns>
    /// Una lista de items correspondientes con la categoria especificada
    /// filtrada por el filtro proveido.
    /// </returns>
    Task<IEnumerable<ItemDto>> GetByFilterAsync(Guid categoriaId, string filter, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los items.
    /// </summary>
    /// <returns>Devuelve una lista de dtos que representan las categorias actuales.</returns>
    Task<IEnumerable<ItemDto>> GetAllAsync(CancellationToken cancellationToken);
}

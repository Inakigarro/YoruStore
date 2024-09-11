using Tienda.Contracts.Items;

namespace Tienda.Contracts.Services;

public interface IItemsService
{
    /// <summary>
    /// Crea un nuevo item.
    /// </summary>
    /// <param name="nuevoItem">El item a crear.</param>
    /// <returns>Un dto que representa el item completamente creado.</returns>
    Task<ItemDto> Create(CrearOActualizarItemDto nuevoItem);

    /// <summary>
    /// Actualiza un item existente.
    /// </summary>
    /// <param name="item">El item a modificar.</param>
    /// <returns>Un dto que representa el item completamente actualizado.</returns>
    Task<ItemDto> Update(CrearOActualizarItemDto item);

    /// <summary>
    /// Elimina el item correspondiente al Id.
    /// </summary>
    /// <param name="itemId">El Id del item a borrar.</param>
    /// <returns>Un dto que representa el item borrado.</returns>
    Task<ItemDto> Delete(Guid itemId);

    /// <summary>
    /// Obtiene un Item por su Id.
    /// </summary>
    /// <param name="itemId">El Id del item a buscar.</param>
    /// <returns>Un dto que representa el item encontrado.</returns>
    Task<ItemDto> Get(Guid itemId);

    /// <summary>
    /// Obtiene todos los items.
    /// </summary>
    /// <returns>Devuelve una lista de dtos que representan las categorias actuales.</returns>
    Task<IEnumerable<ItemDto>> GetAll();
}

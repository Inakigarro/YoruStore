using Tienda.Contracts.Items;
using Tienda.Domain;

namespace Tienda.Contracts.Repositories;

public interface IItemsRepository
{
    /// <summary>
    /// Agrega un Item a la base de datos.
    /// </summary>
    /// <param name="nuevoItem">El item a agregar.</param>
    /// <returns>El item completamente creado.</returns>
    Task<Item> AddAsync(CrearItemDto nuevoItem, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza un Item en la base de datos.
    /// </summary>
    /// <param name="item">El item a actualizar.</param>
    /// <returns>El item completamente actualizado.</returns>
    Task<Item> UpdateAsync(ActualizarItemDto item, CancellationToken cancellationToken);

    /// <summary>
    /// Elimina un Item de la base de datos.
    /// </summary>
    /// <param name="id">El Id del Item a eliminar.</param>
    /// <returns>El Item eliminado.</returns>
    Task<Item> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un Item por su Id.
    /// </summary>
    /// <param name="id">El Id del Item a obtener.</param>
    /// <returns>El Item correspondiente con el Id proveido.</returns>
    Task<Item> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los items que cumplan con filtro especificado.
    /// </summary>
    /// <param name="categoriaId">El id de la categoria cuyos items se filtraran.</param>
    /// <param name="filter">El filtro a aplicar.</param>
    /// <returns>
    /// Una lista de items correspondientes con la categoria especificada
    /// filtrada por el filtro proveido.
    /// </returns>
    Task<IEnumerable<Item>> GetByFilterAsync(Guid categoriaId, string filter, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los Items de la base de datos.
    /// </summary>
    /// <returns>Una lista de Items</returns>
    Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los items de una categoria.
    /// </summary>
    /// <param name="categoriaId">El id de la categoria.</param>
    /// <param name="take">Cantidad de objetos a pedir.</param>
    /// <param name="skip">Cantidad de objetos a saltar.</param>
    /// <returns>Una lista de items filtrados por una categoria.</returns>
    Task<IEnumerable<Item>> GetAllByCategoriaIdAsync(Guid categoriaId, int take, int skip, CancellationToken cancellationToken);
}

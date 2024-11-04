using Tienda.Contracts.Items;
using Tienda.Domain;

namespace Tienda.Contracts.Repositories;

public interface IItemsRepository : IGenericRepository<Item>
{
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
    /// Obtiene todos los items de una categoria.
    /// </summary>
    /// <param name="categoriaId">El id de la categoria.</param>
    /// <param name="take">Cantidad de objetos a pedir.</param>
    /// <param name="skip">Cantidad de objetos a saltar.</param>
    /// <returns>Una lista de items filtrados por una categoria.</returns>
    Task<IEnumerable<Item>> GetAllByCategoriaIdAsync(Guid categoriaId, int take, int skip, CancellationToken cancellationToken);
}

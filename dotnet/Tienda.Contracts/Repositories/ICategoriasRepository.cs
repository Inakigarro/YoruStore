using Tienda.Contracts.Categorias;
using Tienda.Domain;

namespace Tienda.Contracts.Repositories;

public interface ICategoriasRepository : IGenericRepository<Categoria>
{
    /// <summary>
    /// Obtiene una categoria por su nombre.
    /// </summary>
    /// <param name="nombre">El nombre de la categoria a buscar.</param>
    /// <returns>La categoria correspondiente al nombre.</returns>
    Task<Categoria> GetByNombreAsync(string nombre, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una lista de items de la categoria correpondiente con el Id proveido. 
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria.</param>
    /// <returns>Una lista de Items</returns>
    Task<IEnumerable<Item>> GetByCategoria(Guid categoriaId, CancellationToken cancellationToken);
}

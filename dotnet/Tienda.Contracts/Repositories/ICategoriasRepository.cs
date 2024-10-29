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
}

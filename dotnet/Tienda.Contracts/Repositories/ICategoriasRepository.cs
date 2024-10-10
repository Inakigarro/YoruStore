using Tienda.Contracts.Categorias;
using Tienda.Domain;

namespace Tienda.Contracts.Repositories;

public interface ICategoriasRepository
{
    /// <summary>
    /// Agrega una categoria a la base de datos.
    /// </summary>
    /// <param name="categoria">La categoria a agregar.</param>
    /// <returns>La categoria agregada.</returns>
    Task<Categoria> AddAsync(CrearCategoriaDto categoria, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza una categoria en la base de datos.
    /// </summary>
    /// <param name="categoria">La categoria a actualizar.</param>
    /// <returns>La categoria actualizada.</returns>
    Task<Categoria> UpdateAsync(ActualizarCategoriaDto categoria, CancellationToken cancellationToken);

    /// <summary>
    /// Borra una categoria de la base de datos.
    /// </summary>
    /// <param name="id">El id de la categoria a borrar.</param>
    /// <returns></returns>
    Task<Categoria> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una categoria por su id.
    /// </summary>
    /// <param name="id">El id de la categoria a buscar.</param>
    /// <returns>La categoria correspondiente al id.</returns>
    /// <exception cref="ArgumentException"></exception>
    Task<Categoria> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una categoria por su nombre.
    /// </summary>
    /// <param name="nombre">El nombre de la categoria a buscar.</param>
    /// <returns>La categoria correspondiente al nombre.</returns>
    Task<Categoria> GetByNombreAsync(string nombre, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las categorias almacenadas en la base de datos
    /// ordenadas alfabeticamente en orden ascendente.
    /// </summary>
    /// <returns>Una lista de categorias.</returns>
    Task<IEnumerable<Categoria>> GetAllAsync(CancellationToken cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken);
}

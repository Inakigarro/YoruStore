using Tienda.Contracts.Categorias;
using Tienda.Domain;

namespace Tienda.Contracts.Repositories;

/// <summary>
/// Repositorio utilizado para almacenar categorias en la base de datos.
/// </summary>
public interface ICategoriasRepository
{
    /// <summary>
    /// Agrega una categoria a la base de datos.
    /// </summary>
    /// <param name="categoria">La categoria a agregar.</param>
    /// <returns>La categoria agregada.</returns>
    Task<Categoria> Add(CrearOActualizarCategoriaDto categoria);

    /// <summary>
    /// Actualiza una categoria en la base de datos.
    /// </summary>
    /// <param name="categoria">La categoria a actualizar.</param>
    /// <returns>La categoria actualizada.</returns>
    Task<Categoria> Update(CrearOActualizarCategoriaDto categoria);

    /// <summary>
    /// Borra una categoria de la base de datos.
    /// </summary>
    /// <param name="id">El id de la categoria a borrar.</param>
    /// <returns></returns>
    Task<Categoria> Delete(Guid id);

    /// <summary>
    /// Obtiene una categoria por su id.
    /// </summary>
    /// <param name="id">El id de la categoria a buscar.</param>
    /// <returns>La categoria correspondiente al id.</returns>
    Task<Categoria> Get(Guid id);

    /// <summary>
    /// Obtiene todas las categorias almacenadas en la base de datos.
    /// </summary>
    /// <returns>Una lista de categorias.</returns>
    Task<IEnumerable<Categoria>> GetAll();
}

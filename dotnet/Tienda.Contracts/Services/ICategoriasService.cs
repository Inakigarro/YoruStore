using Tienda.Contracts.Categorias;
using Tienda.Contracts.Items;

namespace Tienda.Contracts.Services;

public interface ICategoriasService
{
    /// <summary>
    /// Crea una nueva categoria.
    /// </summary>
    /// <param name="nuevaCategoria">La categoria a crear.</param>
    /// <returns>Un dto que representa la nueva categoria.</returns>
    Task<CategoriaDto> CreateAsync(CrearCategoriaDto nuevaCategoria, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza una categoria existente.
    /// </summary>
    /// <param name="categoria">La categoria a actualizar.</param>
    /// <returns>Un dto que representa la categoria actualizada.</returns>
    Task<CategoriaDto> UpdateAsync(ActualizarCategoriaDto categoria, CancellationToken cancellationToken);

    /// <summary>
    /// Agrega un item a la categoria correspondiente al Id proveido.
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria.</param>
    /// <param name="item">El item a agregar.</param>
    /// <returns>Un dto que representa la categoria actualizada.</returns>
    Task<CategoriaDto> AddItemAsync(Guid categoriaId, ItemDto item, CancellationToken cancellationToken);

    /// <summary>
    /// Elimina la categoria correspondiente al Id.
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria a eliminar.</param>
    /// <returns>Un dto que representa la categoria eliminada.</returns>
    Task<CategoriaDto> DeleteAsync(Guid categoriaId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene la categoria correspondiente al Id.
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria a buscar.</param>
    /// <returns>Un dto que representa la categoria encontrada.</returns>
    Task<CategoriaDto> GetAsync(Guid categoriaId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene la categoria correspondiente al nombre.
    /// </summary>
    /// <param name="nombre">El nombre de la categoria.</param>
    /// <returns>Un dto que representa la categoria encontrada.</returns>
    Task<CategoriaDto> GetByNameAsync(string nombre, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las categorias.
    /// </summary>
    /// <returns>Una lista de dtos que representan las categorias actuales.</returns>
    Task<IEnumerable<CategoriaDto>> GetAllAsync(CancellationToken cancellationToken);
}

using Tienda.Contracts.Categorias;

namespace Tienda.Contracts.Services;

public interface ICategoriasService
{
    /// <summary>
    /// Crea una nueva categoria.
    /// </summary>
    /// <param name="nuevaCategoria">La categoria a crear.</param>
    /// <returns>Un dto que representa la nueva categoria.</returns>
    Task<CategoriaDto> Create(CrearOActualizarCategoriaDto nuevaCategoria);

    /// <summary>
    /// Actualiza una categoria existente.
    /// </summary>
    /// <param name="categoria">La categoria a actualizar.</param>
    /// <returns>Un dto que representa la categoria actualizada.</returns>
    Task<CategoriaDto> Update(CrearOActualizarCategoriaDto categoria);

    /// <summary>
    /// Elimina la categoria correspondiente al Id.
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria a eliminar.</param>
    /// <returns>Un dto que representa la categoria eliminada.</returns>
    Task<CategoriaDto> Delete(Guid categoriaId);

    /// <summary>
    /// Obtiene la categoria correspondiente al Id.
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria a buscar.</param>
    /// <returns>Un dto que representa la categoria encontrada.</returns>
    Task<CategoriaDto> Get(Guid categoriaId);

    /// <summary>
    /// Obtiene todas las categorias.
    /// </summary>
    /// <returns>Una lista de dtos que representan las categorias actuales.</returns>
    Task<IEnumerable<CategoriaDto>> GetAll();
}

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
    Task<CategoriaDto> Create(CrearCategoriaDto nuevaCategoria);

    /// <summary>
    /// Actualiza una categoria existente.
    /// </summary>
    /// <param name="categoria">La categoria a actualizar.</param>
    /// <returns>Un dto que representa la categoria actualizada.</returns>
    Task<CategoriaDto> Update(ActualizarCategoriaDto categoria);

    /// <summary>
    /// Agrega un item a la categoria correspondiente al Id proveido.
    /// </summary>
    /// <param name="categoriaId">El Id de la categoria.</param>
    /// <param name="item">El item a agregar.</param>
    /// <returns>Un dto que representa la categoria actualizada.</returns>
    Task<CategoriaDto> AddItem(Guid categoriaId, ItemDto item);

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

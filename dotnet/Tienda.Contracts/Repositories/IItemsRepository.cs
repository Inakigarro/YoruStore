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
    Task<Item> Add(CrearItemDto nuevoItem);

    /// <summary>
    /// Actualiza un Item en la base de datos.
    /// </summary>
    /// <param name="item">El item a actualizar.</param>
    /// <returns>El item completamente actualizado.</returns>
    Task<Item> Update(ActualizarItemDto item);

    /// <summary>
    /// Elimina un Item de la base de datos.
    /// </summary>
    /// <param name="id">El Id del Item a eliminar.</param>
    /// <returns>El Item eliminado.</returns>
    Task<Item> Delete(Guid id);

    /// <summary>
    /// Obtiene un Item por su Id.
    /// </summary>
    /// <param name="id">El Id del Item a obtener.</param>
    /// <returns>El Item correspondiente con el Id proveido.</returns>
    Task<Item> Get(Guid id);

    /// <summary>
    /// Obtiene todos los Items de la base de datos.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Item>> GetAll();
}

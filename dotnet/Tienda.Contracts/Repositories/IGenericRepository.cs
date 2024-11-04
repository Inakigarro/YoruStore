namespace Tienda.Contracts.Repositories;

/// <summary>
/// Repositorio generico.
/// </summary>
/// <typeparam name="TEntity">Entidad a trabajar en el repositorio.</typeparam>
public interface IGenericRepository<TEntity>: IDisposable
where TEntity: class, IId, new()
{
    /// <summary>
    /// Agrega una entidad a la base de datos.
    /// </summary>
    /// <param name="entity">La entidad a agregar.</param>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza una entidad en la base de datos.
    /// </summary>
    /// <param name="entity">La entidad a actualizar.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Elimina la entidad de la base de datos.
    /// </summary>
    /// <param name="id">El Id de la entidad a borrar.</param>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    Task Delete(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una entidad por su Id.
    /// </summary>
    /// <param name="id">El id de la entidad a buscar.</param>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    /// <returns>La entidad encontrada.</returns>
    Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IQueryable<TEntity> GetAsQueryableAsync(Guid id);

    /// <summary>
    /// Obtiene todas las entidades de la base de datos.
    /// </summary>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Guarda todos los cambios realizados.
    /// </summary>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    Task SaveAsync(CancellationToken cancellationToken);
}
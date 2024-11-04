using Microsoft.EntityFrameworkCore;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Domain;
using Tienda.Utilities.Attributes;

namespace Tienda.Infrastructure.Repositories;

[Scoped]
public class ItemsRepository(TiendaDbContext dbContext) : GenericRepository<Item>(dbContext), IItemsRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Item>> GetByFilterAsync(Guid categoriaId, string filter, CancellationToken cancellationToken)
    {
        var items = await dbContext.Items
            .Include(item => item.Categoria)
            .Where(item => item.CategoriaId == categoriaId)
            .Where(item => item.Titulo.Contains(filter) || item.Descripcion.Contains(filter))
            .ToListAsync(cancellationToken);
        return items;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Item>> GetAllByCategoriaIdAsync(Guid categoriaId, int take, int skip, CancellationToken cancellationToken)
    {
        var items = await GetAll()
            .Include(item => item.Categoria)
            .Where(item => item.CategoriaId == categoriaId)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
        return items;
    }
}

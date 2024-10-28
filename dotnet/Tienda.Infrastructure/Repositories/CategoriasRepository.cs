using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Repositories;
using Tienda.Domain;
using Tienda.Utilities.Attributes;

namespace Tienda.Infrastructure.Repositories;

[Scoped]
public class CategoriasRepository(
    TiendaDbContext context,
    ILogger<CategoriasRepository> logger) : GenericRepository<Categoria>(context), ICategoriasRepository
{
    ///<inheritdoc/>
    public async Task<Categoria> UpdateAsync(ActualizarCategoriaDto categoria, CancellationToken cancellationToken)
    {
        Categoria? categoriaExistente = await GetAsync(categoria.Id, cancellationToken);

        if (categoriaExistente is null)
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(categoria.Id));

        categoriaExistente.SetNombre(categoria.Nombre);
        await AgregarItems(categoriaExistente, categoria.Items, cancellationToken);
        Update(categoriaExistente);
        return categoriaExistente;
    }
    
    /// <inheritdoc/>
    public async Task<Categoria> GetByNombreAsync(string nombre, CancellationToken cancellationToken)
    {
        var categoria = await GetAll()
            .Where(x => x.Nombre == nombre)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        return categoria ?? throw new ArgumentException("No existe una categoria con el nombre ingresado.", nameof(nombre));
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<Item>> GetByCategoria(Guid categoriaId, CancellationToken cancellationToken)
    {
        var categoria = await GetAsQueryableAsync(categoriaId)
            .Include(cat => cat.Items)
            .FirstOrDefaultAsync(cancellationToken);

        return categoria is not null
            ? categoria.Items
            : throw new ArgumentException("No existe una categoria con el Id proveido", nameof(categoriaId));;;
    }

    private async Task AgregarItems(Categoria categoria, IEnumerable<Guid> itemIds, CancellationToken cancellationToken)
    {
        foreach (var itemId in itemIds)
        {
            var item = await context.Set<Item>().FindAsync(itemId, cancellationToken);
            if (item is not null)
            {
                categoria.AddItem(item);
            }
        }
    }
}

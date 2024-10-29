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
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Repositories;
using Tienda.Domain;

namespace Tienda.Infrastructure.Repositories;

public class CategoriasRepository(
    TiendaDbContext context,
    ILogger<CategoriasRepository> logger) : ICategoriasRepository, IDisposable
{
    private TiendaDbContext _context = context;
    private ILogger<CategoriasRepository> _logger = logger;
    private bool _disposed;

    ///<inheritdoc/>
    public async Task<Categoria> AddAsync(CrearCategoriaDto categoria, CancellationToken cancellationToken)
    {
        // Si existe una categoria con el mismo nombre, la devuelvo.
        var cat = await _context.Categorias.Where(cat => cat.Nombre == categoria.Nombre).FirstOrDefaultAsync(cancellationToken);
        if (cat is not null)
        {
            _logger.LogWarning("Ya existe una categoria con el nombre proveido: {nombre}", categoria.Nombre);
            return cat;
        }

        Categoria nuevaCategoria = new Categoria(Guid.NewGuid());
        nuevaCategoria.SetNombre(categoria.Nombre);
        await this._context.AddAsync(nuevaCategoria, cancellationToken);
        return nuevaCategoria;
    }

    ///<inheritdoc/>
    public async Task<Categoria> UpdateAsync(ActualizarCategoriaDto categoria, CancellationToken cancellationToken)
    {
        Categoria? categoriaExistente = await this._context.Categorias.FindAsync(categoria.Id, cancellationToken);

        if (categoriaExistente is null)
        {
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(categoria.Id));
        }

        categoriaExistente.SetNombre(categoria.Nombre);
        await this.AgregarItems(categoriaExistente, categoria.Items, cancellationToken);
        this._context.Categorias.Attach(categoriaExistente);
        this._context.Entry(categoriaExistente).State = EntityState.Modified;
        return categoriaExistente;
    }

    ///<inheritdoc/>
    public async Task<Categoria> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Categoria? categoriaExistente = await this._context.Categorias.FindAsync(id, cancellationToken);
        if (categoriaExistente is null)
        {
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(id));
        }

        this._context.Categorias.Remove(categoriaExistente);
        return categoriaExistente;
    }

    ///<inheritdoc/>
    public async Task<Categoria> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Categoria? categoria = await this._context.Categorias
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (categoria is null)
        {
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(id));
        }

        return categoria;
    }

    /// <inheritdoc/>
    public async Task<Categoria> GetByNombreAsync(string nombre, CancellationToken cancellationToken)
    {
        Categoria? categoria = await this._context.Categorias
            .Where(x => x.Nombre == nombre)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (categoria is null)
        {
            throw new ArgumentException("No existe una categoria con el nombre ingresado.", nameof(nombre));
        }

        return categoria;
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<Categoria>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await this._context.Categorias
            .OrderBy(categoria => categoria.Nombre)
            .ToListAsync(cancellationToken);
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<Item>> GetByCategoria(Guid categoriaId, CancellationToken cancellationToken)
    {
        Categoria? categoria = await this._context.Categorias.FindAsync(categoriaId, cancellationToken);
        if (categoria is null)
        {
            throw new ArgumentException("No existe una categoria con el Id proveido", nameof(categoriaId));
        }

        return categoria.Items;
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                this._context.Dispose();
            }

            this._disposed = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private async Task AgregarItems(Categoria categoria, IEnumerable<Guid> itemIds, CancellationToken cancellationToken)
    {
        foreach (var itemId in itemIds)
        {
            Item? item = await this._context.Items.FindAsync(itemId, cancellationToken);
            if (item is not null)
            {
                categoria.AddItem(item);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Domain;

namespace Tienda.Infrastructure.Repositories;

public class ItemsRepository(TiendaDbContext dbContext) : IItemsRepository, IDisposable
{
    private readonly TiendaDbContext _dbContext = dbContext;
    private bool _disposed;

    ///<inheritdoc/>
    public async Task<Item> AddAsync(CrearItemDto nuevoItem, CancellationToken cancellationToken)
    {
        Item item = new Item(Guid.NewGuid());
        item.SetTitulo(nuevoItem.Titulo);
        item.SetDescripcion(nuevoItem.Descripcion);
        item.SetPrecio(nuevoItem.Precio);

        await this._dbContext.Items.AddAsync(item, cancellationToken);
        await this._dbContext.SaveChangesAsync(cancellationToken);

        return item;
    }

    ///<inheritdoc/>
    public async Task<Item> UpdateAsync(ActualizarItemDto item, CancellationToken cancellationToken)
    {
        Item? itemExistente = await this._dbContext.Items.FindAsync(item.Id, cancellationToken);
        if (itemExistente is null)
        {
            throw new ArgumentException("No existe un item con el Id ingresado.", nameof(item.Id));
        }

        itemExistente.SetTitulo(item.Titulo);
        itemExistente.SetDescripcion(item.Descripcion);
        itemExistente.SetPrecio(item.Precio);
        this._dbContext.Items.Attach(itemExistente);
        this._dbContext.Entry(itemExistente).State = EntityState.Modified;
        await this._dbContext.SaveChangesAsync(cancellationToken);
        return itemExistente;
    }

    ///<inheritdoc/>
    public async Task<Item> DeleteAsync(Guid itemId, CancellationToken cancellationToken)
    {
        Item? itemExistente = await this._dbContext.Items.FindAsync(itemId, cancellationToken);
        if (itemExistente is null)
        {
            throw new ArgumentException("No existe un item con el Id proveido", nameof(itemId));
        }

        this._dbContext.Remove(itemExistente);
        await this._dbContext.SaveChangesAsync(cancellationToken);
        return itemExistente;
    }

    /// <inheritdoc/>
    public async Task<Item> GetAsync(Guid itemId, CancellationToken cancellationToken)
    {
        Item? item = await this._dbContext.Items.FindAsync(itemId, cancellationToken);
        if (item is null)
        {
            throw new ArgumentException("No existe un item con el Id proveido", nameof(itemId));
        }

        return item;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Item>> GetByFilterAsync(Guid categoriaId, string filter, CancellationToken cancellationToken)
    {
        var items = await this._dbContext.Items
            .Include(item => item.Categoria)
            .Where(item => item.CategoriaId == categoriaId)
            .Where(item => item.Titulo.Contains(filter) || item.Descripcion.Contains(filter))
            .ToListAsync(cancellationToken);
        return items;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await this._dbContext.Items.Include(item => item.Categoria).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetAllByCategoriaIdAsync(Guid categoriaId, int take, int skip, CancellationToken cancellationToken)
    {
        var items = await this._dbContext.Items
            .Include(item => item.Categoria)
            .Where(item => item.CategoriaId == categoriaId)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
        return items;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                this._dbContext.Dispose();
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
}

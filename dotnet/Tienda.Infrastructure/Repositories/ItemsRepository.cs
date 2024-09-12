using Microsoft.EntityFrameworkCore;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Domain;

namespace Tienda.Infrastructure.Repositories;

public class ItemsRepository : IItemsRepository, IDisposable
{
    private readonly TiendaDbContext _dbContext;
    private bool _disposed;

    public ItemsRepository(TiendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    ///<inheritdoc/>
    public async Task<Item> Add(CrearItemDto nuevoItem)
    {
        Item item = new Item(Guid.NewGuid());
        item.SetTitulo(nuevoItem.Titulo);
        item.SetDescripcion(nuevoItem.Descripcion);
        item.SetPrecio(nuevoItem.Precio);

        await this._dbContext.Items.AddAsync(item);
        await this._dbContext.SaveChangesAsync();

        return item;
    }

    ///<inheritdoc/>
    public async Task<Item> Update(ActualizarItemDto item)
    {
        Item? itemExistente = await this._dbContext.Items.FindAsync(item.Id);
        if (itemExistente is null)
        {
            throw new ArgumentException("No existe un item con el Id ingresado.", nameof(item.Id));
        }

        itemExistente.SetTitulo(item.Titulo);
        itemExistente.SetDescripcion(item.Descripcion);
        itemExistente.SetPrecio(item.Precio);
        this._dbContext.Items.Attach(itemExistente);
        this._dbContext.Entry(itemExistente).State = EntityState.Modified;
        await this._dbContext.SaveChangesAsync();
        return itemExistente;
    }

    ///<inheritdoc/>
    public async Task<Item> Delete(Guid itemId)
    {
        Item? itemExistente = await this._dbContext.Items.FindAsync(itemId);
        if (itemExistente is null)
        {
            throw new ArgumentException("No existe un item con el Id proveido", nameof(itemId));
        }

        this._dbContext.Remove(itemExistente);
        await this._dbContext.SaveChangesAsync();
        return itemExistente;
    }

    ///<inheritdoc/>
    public async Task<Item> Get(Guid itemId)
    {
        Item? item = await this._dbContext.Items.FindAsync(itemId);
        if (item is null)
        {
            throw new ArgumentException("No existe un item con el Id proveido", nameof(itemId));
        }

        return item;
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<Item>> GetAll()
    {
        return await this._dbContext.Items.ToListAsync();
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

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
    public async Task<Item> Add(CrearOActualizarItemDto nuevoItem)
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
    public async Task<Item> Update(CrearOActualizarItemDto item)
    {
        Item? itemExistente = await this._dbContext.Items.FindAsync(item.Id);
        if (itemExistente is null)
        {
            throw new ArgumentException("No existe un item con el Id ingresado.", nameof(item.Id));
        }

        itemExistente.SetTitulo(item.Titulo);
        itemExistente.SetDescripcion(item.Descripcion);
        itemExistente.SetPrecio(item.Precio);
        this._dbContext.Items.Update(itemExistente);
        await this._dbContext.SaveChangesAsync();
        return itemExistente;
    }

    ///<inheritdoc/>
    public Task<Item> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    ///<inheritdoc/>
    public Task<Item> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    ///<inheritdoc/>
    public Task<IEnumerable<Item>> GetAll()
    {
        throw new NotImplementedException();
    }

    ///<inheritdoc/>
    public Task<IEnumerable<Item>> GetByCategoria(Guid categoriaId)
    {
        throw new NotImplementedException();
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

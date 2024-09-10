using Microsoft.EntityFrameworkCore;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Repositories;
using Tienda.Domain;

namespace Tienda.Infrastructure.Repositories;

public class CategoriasRepository : ICategoriasRepository, IDisposable
{
    private TiendaDbContext _context;
    private bool _disposed;

    public CategoriasRepository(TiendaDbContext context)
    {
        this._context = context;
    }

    ///<inheritdoc/>
    public async Task<Categoria> Add(CrearOActualizarCategoriaDto categoria)
    {
        Categoria nuevaCategoria = new Categoria(Guid.NewGuid());
        nuevaCategoria.SetNombre(categoria.Nombre);
        await this._context.AddAsync(nuevaCategoria);
        await this._context.SaveChangesAsync();

        return nuevaCategoria;
    }

    ///<inheritdoc/>
    public async Task<Categoria> Update(CrearOActualizarCategoriaDto categoria)
    {
        Categoria? categoriaExistente = await this._context.Categorias.FindAsync(categoria.Id);

        if(categoriaExistente is null)
        {
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(categoria.Id));
        }

        categoriaExistente.SetNombre(categoria.Nombre);
        this._context.Categorias.Update(categoriaExistente);
        await this._context.SaveChangesAsync();
        return categoriaExistente;
    }

    ///<inheritdoc/>
    public async Task<Categoria> Delete(Guid id)
    {
        Categoria? categoriaExistente = await this._context.Categorias.FindAsync(id);
        if (categoriaExistente is null)
        {
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(id));
        }

        this._context.Categorias.Remove(categoriaExistente);
        await this._context.SaveChangesAsync();
        return categoriaExistente;
    }

    ///<inheritdoc/>
    public async Task<Categoria> Get(Guid id)
    {
        Categoria? categoria = await this._context.Categorias.FindAsync(id);
        if (categoria is null)
        {
            throw new ArgumentException("No existe una categoria con el Id ingresado.", nameof(id));
        }

        return categoria;
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<Categoria>> GetAll()
    {
        return await this._context.Categorias.ToListAsync();
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
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;
using Tienda.Domain;

namespace Tienda.Infrastructure.Services;

public class CategoriasServices(
    ICategoriasRepository categoriasRepository,
    ILogger<CategoriasServices> logger,
    IMapper mapper) : ICategoriasService
{
    private readonly ICategoriasRepository _categoriasRepository = categoriasRepository;
    private readonly ILogger<CategoriasServices> _logger = logger;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc/>
    public async Task<CategoriaDto> CreateAsync(CrearCategoriaDto nuevaCategoria, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creando una nueva categoria con el nombre: {nuevaCategoria.Nombre}");
        var categoria = await this._categoriasRepository.AddAsync(nuevaCategoria, cancellationToken);
        await _categoriasRepository.SaveChangesAsync(cancellationToken);
        return this._mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> UpdateAsync(ActualizarCategoriaDto categoria, CancellationToken cancellationToken)
    {
        var categoriaExistente = await this._categoriasRepository.GetByIdAsync(categoria.Id, cancellationToken);
        if (categoriaExistente is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoria.Id}");
        }

        _logger.LogInformation($"Actualizando la categoria: {categoriaExistente}, con la informacion: {categoria}");
        var categoriaActualizada = await this._categoriasRepository.UpdateAsync(categoria, cancellationToken);
        await _categoriasRepository.SaveChangesAsync(cancellationToken);
        return this._mapper.Map<CategoriaDto>(categoriaActualizada);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> AddItemAsync(Guid categoriaId, ItemDto item, CancellationToken cancellationToken)
    {
        var categoria = await this._categoriasRepository.GetByIdAsync(categoriaId, cancellationToken);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        _logger.LogInformation($"Agregando el item: {item.Id} - {item.Titulo}, a la categoria: {categoria.Nombre}");
        categoria.AddItem(this._mapper.Map<Item>(item));
        var categoriaActualizada = await _categoriasRepository.UpdateAsync(this._mapper.Map <ActualizarCategoriaDto>(categoria), cancellationToken);
        await _categoriasRepository.SaveChangesAsync(cancellationToken);
        return this._mapper.Map<CategoriaDto>(categoriaActualizada);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> DeleteAsync(Guid categoriaId, CancellationToken cancellationToken)
    {
        var categoria = await this._categoriasRepository.GetByIdAsync(categoriaId, cancellationToken);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        _logger.LogInformation($"Eliminando la categoria correspondiente al Id: {categoriaId}");
        var categoriaEliminada = await this._categoriasRepository.DeleteAsync(categoriaId, cancellationToken);
        await _categoriasRepository.SaveChangesAsync(cancellationToken);
        return this._mapper.Map<CategoriaDto>(categoriaEliminada);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> GetAsync(Guid categoriaId, CancellationToken cancellationToken)
    {
        var categoria = await this._categoriasRepository.GetByIdAsync(categoriaId, cancellationToken);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        return this._mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoriaDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categorias = await this._categoriasRepository.GetAllAsync(cancellationToken) ?? new List<Categoria>();

        return this._mapper.Map<IEnumerable<CategoriaDto>>(categorias);
    }
}

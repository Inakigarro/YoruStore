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
    public async Task<CategoriaDto> Create(CrearCategoriaDto nuevaCategoria)
    {
        _logger.LogInformation($"Creando una nueva categoria con el nombre: {nuevaCategoria.Nombre}");
        var categoria = await this._categoriasRepository.Add(nuevaCategoria);
        return this._mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> Update(ActualizarCategoriaDto categoria)
    {
        var categoriaExistente = await this._categoriasRepository.Get(categoria.Id);
        if (categoriaExistente is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoria.Id}");
        }

        _logger.LogInformation($"Actualizando la categoria: {categoriaExistente}, con la informacion: {categoria}");
        var categoriaActualizada = await this._categoriasRepository.Update(categoria);
        return this._mapper.Map<CategoriaDto>(categoriaActualizada);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> AddItem(Guid categoriaId, ItemDto item)
    {
        var categoria = await this._categoriasRepository.Get(categoriaId);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        _logger.LogInformation($"Agregando el item: {item.Id} - {item.Titulo}, a la categoria: {categoria.Nombre}");
        categoria.AddItem(this._mapper.Map<Item>(item));
        var categoriaActualizada = await _categoriasRepository.Update(this._mapper.Map <ActualizarCategoriaDto>(categoria));
        return this._mapper.Map<CategoriaDto>(categoriaActualizada);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> Delete(Guid categoriaId)
    {
        var categoria = await this._categoriasRepository.Get(categoriaId);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        _logger.LogInformation($"Eliminando la categoria correspondiente al Id: {categoriaId}");
        var categoriaEliminada = await this._categoriasRepository.Delete(categoriaId);
        return this._mapper.Map<CategoriaDto>(categoriaEliminada);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> Get(Guid categoriaId)
    {
        var categoria = await this._categoriasRepository.Get(categoriaId);
        if (categoria is null)
        {
            throw new InvalidOperationException($"No existe una categoria con el Id: {categoriaId}");
        }

        return this._mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoriaDto>> GetAll()
    {
        var categorias = await this._categoriasRepository.GetAll() ?? new List<Categoria>();

        return this._mapper.Map<IEnumerable<CategoriaDto>>(categorias);
    }
}

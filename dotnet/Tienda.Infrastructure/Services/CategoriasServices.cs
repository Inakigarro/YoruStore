using AutoMapper;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;
using Tienda.Domain;

namespace Tienda.Infrastructure.Services;

public class CategoriasServices : ICategoriasService
{
    private readonly ICategoriasRepository _categoriasRepository;
    private readonly ILogger<CategoriasServices> _logger;
    private readonly IMapper _mapper;
    public CategoriasServices(
        ICategoriasRepository categoriasRepository,
        ILogger<CategoriasServices> logger,
        IMapper mapper)
    {
        _categoriasRepository = categoriasRepository;
        _logger = logger;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> Create(CrearOActualizarCategoriaDto nuevaCategoria)
    {
        var categoria = await this._categoriasRepository.Get(nuevaCategoria.Id);
        if (categoria is null)
        {
            throw new InvalidOperationException($"Ya existe una categoria con el Id: {nuevaCategoria.Id}");
        }

        _logger.LogInformation($"Creando una nueva categoria con el nombre: {nuevaCategoria.Nombre}");
        categoria = await this._categoriasRepository.Add(nuevaCategoria);
        return this._mapper.Map<CategoriaDto>(categoria);
    }

    /// <inheritdoc/>
    public async Task<CategoriaDto> Update(CrearOActualizarCategoriaDto categoria)
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

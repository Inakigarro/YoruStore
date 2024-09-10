using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tienda.Contracts.Categorias;
using Tienda.Contracts.Repositories;
using Tienda.Domain;
using Tienda.Infrastructure.Repositories;

namespace Tienda.Infrastructure.UnitTests;

public class CategoriasUnitTests
{
    private ServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TiendaDbContext>(opts =>
            opts.UseInMemoryDatabase("TestDb"));
        services.AddTransient<ICategoriasRepository, CategoriasRepository>();
        this._serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        var dbContext = this._serviceProvider.GetRequiredService<TiendaDbContext>();
        dbContext.Database.EnsureDeleted();
        this._serviceProvider.Dispose();
    }

    [Test]
    public async Task Add_ConDataValida_DebeAgregarYDevolverCategoriaAgregada()
    {
        // Arrange.
        using var scope = this._serviceProvider.CreateScope();
        // Repositorio a probar.
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        // Categoria a guardar en db.
        Guid categoriaId = Guid.NewGuid();
        CrearOActualizarCategoriaDto nuevaCategoria = new CrearOActualizarCategoriaDto()
        {
            Id = categoriaId,
            Nombre = "Medias",
        };

        // Act.
        var categoria = await repository.Add(nuevaCategoria);

        // Assert.
        Assert.That(categoria, Is.Not.Null);
        Assert.That(categoria.Nombre, Is.EqualTo("Medias"));
    }

    [Test]
    public async Task Actualizar_ConDataValida_DebeActualizarYDevolverCategoriaActualizada()
    {
        // Arrange.
        using var scope = this._serviceProvider.CreateScope();
        // Repositorio a probar.
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        // Agrego categoria a modificar.
        CrearOActualizarCategoriaDto categoria = new CrearOActualizarCategoriaDto()
        {
            Id = Guid.NewGuid(),
            Nombre = "Medias",
        };

        Categoria nuevaCategoria = await repository.Add(categoria);

        // Me aseguro que la categoria se creo correctamente.
        Assert.That(nuevaCategoria, Is.Not.Null);
        Assert.That(nuevaCategoria.Nombre, Is.EqualTo("Medias"));

        CrearOActualizarCategoriaDto categoriaAModificar = new CrearOActualizarCategoriaDto()
        {
            Id = nuevaCategoria.Id, 
            Nombre = "Pantalones",
        };

        // Act.
        Categoria categoriaActualizada = await repository.Update(categoriaAModificar);

        // Assert.
        Assert.That(categoriaActualizada, Is.Not.Null);
        Assert.That(categoriaActualizada.Nombre, Is.EqualTo("Pantalones"));
    }
}
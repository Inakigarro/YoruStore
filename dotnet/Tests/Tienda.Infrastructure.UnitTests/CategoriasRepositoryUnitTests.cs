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
        CrearCategoriaDto nuevaCategoria = new CrearCategoriaDto()
        {
            Nombre = "Medias",
        };

        // Act.
        var categoria = await repository.AddAsync(nuevaCategoria, default);

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
        CrearCategoriaDto categoria = new CrearCategoriaDto()
        {
            Nombre = "Medias",
        };

        Categoria nuevaCategoria = await repository.AddAsync(categoria, default);

        // Me aseguro que la categoria se creo correctamente.
        Assert.That(nuevaCategoria, Is.Not.Null);
        Assert.That(nuevaCategoria.Nombre, Is.EqualTo("Medias"));

        ActualizarCategoriaDto categoriaAModificar = new ActualizarCategoriaDto()
        {
            Id = nuevaCategoria.Id, 
            Nombre = "Pantalones",
        };

        // Act.
        Categoria categoriaActualizada = await repository.UpdateAsync(categoriaAModificar, default);

        // Assert.
        Assert.That(categoriaActualizada, Is.Not.Null);
        Assert.That(categoriaActualizada.Nombre, Is.EqualTo("Pantalones"));
    }

    [Test]
    public async Task ActualizarCategoria_AgregandoUnItemALista_DebeDevolverCategoriaConItemAgregado()
    {
        // Arrange.
        Guid itemId = Guid.NewGuid();
        Item item = new Item(itemId);
        item.SetTitulo("Titulo");
        item.SetDescripcion("Descripcion");
        item.SetPrecio(1000);

        using var scope = this._serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TiendaDbContext>();
        dbContext.Items.Add(item);
        dbContext.SaveChanges();

        // Repositorio a probar.
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        // Agrego categoria a modificar.
        CrearCategoriaDto categoria = new CrearCategoriaDto()
        {
            Nombre = "Medias",
        };

        Categoria nuevaCategoria = await repository.AddAsync(categoria, default);

        // Me aseguro que la categoria se creo correctamente.
        Assert.That(nuevaCategoria, Is.Not.Null);
        Assert.That(nuevaCategoria.Nombre, Is.EqualTo("Medias"));

        ActualizarCategoriaDto categoriaAModificar = new ActualizarCategoriaDto()
        {
            Id = nuevaCategoria.Id,
            Nombre = nuevaCategoria.Nombre,
            Items = new List<Guid>() { itemId }
        };

        // Act.
        Categoria categoriaActualizada = await repository.UpdateAsync(categoriaAModificar, default);

        // Assert.
        Assert.That(categoriaActualizada, Is.Not.Null);
        Assert.That(categoriaActualizada.Items, Has.Count.EqualTo(1));
        Assert.That(categoriaActualizada.Items, Does.Contain(item));
    }
}
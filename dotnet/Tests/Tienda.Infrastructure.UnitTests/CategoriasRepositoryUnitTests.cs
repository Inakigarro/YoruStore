using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tienda.Contracts.Repositories;
using Tienda.Domain;
using Tienda.Infrastructure.Repositories;
using Tienda.Utilities.Attributes;

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
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddInfrastructureDependencies();
        
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
    public async Task Add_ConDataValida_DebeAgregarCategoria()
    {
        // Arrange.
        using var scope = this._serviceProvider.CreateScope();
        // Repositorio a probar.
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        // Categoria a guardar en db.
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);

        // Act.
        await repository.AddAsync(categoria, default);
        await repository.SaveAsync(default);

        // Assert.
        Categoria categoriaGuardada = await repository.GetAsync(categoria.Id, default);
        Assert.That(categoriaGuardada, Is.Not.Null);
        Assert.That(categoria.Nombre, Is.EqualTo(nombreCategoria));
    }

    [Test]
    public async Task Actualizar_ConDataValida_DebeActualizarYDevolverCategoriaActualizada()
    {
        // Arrange.
        using var scope = this._serviceProvider.CreateScope();
        // Repositorio a probar.
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        // Agrego categoria a modificar.
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);

        await repository.AddAsync(categoria, default);
        await repository.SaveAsync(default);

        // Me aseguro que la categoria se creo correctamente.
        Categoria categoriaGuardada = await repository.GetAsync(categoria.Id, default);
        Assert.That(categoriaGuardada, Is.Not.Null);
        Assert.That(categoria.Nombre, Is.EqualTo(nombreCategoria));
        
        // Act.
        string nuevoNombre = "Nuevo Nombre";
        categoriaGuardada.SetNombre(nuevoNombre);
        repository.Update(categoriaGuardada);
        await repository.SaveAsync(default);
        
        // Assert.
        Assert.That(categoriaGuardada, Is.Not.Null);
        Assert.That(categoriaGuardada.Nombre, Is.EqualTo(nuevoNombre));
    }

    [Test]
    public async Task ActualizarCategoria_AgregandoUnItemALista_DebeActualizarCategoriaItem()
    {
        // Arrange.
        Item item = new Item();
        item.SetTitulo("Titulo");
        item.SetDescripcion("Descripcion");
        item.SetPrecio(1000);

        using var scope = this._serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TiendaDbContext>();
        dbContext.Items.Add(item);
        dbContext.SaveChanges(default);

        // Repositorio a probar.
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        // Agrego categoria a modificar.
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);

        await repository.AddAsync(categoria, default);
        await repository.SaveAsync(default);
        
        // Me aseguro que la categoria se creo correctamente.
        var categoriaGuardada = await repository.GetAsync(categoria.Id, default);
        Assert.That(categoriaGuardada, Is.Not.Null);
        Assert.That(categoriaGuardada.Nombre, Is.EqualTo(nombreCategoria));

        // Act.
        categoriaGuardada.AddItem(item);
        repository.Update(categoriaGuardada);
        await repository.SaveAsync(default);

        // Assert.
        Categoria categoriaActualizada = await repository.GetAsync(categoriaGuardada.Id, default);
        Assert.That(categoriaActualizada, Is.Not.Null);
        Assert.That(categoriaActualizada.Items, Has.Count.EqualTo(1));
        Assert.That(categoriaActualizada.Items, Does.Contain(item));
    }
}
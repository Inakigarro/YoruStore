using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tienda.Contracts.Items;
using Tienda.Contracts.Repositories;
using Tienda.Domain;
using Tienda.Infrastructure.Repositories;

namespace Tienda.Infrastructure.UnitTests;

public class ItemsRepositoryUnitTests
{
    private ServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TiendaDbContext>(
            opts => opts.UseInMemoryDatabase("TestDb"));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<ICategoriasRepository, CategoriasRepository>()
            .AddScoped<IItemsRepository, ItemsRepository>()
            .AddLogging();
        _serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        var dbContext = this._serviceProvider.GetRequiredService<TiendaDbContext>();
        dbContext.Database.EnsureDeleted();
        this._serviceProvider.Dispose();
    }

    [Test]
    public async Task AgregarUnItem_ConDataValida_DeberiaAgregarItem()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        string nuevoTitulo = "Titulo";
        string nuevaDescripcion = "Descripcion";
        double nuevoPrecio = 1000;
        Item item = new();
        item.SetTitulo(nuevoTitulo);
        item.SetDescripcion(nuevaDescripcion);
        item.SetPrecio(nuevoPrecio);

        // Act.
        await repository.AddAsync(item, default);
        await repository.SaveAsync(default);

        // Assert.
        Item itemGuardado = await repository.GetAsync(item.Id, default);
        Assert.That(itemGuardado, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(itemGuardado.Titulo, Is.EqualTo(nuevoTitulo));
            Assert.That(itemGuardado.Descripcion, Is.EqualTo(nuevaDescripcion));
            Assert.That(itemGuardado.Precio, Is.EqualTo(nuevoPrecio));
        });
    }
    
    [Test]
    public async Task ActualizarItem_ConDataValida_DeberiaActualizar()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        string nuevoTitulo = "Titulo";
        string nuevaDescripcion = "Descripcion";
        double nuevoPrecio = 1000;
        Item item = new();
        item.SetTitulo(nuevoTitulo);
        item.SetDescripcion(nuevaDescripcion);
        item.SetPrecio(nuevoPrecio);

        await repository.AddAsync(item, default);
        await repository.SaveAsync(default);

        Item itemGuardado = await repository.GetAsync(item.Id, default);
        Assert.That(itemGuardado, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(itemGuardado.Titulo, Is.EqualTo(nuevoTitulo));
            Assert.That(itemGuardado.Descripcion, Is.EqualTo(nuevaDescripcion));
            Assert.That(itemGuardado.Precio, Is.EqualTo(nuevoPrecio));
        });

        // Act.
        double precioActualizado = 1456.2;
        itemGuardado.SetPrecio(precioActualizado);
        repository.Update(itemGuardado);
        await repository.SaveAsync(default);

        // Assert.
        Assert.That(itemGuardado, Is.Not.Null);
        Assert.That(itemGuardado.Precio, Is.EqualTo(precioActualizado));
    }

    [Test]
    public async Task EliminarItem_ConDataValida_DeberiaEliminarItem()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        string nuevoTitulo = "Titulo";
        string nuevaDescripcion = "Descripcion";
        double nuevoPrecio = 1000;
        Item item = new();
        item.SetTitulo(nuevoTitulo);
        item.SetDescripcion(nuevaDescripcion);
        item.SetPrecio(nuevoPrecio);

        await repository.AddAsync(item, default);
        await repository.SaveAsync(default);

        Item itemGuardado = await repository.GetAsync(item.Id, default);
        Assert.That(itemGuardado, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(itemGuardado.Titulo, Is.EqualTo(nuevoTitulo));
            Assert.That(itemGuardado.Descripcion, Is.EqualTo(nuevaDescripcion));
            Assert.That(itemGuardado.Precio, Is.EqualTo(nuevoPrecio));
        });
        
        // Act.
        await repository.Delete(item.Id, default);
        await repository.SaveAsync(default);
        // Assert.
        Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.GetAsync(item.Id, default));
    }
    
    [Test]
    public async Task EliminarItem_ConDataInvalida_DeberiaLanzarException()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        
        // Assert.
        Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Delete(Guid.NewGuid(), default));
    }

    [Test]
    public async Task ObtenerPorFiltro_ConDataValida_DeberiaObtenerListaDeItemsFiltradas()
    {
        // Arrange.
        string titulo1 = "Titulo 1";
        string titulo2 = "Titulo 2";
        Item item1 = new();
        item1.SetTitulo(titulo1);
        item1.SetDescripcion("Descripcion");
        item1.SetPrecio(1000);
        Item item2 = new();
        item2.SetTitulo(titulo2);
        item2.SetDescripcion("Descripcion");
        item2.SetPrecio(1000);
        
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);
        categoria.AddItem(item1);
        categoria.AddItem(item2);
        
        using var scope = _serviceProvider.CreateScope();
        var categoriasRepository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        await repository.AddAsync(item1, default);
        await categoriasRepository.AddAsync(categoria, default);
        await repository.SaveAsync(default);
        
        // Act.
        var itemsFiltrados = await repository.GetByFilterAsync(categoria.Id, "2", default);
        
        // Assert.
        Assert.That(itemsFiltrados, Is.Not.Empty);
        Assert.That(itemsFiltrados, Does.Contain(item2));
    }

    [Test]
    public async Task ObtenerTodosPorCategoria_ConDataValida_DeberiaDevolverListaDeItems()
    {
        string titulo1 = "Titulo 1";
        string titulo2 = "Titulo 2";
        Item item1 = new();
        item1.SetTitulo(titulo1);
        item1.SetDescripcion("Descripcion");
        item1.SetPrecio(1000);
        Item item2 = new();
        item2.SetTitulo(titulo2);
        item2.SetDescripcion("Descripcion");
        item2.SetPrecio(1000);
        
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);
        categoria.AddItem(item1);
        categoria.AddItem(item2);
        
        using var scope = _serviceProvider.CreateScope();
        var categoriasRepository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        await repository.AddAsync(item1, default);
        await categoriasRepository.AddAsync(categoria, default);
        await repository.SaveAsync(default);
        
        // Act.
        var itemsFiltrados = await repository.GetAllByCategoriaIdAsync(categoria.Id, 10, 0, default);
        
        // Assert.
        Assert.That(itemsFiltrados, Is.Not.Empty);
        Assert.That(itemsFiltrados.Count(), Is.EqualTo(2));
    }
}

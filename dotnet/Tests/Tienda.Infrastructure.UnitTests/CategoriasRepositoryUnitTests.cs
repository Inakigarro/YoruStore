using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<ICategoriasRepository, CategoriasRepository>()
            .AddScoped<IItemsRepository, ItemsRepository>()
            .AddLogging();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        var dbContext = _serviceProvider.GetRequiredService<TiendaDbContext>();
        dbContext.Database.EnsureDeleted();
        _serviceProvider.Dispose();
    }

    [Test]
    public async Task Add_ConDataValida_DebeAgregarCategoria()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
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
        Assert.Multiple(() =>
        {
            Assert.That(categoriaGuardada, Is.Not.Null);
            Assert.That(categoria.Nombre, Is.EqualTo(nombreCategoria));
        });
    }

    [Test]
    public async Task Actualizar_ConDataValida_DebeActualizarYDevolverCategoriaActualizada()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
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
        Assert.Multiple(() =>
        {
            Assert.That(categoriaGuardada, Is.Not.Null);
            Assert.That(categoria.Nombre, Is.EqualTo(nombreCategoria));
        });

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

        using var scope = _serviceProvider.CreateScope();
        var itemsRepository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        await itemsRepository.AddAsync(item, default);
        await itemsRepository.SaveAsync(default);

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

    [Test]
    public async Task EliminarCategoria_ConDataValida_DeberiaBorrarCategoriaDeBaseDeDatos()
    {
        // Arrange.
        Categoria categoria = new();
        categoria.SetNombre("Nombre");

        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();

        await repository.AddAsync(categoria, default);
        await repository.SaveAsync(default);
        
        // Verifico que se haya creado correctamente.
        Assert.That(categoria, Is.Not.Null);
        Assert.That(categoria.Nombre, Is.EqualTo("Nombre"));
        
        // Act.
        await repository.Delete(categoria.Id, default);
        await repository.SaveAsync(default);
        
        // Assert.
        Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.GetAsync(categoria.Id, default));
    }

    [Test]
    public async Task EliminarCategoria_ConDataInvalida_DeberiaLanzarException()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        
        // Assert.
        Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Delete(Guid.NewGuid(), default));
    }

    [Test]
    public async Task ObtenerComoQuery_ConIdValido_DeberiaDevolverCategoria()
    {
        // Arrange.
        Item item = new Item();
        item.SetTitulo("Titulo");
        item.SetDescripcion("Descripcion");
        item.SetPrecio(1000);
        
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);
        categoria.AddItem(item);
        
        using var scope = _serviceProvider.CreateScope();
        var itemsRepository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        await itemsRepository.AddAsync(item, default);
        await repository.AddAsync(categoria, default);
        await itemsRepository.SaveAsync(default);
        
        // Act.
        var categoriaConItems = await repository.GetAsQueryableAsync(categoria.Id)
            .Include(cat => cat.Items)
            .FirstOrDefaultAsync(default);
        
        // Assert.
        Assert.That(categoriaConItems, Is.Not.Null);
        Assert.That(categoriaConItems.Items, Is.Not.Empty);
        Assert.That(categoriaConItems.Items, Does.Contain(item));
    }

    [Test]
    public async Task ObtenerPorNombre_ConDataValida_DeberiaObtenerCategoria()
    {
        // Arrange.
        Item item = new Item();
        item.SetTitulo("Titulo");
        item.SetDescripcion("Descripcion");
        item.SetPrecio(1000);
        
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);
        categoria.AddItem(item);
        
        using var scope = _serviceProvider.CreateScope();
        var itemsRepository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        await itemsRepository.AddAsync(item, default);
        await repository.AddAsync(categoria, default);
        await itemsRepository.SaveAsync(default);
        
        // Act.
        var categoriaGuardada = await repository.GetByNombreAsync(nombreCategoria, default);
        
        // Assert.
        Assert.That(categoriaGuardada, Is.Not.Null);
        Assert.That(categoriaGuardada.Items, Is.Not.Empty);
    }
    
    [Test]
    public async Task ObtenerPorNombre_ConDataInvalida_DeberiaLanzarException()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        
        // Act.
        Assert.ThrowsAsync<ArgumentException>(async () => await repository.GetByNombreAsync("nombre", default));
    }
    
    [Test]
    public async Task ObtenerTodas_DeberiaDevolverTodasLasCategorias()
    {
        // Arrange.
        string nombreCat1 = "Nombre1";
        string nombreCat2 = "Nombre2";

        Categoria cat1 = new();
        cat1.SetNombre(nombreCat1);

        Categoria cat2 = new();
        cat2.SetNombre(nombreCat2);

        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        
        // Me aseguro que las categorias se hayan guardado correctamente.
        repository.AddAsync(cat1, default);
        repository.AddAsync(cat2, default);
        repository.SaveAsync(default);

        var categoriaGuardada1 = await repository.GetAsync(cat1.Id, default);
        var categoriaGuardada2 = await repository.GetAsync(cat2.Id, default);
        
        Assert.Multiple(() =>
        {
            Assert.That(categoriaGuardada1, Is.Not.Null);
            Assert.That(categoriaGuardada2, Is.Not.Null);
        });
        
        // Act.
        var categorias = await repository.GetAll().ToListAsync(default);
        
        // Assert.
        Assert.That(categorias, Is.Not.Empty);
    }

    [Test]
    public async Task ObtenerItemsPorCategoria_ConDataValida_DeberiaObtenerUnaListaDeItems()
    {
        // Arrange.
        Item item = new Item();
        item.SetTitulo("Titulo");
        item.SetDescripcion("Descripcion");
        item.SetPrecio(1000);
        
        string nombreCategoria = "Nombre";
        Categoria categoria = new();
        categoria.SetNombre(nombreCategoria);
        categoria.AddItem(item);
        
        using var scope = _serviceProvider.CreateScope();
        var itemsRepository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        await itemsRepository.AddAsync(item, default);
        await repository.AddAsync(categoria, default);
        await itemsRepository.SaveAsync(default);
        
        // Act.
        var categoriaGuardada = await repository.GetByCategoria(categoria.Id, default);
        
        // Assert.
        Assert.That(categoriaGuardada, Is.Not.Empty);
    }
    
    [Test]
    public async Task ObtenerItemsPorCategoria_ConDataInvalida_DeberiaLanzarException()
    {
        // Arrange.
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ICategoriasRepository>();
        
        // Act.
        Assert.ThrowsAsync<ArgumentException>(async () => await repository.GetByCategoria(Guid.NewGuid(), default));
    }
}
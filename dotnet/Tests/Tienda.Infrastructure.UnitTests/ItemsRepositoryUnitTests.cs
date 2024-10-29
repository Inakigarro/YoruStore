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
        services.AddTransient<IItemsRepository, ItemsRepository>();
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
    public async Task AgregarUnItem_ConDataValida_DebeAgregarYDevolverItemCompleto()
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
        Assert.That(itemGuardado.Titulo, Is.EqualTo(nuevoTitulo));
        Assert.That(itemGuardado.Descripcion, Is.EqualTo(nuevaDescripcion));
        Assert.That(itemGuardado.Precio, Is.EqualTo(nuevoPrecio));
    }

    [Test]
    public async Task ActualizarItem_ConDataValida_DebeActualizarYDevolverItemCompletamenteActualizado()
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
        Assert.That(itemGuardado.Titulo, Is.EqualTo(nuevoTitulo));
        Assert.That(itemGuardado.Descripcion, Is.EqualTo(nuevaDescripcion));
        Assert.That(itemGuardado.Precio, Is.EqualTo(nuevoPrecio));

        // Act.
        double precioActualizado = 1456.2;
        itemGuardado.SetPrecio(precioActualizado);
        repository.Update(itemGuardado);
        await repository.SaveAsync(default);

        // Assert.
        Assert.That(itemGuardado, Is.Not.Null);
        Assert.That(itemGuardado.Precio, Is.EqualTo(precioActualizado));
    }

}

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
        using var scope = this._serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        Guid itemId = Guid.NewGuid();
        CrearItemDto nuevoItem = new CrearItemDto()
        {
            Titulo = "Titulo",
            Descripcion = "Descripcion",
            Precio = 1000,
        };

        // Act.
        var item = await repository.AddAsync(nuevoItem, default);

        // Assert.
        Assert.That(item, Is.Not.Null);
        Assert.That(item.Titulo, Is.EqualTo("Titulo"));
        Assert.That(item.Descripcion, Is.EqualTo("Descripcion"));
        Assert.That(item.Precio, Is.EqualTo(1000));
    }

    [Test]
    public async Task ActualizarItem_ConDataValida_DebeActualizarYDevolverItemCompletamenteActualizado()
    {
        // Arrange.
        using var scope = this._serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IItemsRepository>();
        Guid itemId = Guid.NewGuid();
        CrearItemDto item = new CrearItemDto()
        {
            Titulo = "Titulo",
            Descripcion = "Descripcion",
            Precio = 1000,
        };

        var nuevoItem = await repository.AddAsync(item, default);

        Assert.That(nuevoItem, Is.Not.Null);

        ActualizarItemDto itemAModificar = new ActualizarItemDto()
        {
            Id = nuevoItem.Id,
            Titulo = nuevoItem.Titulo,
            Descripcion = nuevoItem.Descripcion,
            Precio = 2000,
        };

        // Act.
        Item itemActualizado = await repository.UpdateAsync(itemAModificar, default);

        // Assert.
        Assert.That(itemActualizado, Is.Not.Null);
        Assert.That(itemActualizado.Precio, Is.EqualTo(itemAModificar.Precio));
    }

}

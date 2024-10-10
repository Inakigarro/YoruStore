using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;
using Tienda.Infrastructure;
using Tienda.Infrastructure.AutoMapper;
using Tienda.Infrastructure.Repositories;
using Tienda.Infrastructure.Services;

namespace Tienda.InitialData;

internal class Program
{
    static async Task Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

        var services = new ServiceCollection();
        services.AddDbContext<TiendaDbContext>(opts =>
            opts
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(TiendaDbContext).Assembly.FullName)));

        services.AddLogging(cfg => cfg.AddConsole());

        // Automapper.
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new CategoriaProfile());
            cfg.AddProfile(new ItemProfile());
        });
        // Categorias.
        services
            .AddScoped<ICategoriasRepository, CategoriasRepository>()
            .AddScoped<ICategoriasService, CategoriasServices>();

        // Items.
        services
            .AddScoped<IItemsRepository, ItemsRepository>()
            .AddScoped<IItemsService, ItemsService>();

        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var categoriaService = scope.ServiceProvider.GetRequiredService<ICategoriasService>();
        var itemService = scope.ServiceProvider.GetRequiredService<IItemsService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Creando categorias.");
        foreach (var categoria in CrearCategorias.categorias)
        {
            logger.LogInformation($"Categoria {categoria.Nombre}");
            await categoriaService.CreateAsync(categoria, new CancellationToken());
        }
        logger.LogInformation("Proceso de creacion de categorias terminado.");

        logger.LogInformation("Creando items");
        foreach (var item in CrearItems.Items)
        {
            logger.LogInformation($"Item {item.Item.Titulo} para cateogira {item.CategoriaNombre}");
            await itemService.CreateAsync(item.Item, new CancellationToken(), categoriaNombre: item.CategoriaNombre);
        }
        logger.LogInformation("Proceso de creacion de items terminado.");
    }
}

using Microsoft.EntityFrameworkCore;
using Tienda.Contracts.Repositories;
using Tienda.Contracts.Services;
using Tienda.Infrastructure;
using Tienda.Infrastructure.AutoMapper;
using Tienda.Infrastructure.Repositories;
using Tienda.Infrastructure.Services;

namespace Tienda.Api;

public class Program
{
    public const string LocalHostOrigin = "LocalHostOrigin";
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<TiendaDbContext>(opts =>
            opts
                .EnableSensitiveDataLogging()
                .UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(TiendaDbContext).Assembly.FullName)));

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new CategoriaProfile());
            cfg.AddProfile(new ItemProfile());
        });

        // Categorias.
        builder.Services
            .AddScoped<ICategoriasRepository, CategoriasRepository>()
            .AddScoped<ICategoriasService, CategoriasServices>();

        // Items.
        builder.Services
            .AddScoped<IItemsRepository, ItemsRepository>()
            .AddScoped<IItemsService, ItemsService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy(LocalHostOrigin, policy =>
                policy.WithOrigins("http://localhost:4200")
                    .WithHeaders("Content-Type"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(LocalHostOrigin);
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

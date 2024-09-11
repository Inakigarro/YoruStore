
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
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<TiendaDbContext>(opts =>
            opts.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                assembly => assembly.MigrationsAssembly(typeof(TiendaDbContext).Assembly.FullName)));

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new CategoriaProfile());
        });

        builder.Services.AddTransient<ICategoriasRepository,CategoriasRepository>();
        builder.Services.AddTransient<ICategoriasService, CategoriasServices>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

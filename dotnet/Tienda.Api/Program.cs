using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tienda.Contracts.Auth;
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
                .UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(TiendaDbContext).Assembly.FullName)));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => opts.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<TiendaDbContext>();

        var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
        builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
        var jwtSettings = jwtSection.Get<JwtBearerTokenSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        builder.Services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            opts.RequireHttpsMetadata = false;
            opts.SaveToken = true;
            opts.TokenValidationParameters = new TokenValidationParameters()
            { 
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });

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
        builder.Services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Tienda.Api",
                Version = "v1"
            });
            opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Auth header using Bearer scheme."
            });
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

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
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

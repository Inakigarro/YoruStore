using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tienda.Domain;
using Tienda.Infrastructure.EntityConfigurations;

namespace Tienda.Infrastructure;

public class TiendaDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoriaEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration());
    }
}

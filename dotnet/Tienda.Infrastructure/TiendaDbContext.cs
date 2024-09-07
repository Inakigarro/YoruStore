using Microsoft.EntityFrameworkCore;
using Tienda.Domain;
using Tienda.Infrastructure.EntityConfigurations;

namespace Tienda.Infrastructure;

public class TiendaDbContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Item> Items { get; set; }

    public TiendaDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoriaEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration());
    }
}

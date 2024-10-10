using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain;

namespace Tienda.Infrastructure.EntityConfigurations;

public class CategoriaEntityTypeConfiguration: IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Nombre).IsUnique();
        
        builder.Property(x => x.Nombre)
            .IsRequired();

        builder.HasMany(x => x.Items)
            .WithOne();
    }
}

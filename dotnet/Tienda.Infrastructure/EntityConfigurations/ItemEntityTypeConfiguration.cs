using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain;

namespace Tienda.Infrastructure.EntityConfigurations;

public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titulo)
            .IsRequired();
        builder.Property(x => x.Descripcion)
            .IsRequired();
        builder.Property(x => x.Precio)
            .IsRequired();
    }
}

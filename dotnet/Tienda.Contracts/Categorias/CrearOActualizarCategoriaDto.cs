using Tienda.Contracts.Items;

namespace Tienda.Contracts.Categorias;

public record CrearOActualizarCategoriaDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public IEnumerable<Guid> Items { get; set; } = Enumerable.Empty<Guid>();
}

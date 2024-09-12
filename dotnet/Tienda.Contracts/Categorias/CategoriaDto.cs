using Tienda.Contracts.Items;

namespace Tienda.Contracts.Categorias;

/// <summary>
/// Dto utilizado para representar una categoria.
/// </summary>
public record CategoriaDto
{
    /// <summary>
    /// Obtiene o asigna el Id de la Categoria.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtiene o asigna el Nombre de la Categoria.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o asigna la lista de items pertenecientes a la Categoria.
    /// </summary>
    public IEnumerable<ItemDto> Items { get; set; } = Enumerable.Empty<ItemDto>();
}

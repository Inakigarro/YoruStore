using Tienda.Contracts.Items;

namespace Tienda.Contracts.Categorias;

/// <summary>
/// Dto utilizado para actualizar una categoria.
/// </summary>
public record ActualizarCategoriaDto
{
    /// <summary>
    /// Obtiene o asigna el Id de la categoria.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtiene o asigna el nombre de la categoria.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o asigna la lista de items que tiene la categoria.
    /// </summary>
    public IEnumerable<Guid> Items { get; set; } = [];
}

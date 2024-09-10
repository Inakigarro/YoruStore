using Tienda.Contracts.Categorias;
using Tienda.Domain;

namespace Tienda.Contracts.Items;

/// <summary>
/// Dto utilizado para crear o actualizar un Item.
/// </summary>
public record CrearOActualizarItemDto
{
    /// <summary>
    /// Obtiene o asigna el Id del Item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtiene o asigna el Titulo del Item.
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o asigna la Descripcion del Item.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o asigna el Precio del Item.
    /// </summary>
    public double Precio { get; set; } = double.NaN;
}

namespace Tienda.Contracts.Items;

/// <summary>
/// Dto utilizado para crear un Item.
/// </summary>
public record CrearItemDto
{
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

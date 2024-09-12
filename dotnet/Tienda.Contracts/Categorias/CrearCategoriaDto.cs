namespace Tienda.Contracts.Categorias;

/// <summary>
/// Dto utilizado para crear una Categoria.
/// </summary>
public record CrearCategoriaDto
{
    /// <summary>
    /// Obtiene o asigna el nombre de la categoria.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;
}

using Tienda.Contracts.Categorias;

namespace Tienda.InitialData;

public class CrearCategorias
{
    public static IEnumerable<CrearCategoriaDto> categorias = new List<CrearCategoriaDto>()
    {
        new() { Nombre = Categorias.Remeras},
        new() { Nombre = Categorias.Pantalones },
        new() { Nombre = Categorias.Abrigos },
        new() { Nombre = Categorias.Vestidos },
        new() { Nombre = Categorias.Polleras },
        new() { Nombre = Categorias.Interior },
        new() { Nombre = Categorias.Deportiva },
        new() { Nombre = Categorias.Baño },
        new() { Nombre = Categorias.Zapatos },
        new() { Nombre = Categorias.Accesorios }
    };
}

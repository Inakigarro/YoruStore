using Tienda.Contracts.Items;

namespace Tienda.InitialData;

public class CrearItem
{
    public string CategoriaNombre { get; set; }
    public CrearItemDto Item { get; set; }
}

public class CrearItems
{
    public static IEnumerable<CrearItem> Items = new List<CrearItem>()
    {
        // Accesorios.
        new()
        {
            CategoriaNombre = Categorias.Accesorios,
            Item = new()
            {
                Titulo = "Accesorio 1",
                Descripcion = "Accesorio 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Accesorios,
            Item = new()
            {
                Titulo = "Accesorio 2",
                Descripcion = "Accesorio 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Accesorios,
            Item = new()
            {
                Titulo = "Accesorio 3",
                Descripcion = "Accesorio 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Accesorios,
            Item = new()
            {
                Titulo = "Accesorio 4",
                Descripcion = "Accesorio 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Accesorios,
            Item = new()
            {
                Titulo = "Accesorio 5",
                Descripcion = "Accesorio 5",
                Precio = 1000
            }
        },

        // Abrigos.
        new()
        {
            CategoriaNombre = Categorias.Abrigos,
            Item = new()
            {
                Titulo = "Abrigo 1",
                Descripcion = "Abrigo 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Abrigos,
            Item = new()
            {
                Titulo = "Abrigo 2",
                Descripcion = "Abrigo 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Abrigos,
            Item = new()
            {
                Titulo = "Abrigo 3",
                Descripcion = "Abrigo 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Abrigos,
            Item = new()
            {
                Titulo = "Abrigo 4",
                Descripcion = "Abrigo 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Abrigos,
            Item = new()
            {
                Titulo = "Abrigo 5",
                Descripcion = "Abrigo 5",
                Precio = 1000
            }
        },

        // Baño.
        new()
        {
            CategoriaNombre = Categorias.Baño,
            Item = new()
            {
                Titulo = "Traje de Baño 1",
                Descripcion = "Traje de Baño 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Baño,
            Item = new()
            {
                Titulo = "Traje de Baño 2",
                Descripcion = "Traje de Baño 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Baño,
            Item = new()
            {
                Titulo = "Traje de Baño 3",
                Descripcion = "Traje de Baño 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Baño,
            Item = new()
            {
                Titulo = "Traje de Baño 4",
                Descripcion = "Traje de Baño 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Baño,
            Item = new()
            {
                Titulo = "Traje de Baño 5",
                Descripcion = "Traje de Baño 5",
                Precio = 1000
            }
        },

        // Pantalones.
        new()
        {
            CategoriaNombre = Categorias.Pantalones,
            Item = new()
            {
                Titulo = "Pantalones 1",
                Descripcion = "Pantalones 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Pantalones,
            Item = new()
            {
                Titulo = "Pantalones 2",
                Descripcion = "Pantalones 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Pantalones,
            Item = new()
            {
                Titulo = "Pantalones 3",
                Descripcion = "Pantalones 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Pantalones,
            Item = new()
            {
                Titulo = "Pantalones 4",
                Descripcion = "Pantalones 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Pantalones,
            Item = new()
            {
                Titulo = "Pantalones 5",
                Descripcion = "Pantalones 5",
                Precio = 1000
            }
        },

        // Polleras.
        new()
        {
            CategoriaNombre = Categorias.Polleras,
            Item = new()
            {
                Titulo = "Pollera 1",
                Descripcion = "Pollera 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Polleras,
            Item = new()
            {
                Titulo = "Pollera 2",
                Descripcion = "Pollera 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Polleras,
            Item = new()
            {
                Titulo = "Pollera 3",
                Descripcion = "Pollera 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Polleras,
            Item = new()
            {
                Titulo = "Pollera 4",
                Descripcion = "Pollera 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Polleras,
            Item = new()
            {
                Titulo = "Pollera 5",
                Descripcion = "Pollera 5",
                Precio = 1000
            }
        },

        // Remeras.
        new()
        {
            CategoriaNombre = Categorias.Remeras,
            Item = new()
            {
                Titulo = "Remera 1",
                Descripcion = "Remera 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Remeras,
            Item = new()
            {
                Titulo = "Remera 2",
                Descripcion = "Remera 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Remeras,
            Item = new()
            {
                Titulo = "Remera 3",
                Descripcion = "Remera 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Remeras,
            Item = new()
            {
                Titulo = "Remera 4",
                Descripcion = "Remera 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Remeras,
            Item = new()
            {
                Titulo = "Remera 5",
                Descripcion = "Remera 5",
                Precio = 1000
            }
        },
        
        // Ropa deportiva.
        new()
        {
            CategoriaNombre = Categorias.Deportiva,
            Item = new()
            {
                Titulo = "Ropa Deportiva 1",
                Descripcion = "Ropa Deportiva 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Deportiva,
            Item = new()
            {
                Titulo = "Ropa Deportiva 2",
                Descripcion = "Ropa Deportiva 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Deportiva,
            Item = new()
            {
                Titulo = "Ropa Deportiva 3",
                Descripcion = "Ropa Deportiva 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Deportiva,
            Item = new()
            {
                Titulo = "Ropa Deportiva 4",
                Descripcion = "Ropa Deportiva 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Deportiva,
            Item = new()
            {
                Titulo = "Ropa Deportiva 5",
                Descripcion = "Ropa Deportiva 5",
                Precio = 1000
            }
        },

        // Ropa Interior.
        new()
        {
            CategoriaNombre = Categorias.Interior,
            Item = new()
            {
                Titulo = "Ropa Interior 1",
                Descripcion = "Ropa Interior 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Interior,
            Item = new()
            {
                Titulo = "Ropa Interior 2",
                Descripcion = "Ropa Interior 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Interior,
            Item = new()
            {
                Titulo = "Ropa Interior 3",
                Descripcion = "Ropa Interior 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Interior,
            Item = new()
            {
                Titulo = "Ropa Interior 4",
                Descripcion = "Ropa Interior 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Interior,
            Item = new()
            {
                Titulo = "Ropa Interior 5",
                Descripcion = "Ropa Interior 5",
                Precio = 1000
            }
        },

        // Vestidos.
        new()
        {
            CategoriaNombre = Categorias.Vestidos,
            Item = new()
            {
                Titulo = "Vestido 1",
                Descripcion = "Vestido 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Vestidos,
            Item = new()
            {
                Titulo = "Vestido 2",
                Descripcion = "Vestido 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Vestidos,
            Item = new()
            {
                Titulo = "Vestido 3",
                Descripcion = "Vestido 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Vestidos,
            Item = new()
            {
                Titulo = "Vestido 4",
                Descripcion = "Vestido 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Vestidos,
            Item = new()
            {
                Titulo = "Vestido 5",
                Descripcion = "Vestido 5",
                Precio = 1000
            }
        },

        // Zapatos.
        new()
        {
            CategoriaNombre = Categorias.Zapatos,
            Item = new()
            {
                Titulo = "Zapatos 1",
                Descripcion = "Zapatos 1",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Zapatos,
            Item = new()
            {
                Titulo = "Zapatos 2",
                Descripcion = "Zapatos 2",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Zapatos,
            Item = new()
            {
                Titulo = "Zapatos 3",
                Descripcion = "Zapatos 3",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Zapatos,
            Item = new()
            {
                Titulo = "Zapatos 4",
                Descripcion = "Zapatos 4",
                Precio = 1000
            }
        },
        new()
        {
            CategoriaNombre = Categorias.Zapatos,
            Item = new()
            {
                Titulo = "Zapatos 5",
                Descripcion = "Zapatos 5",
                Precio = 1000
            }
        },
    };
}

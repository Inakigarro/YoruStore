namespace Tienda.Domain;

public class Categoria
{
    public Guid Id { get; private set; }

    public string Nombre { get; private set; } = string.Empty;

    public List<Item> Items { get; private set; }

    public Categoria(Guid id)
    {
        this.Id = id;
        this.Items = new List<Item>();
    }

    /// <summary>
    /// Asigna un nombre a la categoria.
    /// Si el nombre es nulo o una cadena vacia, lanza una excepcion.
    /// </summary>
    /// <param name="nombre">El nombre de la categoria.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentNullException("El nombre de la categoria no puede ser nulo ni estar vacio.");
        }
        this.Nombre = nombre;
    }

    /// <summary>
    /// Agrega un Item a la Categoria. Si el Item ya se encuentra, termina.
    /// </summary>
    /// <param name="item">El item a agregar a la categoria.</param>
    public void AddItem(Item item)
    {
        // Si el item ya se encuentra en la categoria, termino.
        if (this.Items.Any(x => x.Id == item.Id))
        {
            return;
        }
        this.Items.Add(item);
    }
}

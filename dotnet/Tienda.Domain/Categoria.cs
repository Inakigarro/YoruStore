namespace Tienda.Domain;

public class Categoria(Guid id)
{
    public Guid Id { get; private set; } = id;

    public string Nombre { get; private set; } = string.Empty;

    public List<Item> Items { get; private set; } = [];

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
            throw new ArgumentNullException(nameof(nombre), "El nombre de la categoria no puede ser nulo ni estar vacio.");
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

using Tienda.Contracts;
using Tienda.Utilities.Extensions;

namespace Tienda.Domain;

public class Categoria : IId
{
    public Guid Id { get; set; } = Guid.NewGuid();

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
        Nombre = !nombre.IsNullOrWhiteSpace()
            ? nombre
            : throw new ArgumentNullException(nameof(nombre), "El nombre de la categoria no puede ser nulo ni estar vacio.");
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
        item.Categoria = this;
    }

    public void RemoveItem(Item item)
    {
        // Si el item no existe en la categoria, termino.
        if (Items.Any(x => x.Id == item.Id))
            return;
        item.Categoria = null;
        Items.Remove(item);
    }
}

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

    public void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentNullException("El nombre de la categoria no puede ser nulo ni estar vacio.");
        }
        this.Nombre = nombre;
    }

    public void AddItem(Item item)
    {
        if (this.Items.Any(x => x.Id == item.Id))
        {
            throw new InvalidOperationException($"Ya existe un Item con el Id: {item.Id} en la categoria: {this.Nombre}");
        }
        this.Items.Add(item);
    }
}

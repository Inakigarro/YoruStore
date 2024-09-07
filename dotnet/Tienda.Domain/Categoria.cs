namespace Tienda.Domain;

public class Categoria
{
    public Guid Id { get; private set; }

    public string Nombre { get; private set; } = string.Empty;

    public Categoria(Guid id)
    {
        this.Id = id;
    }

    public void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentNullException("El nombre de la categoria no puede ser nulo ni estar vacio.");
        }
        this.Nombre = nombre;
    }
}

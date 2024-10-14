namespace Tienda.Domain;

public class Item(Guid id)
{
    public Guid Id { get; private set; } = id;

    public string Titulo { get; private set; } = string.Empty;

	public string Descripcion { get; private set; } = string.Empty;

	public double Precio { get; private set; } = double.NaN;

	public virtual Guid CategoriaId { get; set; }
	public Categoria Categoria { get; set; } = null!;

    public void SetTitulo(string titulo)
	{
		this.Titulo = string.IsNullOrWhiteSpace(titulo) 
			? throw new ArgumentNullException(nameof(titulo), "El titulo no puede ser nulo ni estar vacio.")
			: titulo;
	}

	public void SetDescripcion(string descripcion)
	{
		this.Descripcion = string.IsNullOrWhiteSpace(descripcion)
			? throw new ArgumentNullException(nameof(descripcion), "La descripcion no puede ser nulo ni estar vacio.")
			: descripcion;
	}

	public void SetPrecio(double precio)
	{
		if (double.IsNaN(precio))
			throw new ArgumentException("El precio debe ser un numero.");

		if (precio <= 0)
			throw new ArgumentException("El precio debe ser mayor a cero.");

		this.Precio = precio;
	}
}

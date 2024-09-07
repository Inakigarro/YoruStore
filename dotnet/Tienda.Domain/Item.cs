namespace Tienda.Domain;

public class Item
{
	public Guid Id { get; private set; }

	public string Titulo { get; private set; } = string.Empty;

	public string Descripcion { get; private set; } = string.Empty;

	public Guid CategoriaId { get; private set; }

	public virtual Categoria Categoria { get; private set; } = null!;

	public double Precio { get; private set; } = double.NaN;

    public Item(Guid id)
    {
		this.Id = id;
    }

	public void SetTitulo(string titulo)
	{
		if (string.IsNullOrWhiteSpace(titulo))
		{
			throw new ArgumentNullException("El titulo no puede ser nulo ni estar vacio.");
		}
		this.Titulo = titulo;
	}

	public void SetDescripcion(string descripcion)
	{
		if (string.IsNullOrWhiteSpace(descripcion))
		{
			throw new ArgumentNullException("La descripcion no puede ser nulo ni estar vacio.");
		}
		this.Descripcion = descripcion;
	}

	public void SetCategoria(Categoria categoria)
	{
		if(categoria is null)
		{
			throw new ArgumentNullException("La categoria no puede ser nulo");
		}

		this.CategoriaId = categoria.Id;
		this.Categoria = categoria;
	}

	public void SetPrecio(double precio)
	{
		if (double.IsNaN(precio))
		{
			throw new ArgumentException("El precio debe ser un numero.");
		}

		if (precio <= 0)
		{
			throw new ArgumentException("El precio debe ser mayor a cero.");
		}

		this.Precio = precio;
	}
}

namespace Tienda.Domain.UnitTests;

public class ItemUnitTests
{
    [Test]
    public void SetTitulo_ConDataValida_DeberiaSettearTitulo()
    {
        // Arrange.
        string nuevoTitulo = "Titulo";
        Item item = new();
        
        // Act.
        item.SetTitulo(nuevoTitulo);
        
        // Assert.
        Assert.Multiple(() =>
        {
            Assert.That(item, Is.Not.Null);
            Assert.That(item.Titulo, Is.EqualTo(nuevoTitulo));
        });
    }

    [Test]
    public void SetTitulo_ConDataInvalida_DeberiaLanzarException()
    {
        Item item = new();
        Assert.Throws<ArgumentNullException>(() => item.SetTitulo(""));
    }

    [Test]
    public void SetDescripcion_ConDataValida_DeberiaSettearDescripcion()
    {
        // Arrange.
        string nuevaDescripcion = "Descripcion";
        Item item = new();
        
        // Act.
        item.SetDescripcion(nuevaDescripcion);
        
        // Assert.
        Assert.Multiple(() =>
        {
            Assert.That(item, Is.Not.Null);
            Assert.That(item.Descripcion, Is.EqualTo(nuevaDescripcion));
        });
    }

    [Test]
    public void SetDescripcion_ConDataInvalida_DeberiaLanzarException()
    {
        Item item = new();

        Assert.Throws<ArgumentNullException>(() => item.SetDescripcion(""));
    }

    [Test]
    public void SetPrecio_ConDataValida_DeberiaSettearPrecio()
    {
        // Arrange.
        double nuevoPrecio = 1000;
        Item item = new();
        
        // Act.
        item.SetPrecio(nuevoPrecio);
        
        // Assert.
        Assert.Multiple(() =>
        {
            Assert.That(item, Is.Not.Null);
            Assert.That(item.Precio, Is.EqualTo(nuevoPrecio));
        });
    }

    [Test]
    public void SetPrecio_ConDoubleNaN_DeberiaLanzarException()
    {
        Item item = new();
        
        Assert.Throws<ArgumentException>(() => item.SetPrecio(double.NaN));
    }
    
    [Test]
    public void SetPrecio_ConPrecioNegativo_DeberiaLanzarException()
    {
        Item item = new();
        
        Assert.Throws<ArgumentException>(() => item.SetPrecio(-1));
    }
}
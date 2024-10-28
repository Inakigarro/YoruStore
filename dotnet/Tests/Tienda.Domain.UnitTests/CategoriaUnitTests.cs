using Tienda.Domain;
namespace Tienda.Domain.UnitTests;

public class CategoriaUnitTests
{
    [Test]
    public void SetNombre_ConDataValida_DeberiaGuardarElNombre()
    {
        // Arrange.
        Categoria categoria1 = new();
        string nombre = "Un Nombre";
        // Act.
        categoria1.SetNombre(nombre);

        // Assert.
        Assert.That(categoria1, Is.Not.Null);
        Assert.That(categoria1.Nombre, Is.EqualTo(nombre));
    }

    [Test]
    public void SetNombre_ConNombreVacio_DeberiaDispararExcepcion()
    {
        // Arrange.
        Categoria categoria1 = new();
        string nombreInvalido = string.Empty;

        // Act.
        Assert.Throws<ArgumentNullException>(() => categoria1.SetNombre(nombreInvalido));
    }

    [Test]
    public void AddItem_ConItemInexistente_DeberiaAgregarItemACategoria()
    {
        // Arrange.
        Categoria categoria = new();
        
        Item item = new();
        item.SetTitulo("Titulo");
        item.SetDescripcion("Descripcion");
        item.SetPrecio(1000);
        
        // Act.
        categoria.AddItem(item);
        
        // Assert.
        Assert.That(categoria.Items, Has.Count.EqualTo(1));
    }
}
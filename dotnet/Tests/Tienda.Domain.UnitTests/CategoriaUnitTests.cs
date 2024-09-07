using Tienda.Domain;
namespace Tienda.Domain.UnitTests;

public class CategoriaUnitTests
{
    [Test]
    public void SetNombre_ConDataValida_DeberiaGuardarElNombre()
    {
        // Arrange.
        Categoria categoria1 = new Categoria(Guid.NewGuid());
        string nombre = "Un Nombre";
        // Act.
        categoria1.SetNombre(nombre);

        // Assert.
        Assert.NotNull(categoria1);
        Assert.That(categoria1.Nombre, Is.EqualTo(nombre));
    }

    [Test]
    public void SetNombre_ConNombreVacio_DeberiaDispararExcepcion()
    {
        // Arrange.
        Categoria categoria1 = new Categoria(Guid.NewGuid());
        string nombreInvalido = string.Empty;

        // Act.
        Assert.Throws<ArgumentNullException>(() => categoria1.SetNombre(nombreInvalido));
    }
}
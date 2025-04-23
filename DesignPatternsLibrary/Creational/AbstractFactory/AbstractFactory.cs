namespace DesignPatternsLibrary.Creational.AbstractFactory;

/// <summary>
/// La interfaz Abstract Factory declara un conjunto de métodos que devuelven
/// diferentes productos abstractos. Estos productos se llaman familia y están
/// relacionados por un tema o concepto de alto nivel. Los productos de una familia
/// suelen ser capaces de colaborar entre sí. Una familia de productos puede
/// tener varias variantes, pero los productos de una variante son incompatibles
/// con los productos de otra.
/// </summary>
public interface IAbstractFactory
{
    IAbstractProductA CreateProductA();

    IAbstractProductB CreateProductB();
}

/// <summary>
/// Concrete Factories producen una familia de productos que pertenecen a una única
/// variante. La fábrica garantiza que los productos resultantes sean compatibles.
/// Nótese que las firmas de los métodos de Concrete Factory devuelven un producto
/// abstracto, mientras que dentro del método se instancia un producto concreto.
/// </summary>
public class ConcreteFactory1 : IAbstractFactory
{
    public IAbstractProductA CreateProductA()
    {
        return new ConcreteProductA1();
    }

    public IAbstractProductB CreateProductB()
    {
        return new ConcreteProductB1();
    }
}

/// <summary>
/// Cada Concrete Factory tiene una variante de producto correspondiente.
/// </summary>
public class ConcreteFactory2 : IAbstractFactory
{
    public IAbstractProductA CreateProductA()
    {
        return new ConcreteProductA2();
    }

    public IAbstractProductB CreateProductB()
    {
        return new ConcreteProductB2();
    }
}
namespace DesignPatternsLibrary.Creational.FactoryMethod.Legacy;

/// <summary>
/// Este archivo se mantiene para compatibilidad con el código existente.
/// La implementación principal del patrón Factory Method ahora está en Creator.cs
/// con el ejemplo práctico de gestión de documentos.
/// </summary>

/// <summary>
/// Define la interfaz para los objetos que el método fábrica crea.
/// </summary>
public interface IProduct
{
    string Operation();
}

/// <summary>
/// Implementa la interfaz IProduct.
/// </summary>
internal class ConcreteProductA : IProduct
{
    public string Operation()
    {
        return "{Resultado de ConcreteProductA}";
    }
}

/// <summary>
/// Implementa la interfaz IProduct.
/// </summary>
internal class ConcreteProductB : IProduct
{
    public string Operation()
    {
        return "{Resultado de ConcreteProductB}";
    }
}
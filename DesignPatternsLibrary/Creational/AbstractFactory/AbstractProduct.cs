namespace DesignPatternsLibrary.Creational.AbstractFactory;

/// <summary>
/// Define una interfaz para un tipo de objeto producto.
/// </summary>
public interface IAbstractProductA
{
    string UsefulFunctionA();
}

/// <summary>
/// Define una interfaz para otro tipo de objeto producto.
/// Todos los productos creados por la misma fábrica concreta deben implementar esta interfaz.
/// </summary>
public interface IAbstractProductB
{
    string UsefulFunctionB();

    // ...pero también puede colaborar con el ProductoA.
    string AnotherUsefulFunctionB(IAbstractProductA collaborator);
}
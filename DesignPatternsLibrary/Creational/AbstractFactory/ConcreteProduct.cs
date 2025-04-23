namespace DesignPatternsLibrary.Creational.AbstractFactory;

/// <summary>
/// Implementa la interfaz IAbstractProductA.
/// </summary>
internal class ConcreteProductA1 : IAbstractProductA
{
    public string UsefulFunctionA()
    {
        return "El resultado del producto A1.";
    }

    // Método requerido por el menú de consola
    public string OperacionA()
    {
        return "Operación A ejecutada en ConcreteProductA1.";
    }
}

/// <summary>
/// Implementa la interfaz IAbstractProductB.
/// </summary>
internal class ConcreteProductB1 : IAbstractProductB
{
    public string UsefulFunctionB()
    {
        return "El resultado del producto B1.";
    }

    public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
    {
        var result = collaborator.UsefulFunctionA();
        return $"El resultado del B1 colaborando con el ({result})";
    }

    // Método requerido por el menú de consola
    public string OperacionB()
    {
        return "Operación B ejecutada en ConcreteProductB1.";
    }
}

/// <summary>
/// Implementa la interfaz IAbstractProductA.
/// </summary>
internal class ConcreteProductA2 : IAbstractProductA
{
    public string UsefulFunctionA()
    {
        return "El resultado del producto A2.";
    }

    // Método requerido por el menú de consola
    public string OperacionA()
    {
        return "Operación A ejecutada en ConcreteProductA2.";
    }
}

/// <summary>
/// Implementa la interfaz IAbstractProductB.
/// </summary>
internal class ConcreteProductB2 : IAbstractProductB
{
    public string UsefulFunctionB()
    {
        return "El resultado del producto B2.";
    }

    public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
    {
        var result = collaborator.UsefulFunctionA();
        return $"El resultado del B2 colaborando con el ({result})";
    }

    // Método requerido por el menú de consola
    public string OperacionB()
    {
        return "Operación B ejecutada en ConcreteProductB2.";
    }
}
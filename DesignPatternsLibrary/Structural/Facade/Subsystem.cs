namespace DesignPatternsLibrary.Structural.Facade;

/// <summary>
/// Algunas clases del subsistema complejo.
/// </summary>
public class Subsystem1
{
    public string Operation1()
    {
        return "Subsystem1: ¡Listo!\n";
    }

    public string OperationN()
    {
        return "Subsystem1: ¡Adelante!\n";
    }
}

/// <summary>
/// Otra clase del subsistema.
/// </summary>
public class Subsystem2
{
    public string Operation1()
    {
        return "Subsystem2: ¡Prepárate!\n";
    }

    public string OperationZ()
    {
        return "Subsystem2: ¡Fuego!\n";
    }
}
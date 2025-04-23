namespace DesignPatternsLibrary.Structural.Decorator;

/// <summary>
/// Concrete Components proporcionan implementaciones predeterminadas de las operaciones.
/// Puede haber varias variaciones de estas clases.
/// </summary>
public class ConcreteComponent : IComponent
{
    public string Operation()
    {
        return "ConcreteComponent";
    }
}
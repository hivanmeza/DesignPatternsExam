namespace DesignPatternsLibrary.Structural.Decorator;

/// <summary>
/// La interfaz Component define operaciones que pueden ser alteradas por los decoradores.
/// </summary>
public interface IComponent
{
    string Operation();
}
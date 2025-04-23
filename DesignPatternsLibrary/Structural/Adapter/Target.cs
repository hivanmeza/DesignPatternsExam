namespace DesignPatternsLibrary.Structural.Adapter;

/// <summary>
/// La interfaz Target define la interfaz específica del dominio utilizada por el código del cliente.
/// </summary>
public interface ITarget
{
    string GetRequest();
}
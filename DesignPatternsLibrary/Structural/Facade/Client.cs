namespace DesignPatternsLibrary.Structural.Facade;

/// <summary>
/// El código cliente trabaja con subsistemas complejos a través de una interfaz simple
/// proporcionada por el Facade. Cuando un facade gestiona el ciclo de vida del
/// subsistema, el cliente podría ni siquiera saber sobre la existencia del
/// subsistema. Este enfoque le permite mantener la complejidad bajo control.
/// </summary>
public class Client
{
    // El código cliente trabaja con subsistemas complejos a través de una
    // interfaz simple proporcionada por el Facade. Cuando un facade gestiona el
    // ciclo de vida del subsistema, el cliente podría ni siquiera saber sobre
    // la existencia del subsistema. Este enfoque le permite mantener la
    // complejidad bajo control.
    public static string ClientCode(Facade facade)
    {
        return facade.Operation();
    }
}
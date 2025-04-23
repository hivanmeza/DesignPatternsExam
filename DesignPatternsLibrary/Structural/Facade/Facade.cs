using System.Text;

namespace DesignPatternsLibrary.Structural.Facade;

/// <summary>
/// La clase Facade proporciona una interfaz simple para la lógica compleja de uno
/// o varios subsistemas. El Facade delega las solicitudes del cliente a los
/// objetos apropiados dentro del subsistema. El Facade también es responsable de
/// gestionar su ciclo de vida. Todo esto protege al cliente de la complejidad
/// no deseada del subsistema.
/// </summary>
public class Facade
{
    protected Subsystem1 _subsystem1;

    protected Subsystem2 _subsystem2;

    public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
    {
        _subsystem1 = subsystem1 ?? new Subsystem1();
        _subsystem2 = subsystem2 ?? new Subsystem2();
    }

    /// <summary>
    /// Los métodos del Facade son atajos convenientes para la sofisticada
    /// funcionalidad de los subsistemas. Sin embargo, los clientes solo obtienen una
    /// fracción de las capacidades de un subsistema.
    /// </summary>
    /// <returns>Una cadena que representa el resultado de las operaciones del subsistema.</returns>
    public string Operation()
    {        
        var result = new StringBuilder();
        result.Append("Facade inicializa subsistemas:\n");
        result.Append(_subsystem1.Operation1());
        result.Append(_subsystem2.Operation1());
        result.Append("Facade ordena a los subsistemas realizar la acción:\n");
        result.Append(_subsystem1.OperationN());
        result.Append(_subsystem2.OperationZ());
        return result.ToString();
    }
}
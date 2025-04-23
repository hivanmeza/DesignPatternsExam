namespace DesignPatternsLibrary.Structural.Adapter;

/// <summary>
/// La clase Adaptee contiene algún comportamiento útil, pero su interfaz es
/// incompatible con el código cliente existente.
/// El Adaptee necesita alguna adaptación antes de que el código cliente pueda usarlo.
/// </summary>
public class Adaptee
{
    public string GetSpecificRequest()
    {
        return "Solicitud específica.";
    }
}
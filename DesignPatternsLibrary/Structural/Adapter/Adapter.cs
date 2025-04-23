namespace DesignPatternsLibrary.Structural.Adapter;

/// <summary>
/// El Adapter hace que la interfaz del Adaptee sea compatible con la interfaz del Target.
/// </summary>
public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    // El Adapter obtiene una interfaz incompatible con otra interfaz
    // y la hace compatible.
    public string GetRequest()
    {
        return $"Esto es '{_adaptee.GetSpecificRequest()}'";
    }
}
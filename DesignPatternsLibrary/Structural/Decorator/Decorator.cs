namespace DesignPatternsLibrary.Structural.Decorator;

/// <summary>
/// La clase base Decorator sigue la misma interfaz que los otros componentes.
/// El propósito principal de esta clase es definir la interfaz de envoltura para todos
/// los decoradores concretos. La implementación predeterminada del código de envoltura
/// podría incluir un campo para almacenar un componente envuelto y los medios para
/// inicializarlo.
/// </summary>
public abstract class Decorator : IComponent
{
    protected IComponent _component;

    public Decorator(IComponent component)
    {
        _component = component;
    }

    public void SetComponent(IComponent component)
    {
        _component = component;
    }

    // El Decorator delega todo el trabajo al componente envuelto.
    public virtual string Operation()
    {
        if (_component != null)
        {
            return _component.Operation();
        }
        else
        {
            return string.Empty;
        }
    }
}
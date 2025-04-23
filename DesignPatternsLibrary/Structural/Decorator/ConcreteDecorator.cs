namespace DesignPatternsLibrary.Structural.Decorator;

/// <summary>
/// Concrete Decorators llaman al objeto envuelto y alteran su resultado de alguna manera.
/// </summary>
public class ConcreteDecoratorA : Decorator
{
    public ConcreteDecoratorA(IComponent comp) : base(comp)
    {
    }

    // Los decoradores pueden llamar a la implementación padre de la operación,
    // en lugar de llamar directamente al objeto envuelto.
    // Este enfoque simplifica la extensión de las clases de decoradores.
    public override string Operation()
    {
        return $"ConcreteDecoratorA({base.Operation()})";
    }
}

/// <summary>
/// Los decoradores pueden ejecutar su comportamiento antes o después de la llamada a un objeto envuelto.
/// </summary>
public class ConcreteDecoratorB : Decorator
{
    public ConcreteDecoratorB(IComponent comp) : base(comp)
    {
    }

    public override string Operation()
    {
        return $"ConcreteDecoratorB({base.Operation()})";
    }
}
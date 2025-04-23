namespace DesignPatternsLibrary.Behavioral;

/// <summary>
/// Ejemplo del patrón Observer aplicado a un sistema de notificaciones en tiempo real.
/// Este patrón permite que múltiples objetos (suscriptores) sean notificados automáticamente
/// cuando ocurre un cambio en el estado de otro objeto (publicador), sin acoplar el publicador
/// a los suscriptores específicos.
/// </summary>

// Definición de tipos de notificaciones para categorizar eventos
public enum TipoNotificacion
{
    Informacion,
    Advertencia,
    Error,
    NuevoContenido
}

// Clase que representa una notificación con información detallada
public class Notificacion
{
    public string Mensaje { get; }
    public DateTime Timestamp { get; }
    public TipoNotificacion Tipo { get; }
    public string Origen { get; }

    public Notificacion(string mensaje, TipoNotificacion tipo, string origen)
    {
        Mensaje = mensaje;
        Timestamp = DateTime.Now;
        Tipo = tipo;
        Origen = origen;
    }

    public override string ToString() => 
        $"[{Timestamp:HH:mm:ss}] [{Tipo}] {Origen}: {Mensaje}";
}

// Interfaz para los observadores que recibirán notificaciones
public interface IObservadorNotificaciones
{
    void RecibirNotificacion(Notificacion notificacion);
    // Permite a los observadores filtrar qué tipos de notificaciones quieren recibir
    bool EstaInteresadoEn(TipoNotificacion tipo);
}

// Implementaciones concretas de observadores
public class ClienteMovil : IObservadorNotificaciones
{
    private readonly string _idDispositivo;
    private readonly HashSet<TipoNotificacion> _tiposDeInteres = new();

    public ClienteMovil(string idDispositivo, params TipoNotificacion[] tiposDeInteres)
    {
        _idDispositivo = idDispositivo;
        foreach (var tipo in tiposDeInteres)
        {
            _tiposDeInteres.Add(tipo);
        }
    }

    public void RecibirNotificacion(Notificacion notificacion)
    {
        // Simula la entrega de notificación push a un dispositivo móvil
        Console.WriteLine($"📱 Dispositivo {_idDispositivo} recibió notificación push: {notificacion}");
    }

    public bool EstaInteresadoEn(TipoNotificacion tipo) => _tiposDeInteres.Contains(tipo);
}

public class SuscriptorEmail : IObservadorNotificaciones
{
    private readonly string _direccionEmail;
    private readonly HashSet<TipoNotificacion> _tiposDeInteres = new();

    public SuscriptorEmail(string direccionEmail, params TipoNotificacion[] tiposDeInteres)
    {
        _direccionEmail = direccionEmail;
        foreach (var tipo in tiposDeInteres)
        {
            _tiposDeInteres.Add(tipo);
        }
    }

    public void RecibirNotificacion(Notificacion notificacion)
    {
        // Simula el envío de un email
        Console.WriteLine($"📧 Email enviado a {_direccionEmail}: {notificacion}");
    }

    public bool EstaInteresadoEn(TipoNotificacion tipo) => _tiposDeInteres.Contains(tipo);
}

public class PanelAdministracion : IObservadorNotificaciones
{
    private readonly string _nombreAdmin;

    public PanelAdministracion(string nombreAdmin)
    {
        _nombreAdmin = nombreAdmin;
    }

    public void RecibirNotificacion(Notificacion notificacion)
    {
        // Los administradores reciben todas las notificaciones en su panel
        Console.WriteLine($"🖥️ Panel de {_nombreAdmin} muestra: {notificacion}");
    }

    // Los administradores están interesados en todos los tipos de notificaciones
    public bool EstaInteresadoEn(TipoNotificacion tipo) => true;
}

// Sistema central de notificaciones (Subject)
public class Subject
{
    private readonly List<IObservadorNotificaciones> _observadores = new();
    private readonly Dictionary<TipoNotificacion, List<Notificacion>> _historialPorTipo = new();

    public Subject()
    {
        // Inicializar el historial para cada tipo de notificación
        foreach (TipoNotificacion tipo in Enum.GetValues(typeof(TipoNotificacion)))
        {
            _historialPorTipo[tipo] = new List<Notificacion>();
        }
    }

    public void Suscribir(IObservadorNotificaciones observador)
    {
        if (!_observadores.Contains(observador))
        {
            _observadores.Add(observador);
            Console.WriteLine("Nuevo observador suscrito al sistema de notificaciones.");
        }
    }

    public void Desuscribir(IObservadorNotificaciones observador)
    {
        if (_observadores.Remove(observador))
        {
            Console.WriteLine("Observador desuscrito del sistema de notificaciones.");
        }
    }

    public void PublicarNotificacion(Notificacion notificacion)
    {
        // Guardar la notificación en el historial
        _historialPorTipo[notificacion.Tipo].Add(notificacion);

        Console.WriteLine($"\nNueva notificación generada: {notificacion}");
        Console.WriteLine("Distribuyendo a los observadores interesados...");

        // Notificar solo a los observadores interesados en este tipo
        foreach (var observador in _observadores)
        {
            if (observador.EstaInteresadoEn(notificacion.Tipo))
            {
                observador.RecibirNotificacion(notificacion);
            }
        }
    }

    public void MostrarHistorialNotificaciones(TipoNotificacion? tipoFiltro = null)
    {
        Console.WriteLine("\n=== Historial de Notificaciones ===\n");

        if (tipoFiltro.HasValue)
        {
            // Mostrar solo notificaciones del tipo especificado
            Console.WriteLine($"Filtrando por: {tipoFiltro.Value}");
            foreach (var notificacion in _historialPorTipo[tipoFiltro.Value])
            {
                Console.WriteLine(notificacion);
            }
        }
        else
        {
            // Mostrar todas las notificaciones
            foreach (var tipo in _historialPorTipo.Keys)
            {
                foreach (var notificacion in _historialPorTipo[tipo])
                {
                    Console.WriteLine(notificacion);
                }
            }
        }
    }

    // Método de demostración
    public static void DemoObserver()
    {
        // Crear el sistema de notificaciones
        var sistemaNotificaciones = new Subject();

        // Crear diferentes tipos de observadores
        var clienteMovil1 = new ClienteMovil("iPhone-12345", 
            TipoNotificacion.NuevoContenido, TipoNotificacion.Informacion);
        
        var clienteMovil2 = new ClienteMovil("Android-67890", 
            TipoNotificacion.Error, TipoNotificacion.Advertencia, TipoNotificacion.NuevoContenido);
        
        var suscriptorEmail = new SuscriptorEmail("usuario@ejemplo.com", 
            TipoNotificacion.NuevoContenido);
        
        var panelAdmin = new PanelAdministracion("Administrador Principal");

        // Suscribir los observadores al sistema
        sistemaNotificaciones.Suscribir(clienteMovil1);
        sistemaNotificaciones.Suscribir(clienteMovil2);
        sistemaNotificaciones.Suscribir(suscriptorEmail);
        sistemaNotificaciones.Suscribir(panelAdmin);

        // Generar diferentes tipos de notificaciones
        sistemaNotificaciones.PublicarNotificacion(
            new Notificacion("Nuevo artículo publicado: Patrones de Diseño en C#", 
                TipoNotificacion.NuevoContenido, "Sistema de Blog"));

        sistemaNotificaciones.PublicarNotificacion(
            new Notificacion("El servidor está experimentando alta carga", 
                TipoNotificacion.Advertencia, "Monitor de Sistema"));

        // Desuscribir un observador
        sistemaNotificaciones.Desuscribir(suscriptorEmail);

        sistemaNotificaciones.PublicarNotificacion(
            new Notificacion("Error en la base de datos: Conexión perdida", 
                TipoNotificacion.Error, "Servicio de Base de Datos"));

        // Mostrar el historial completo de notificaciones
        sistemaNotificaciones.MostrarHistorialNotificaciones();
    }
}
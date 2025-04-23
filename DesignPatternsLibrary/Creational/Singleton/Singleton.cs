namespace DesignPatternsLibrary.Creational.Singleton;

/// <summary>
/// Ejemplo del patrón Singleton aplicado a un gestor de configuración global.
/// Este patrón asegura que una clase solo tenga una instancia y proporciona
/// un punto de acceso global a ella, ideal para recursos compartidos como
/// configuraciones, conexiones a bases de datos o cachés.
/// </summary>
public sealed class ConfiguracionGlobal
{
    // Instancia privada estática utilizando inicialización perezosa con bloqueo doble
    private static volatile ConfiguracionGlobal _instancia;
    private static readonly object _lock = new object();
    
    // Diccionario para almacenar los valores de configuración
    private readonly Dictionary<string, object> _configuraciones;
    
    // Ruta del archivo de configuración
    private readonly string _rutaArchivo;
    
    // Constructor privado para prevenir la instanciación desde fuera de la clase
    private ConfiguracionGlobal()
    {
        _configuraciones = new Dictionary<string, object>();
        _rutaArchivo = "config.json";
        Console.WriteLine("Instancia de ConfiguracionGlobal creada.");
        CargarConfiguracionPredeterminada();
    }
    
    /// <summary>
    /// Propiedad estática para acceder a la única instancia de la clase.
    /// Implementa inicialización perezosa con bloqueo doble para thread-safety.
    /// </summary>
    public static ConfiguracionGlobal Instancia
    {
        get
        {
            if (_instancia == null)
            {
                lock (_lock)
                {
                    if (_instancia == null)
                    {
                        _instancia = new ConfiguracionGlobal();
                    }
                }
            }
            return _instancia;
        }
    }
    
    // Carga valores predeterminados de configuración
    private void CargarConfiguracionPredeterminada()
    {
        _configuraciones["TemaOscuro"] = false;
        _configuraciones["IdiomaPreferido"] = "es-ES";
        _configuraciones["TamañoFuente"] = 12;
        _configuraciones["ModoDebug"] = false;
        _configuraciones["IntervaloRespaldo"] = 30; // minutos
        
        Console.WriteLine("Configuración predeterminada cargada.");
    }
    
    /// <summary>
    /// Obtiene un valor de configuración por su clave.
    /// </summary>
    /// <param name="clave">La clave de la configuración.</param>
    /// <param name="valorPredeterminado">Valor a devolver si la clave no existe.</param>
    /// <returns>El valor de la configuración o el valor predeterminado si no existe.</returns>
    public T ObtenerValor<T>(string clave, T valorPredeterminado = default)
    {
        if (_configuraciones.TryGetValue(clave, out var valor) && valor is T tipado)
        {
            return tipado;
        }
        return valorPredeterminado;
    }
    
    /// <summary>
    /// Establece un valor de configuración.
    /// </summary>
    /// <param name="clave">La clave de la configuración.</param>
    /// <param name="valor">El valor a establecer.</param>
    public void EstablecerValor<T>(string clave, T valor)
    {
        _configuraciones[clave] = valor;
        Console.WriteLine($"Configuración actualizada: {clave} = {valor}");
    }
    
    /// <summary>
    /// Guarda la configuración actual (simulado).
    /// </summary>
    /// <returns>True si la operación fue exitosa, false en caso contrario.</returns>
    public bool GuardarConfiguracion()
    {
        try
        {
            // En una implementación real, aquí se guardaría en un archivo o base de datos
            Console.WriteLine($"Configuración guardada en {_rutaArchivo}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar la configuración: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Carga la configuración desde una fuente externa (simulado).
    /// </summary>
    /// <returns>True si la operación fue exitosa, false en caso contrario.</returns>
    public bool CargarConfiguracion()
    {
        try
        {
            // En una implementación real, aquí se cargaría desde un archivo o base de datos
            Console.WriteLine($"Configuración cargada desde {_rutaArchivo}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar la configuración: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Restablece la configuración a los valores predeterminados.
    /// </summary>
    public void RestablecerConfiguracion()
    {
        _configuraciones.Clear();
        CargarConfiguracionPredeterminada();
        Console.WriteLine("Configuración restablecida a valores predeterminados.");
    }
    
    /// <summary>
    /// Método de demostración para el patrón Singleton.
    /// </summary>
    public static void DemoSingleton()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DEL PATRÓN SINGLETON (CONFIGURACIÓN GLOBAL) ===\n");
        
        // Obtener la instancia del Singleton
        var config = ConfiguracionGlobal.Instancia;
        
        // Mostrar valores predeterminados
        Console.WriteLine("\n--- Valores predeterminados ---");
        Console.WriteLine($"Tema Oscuro: {config.ObtenerValor<bool>("TemaOscuro")}");
        Console.WriteLine($"Idioma: {config.ObtenerValor<string>("IdiomaPreferido")}");
        Console.WriteLine($"Tamaño de Fuente: {config.ObtenerValor<int>("TamañoFuente")}");
        
        // Modificar algunos valores
        Console.WriteLine("\n--- Modificando valores ---");
        config.EstablecerValor("TemaOscuro", true);
        config.EstablecerValor("TamañoFuente", 14);
        config.EstablecerValor("NuevaOpcion", "Valor personalizado");
        
        // Mostrar valores actualizados
        Console.WriteLine("\n--- Valores actualizados ---");
        Console.WriteLine($"Tema Oscuro: {config.ObtenerValor<bool>("TemaOscuro")}");
        Console.WriteLine($"Tamaño de Fuente: {config.ObtenerValor<int>("TamañoFuente")}");
        Console.WriteLine($"Nueva Opción: {config.ObtenerValor<string>("NuevaOpcion")}");
        
        // Demostrar que siempre se obtiene la misma instancia
        Console.WriteLine("\n--- Verificando unicidad de instancia ---");
        var otraReferencia = ConfiguracionGlobal.Instancia;
        Console.WriteLine($"¿Son la misma instancia? {ReferenceEquals(config, otraReferencia)}");
        
        // Guardar y restablecer configuración
        Console.WriteLine("\n--- Operaciones de persistencia ---");
        config.GuardarConfiguracion();
        config.RestablecerConfiguracion();
        
        // Mostrar valores restablecidos
        Console.WriteLine("\n--- Valores restablecidos ---");
        Console.WriteLine($"Tema Oscuro: {config.ObtenerValor<bool>("TemaOscuro")}");
        Console.WriteLine($"Tamaño de Fuente: {config.ObtenerValor<int>("TamañoFuente")}");
        Console.WriteLine($"Nueva Opción: {config.ObtenerValor<string>("NuevaOpcion", "No existe")}");
    }
    
    // Método requerido por el menú de consola anterior
    public string HacerAlgo()
    {
        return "Configuración global lista para usar.";
    }
}

// Clase de compatibilidad para mantener el código existente funcionando
public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();
    
    private Singleton() { }
    
    public static Singleton Instance => instance;
    
    public string HacerAlgo()
    {
        // Redirigir al nuevo Singleton
        ConfiguracionGlobal.Instancia.HacerAlgo();
        return "Redirigiendo al nuevo ConfiguracionGlobal. Por favor actualice su código.";
    }
}
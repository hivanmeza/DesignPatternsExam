namespace DesignPatternsLibrary.Behavioral;

/// <summary>
/// Ejemplo del patrón Strategy aplicado a un sistema de validación de datos.
/// Este patrón permite intercambiar algoritmos de validación en tiempo de ejecución,
/// facilitando la implementación de diferentes reglas de validación para distintos
/// escenarios sin modificar el código cliente.
/// </summary>

// Modelo de datos a validar
public class Usuario
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Contraseña { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public Usuario(string nombre, string email, string contraseña, string telefono, DateTime fechaNacimiento)
    {
        Nombre = nombre;
        Email = email;
        Contraseña = contraseña;
        Telefono = telefono;
        FechaNacimiento = fechaNacimiento;
    }
}

// Resultado de la validación
public class ResultadoValidacion
{
    public bool EsValido { get; private set; }
    public List<string> Errores { get; private set; } = new List<string>();

    public ResultadoValidacion()
    {
        EsValido = true;
    }

    public void AgregarError(string error)
    {
        Errores.Add(error);
        EsValido = false;
    }

    public override string ToString()
    {
        if (EsValido)
            return "Validación exitosa. No se encontraron errores.";

        return $"Validación fallida. Se encontraron {Errores.Count} errores:\n" + 
               string.Join("\n", Errores.Select((error, index) => $"{index + 1}. {error}"));
    }
}

// Interfaz Strategy para validación
public interface IEstrategiaValidacion
{
    ResultadoValidacion Validar(Usuario usuario);
    string ObtenerDescripcion();
}

// Implementaciones concretas de estrategias de validación
public class ValidacionRegistroBasico : IEstrategiaValidacion
{
    public ResultadoValidacion Validar(Usuario usuario)
    {
        var resultado = new ResultadoValidacion();

        // Validar nombre
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            resultado.AgregarError("El nombre es obligatorio.");
        else if (usuario.Nombre.Length < 2)
            resultado.AgregarError("El nombre debe tener al menos 2 caracteres.");

        // Validar email
        if (string.IsNullOrWhiteSpace(usuario.Email))
            resultado.AgregarError("El email es obligatorio.");
        else if (!usuario.Email.Contains("@") || !usuario.Email.Contains("."))
            resultado.AgregarError("El email no tiene un formato válido.");

        // Validar contraseña
        if (string.IsNullOrWhiteSpace(usuario.Contraseña))
            resultado.AgregarError("La contraseña es obligatoria.");
        else if (usuario.Contraseña.Length < 6)
            resultado.AgregarError("La contraseña debe tener al menos 6 caracteres.");

        return resultado;
    }

    public string ObtenerDescripcion() => "Validación básica para registro de usuarios";
}

public class ValidacionSeguridadAvanzada : IEstrategiaValidacion
{
    public ResultadoValidacion Validar(Usuario usuario)
    {
        var resultado = new ResultadoValidacion();

        // Validar contraseña con reglas más estrictas
        if (string.IsNullOrWhiteSpace(usuario.Contraseña))
        {
            resultado.AgregarError("La contraseña es obligatoria.");
        }
        else
        {
            if (usuario.Contraseña.Length < 8)
                resultado.AgregarError("La contraseña debe tener al menos 8 caracteres.");

            if (!usuario.Contraseña.Any(char.IsUpper))
                resultado.AgregarError("La contraseña debe contener al menos una letra mayúscula.");

            if (!usuario.Contraseña.Any(char.IsLower))
                resultado.AgregarError("La contraseña debe contener al menos una letra minúscula.");

            if (!usuario.Contraseña.Any(char.IsDigit))
                resultado.AgregarError("La contraseña debe contener al menos un número.");

            if (!usuario.Contraseña.Any(c => !char.IsLetterOrDigit(c)))
                resultado.AgregarError("La contraseña debe contener al menos un carácter especial.");

            // Verificar que la contraseña no contenga información personal
            if (usuario.Contraseña.Contains(usuario.Nombre, StringComparison.OrdinalIgnoreCase))
                resultado.AgregarError("La contraseña no debe contener su nombre.");
        }

        return resultado;
    }

    public string ObtenerDescripcion() => "Validación de seguridad avanzada para contraseñas";
}

public class ValidacionPerfilCompleto : IEstrategiaValidacion
{
    public ResultadoValidacion Validar(Usuario usuario)
    {
        var resultado = new ResultadoValidacion();

        // Validar todos los campos para un perfil completo
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            resultado.AgregarError("El nombre es obligatorio para completar el perfil.");

        if (string.IsNullOrWhiteSpace(usuario.Email))
            resultado.AgregarError("El email es obligatorio para completar el perfil.");

        if (string.IsNullOrWhiteSpace(usuario.Telefono))
            resultado.AgregarError("El teléfono es obligatorio para completar el perfil.");
        else if (!usuario.Telefono.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' '))
            resultado.AgregarError("El teléfono contiene caracteres no válidos.");

        // Validar edad (mayor de 18 años)
        var edad = DateTime.Today.Year - usuario.FechaNacimiento.Year;
        if (usuario.FechaNacimiento > DateTime.Today.AddYears(-edad))
            edad--;

        if (edad < 18)
            resultado.AgregarError("Debe ser mayor de 18 años para completar el perfil.");

        return resultado;
    }

    public string ObtenerDescripcion() => "Validación de perfil completo de usuario";
}

// Contexto que utiliza las estrategias de validación
public class Contexto
{
    private IEstrategiaValidacion _estrategia;

    public Contexto(IEstrategiaValidacion estrategia)
    {
        _estrategia = estrategia;
    }

    public void SetStrategy(IEstrategiaValidacion estrategia)
    {
        _estrategia = estrategia;
        Console.WriteLine($"Estrategia cambiada a: {estrategia.ObtenerDescripcion()}");
    }

    public ResultadoValidacion ValidarUsuario(Usuario usuario)
    {
        Console.WriteLine($"Aplicando estrategia: {_estrategia.ObtenerDescripcion()}");
        return _estrategia.Validar(usuario);
    }

    // Método de demostración
    public static void DemoStrategy()
    {
        // Crear algunos usuarios para validar
        var usuarioNuevo = new Usuario(
            "Ana", 
            "ana@ejemplo.com", 
            "pass123", 
            "555-123-4567", 
            new DateTime(1990, 5, 15));

        var usuarioContraseñaDebil = new Usuario(
            "Carlos", 
            "carlos@ejemplo.com", 
            "carlos123", // Contraseña que contiene el nombre
            "555-987-6543", 
            new DateTime(1985, 8, 22));

        var usuarioIncompleto = new Usuario(
            "Elena", 
            "elena@ejemplo.com", 
            "Secure$123", 
            "", // Teléfono vacío
            new DateTime(2010, 3, 10)); // Menor de edad

        // Crear el contexto con una estrategia inicial
        Console.WriteLine("\n=== VALIDACIÓN DE USUARIOS CON DIFERENTES ESTRATEGIAS ===\n");
        var validador = new Contexto(new ValidacionRegistroBasico());

        // Validar usuario con estrategia básica
        Console.WriteLine("\n--- Validando usuario nuevo con estrategia básica ---");
        var resultado1 = validador.ValidarUsuario(usuarioNuevo);
        Console.WriteLine(resultado1);

        // Cambiar a estrategia de seguridad avanzada
        Console.WriteLine("\n--- Cambiando a estrategia de seguridad avanzada ---");
        validador.SetStrategy(new ValidacionSeguridadAvanzada());
        
        // Validar usuario con contraseña débil
        Console.WriteLine("\n--- Validando usuario con contraseña débil ---");
        var resultado2 = validador.ValidarUsuario(usuarioContraseñaDebil);
        Console.WriteLine(resultado2);

        // Cambiar a estrategia de perfil completo
        Console.WriteLine("\n--- Cambiando a estrategia de perfil completo ---");
        validador.SetStrategy(new ValidacionPerfilCompleto());
        
        // Validar usuario incompleto
        Console.WriteLine("\n--- Validando usuario con perfil incompleto ---");
        var resultado3 = validador.ValidarUsuario(usuarioIncompleto);
        Console.WriteLine(resultado3);
    }
}
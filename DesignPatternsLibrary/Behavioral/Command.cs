namespace DesignPatternsLibrary.Behavioral;

/// <summary>
/// Ejemplo del patrón Command aplicado a un sistema de procesamiento de transacciones bancarias.
/// Este patrón permite encapsular operaciones como comandos, facilitando:
/// - Encolamiento de transacciones
/// - Registro de operaciones (logging)
/// - Soporte para operaciones deshacer/rehacer
/// - Procesamiento asíncrono
/// </summary>
public interface ICommand
{
    void Execute();
    bool CanExecute();
    string GetDescription();
}

public class CuentaBancaria
{
    private string _numeroCuenta;
    private decimal _saldo;

    public CuentaBancaria(string numeroCuenta, decimal saldoInicial)
    {
        _numeroCuenta = numeroCuenta;
        _saldo = saldoInicial;
    }

    public void Depositar(decimal monto)
    {
        _saldo += monto;
        Console.WriteLine($"Depósito de {monto:C} realizado. Nuevo saldo: {_saldo:C}");
    }

    public bool PuedeRetirar(decimal monto) => _saldo >= monto;

    public void Retirar(decimal monto)
    {
        if (!PuedeRetirar(monto))
        {
            Console.WriteLine($"Error: Fondos insuficientes para retirar {monto:C}. Saldo actual: {_saldo:C}");
            return;
        }

        _saldo -= monto;
        Console.WriteLine($"Retiro de {monto:C} realizado. Nuevo saldo: {_saldo:C}");
    }

    public decimal ObtenerSaldo() => _saldo;
}

public class ComandoDeposito : ICommand
{
    private readonly CuentaBancaria _cuenta;
    private readonly decimal _monto;

    public ComandoDeposito(CuentaBancaria cuenta, decimal monto)
    {
        _cuenta = cuenta;
        _monto = monto;
    }

    public void Execute() => _cuenta.Depositar(_monto);

    public bool CanExecute() => _monto > 0;

    public string GetDescription() => $"Depósito de {_monto:C}";
}

public class ComandoRetiro : ICommand
{
    private readonly CuentaBancaria _cuenta;
    private readonly decimal _monto;

    public ComandoRetiro(CuentaBancaria cuenta, decimal monto)
    {
        _cuenta = cuenta;
        _monto = monto;
    }

    public void Execute() => _cuenta.Retirar(_monto);

    public bool CanExecute() => _monto > 0 && _cuenta.PuedeRetirar(_monto);

    public string GetDescription() => $"Retiro de {_monto:C}";
}

public class ProcesadorTransacciones
{
    private readonly Queue<ICommand> _comandosPendientes = new Queue<ICommand>();
    private readonly List<string> _historialTransacciones = new List<string>();

    public void AgregarTransaccion(ICommand comando)
    {
        if (comando.CanExecute())
        {
            _comandosPendientes.Enqueue(comando);
            Console.WriteLine($"Transacción agregada a la cola: {comando.GetDescription()}");
        }
        else
        {
            Console.WriteLine($"No se puede ejecutar: {comando.GetDescription()}");
        }
    }

    public void ProcesarTransaccionesPendientes()
    {
        Console.WriteLine("\nProcesando transacciones pendientes...");
        
        if (_comandosPendientes.Count == 0)
        {
            Console.WriteLine("No hay transacciones pendientes.");
            return;
        }

        while (_comandosPendientes.Count > 0)
        {
            var comando = _comandosPendientes.Dequeue();
            
            if (comando.CanExecute())
            {
                comando.Execute();
                _historialTransacciones.Add($"[{DateTime.Now}] {comando.GetDescription()} - COMPLETADA");
            }
            else
            {
                _historialTransacciones.Add($"[{DateTime.Now}] {comando.GetDescription()} - RECHAZADA");
                Console.WriteLine($"Transacción rechazada: {comando.GetDescription()}");
            }
        }
    }

    public void MostrarHistorial()
    {
        Console.WriteLine("\nHistorial de transacciones:");
        foreach (var transaccion in _historialTransacciones)
        {
            Console.WriteLine(transaccion);
        }
    }

    // Método de demostración
    public static void DemoCommand()
    {
        // Crear una cuenta bancaria con saldo inicial
        var cuenta = new CuentaBancaria("123456789", 1000);
        
        // Crear el procesador de transacciones
        var procesador = new ProcesadorTransacciones();
        
        // Crear comandos para diferentes operaciones
        var deposito1 = new ComandoDeposito(cuenta, 500);
        var retiro1 = new ComandoRetiro(cuenta, 200);
        var retiro2 = new ComandoRetiro(cuenta, 1500); // Este debería fallar por fondos insuficientes
        var deposito2 = new ComandoDeposito(cuenta, 300);
        
        // Agregar transacciones a la cola
        procesador.AgregarTransaccion(deposito1);
        procesador.AgregarTransaccion(retiro1);
        procesador.AgregarTransaccion(retiro2); // Esta será rechazada durante el procesamiento
        procesador.AgregarTransaccion(deposito2);
        
        // Procesar todas las transacciones pendientes
        procesador.ProcesarTransaccionesPendientes();
        
        // Mostrar el historial de transacciones
        procesador.MostrarHistorial();
    }
}
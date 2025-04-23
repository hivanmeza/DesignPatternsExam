using System;
using DesignPatternsLibrary.Structural.Facade;
using DesignPatternsLibrary.Creational.Singleton;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== MENÚ PRINCIPAL DE PATRONES DE DISEÑO ====");
            Console.WriteLine("1. Patrones Creacionales");
            Console.WriteLine("2. Patrones Estructurales");
            Console.WriteLine("3. Patrones de Comportamiento");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una categoría: ");
            var opcionCategoria = Console.ReadLine();

            switch (opcionCategoria)
            {
                case "1":
                    MostrarMenuCreacionales();
                    break;
                case "2":
                    MostrarMenuEstructurales();
                    break;
                case "3":
                    MostrarMenuComportamiento();
                    break;
                case "0":
                    Console.WriteLine("Saliendo...");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MostrarMenuCreacionales()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== PATRONES CREACIONALES ====");
            Console.WriteLine("1. Singleton - Gestión de Configuración Global (Conexiones, Caché, Logs)");
            Console.WriteLine("2. Factory Method - Creación de Documentos (PDF, Word, Excel)");
            Console.WriteLine("3. Abstract Factory - Interfaces de Usuario (Web, Móvil, Escritorio)");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("\nSeleccione un patrón: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    // Ejecutar demostración de Singleton
                    ConfiguracionGlobal.DemoSingleton();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "2":
                    // Ejecutar demostración de Factory Method
                    DesignPatternsLibrary.Creational.FactoryMethod.CreadorDocumento.DemoFactoryMethod();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "3":
                    ProbarAbstractFactory();
                    Console.WriteLine("Presione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "0":
                    return; // Volver al menú principal
                default:
                    Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MostrarMenuEstructurales()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== PATRONES ESTRUCTURALES ====");
            Console.WriteLine("1. Adapter - Integración de APIs de Pago (Stripe, PayPal, MercadoPago)");
            Console.WriteLine("2. Decorator - Sistema de Notificaciones Multicapa (Email, SMS, Push)");
            Console.WriteLine("3. Facade - Sistema de Reservas de Viajes (Hoteles, Vuelos, Transporte)");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("\nSeleccione un patrón: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ProbarAdapter();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "2":
                    ProbarDecorator();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "3":
                    // Ejecutar demostración de Facade
                    ProbarFacade();
                    break;
                case "0":
                    return; // Volver al menú principal
                default:
                    Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MostrarMenuComportamiento()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== PATRONES DE COMPORTAMIENTO ====");
            Console.WriteLine("1. Observer - Sistema de Notificaciones (Suscripciones, Alertas, Eventos)");
            Console.WriteLine("2. Command - Transacciones Bancarias (Depósitos, Retiros, Transferencias)");
            Console.WriteLine("3. Strategy - Validación de Datos (Formularios, Documentos, Pagos)");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("\nSeleccione un patrón: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    // Ejecutar demostración de Observer
                    DesignPatternsLibrary.Behavioral.Subject.DemoObserver();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "2":
                    // Ejecutar demostración de Command
                    DesignPatternsLibrary.Behavioral.ProcesadorTransacciones.DemoCommand();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "3":
                    // Ejecutar demostración de Strategy
                    DesignPatternsLibrary.Behavioral.Contexto.DemoStrategy();
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                    break;
                case "0":
                    return; // Volver al menú principal
                default:
                    Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ProbarFacade()
    {
        var subsystem1 = new Subsystem1(); // Simula el sistema de reserva de hoteles
        var subsystem2 = new Subsystem2(); // Simula el sistema de reserva de vuelos
        var facade = new Facade(subsystem1, subsystem2);
        
        Console.WriteLine("\n===== SISTEMA DE RESERVAS DE VIAJES (PATRÓN FACADE) =====");
        Console.WriteLine("El patrón Facade simplifica la interacción con múltiples subsistemas complejos.");
        Console.WriteLine("En este ejemplo, el cliente puede reservar un viaje completo sin conocer los detalles de cada sistema.");
        Console.WriteLine("\n--- Realizando reserva completa de viaje ---");
        
        var resultado = Client.ClientCode(facade);
        Console.WriteLine(resultado);
        
        Console.WriteLine("\nBeneficios obtenidos:");
        Console.WriteLine("✓ Simplificación de la interfaz para el cliente");
        Console.WriteLine("✓ Desacoplamiento del cliente de los subsistemas");
        Console.WriteLine("✓ Centralización de la lógica de negocio");
        
        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    // Método antiguo mantenido por compatibilidad
    static void ProbarSingleton()
    {
        // Redirigir a la nueva implementación
        ConfiguracionGlobal.DemoSingleton();
    }

    // Método antiguo mantenido por compatibilidad
    static void ProbarAbstractFactory()
    {
        Console.WriteLine("\n===== INTERFACES DE USUARIO CON ABSTRACT FACTORY =====");
        Console.WriteLine("Este patrón permite crear familias de objetos relacionados sin especificar sus clases concretas.");
        
        Console.WriteLine("\n--- Creando interfaz para aplicación WEB ---");
        var webFactory = new DesignPatternsLibrary.Creational.AbstractFactory.ConcreteFactory1(); // Simula fábrica de UI Web
        var webButton = webFactory.CreateProductA(); // Simula botón web
        var webMenu = webFactory.CreateProductB();   // Simula menú web
        
        Console.WriteLine("\n--- Creando interfaz para aplicación MÓVIL ---");
        var mobileFactory = new DesignPatternsLibrary.Creational.AbstractFactory.ConcreteFactory2(); // Simula fábrica de UI Móvil
        var mobileButton = mobileFactory.CreateProductA(); // Simula botón móvil
        var mobileMenu = mobileFactory.CreateProductB();   // Simula menú móvil
        
        Console.WriteLine("\n--- Resultado de los componentes creados ---");
        Console.WriteLine("Componentes Web:");
        Console.WriteLine(" - " + webButton.UsefulFunctionA());
        Console.WriteLine(" - " + webMenu.UsefulFunctionB());
        
        Console.WriteLine("\nComponentes Móviles:");
        Console.WriteLine(" - " + mobileButton.UsefulFunctionA());
        Console.WriteLine(" - " + mobileMenu.UsefulFunctionB());
        
        Console.WriteLine("\nBeneficios obtenidos:");
        Console.WriteLine("✓ Consistencia entre componentes relacionados");
        Console.WriteLine("✓ Facilidad para cambiar entre familias de productos");
        Console.WriteLine("✓ Código más mantenible y escalable");
        
        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    static void ProbarAdapter()
    {
        Console.WriteLine("\n===== INTEGRACIÓN DE APIS DE PAGO CON ADAPTER =====");
        Console.WriteLine("Este patrón permite que interfaces incompatibles trabajen juntas.");
        Console.WriteLine("En este ejemplo, adaptamos diferentes APIs de pago a una interfaz común.");
        
        // Simulamos que Adaptee es una API externa de pago (como PayPal)
        var paypalAPI = new DesignPatternsLibrary.Structural.Adapter.Adaptee();
        var paypalAdapter = new DesignPatternsLibrary.Structural.Adapter.Adapter(paypalAPI);
        
        Console.WriteLine("\n--- Procesando pago con PayPal ---");
        Console.WriteLine("Solicitud original a PayPal: SpecialRequest()");
        Console.WriteLine("Solicitud adaptada: " + paypalAdapter.GetRequest());
        
        Console.WriteLine("\n--- El mismo código podría procesar pagos con Stripe o MercadoPago ---");
        Console.WriteLine("Sin necesidad de modificar el código cliente que usa la interfaz común.");
        
        Console.WriteLine("\nBeneficios obtenidos:");
        Console.WriteLine("✓ Reutilización de código existente");
        Console.WriteLine("✓ Interoperabilidad entre sistemas incompatibles");
        Console.WriteLine("✓ Facilidad para integrar nuevos proveedores de pago");
        
        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    static void ProbarDecorator()
    {
        Console.WriteLine("\n===== SISTEMA DE NOTIFICACIONES MULTICAPA CON DECORATOR =====");
        Console.WriteLine("Este patrón permite añadir funcionalidades a objetos dinámicamente sin alterar su estructura.");
        Console.WriteLine("En este ejemplo, creamos un sistema de notificaciones con múltiples canales.");
        
        // Creamos una notificación básica (componente base)
        DesignPatternsLibrary.Structural.Decorator.IComponent notificacionBasica = 
            new DesignPatternsLibrary.Structural.Decorator.ConcreteComponent();
        Console.WriteLine("\n--- Notificación básica ---");
        Console.WriteLine(notificacionBasica.Operation());
        
        // Añadimos funcionalidad de email (primer decorador)
        var notificacionEmail = new DesignPatternsLibrary.Structural.Decorator.ConcreteDecoratorA(notificacionBasica);
        Console.WriteLine("\n--- Notificación con email ---");
        Console.WriteLine(notificacionEmail.Operation());
        
        // Añadimos funcionalidad de SMS (segundo decorador)
        var notificacionCompleta = new DesignPatternsLibrary.Structural.Decorator.ConcreteDecoratorB(notificacionEmail);
        Console.WriteLine("\n--- Notificación completa (básica + email + SMS) ---");
        Console.WriteLine(notificacionCompleta.Operation());
        
        Console.WriteLine("\nBeneficios obtenidos:");
        Console.WriteLine("✓ Extensibilidad sin modificar código existente");
        Console.WriteLine("✓ Combinación flexible de comportamientos");
        Console.WriteLine("✓ Principio de responsabilidad única");
        
        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }
}

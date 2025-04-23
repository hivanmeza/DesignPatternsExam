namespace DesignPatternsLibrary.Creational.FactoryMethod;

/// <summary>
/// Ejemplo del patrón Factory Method aplicado a un sistema de gestión de documentos.
/// Este patrón define una interfaz para crear objetos, pero permite a las subclases
/// decidir qué clase instanciar, facilitando la creación de diferentes tipos de documentos
/// sin acoplar el código cliente a clases específicas.
/// </summary>

// Interfaz común para todos los tipos de documentos
public interface IDocumento
{
    string Nombre { get; }
    string Extension { get; }
    string Contenido { get; set; }
    
    void Abrir();
    bool Guardar(string ruta);
    string Exportar(string formato);
    string ObtenerInformacion();
}

// Implementaciones concretas de documentos
public class DocumentoPDF : IDocumento
{
    public string Nombre { get; private set; }
    public string Extension => ".pdf";
    public string Contenido { get; set; }
    
    public DocumentoPDF(string nombre)
    {
        Nombre = nombre;
        Contenido = string.Empty;
    }
    
    public void Abrir()
    {
        Console.WriteLine($"Abriendo documento PDF '{Nombre}{Extension}' con Adobe Reader...");
    }
    
    public bool Guardar(string ruta)
    {
        Console.WriteLine($"Guardando PDF en {ruta}\\{Nombre}{Extension}");
        Console.WriteLine("Aplicando compresión y optimización para PDF...");
        return true;
    }
    
    public string Exportar(string formato)
    {
        return formato.ToLower() switch
        {
            "imagen" => $"Exportando {Nombre} como conjunto de imágenes PNG",
            "texto" => $"Extrayendo texto de {Nombre} (sin formato)",
            _ => $"Formato de exportación '{formato}' no soportado para PDF"
        };
    }
    
    public string ObtenerInformacion()
    {
        return $"Documento PDF: {Nombre}{Extension}\n" +
               $"Tamaño: {(Contenido.Length / 1024.0):F2} KB\n" +
               $"Características: Soporta texto, imágenes, enlaces y formularios";
    }
}

public class DocumentoWord : IDocumento
{
    public string Nombre { get; private set; }
    public string Extension => ".docx";
    public string Contenido { get; set; }
    
    public DocumentoWord(string nombre)
    {
        Nombre = nombre;
        Contenido = string.Empty;
    }
    
    public void Abrir()
    {
        Console.WriteLine($"Abriendo documento Word '{Nombre}{Extension}' con Microsoft Word...");
    }
    
    public bool Guardar(string ruta)
    {
        Console.WriteLine($"Guardando documento Word en {ruta}\\{Nombre}{Extension}");
        Console.WriteLine("Verificando ortografía y gramática...");
        return true;
    }
    
    public string Exportar(string formato)
    {
        return formato.ToLower() switch
        {
            "pdf" => $"Exportando {Nombre} a formato PDF",
            "html" => $"Exportando {Nombre} a página web HTML",
            "txt" => $"Exportando {Nombre} a texto plano",
            _ => $"Formato de exportación '{formato}' no soportado para Word"
        };
    }
    
    public string ObtenerInformacion()
    {
        return $"Documento Word: {Nombre}{Extension}\n" +
               $"Tamaño: {(Contenido.Length / 1024.0):F2} KB\n" +
               $"Características: Procesador de texto con formato, estilos y revisiones";
    }
}

public class DocumentoExcel : IDocumento
{
    public string Nombre { get; private set; }
    public string Extension => ".xlsx";
    public string Contenido { get; set; }
    
    public DocumentoExcel(string nombre)
    {
        Nombre = nombre;
        Contenido = string.Empty;
    }
    
    public void Abrir()
    {
        Console.WriteLine($"Abriendo hoja de cálculo '{Nombre}{Extension}' con Microsoft Excel...");
    }
    
    public bool Guardar(string ruta)
    {
        Console.WriteLine($"Guardando hoja de cálculo en {ruta}\\{Nombre}{Extension}");
        Console.WriteLine("Recalculando fórmulas y actualizando gráficos...");
        return true;
    }
    
    public string Exportar(string formato)
    {
        return formato.ToLower() switch
        {
            "pdf" => $"Exportando {Nombre} a formato PDF",
            "csv" => $"Exportando {Nombre} a valores separados por comas",
            "xml" => $"Exportando {Nombre} a formato XML",
            _ => $"Formato de exportación '{formato}' no soportado para Excel"
        };
    }
    
    public string ObtenerInformacion()
    {
        return $"Hoja de cálculo Excel: {Nombre}{Extension}\n" +
               $"Tamaño: {(Contenido.Length / 1024.0):F2} KB\n" +
               $"Características: Cálculos, tablas dinámicas y gráficos";
    }
}

// Creador abstracto que define el método fábrica
public abstract class CreadorDocumento
{
    // Método fábrica que las subclases deben implementar
    public abstract IDocumento CrearDocumento(string nombreDocumento);
    
    // Lógica común para todos los tipos de documentos
    public void EditarDocumento(IDocumento documento, string nuevoContenido)
    {
        Console.WriteLine($"Editando documento: {documento.Nombre}{documento.Extension}");
        documento.Contenido = nuevoContenido;
        Console.WriteLine($"Contenido actualizado ({nuevoContenido.Length} caracteres)");
    }
    
    public void MostrarVistaPrevia(IDocumento documento)
    {
        Console.WriteLine("\n--- VISTA PREVIA ---");
        Console.WriteLine(documento.ObtenerInformacion());
        Console.WriteLine("\nContenido (primeros 100 caracteres):");
        var contenidoMostrado = documento.Contenido.Length > 100 
            ? documento.Contenido.Substring(0, 97) + "..." 
            : documento.Contenido;
        Console.WriteLine(contenidoMostrado);
        Console.WriteLine("------------------\n");
    }
    
    // Método de demostración para el patrón Factory Method
    public static void DemoFactoryMethod()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DEL PATRÓN FACTORY METHOD (GESTIÓN DE DOCUMENTOS) ===\n");
        
        // Crear diferentes tipos de documentos usando sus creadores específicos
        var creadorPDF = new CreadorPDF();
        var creadorWord = new CreadorWord();
        var creadorExcel = new CreadorExcel();
        
        // Crear documentos
        Console.WriteLine("--- Creando diferentes tipos de documentos ---\n");
        var documentoPDF = creadorPDF.CrearDocumento("Informe2023");
        var documentoWord = creadorWord.CrearDocumento("Contrato");
        var documentoExcel = creadorExcel.CrearDocumento("Presupuesto");
        
        // Editar documentos
        Console.WriteLine("\n--- Editando documentos ---\n");
        creadorPDF.EditarDocumento(documentoPDF, "Este es un informe anual generado en formato PDF con gráficos y tablas.");
        creadorWord.EditarDocumento(documentoWord, "CONTRATO DE SERVICIOS\n\nEn la ciudad de..., a fecha de..., las partes acuerdan...");
        creadorExcel.EditarDocumento(documentoExcel, "Producto,Cantidad,Precio\nLaptop,5,1200\nMonitor,10,300\nTeclado,15,50");
        
        // Mostrar vista previa
        Console.WriteLine("\n--- Mostrando vista previa de documentos ---");
        creadorPDF.MostrarVistaPrevia(documentoPDF);
        creadorWord.MostrarVistaPrevia(documentoWord);
        creadorExcel.MostrarVistaPrevia(documentoExcel);
        
        // Abrir documentos
        Console.WriteLine("--- Abriendo documentos ---\n");
        documentoPDF.Abrir();
        documentoWord.Abrir();
        documentoExcel.Abrir();
        
        // Guardar documentos
        Console.WriteLine("\n--- Guardando documentos ---\n");
        documentoPDF.Guardar("C:\\Documentos");
        documentoWord.Guardar("C:\\Documentos");
        documentoExcel.Guardar("C:\\Documentos");
        
        // Exportar documentos
        Console.WriteLine("\n--- Exportando documentos a diferentes formatos ---\n");
        Console.WriteLine(documentoPDF.Exportar("imagen"));
        Console.WriteLine(documentoWord.Exportar("pdf"));
        Console.WriteLine(documentoExcel.Exportar("csv"));
        Console.WriteLine(documentoExcel.Exportar("xml"));
    }
}

// Implementaciones concretas de creadores
public class CreadorPDF : CreadorDocumento
{
    public override IDocumento CrearDocumento(string nombreDocumento)
    {
        var documento = new DocumentoPDF(nombreDocumento);
        Console.WriteLine($"Creando nuevo documento PDF: {documento.Nombre}{documento.Extension}");
        return documento;
    }
}

public class CreadorWord : CreadorDocumento
{
    public override IDocumento CrearDocumento(string nombreDocumento)
    {
        var documento = new DocumentoWord(nombreDocumento);
        Console.WriteLine($"Creando nuevo documento Word: {documento.Nombre}{documento.Extension}");
        return documento;
    }
}

public class CreadorExcel : CreadorDocumento
{
    public override IDocumento CrearDocumento(string nombreDocumento)
    {
        var documento = new DocumentoExcel(nombreDocumento);
        Console.WriteLine($"Creando nueva hoja de cálculo Excel: {documento.Nombre}{documento.Extension}");
        return documento;
    }
}
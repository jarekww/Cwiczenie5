using System;

// Interfejs rejestrowania wiadomości
public interface ILogger
{
    void LogMessage(string message);
}

// Implementacja rejestrowania wiadomości debugowania
public class DebugLogger : ILogger
{
    public void LogMessage(string message)
    {
        Console.WriteLine($"[DEBUG] {message}");
    }
}

// Implementacja rejestrowania wiadomości informacyjnych
public class InfoLogger : ILogger
{
    public void LogMessage(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }
}

// Implementacja rejestrowania wiadomości ostrzeżeń
public class WarningLogger : ILogger
{
    public void LogMessage(string message)
    {
        Console.WriteLine($"[WARNING] {message}");
    }
}

// Implementacja rejestrowania wiadomości błędów
public class ErrorLogger : ILogger
{
    public void LogMessage(string message)
    {
        Console.WriteLine($"[ERROR] {message}");
    }
}

// Interfejs nowej biblioteki rejestrowania wiadomości
public interface IEnhancedLogger
{
    void WriteLog(LogLevel level, string message);
}

// Enumeracja poziomów logowania
public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error
}

// Implementacja nowej biblioteki rejestrowania wiadomości
public class EnhancedLogger : IEnhancedLogger
{
    public void WriteLog(LogLevel level, string message)
    {
        switch (level)
        {
            case LogLevel.Debug:
                Console.WriteLine($"[DEBUG] {message}");
                break;
            case LogLevel.Info:
                Console.WriteLine($"[INFO] {message}");
                break;
            case LogLevel.Warning:
                Console.WriteLine($"[WARNING] {message}");
                break;
            case LogLevel.Error:
                Console.WriteLine($"[ERROR] {message}");
                break;
            default:
                throw new ArgumentException("Invalid log level");
        }
    }
}

// Adapter mapujący nowy interfejs na stary interfejs
public class EnhancedToBasicLoggerAdapter : ILogger
{
    private readonly IEnhancedLogger _enhancedLogger;

    public EnhancedToBasicLoggerAdapter(IEnhancedLogger enhancedLogger)
    {
        _enhancedLogger = enhancedLogger;
    }

    public void LogMessage(string message)
    {
        // Domyślnie logujemy jako informacyjne wiadomości
        _enhancedLogger.WriteLog(LogLevel.Info, message);
    }
}

// Przykładowe użycie
class Program
{
    static void Main(string[] args)
    {
        // Inicjalizacja nowej biblioteki rejestrowania
        IEnhancedLogger enhancedLogger = new EnhancedLogger();

        // Utworzenie adaptera, który używa nowej biblioteki, ale implementuje stary interfejs
        ILogger logger = new EnhancedToBasicLoggerAdapter(enhancedLogger);

        // Przykładowe logowanie
        logger.LogMessage("This is a test message");

        // Przykładowe logowanie na różnych poziomach
        logger.LogMessage("This is a debug message");
        logger.LogMessage("This is an info message");
        logger.LogMessage("This is a warning message");
        logger.LogMessage("This is an error message");
    }
}

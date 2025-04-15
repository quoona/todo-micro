using Rabbit.Application.Interfaces;
using Rabbit.Application.Interfaces.Logging;
using Serilog;

namespace Rabbit.Infrastructure.Services;

public class LoggingService : ILoggingService
{
    public LoggingService()
    {
        var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "loggers");
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(logPath, "log-.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14
            )
            .CreateLogger();

        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
    }

    public void LogInformation(string message) => Log.Information(message);
    public void LogWarning(string message) => Log.Warning(message);
    public void LogError(string message, Exception ex = null) => Log.Error(ex, message);
}
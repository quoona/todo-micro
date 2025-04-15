﻿namespace Rabbit.Application.Interfaces.Logging;

public interface ILoggingService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex = null);
}
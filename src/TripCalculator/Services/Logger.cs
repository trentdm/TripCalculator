using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TripCalculator.Services
{
    public interface ILogger
    {
        void LogDebug(string message, params object[] arguments);
        void LogInfo(string message, params object[] arguments);
        void LogWarning(string message, params object[] arguments);
        void LogException(Exception ex, string message = "", params object[] arguments);
    }

    public class Logger : ILogger
    {
        public void LogDebug(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }

        public void LogInfo(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }

        public void LogWarning(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }

        public void LogException(Exception ex, string message = "", params object[] arguments)
        {
            var logInfo = $"Exception: {ex.GetType().Name}, {ex.Message}\r\n" +
                          $"{ex.StackTrace}\r\n";
            Debug.WriteLine(logInfo + message, arguments);
        }
    }
}
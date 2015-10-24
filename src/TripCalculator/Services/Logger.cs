using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TripCalculator.Services
{
    public interface ILogger
    {
        void LogInfo(string message, params object[] arguments);
        void LogWarning(string message, params object[] arguments);

        void LogDebug(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            params object[] arguments);

        void LogException(Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            params object[] arguments);
    }

    public class Logger : ILogger
    {
        public void LogInfo(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }

        public void LogWarning(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }

        public void LogDebug(string message, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0,
            params object[] arguments)
        {
            var logInfo = $"Debug Info: {memberName}, {sourceFilePath}, {sourceLineNumber}> ";
            Debug.WriteLine(logInfo + message, arguments);
        }

        public void LogException(Exception ex, string message = "", string memberName = "", string sourceFilePath = "",
            int sourceLineNumber = 0, params object[] arguments)
        {
            var logInfo = $"Exception Info: {ex.GetType().Name}, {ex.Message}\r\n" +
                          $"{ex.StackTrace}\r\n" +
                          $"{memberName}, {sourceFilePath}, {sourceLineNumber}> ";
            Debug.WriteLine(logInfo + message, arguments);
        }
    }
}
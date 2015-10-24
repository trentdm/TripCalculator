using System.Diagnostics;

namespace TripCalculator.Services
{
    public interface ILogger
    {
        void Log(string message, params object[] arguments);
    }

    public class Logger : ILogger
    {
        public void Log(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }
    }
}
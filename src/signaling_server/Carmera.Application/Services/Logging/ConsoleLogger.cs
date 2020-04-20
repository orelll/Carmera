using System;

namespace Carmera.Application.Services.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message)
        {
            Console.WriteLine($@"{Timestamp} => DEBUG: {message}");
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine($@"{Timestamp} => ERROR: {message}, {Environment.NewLine}Exception: {exception}");
        }

        public void Info(string message)
        {
            Console.WriteLine($@"{Timestamp} => INFO: {message}");
        }

        public void Log(string message)
        {
            Console.WriteLine($@"{Timestamp} => LOG: {message}");
        }

        public void Warn(string message)
        {
            Console.WriteLine($@"{Timestamp} => WARN: {message}");
        }

        private string Timestamp => DateTime.Now.ToString();
    }
}
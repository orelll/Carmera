using System;

namespace Carmera.Application.Services.Logging
{
    public interface ILogger
    {
        void Log(string message);

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message, Exception exception);
    }
}
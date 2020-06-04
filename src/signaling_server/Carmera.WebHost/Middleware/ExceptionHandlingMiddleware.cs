using System;
using Microsoft.Extensions.Logging;

namespace Carmera.WebHost.Middleware
{
    public class ExceptionHandlingMiddleware

    {
        private readonly ILogger<ExceptionHandlingMiddleware> _log;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> log) => _log = log ?? throw new ArgumentNullException(nameof(log));

        public void LogException(Exception exception)
        {
            _log.LogError($"There was an exception xD{ Environment.NewLine }", exception);
        }
    }
}

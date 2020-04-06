using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Common;
using Carmera.Common.Extensions;
using System;

namespace Carmera.Application.Services.RequestHandling
{
    public abstract class ResultBase : Result
    {
        public bool Success { get; }
        public Maybe<Exception> Exception { get; }
        public Maybe<string> Message { get; }

        public ResultBase(bool success, Exception exception = null, string message = null)
        {
            Success = success;
            Exception = exception.ToMaybe();
            Message = message.ToMaybe();
        }
    }
}
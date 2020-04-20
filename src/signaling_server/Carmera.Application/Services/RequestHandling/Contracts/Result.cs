using Carmera.Common;
using System;

namespace Carmera.Application.Services.RequestHandling.Contracts
{
    public class Result
    {
        bool Success { get; }
        Maybe<Exception> Exception { get;  }
        Maybe<string> Message { get;  }
    }
}
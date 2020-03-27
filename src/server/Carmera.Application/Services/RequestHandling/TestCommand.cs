using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling
{
    public class TestCommand<TOut> : IRequest<TOut> where TOut: IResult
    {
        public string PeerName { get; }
        public IPAddress Address { get; }
        public int Port { get; }
    }
}

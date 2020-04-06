using Carmera.Application.Entities;
using System;
using System.Collections.Generic;

namespace Carmera.Application.Services.RequestHandling.Queries.Results
{
    public class GetPeerQueryResult : ResultBase
    {
        public IEnumerable<ClientInfo> PeersFound { get; }

        public GetPeerQueryResult(IEnumerable<ClientInfo> peersFound, bool success, Exception exception = null, string message = null) : base(success, exception, message)
        {
            PeersFound = peersFound;
        }
    }
}
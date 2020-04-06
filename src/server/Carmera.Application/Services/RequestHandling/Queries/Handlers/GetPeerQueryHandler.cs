using Carmera.Application.Services.RequestHandling.Queries.Results;
using System;
using System.Threading.Tasks;

namespace Carmera.Application.Services.RequestHandling.Queries.Handlers
{
    public class GetPeerQueryHandler : QueryHandler<GetPeerQuery, GetPeerQueryResult>
    {
        public override async Task<GetPeerQueryResult> HandleAsync(GetPeerQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
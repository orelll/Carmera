using System;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.Query.Results;

namespace Carmera.Application.Services.RequestHandling.Query.Handlers
{
    public class GetPeerQueryHandler : IQueryHandler<GetPeerQuery, GetPeerQueryResult>
    {
        public GetPeerQueryResult Handle(IRequest<GetPeerQueryResult> request)
        {
            throw new NotImplementedException();
        }
    }
}
using Carmera.Application.Entities;
using Carmera.Application.Services.Cache;
using Carmera.Application.Services.RequestHandling.Queries.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carmera.Application.Services.RequestHandling.Queries.Handlers
{
    public class GetPeerQueryHandler : QueryHandler<GetPeerQuery, GetPeerQueryResult>
    {
        private IRepository<ClientInfo> _repository;

        public GetPeerQueryHandler(IRepository<ClientInfo> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override GetPeerQueryResult Handle(GetPeerQuery request)
        {
            var searchKey = new StringCacheKey(request.SecondPeerName);
            var found = _repository.GetEntry(searchKey);


            return new GetPeerQueryResult(new List<ClientInfo> { found?.Value });
        }
    }
}
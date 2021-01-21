using Microsoft.Extensions.DependencyInjection;
using signaling_server.Requests;
using signaling_server.Responses;
using System;

namespace signaling_server.RequestHandlers
{
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IServiceProvider _services;

        public RequestHandlerFactory(IServiceProvider services) {
            _services = services;
        }

        public IRequestHandler<TIn, TOut> Create<TIn, TOut>(TIn requestType, TOut responseType)
            where TIn : RequestBase<TOut> where TOut: ResponseBase
        {

            return _services.GetService<IRequestHandler<TIn, TOut>>();
        }
    }
}

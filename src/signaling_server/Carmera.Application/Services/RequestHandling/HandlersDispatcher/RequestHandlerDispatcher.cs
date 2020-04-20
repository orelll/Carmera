using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Application.Services.RequestHandling.Commands.Handlers;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.Queries;
using Carmera.Application.Services.RequestHandling.Queries.Handlers;
using System;
using System.Collections.Generic;

namespace Carmera.Application.Services.RequestHandling.HandlersDispatcher
{
    public class RequestHandlerDispatcher : IRequestHandlerDispatcher
    {
        private Dictionary<Type, Func<RequestHandler<Request, Result>>> _handlers;
        private readonly IServiceProvider _serviceProvider;
        public RequestHandlerDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            PrepareHandlersList();
        }

        public RequestHandler<TReq, Result> Dispatch<TReq>(TReq request) where TReq : Request
        {
            var y =_handlers[request.GetType()];
            if (y != null)
            {
                try
                {
                    var zzz = _serviceProvider.GetService(typeof(CheckInCommandHandler));
                    var xx = (RequestHandler<TReq, Result>)zzz;
                    var z = y();
                }
                catch (Exception e)
                {
                }
               
            }

            var x = (RequestHandler<TReq, Result>)_serviceProvider.GetService(typeof(RequestHandler<TReq, Result>));
            return x;
        }

        private void PrepareHandlersList() 
        {
            _handlers = new Dictionary<Type, Func<RequestHandler<Request, Result>>>
            {
                { typeof(CheckInCommand), () => (RequestHandler<Request, Result>)_serviceProvider.GetService(typeof(CheckInCommandHandler)) },
                { typeof(CheckOutCommand), () => (RequestHandler<Request, Result>)_serviceProvider.GetService(typeof(CheckOutCommandHandler)) },
                { typeof(GetPeerQuery), () => (RequestHandler<Request, Result>)_serviceProvider.GetService(typeof(GetPeerQueryHandler)) }
            };
        }

    }
}
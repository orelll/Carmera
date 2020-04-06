using Carmera.Application.Services.Logging;
using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Application.Services.RequestHandling.Commands.Handlers;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.Queries;
using Carmera.Application.Services.RequestHandling.Queries.Handlers;
using System;
using System.Collections.Generic;

namespace Carmera.Application.Services.RequestHandling
{
    public class RequestHandlingService : IRequestHandlingService
    {
        private readonly ILogger _logger;
        private Dictionary<Type, Func<object>> _handlers;
        private readonly IServiceProvider _serviceProvider;

        public RequestHandlingService(IServiceProvider serviceProvider, ILogger logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            PrepareHandlersList();
        }

        public Result HandleRequest<TReq>(TReq request) where TReq : Request
        {
            var found = _handlers[request.GetType()];
            try
            {
                var handler = found.Invoke();
                var handlerType = handler.GetType();
                var handlingMethodInfo = handlerType.GetMethod("HandleAsync");
                handlingMethodInfo.Invoke(handler, new object[] { request });
            }
            catch (Exception a)
            {
                _logger.Error("Error during invoking handler", a);
            }

            return null;
        }

        private void PrepareHandlersList()
        {
            _handlers = new Dictionary<Type, Func<object>>
            {
                { typeof(CheckInCommand), () => _serviceProvider.GetService(typeof(CheckInCommandHandler)) },
                { typeof(CheckOutCommand), () => _serviceProvider.GetService(typeof(CheckOutCommandHandler)) },
                { typeof(GetPeerQuery), () => _serviceProvider.GetService(typeof(GetPeerQueryHandler)) }
            };
        }
    }
}
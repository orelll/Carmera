using System;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.HandlersDispatcher;

namespace Carmera.Application.Services.RequestHandling
{
    public class RequestHandlingService : IRequestHandlingService
    {
        private readonly IRequestHandlerDispatcher _handlerDispatcher;

        public RequestHandlingService(IRequestHandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher ?? throw new ArgumentNullException(nameof(handlerDispatcher));
        }

        public IResult HandleRequest(IRequest<IResult> request)
        {
            var requestHandler = _handlerDispatcher.Dispatch(request);

            throw new NotImplementedException();
        }
    }
}
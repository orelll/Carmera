using System;
using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.HandlersDispatcher
{
    public class RequestHandlerDispatcher : IRequestHandlerDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestHandlerDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IRequestHandler<IRequest<IResult>, IResult> Dispatch(IRequest<IResult> request) => (IRequestHandler<IRequest<IResult>, IResult>)_serviceProvider.GetService(typeof(IRequestHandler<IRequest<IResult>, IResult>));
    }
}
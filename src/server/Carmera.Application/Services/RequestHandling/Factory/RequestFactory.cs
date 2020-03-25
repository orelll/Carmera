using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Common.DTO.Request;
using System;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest CreateRequest<TIn>(TIn request) where TIn : RequestDTOBase
        {
            switch (request)
            {
                case CheckoutRequestDTO c:
                    return new CheckoutCommand();

                default:
                    throw new ArgumentException(request.GetType().Name);
            }
        }
    }
}
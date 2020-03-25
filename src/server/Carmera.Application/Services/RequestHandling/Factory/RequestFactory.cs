using System;
using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Common.DTO.Request;
using Carmera.Common.DTO.Response;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public class RequestFactory : IRequestFactory
    {
        public IGenericRequest<ResponseDTOBase<TIn, TOut>> CreateRequest<TIn, TOut>(TIn request) where TIn : RequestDTOBase
        {
            switch (request)
            {
                case CheckoutRequestDTO c:
                    return new CheckoutCommand() as IGenericRequest<ResponseDTOBase<TIn, TOut>>;

                default:
                    throw new ArgumentException(request.GetType().Name);
            }
        }

    }
}
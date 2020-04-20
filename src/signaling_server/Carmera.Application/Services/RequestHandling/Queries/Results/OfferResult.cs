using System;

namespace Carmera.Application.Services.RequestHandling.Queries.Results
{
    public class OfferResult : ResultBase
    {
        public string OfferAnswer { get; }

        public OfferResult(string pferAnswer, bool success = true, Exception exception = null, string message = null) : base(success, exception, message)
        {
            OfferAnswer = pferAnswer;
        }
    }
}
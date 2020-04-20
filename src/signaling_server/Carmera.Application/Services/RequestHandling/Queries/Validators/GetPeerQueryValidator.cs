using FluentValidation;

namespace Carmera.Application.Services.RequestHandling.Queries.Validators
{
    public class GetPeerQueryValidator : AbstractValidator<GetPeerQuery>
    {
        public GetPeerQueryValidator()
        {
            RuleFor(com => com.Address).NotNull();
            RuleFor(com => com.Port).GreaterThan(0);
            RuleFor(com => com.PeerName).NotNull().NotEmpty();
            RuleFor(com => com.SecondPeerName).NotNull().NotEmpty();
        }
    }
}
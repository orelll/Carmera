using FluentValidation;

namespace Carmera.Application.Services.RequestHandling.Commands.Validators
{
    public class CheckInCommandValidator : AbstractValidator<CheckInCommand>
    {
        public CheckInCommandValidator()
        {
            RuleFor(com => com.Address).NotNull();
            RuleFor(com => com.Port).GreaterThan(0);
            RuleFor(com => com.PeerName).NotNull().NotEmpty();
        }
    }
}
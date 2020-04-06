using Carmera.Application.Services.RequestHandling.Commands.Results;
using System;
using System.Threading.Tasks;

namespace Carmera.Application.Services.RequestHandling.Commands.Handlers
{
    public class CheckOutCommandHandler : CommandHandler<CheckOutCommand, CheckOutCommandResult>
    {
        public override Task<CheckOutCommandResult> HandleAsync(CheckOutCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
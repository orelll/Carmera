using Carmera.Application.Services.RequestHandling.Commands.Results;
using System;

namespace Carmera.Application.Services.RequestHandling.Commands.Handlers
{
    public class CheckOutCommandHandler : CommandHandler<CheckOutCommand, CheckOutCommandResult>
    {
        public override CheckOutCommandResult Handle(CheckOutCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
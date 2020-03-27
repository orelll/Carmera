using System;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Commands.Handlers
{
    public class CheckOutCommandHandler : ICommandHandler<CheckOutCommand, CheckOutCommandResult>
    {
        public CheckOutCommandResult Handle(IRequest<CheckOutCommandResult> request)
        {
            throw new NotImplementedException();
        }
    }
}
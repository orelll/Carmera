using System;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Commands.Handlers
{
    public class CheckInCommandHandler : ICommandHandler<CheckInCommand, CheckInCommandResult>
    {
        public CheckInCommandResult Handle(IRequest<CheckInCommandResult> request)
        {
            throw new NotImplementedException();
        }
    }
}
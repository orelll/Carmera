using Carmera.Application.Entities;
using Carmera.Application.Services.Cache;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace Carmera.Application.Services.RequestHandling.Commands.Handlers
{
    public class CheckInCommandHandler : CommandHandler<CheckInCommand, CheckInCommandResult>
    {
        private AbstractValidator<CheckInCommand> _requestValidator;
        private IRepository<ClientInfo> _repository;

        public CheckInCommandHandler(AbstractValidator<CheckInCommand> validator, IRepository<ClientInfo> repository)
        {
            _requestValidator = validator ?? throw new ArgumentNullException(nameof(validator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override CheckInCommandResult Handle(CheckInCommand request)
        {
            //TODO validate
            var castedCommand = request as CheckInCommand;
            _requestValidator.ValidateAndThrow(castedCommand);

            var key = new StringCacheKey(request.PeerName.ToLower());
            var repositoryEntry = _repository.GetOrCreateEntry(key, () => CreatePeerInfoPredicate(castedCommand));

            var result = new CheckInCommandResult(repositoryEntry.Value.Id, true);

            return result;
        }

        private ClientInfo CreatePeerInfoPredicate(CheckInCommand command) => new ClientInfo { Address = command.Address, Name = command.PeerName, Port = command.Port, Id = Guid.NewGuid() };
    }
}
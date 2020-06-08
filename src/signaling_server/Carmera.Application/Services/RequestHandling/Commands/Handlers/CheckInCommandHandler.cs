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
        private IRepository _repository;

        public CheckInCommandHandler(AbstractValidator<CheckInCommand> validator, IRepository repository)
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
            var repositoryEntry = _repository.GetOrCreateEntry(key, () => CreatePeerInfoCacheKey(castedCommand, key));

            var result = new CheckInCommandResult(((ClientInfo)repositoryEntry.Value.Value).Id, true);

            return result;
        }

        private CacheEntry CreatePeerInfoCacheKey(CheckInCommand request, StringCacheKey key) => new CacheEntry(key, GetClientInfo(request));

        private ClientInfo GetClientInfo(CheckInCommand request) => new ClientInfo { Address = request.Address, Name = request.PeerName, Port = request.Port, Id = Guid.NewGuid() };
    }
}
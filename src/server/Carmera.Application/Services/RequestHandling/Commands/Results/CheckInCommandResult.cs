using System;

namespace Carmera.Application.Services.RequestHandling.Commands.Results
{
    public class CheckInCommandResult : ResultBase
    {
        public Guid EntryId { get; set; }

        public CheckInCommandResult(Guid entryId, bool success, Exception exception = null, string message = null) : base(success, exception, message)
        {
            EntryId = entryId;
        }
    }
}
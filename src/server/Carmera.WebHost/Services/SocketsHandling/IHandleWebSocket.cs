using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Carmera.WebHost.Services.SocketsHandling
{
    public interface IHandleWebSocket
    {
        Task CatchWebSocket(HttpContext context, Func<Task> next);
    }
}
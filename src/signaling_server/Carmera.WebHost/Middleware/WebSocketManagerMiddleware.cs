using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Carmera.WebHost.Services.SocketsHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Carmera.WebHost.Middleware
{
    public class WebSocketManagerMiddleware
    {

        private ILogger<WebSocketManagerMiddleware> _log;
        private IHandleWebSocket _socketsHandler;

        public WebSocketManagerMiddleware(ILogger<WebSocketManagerMiddleware> log, IHandleWebSocket socketsHandler)
        {
            _log = log ?? throw new ArgumentNullException(nameof(_log));
            _socketsHandler = socketsHandler ?? throw new ArgumentNullException(nameof(socketsHandler));
        }

        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            _log.LogDebug($@"New request {context.Request.Body}");

            if (context.WebSockets.IsWebSocketRequest)
            {
                _log.LogDebug("Its a socket");
                await _socketsHandler.CatchWebSocket(context);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
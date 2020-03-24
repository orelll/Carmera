using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Carmera.WebHost.Services.Cache;
using Microsoft.AspNetCore.Http;

namespace Carmera.WebHost.Services.SocketsHandling
{
    public class WebSocketHandler : IHandleWebSocket
    {
        private readonly IRepository<ClientInfo> _cacheHandler;

        public WebSocketHandler(IRepository<ClientInfo> cacheHandler) => _cacheHandler = cacheHandler ?? throw new ArgumentNullException(nameof(cacheHandler));

        public async Task CatchWebSocket(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    await HandleRequest(context, webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                await next();
            }
        }

        private async Task HandleRequest(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                await webSocket.SendAsync(new ArraySegment<byte>(_okMessage, 0, _okMessage.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

            var resp = Encoding.UTF8.GetString(buffer);
            int i = resp.IndexOf('\0');
            if (i >= 0) resp = resp.Substring(0, i);

            var key = new StringCacheKey { Value = resp };

            var clientInfo = _cacheHandler.GetOrCreateEntry(key, () => CreateNewClient(context, resp));
        }

        private byte[] _okMessage = Encoding.UTF8.GetBytes("OK");

        ClientInfo CreateNewClient(HttpContext context, string name) =>  new ClientInfo
        {
            Address = context.Connection.RemoteIpAddress,
            Port = context.Connection.RemotePort,
            Name = name
        };
    }
}
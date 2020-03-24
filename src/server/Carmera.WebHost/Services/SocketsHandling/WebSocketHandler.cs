using Carmera.Application.Services.Cache;
using Carmera.Common;
using Carmera.Common.DTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Carmera.WebHost.Services.SocketsHandling
{
    public class WebSocketHandler : IHandleWebSocket
    {
        private readonly IRepository<ClientInfo> _cacheHandler;
        private byte[] _okMessage = Encoding.UTF8.GetBytes("OK");

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
            while (!result.EndOfMessage)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.SendAsync(new ArraySegment<byte>(_okMessage, 0, _okMessage.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "DUPA", CancellationToken.None);

            var message = PrepareIncomingMessage(buffer);

            RequestDTOBase request = null;
            var success = TryCastToBaseRequestDTO(message, out request);

            var x = JsonConvert.DeserializeObject<CheckoutRequestDTO>(message);
            //var key = new StringCacheKey { Value = resp };

            //var clientInfo = _cacheHandler.GetOrCreateEntry(key, () => CreateNewClientPredicate(context, resp));
        }

        private string PrepareIncomingMessage(byte[] buffer)
        {
            var resp = Encoding.UTF8.GetString(buffer);
            int i = resp.IndexOf('\0');
            if (i >= 0) resp = resp.Substring(0, i);

            return resp;
        }

        private bool TryCastToBaseRequestDTO(string value, out RequestDTOBase request)
        {
            request = null;

            try
            {
                request = JsonConvert.DeserializeObject<RequestDTOBase>(value);
                return request != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private ClientInfo CreateNewClientPredicate(HttpContext context, string name) => new ClientInfo
        {
            Address = context.Connection.RemoteIpAddress,
            Port = context.Connection.RemotePort,
            Name = name
        };
    }
}
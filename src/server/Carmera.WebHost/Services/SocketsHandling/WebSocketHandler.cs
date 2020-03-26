using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Carmera.Application.Services.RequestHandling;
using Carmera.Application.Services.RequestHandling.Factory;
using Carmera.WebHost.Services.DTOProduction;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Carmera.Application.Services.RequestHandling.RequestsTypes;

namespace Carmera.WebHost.Services.SocketsHandling
{
    public class WebSocketHandler : IHandleWebSocket
    {
        private byte[] _okMessage = Encoding.UTF8.GetBytes("OK");
        private IDTOFactory _dtoFactory;
        private IRequestFactory _requestFactory;
        private IRequestHandlingService _requestHandlingService;

        public WebSocketHandler(IDTOFactory dtoFactory, IRequestFactory requestFactory, IRequestHandlingService requestHandlingService)
        {
            _dtoFactory = dtoFactory ?? throw new ArgumentNullException(nameof(dtoFactory));
            _requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
            _requestHandlingService = requestHandlingService ?? throw new ArgumentNullException(nameof(requestHandlingService));
        }

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

            var requestKind = GetKind(message);

            if (requestKind > RequestsTypes.RequestType.Unset)
            {
                var dto = _dtoFactory.ObtainDTO(requestKind, message);
                var request = _requestFactory.CreateRequest(dto);
                var response = _requestHandlingService.HandleRequest(request);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private string PrepareIncomingMessage(byte[] buffer)
        {
            var resp = Encoding.UTF8.GetString(buffer);
            int i = resp.IndexOf('\0');
            if (i >= 0) resp = resp.Substring(0, i);

            return resp;
        }

        private RequestType GetKind(string message)
        {
            var requetType = RequestType.Unset;
            var obj = (JObject)JsonConvert.DeserializeObject(message);
            var found = obj.GetValue("kind").ToString();

            if (!string.IsNullOrEmpty(found))
            {
                switch (found)
                {
                    //TODO: it's case sensitive!
                    case nameof(RequestType.Checkout):
                        requetType = RequestType.Checkout;
                        break;

                    case nameof(RequestType.GetPeer):
                        requetType = RequestType.GetPeer;
                        break;

                    case nameof(RequestType.ListPeers):
                        requetType = RequestType.ListPeers;
                        break;

                    case nameof(RequestType.Logout):
                        requetType = RequestType.Logout;
                        break;

                    default:
                        requetType = RequestType.Unset;
                        break;
                }
            }

            return requetType;
        }

        private ClientInfo CreateNewClientPredicate(HttpContext context, string name) => new ClientInfo
        {
            Address = context.Connection.RemoteIpAddress,
            Port = context.Connection.RemotePort,
            Name = name
        };
    }
}
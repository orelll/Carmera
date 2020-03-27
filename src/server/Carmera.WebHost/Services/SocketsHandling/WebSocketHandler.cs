using System;
using System.Linq;
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

            var payload = PrepareIncomingMessage(buffer);

            var requestKind = GetRequestKind(payload);

            if (requestKind > RequestType.Unset)
            {
                var peerInfo = PreparePeerInfo(context, payload);
                var dto = _dtoFactory.ObtainDTO(requestKind, peerInfo);
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

        private RequestType GetRequestKind(string message)
        {
            var requetType = RequestType.Unset;
            var obj = (JObject)JsonConvert.DeserializeObject(message);
            var found = obj.GetValue("kind").ToString();

            if (!string.IsNullOrEmpty(found))
            {
                requetType = ((RequestType[])Enum.GetValues(typeof(RequestType))).FirstOrDefault(type => type.ToString().ToLower() == found.ToLower());
            }

            return requetType;
        }

        private PeerInfo PreparePeerInfo(HttpContext context, string payload) => new PeerInfo(payload, context.Connection.RemoteIpAddress, context.Connection.RemotePort);
    }
}
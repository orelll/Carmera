using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Carmera.Application.Services.RequestHandling;
using Carmera.Application.Services.RequestHandling.Factory;
using Carmera.WebHost.Services.DTOProduction;
using Microsoft.AspNetCore.Http;

namespace Carmera.WebHost.Services.SocketsHandling
{
    public class WebSocketHandler : IHandleWebSocket
    {
        private byte[] _okMessage = Encoding.UTF8.GetBytes("OK");
        private IDTOFactory _dtoFactory;
        private IRequestFactory _requestFactory;
        private IRequestHandlerDispatcher _requestsDispatcher;

        public WebSocketHandler(IDTOFactory dtoFactory, IRequestFactory requestFactory, IRequestHandlerDispatcher requestsDispatcher)
        {
            _dtoFactory = dtoFactory ?? throw new ArgumentNullException(nameof(dtoFactory));
            _requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
            _requestsDispatcher = requestsDispatcher ?? throw new ArgumentNullException(nameof(requestsDispatcher));
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
                var dto = _dtoFactory.ObtainDTO(message);
                var request = _requestFactory.CreateRequest(dto);
                _requestsDispatcher.Dispatch(request);
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

        private RequestsTypes.RequestType GetKind(string value)
        {
            return RequestsTypes.RequestType.Unset;

            //try
            //{
            //    return JsonConvert.DeserializeObject<RequestDTOBase>(value).ToMaybe();
            //}
            //catch (Exception e)
            //{
            //    //TODO: log this
            //    return Maybe<RequestDTOBase>.Empty();
            //}
        }

        private ClientInfo CreateNewClientPredicate(HttpContext context, string name) => new ClientInfo
        {
            Address = context.Connection.RemoteIpAddress,
            Port = context.Connection.RemotePort,
            Name = name
        };
    }
}
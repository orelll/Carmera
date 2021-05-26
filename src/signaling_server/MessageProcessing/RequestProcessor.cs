using Newtonsoft.Json.Linq;
using signaling_server.Converters;
using signaling_server.RequestHandlers;
using signaling_server.Requests;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.MessageProcessing
{
    public class RequestProcessor : IRequestProcessor
    {
        private IConvert<byte[], string> _toStringConverter;
        private IRequestHandlerFactory _handlersFactory;

        public RequestProcessor(IConvert<byte[], string> toStringConverter, IRequestHandlerFactory handlersFactory)
        {
            _toStringConverter = toStringConverter;
            _handlersFactory = handlersFactory;
        }

        public object ProcessRequest(byte[] requestBytes, IPAddress address, WebSocket socket)
        {
            var requestAsString = _toStringConverter.Convert(requestBytes);

            try
            {
                var json = JObject.Parse(requestAsString);
                var type = json["type"].ToString();
                var payload = json["payload"].ToString();



                switch (type)
                {
                    case "clientOffer":
                        var clientOfferRequest = new ClientOfferRequest(address, socket, payload);
                        var clientOfferRequestHandler = _handlersFactory.Create(clientOfferRequest, clientOfferRequest.GetResponseType());

                        return clientOfferRequestHandler.Handle(clientOfferRequest);
                    case "serverOffer":
                        var serverRequest = new ServerOfferRequest(address, socket, payload);
                        var serverRequestHandler = _handlersFactory.Create(serverRequest, serverRequest.GetResponseType());

                        return serverRequestHandler.Handle(serverRequest);
                    case "answer":
                        var answerRequest = new AnswerRequest(address, socket, payload);
                        var answerRequestHandler = _handlersFactory.Create(answerRequest, answerRequest.GetResponseType());

                        return answerRequestHandler.Handle(answerRequest);
                    case "txt":
                    default:
                        var txtRequest = new TxtRequest(address, socket, payload);
                        var txtRequestHandler = _handlersFactory.Create(txtRequest, txtRequest.GetResponseType());
                        return txtRequestHandler.Handle(txtRequest);
                }
            }
            catch (System.Exception ex)
            {
                return $@"An error {ex} occured.";
            }
        }

    }
}

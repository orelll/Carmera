using Newtonsoft.Json.Linq;
using signaling_server.Converters;
using signaling_server.RequestHandlers;
using signaling_server.Requests;
using System.Net;

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

        public object ProcessRequest(byte[] requestBytes, IPAddress address)
        {
            var requestAsString = _toStringConverter.Convert(requestBytes);
            var json = JObject.Parse(requestAsString);
            var type = json["type"].ToString();
            var payload = json["payload"].ToString();

            switch (type)
            {
                case "clientOffer":
                    var clientOfferRequest = new ClientOfferRequest(address, payload);
                    var clientOfferRequestHandler = _handlersFactory.Create(clientOfferRequest, clientOfferRequest.GetResponseType());
                   
                    return clientOfferRequestHandler.Handle(clientOfferRequest);                
                case "serverOffer":
                    var serverRequest = new ServerOfferRequest(address, payload);
                    var serverRequestHandler = _handlersFactory.Create(serverRequest, serverRequest.GetResponseType());
                   
                    return serverRequestHandler.Handle(serverRequest);
                case "txt":
                default:
                    var  txtRequest = new TxtRequest(address, payload);
                    var txtRequestHandler = _handlersFactory.Create(txtRequest, txtRequest.GetResponseType());
                    return txtRequestHandler.Handle(txtRequest);
            }
        }
        
    }
}

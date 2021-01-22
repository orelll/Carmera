using Microsoft.Extensions.DependencyInjection;
using signaling_server.Converters;
using signaling_server.MessageProcessing;
using signaling_server.RequestHandlers;
using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;

namespace signaling_server
{
    public class DI
    {
        public static void DoRegistrations(IServiceCollection services)
        {
            services.AddTransient<IConvert<byte[], string>, ByteArrayToStringConverter>();
            services.AddTransient<IRequestProcessor, RequestProcessor>();
            services.AddTransient<SocketHandler>();
            services.AddTransient<IRequestHandler<ClientOfferRequest, ClientOfferResponse>, ClientOfferRequestHandler>();
            services.AddTransient<IRequestHandler<ServerOfferRequest, ServerOfferResponse>, ServerOfferRequestHandler>();
            services.AddTransient<IRequestHandler<GetServerRequest, GetServerResponse>, GetServerRequestHandler>();
            services.AddTransient<IRequestHandler<AnswerRequest, AnswerResponse>, AnswerRequestHandler>();
            services.AddTransient<IRequestHandlerFactory, RequestHandlerFactory>();
            services.AddSingleton<ISocketRepository, SocketRepository>();
            services.AddSingleton<ISocketNotifier, SocketNotifier>();
        }
    }
}
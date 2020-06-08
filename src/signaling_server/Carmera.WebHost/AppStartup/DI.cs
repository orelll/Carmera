using Carmera.Application.Entities;
using Carmera.Application.Services.Cache;
using Carmera.Application.Services.Logging;
using Carmera.Application.Services.RequestHandling;
using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Application.Services.RequestHandling.Commands.Handlers;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Application.Services.RequestHandling.Commands.Validators;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.Factory;
using Carmera.Application.Services.RequestHandling.HandlersDispatcher;
using Carmera.Application.Services.RequestHandling.Queries;
using Carmera.Application.Services.RequestHandling.Queries.Handlers;
using Carmera.Application.Services.RequestHandling.Queries.Validators;
using Carmera.WebHost.Middleware;
using Carmera.WebHost.Services.DTOProduction;
using Carmera.WebHost.Services.SocketsHandling;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Carmera.WebHost.AppStartup
{
    public class DI
    {
        public static void RegisterAll(IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddTransient<WebSocketManagerMiddleware>();

            RegisterValidators(services);
            RegisterServices(services);
            RegisterRequestHandlers(services);
        }

        public static void RegisterValidators(IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<CheckOutCommand>, CheckOutCommandValidator>();
            services.AddTransient<AbstractValidator<CheckInCommand>, CheckInCommandValidator>();
            services.AddTransient<AbstractValidator<GetPeerQuery>, GetPeerQueryValidator>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IDTOFactory, DTOFactory>();
            services.AddTransient<IRequestFactory, RequestFactory>();
            services.AddTransient<IRequestHandlerDispatcher, RequestHandlerDispatcher>();

            services.AddTransient<IRequestHandlingService, RequestHandlingService>();
            services.AddTransient<IRepository, CacheRepository>();
            services.AddTransient<IHandleWebSocket, WebSocketHandler>();
        }

        public static void RegisterRequestHandlers(IServiceCollection services)
        {
            services.AddTransient<CheckInCommandHandler>();
            services.AddTransient<CheckOutCommandHandler>();
            services.AddTransient<GetPeerQueryHandler>();
            services.AddTransient<OfferQueryHandler>();
        }
    }
}
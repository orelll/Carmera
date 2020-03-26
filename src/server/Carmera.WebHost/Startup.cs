using Carmera.Application.Services.Cache;
using Carmera.Application.Services.Logging;
using Carmera.Application.Services.RequestHandling;
using Carmera.Application.Services.RequestHandling.Factory;
using Carmera.Common.DTO.Request;
using Carmera.WebHost.Services.DTOProduction;
using Carmera.WebHost.Services.SocketsHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Carmera.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();

            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IDTOFactory, DTOFactory>();
            services.AddTransient<IRequestFactory, RequestFactory>();
            services.AddTransient<IRequestHandlingService, RequestHandlingService>();
            services.AddTransient<IRepository<ClientInfo>, CacheRepository<ClientInfo>>();
            services.AddTransient<IHandleWebSocket, WebSocketHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TODO: why it breaks connection?
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseExceptionHandler(app =>
            {
                Console.WriteLine("zesra³o siê xD");
            });
            app.UseAuthorization();

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets();
            var wsHandler = app.ApplicationServices.GetService<IHandleWebSocket>();
            app.Use(async (context, next) => await wsHandler.CatchWebSocket(context, next));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
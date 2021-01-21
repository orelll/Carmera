using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using signaling_server.Socketing;

namespace signaling_server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            DI.DoRegistrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.Use(async (http, next) =>
            {
                if (http.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await http.WebSockets.AcceptWebSocketAsync();
                    
                    var socketHandler = app.ApplicationServices.GetService<SocketHandler>();
                    await socketHandler.ReceiveSocket(webSocket, http.Connection.RemoteIpAddress);
                }
                else
                {
                    await next();
                }
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }


    }
}

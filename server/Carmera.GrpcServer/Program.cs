using Carmera.CameraLoader.Interfaces;
using Carmera.CameraLoader.Options;
using Carmera.CameraLoader.Services;
using Carmera.Grpc.Options;
using Carmera.GrpcServer.Services;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddScoped<ICameraConsumer, CameraConsumer>();

builder.Services.Configure<FfmpegOptions>(
    builder.Configuration.GetSection(FfmpegOptions.FFmpeg));
builder.Services.Configure<UrlsOptions>(
    builder.Configuration.GetSection(UrlsOptions.Config));
builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5017, o => o.Protocols =  HttpProtocols.Http2);
});
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseCors("corsapp");

Console.WriteLine("Loading urls...");
var urls = app.Services.GetService<IOptions<UrlsOptions>>();
Console.WriteLine($"Found: { string.Join(", ", urls?.Value?.Urls ?? Enumerable.Empty<string>())}");
if (urls?.Value?.Urls != null && urls.Value.Urls.Any())
{
    foreach (var url in urls.Value.Urls)
    {
        app.Urls.Add(url);
    }
}

// Configure the HTTP request pipeline.
// app.MapGrpcService<CameraLoaderService>();

app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.MapGrpcService<CameraLoaderService>().EnableGrpcWeb();
app.Run();
using Carmera.CameraLoader.Interfaces;
using Carmera.CameraLoader.Options;
using Carmera.CameraLoader.Services;
using Carmera.Grpc.Options;
using Carmera.Grpc.Services;
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

var app = builder.Build();

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
app.MapGrpcService<CameraLoaderService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
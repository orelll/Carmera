var builder = WebApplication.CreateBuilder(args);
var proxyBuilder = builder.Services.AddReverseProxy();
proxyBuilder.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseCors("corsapp");
app.UseRouting();
app.MapReverseProxy();
app.Run();

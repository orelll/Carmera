namespace Carmera.Grpc.Options;

public class UrlsOptions
{
    public const string Config = "Config";
    public IEnumerable<string> Urls { get; set; }
}
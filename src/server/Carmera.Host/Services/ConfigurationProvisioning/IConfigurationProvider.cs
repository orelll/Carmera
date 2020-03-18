namespace Carmera.Host
{
    public interface IConfigurationProvider<T>
    {
        T GetConfiguration();
    }
}
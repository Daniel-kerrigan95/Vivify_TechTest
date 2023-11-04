namespace Vivify_TechTest.Configuration.Configuration.Interface
{
    public interface IConfigurationHelper
    {
        public ExternalConnections ExternalConnections { get; set; }

        public string GetBaseUrl();
    }
}

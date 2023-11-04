using Microsoft.Extensions.Configuration;
using Vivify_TechTest.Configuration.Configuration.Interface;
using Vivify_TechTest.Configuration.Constants;
using Vivify_TechTest.Configuration.Helpers;

namespace Vivify_TechTest.Configuration.Configuration
{
    public class ConfigurationHelper : IConfigurationHelper
    {

        public ConfigurationHelper(IConfiguration _Config)
        {
            ExternalConnections = _Config.GetSection(nameof(ExternalConnections)).Get<ExternalConnections>();
            RunConfiguration = new RunConfiguration
            {
                RunLocation = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline)
            };
        }
        public RunConfiguration RunConfiguration { get; }
        public ExternalConnections ExternalConnections { get; set; }

        public string GetBaseUrl()
        {
            return ExternalConnections.BaseUrl;
        }
    }
}

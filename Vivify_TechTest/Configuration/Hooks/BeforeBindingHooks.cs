using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.TestFramework;
using Vivify_TechTest.Configuration.Configuration;
using Vivify_TechTest.Configuration.Constants;
using Vivify_TechTest.Configuration.Utilities;
using Vivify_TechTest.Pages;

namespace Vivify_TechTest.Configuration.Hooks
{
    [Binding]
    public class BeforeBindingHooks
    {
        #region Element Identifiers
        private readonly SeleniumContext _seleniumContext;
        private readonly IObjectContainer _objectContainer;
        private readonly FeatureContext _featureContext;
        private readonly ITestRunContext _testRunContext;
        private readonly IWebDriver _driver;
        public static IConfiguration _Config;
        #endregion

        public BeforeBindingHooks(IObjectContainer objectContainer, SeleniumContext seleniumContext,
            FeatureContext featureContext,
            ITestRunContext testRunContext)
        {
            _objectContainer = objectContainer;
            _seleniumContext = seleniumContext;
            _featureContext = featureContext;
            _testRunContext = testRunContext;
        }

        public void BindPages()
        {
            var basePage = new BasePage(_seleniumContext.WebDriver);
            _objectContainer.RegisterInstanceAs(basePage);
            var vivifyPage = new VivifyPage(_seleniumContext.WebDriver, basePage);
            _objectContainer.RegisterInstanceAs(vivifyPage);
        }


        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            CheckAndOverwriteEnvironmentVariables();
            var configurationHelper = BindConfig();
            SetInitialConfiguration(configurationHelper);
            BindPages();

        }

        public void SetInitialConfiguration(ConfigurationHelper configurationHelper)
        {
            _featureContext.Set(configurationHelper.ExternalConnections.BaseUrl, FeatureContextKeys.BaseUrl);
        }

        private void CheckAndOverwriteEnvironmentVariables()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline, EnvironmentVariableValues.Live);
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestEnvironment)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.TestEnvironment, EnvironmentVariableValues.Live);
            }
        }

        private ConfigurationHelper BindConfig()
        {
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestEnvironment);
            string appsettingsPath = environment == null ? "appsettings.json" : $"appsettings.{environment}.json";

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .SetBasePath(_testRunContext.GetTestDirectory())
            .AddJsonFile(appsettingsPath)
            .AddUserSecrets<BeforeBindingHooks>(true);

            _Config = configurationBuilder.Build();


            ConfigurationHelper configurationHelper = new(_Config);

            _objectContainer.RegisterInstanceAs(_Config);
            _objectContainer.RegisterInstanceAs(configurationHelper);

            return configurationHelper;
        }

        [AfterScenario]
        public void AfterScenario(ConfigurationHelper configurationHelper)
        { 
            _seleniumContext.WebDriver.Quit();
        }
    }
}
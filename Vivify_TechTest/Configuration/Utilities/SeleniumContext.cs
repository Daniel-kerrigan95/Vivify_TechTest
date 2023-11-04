using Vivify_TechTest.Configuration.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel)]


namespace Vivify_TechTest.Configuration.Utilities
{
    [Binding]
    public class SeleniumContext
    {
        public SeleniumContext()
        {
            WebDriver = GetTheWebDriverForTheBrowser();
            WebDriver.Manage().Window.Maximize();
        }

        private IWebDriver GetTheWebDriverForTheBrowser()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.Test_Browser)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.Test_Browser, EnvironmentVariableValues.Chrome);
            }
            string browser = Environment.GetEnvironmentVariable("Test_Browser").ToLower();
            switch (browser)
            {
                case "chrome":
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments(new List<string>()
                    {
                        "--disable-gpu",
                        "--no-first-run",
                        "--no-default-browser-check",
                        "--ignore-certificate-errors",
                        "--no-sandbox",
                        //"--window-size=390,844",
                        "--window-size=1920,1200",
                        "--start-maximized",
                        "--disable-dev-shm-usage",
                         "--disable-infobars",
                        "--disable-extensions"
                    });
                        chromeOptions.AddExcludedArgument("enable-automation");
#if !DEBUG
                        chromeOptions.AddArguments("--headless");
#endif
                        var chromeconfig = new ChromeConfig();
                        var version = chromeconfig.GetLatestVersion();
                        var envChromeWebDriver = Environment.GetEnvironmentVariable("ChromeWebDriver");
                        if (string.IsNullOrEmpty(envChromeWebDriver))
                        {
                            new WebDriverManager.DriverManager().SetUpDriver(chromeconfig, version);
                            var driver = new ChromeDriver(chromeOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else if (File.Exists(Path.Combine(envChromeWebDriver, "chromedriver.exe")))
                        {
                            chromeOptions.AddArguments($"window-size=1920,1200");
                            //chromeOptions.AddArguments($"window-size=390,844");

                            var driverPath = Path.Combine(envChromeWebDriver);
                            ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService(driverPath);
                            defaultService.HideCommandPromptWindow = true;
                            var driver = new ChromeDriver(defaultService, chromeOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else
                            throw new DriverServiceNotFoundException("Driver not installed: <null>");

                    }
                case "edge":
                    {
                        EdgeOptions edgeOptions = new EdgeOptions();
                        edgeOptions.AddArguments(new List<string>()
                    {
                        "--disable-gpu",
                        "--no-first-run",
                        "--no-default-browser-check",
                        "--ignore-certificate-errors",
                        "--no-sandbox",
                        //"--window-size=1038,600",
                        "--window-size=1920,1200",
                        "--start-maximized",
                        "--disable-dev-shm-usage",
                        "--disable-infobars",
                        "--disable-extensions",
                        "--blink-settings"

                    });
                        edgeOptions.AcceptInsecureCertificates = true;
                        edgeOptions.AddExcludedArgument("enable-automation");
#if !DEBUG
                        edgeOptions.AddArguments("--headless");
#endif
                        var edgeConfig = new EdgeConfig();
                        var version = edgeConfig.GetLatestVersion();
                        var envChromeWebDriver = Environment.GetEnvironmentVariable("EdgeWebDriver");
                        if (string.IsNullOrEmpty(envChromeWebDriver))
                        {
                            new WebDriverManager.DriverManager().SetUpDriver(edgeConfig, version);
                            var driver = new EdgeDriver(edgeOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else if (File.Exists(Path.Combine(envChromeWebDriver, "edgedriver.exe")))
                        {
                            // chromeOptions.AddArguments($"window-size=1920,1200");
                            edgeOptions.AddArguments($"window-size=390,844");

                            var driverPath = Path.Combine(envChromeWebDriver);
                            EdgeDriverService defaultService = EdgeDriverService.CreateDefaultService(driverPath);
                            defaultService.HideCommandPromptWindow = true;
                            var driver = new EdgeDriver(defaultService, edgeOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else
                            throw new DriverServiceNotFoundException("Driver not installed: <null>");
                    }
                case "chromium":
                    {
                        EdgeOptions edgeOptions = new EdgeOptions();
                        edgeOptions.AddArguments(new List<string>()
                    {
                        "--disable-gpu",
                        "--no-first-run",
                        "--no-default-browser-check",
                        "--ignore-certificate-errors",
                        "--no-sandbox",
                        //"--window-size=1038,600",
                        "--window-size=1920,1200",
                        "--start-maximized",
                        "--disable-dev-shm-usage"
                    });
                        edgeOptions.AcceptInsecureCertificates = true;
                        edgeOptions.AddExcludedArgument("enable-automation");
#if !DEBUG
                        edgeOptions.AddArguments("--headless");
#endif
                        var chromeconfig = new EdgeConfig();
                        var version = chromeconfig.GetLatestVersion();
                        var envChromeWebDriver = Environment.GetEnvironmentVariable("EdgeWebDriver");
                        if (string.IsNullOrEmpty(envChromeWebDriver))
                        {
                            new WebDriverManager.DriverManager().SetUpDriver(chromeconfig, version);
                            var driver = new EdgeDriver(edgeOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else if (File.Exists(Path.Combine(envChromeWebDriver, "edgedriver.exe")))
                        {
                            // chromeOptions.AddArguments($"window-size=1920,1200");
                            edgeOptions.AddArguments($"window-size=390,844");

                            var driverPath = Path.Combine(envChromeWebDriver);
                            EdgeDriverService defaultService = EdgeDriverService.CreateDefaultService(driverPath);
                            defaultService.HideCommandPromptWindow = true;
                            var driver = new EdgeDriver(defaultService, edgeOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else
                            throw new DriverServiceNotFoundException("Driver not installed: <null>");
                    }
                case "firefox":
                    {
                        FirefoxOptions firefoxOptions = new FirefoxOptions();
                        firefoxOptions.AddArguments(new List<string>()
                        {
                            "--disable-gpu",
                            "--no-first-run",
                            "--no-default-browser-check",
                            "--ignore-certificate-errors",
                            "--start-maximized",
                            "--disable-dev-shm-usage",
                            "--disable-infobars",
                            "--disable-extensions"
                        });
                        firefoxOptions.SetPreference("dom.webnotifications.enabled", false);
#if !DEBUG
    firefoxOptions.AddArguments("--headless");
#endif

                        var firefoxConfig = new FirefoxConfig();
                        var version = firefoxConfig.GetLatestVersion();
                        if (version == null)
                        {
                            version = "0.33.0";
                        }
                        var envGeckoWebDriver = Environment.GetEnvironmentVariable("GeckoWebDriver");

                        if (string.IsNullOrEmpty(envGeckoWebDriver))
                        {
                            new WebDriverManager.DriverManager().SetUpDriver(firefoxConfig, version);
                            var driver = new FirefoxDriver(firefoxOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else if (File.Exists(Path.Combine(envGeckoWebDriver, "geckodriver.exe")))
                        {
                            var driverPath = Path.Combine(envGeckoWebDriver);
                            FirefoxDriverService defaultService = FirefoxDriverService.CreateDefaultService(driverPath);
                            defaultService.HideCommandPromptWindow = true;
                            var driver = new FirefoxDriver(defaultService, firefoxOptions);
                            driver.Manage().Cookies.DeleteAllCookies();
                            return driver;
                        }
                        else
                            throw new DriverServiceNotFoundException("Driver not installed: <null>");
                    }

                case "remote":
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments(new List<string>()
                        {
                        "--disable-gpu",
                        "--no-first-run",
                        "--no-default-browser-check",
                        "--ignore-certificate-errors",
                        "--no-sandbox",
                        //"--window-size=390,844",
                        "--window-size=1920,1200",
                        "--start-maximized",
                        "--disable-dev-shm-usage",
                         "--disable-infobars",
                        "--disable-extensions",
                        "--headless"
                    });
                        chromeOptions.AddExcludedArgument("enable-automation");

                        return new RemoteWebDriver(new Uri("http://localhost:4444/"), chromeOptions);

                    }
                default: throw new NotSupportedException("not supported browser: <null>");
            }

        }
        public IWebDriver WebDriver { get; set; }
    }
}

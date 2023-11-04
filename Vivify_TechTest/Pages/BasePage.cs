using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Net.Mail;

namespace Vivify_TechTest.Pages
{
    public class BasePage
    {
        public readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToTheApp(string url)
        {
            int counter = 5;
            for (int i = 0; i < counter; i++)
            {
                if (TryGoToTheAppAgain(url))
                    break;
            }
        }

        private bool TryGoToTheAppAgain(string url)
        {
            try
            {
                _driver.Navigate().GoToUrl(url);
            }
            catch (Exception e)
            {
                if (e is WebDriverException)
                {
                    return false;
                }
            }

            return true;
        }


        internal void ScrollToView(By element)
        {
            IWebElement scrollElement = _driver.FindElement(element);
        }

       
        #region Waits
        public void WaitForTimeOut()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        public void WaitUntilElementIsClickable(By element)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitUntilElementIsVisibleExplicitWait(By element)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
        }

        #endregion Waits

        #region Clicks
        public void ClickOn(By element)
        {
            WaitUntilElementIsClickable(element);
            try
            {
                _driver.FindElement(element).Click();
            }
            catch (Exception ex)
            {
                if (ex is ElementClickInterceptedException)
                {
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
                    jse.ExecuteScript("arguments[0].click()", _driver.FindElement(element));
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion Clicks

        #region EnterText

        public void ClearText(By element)
        {
            _driver.FindElement(element).Clear();
        }
        public void EnterText(By element, string text)
        {
            WaitUntilElementIsVisibleExplicitWait(element);
            ClickOn(element);
            ClearText(element);
            _driver.FindElement(element).SendKeys(text);
        }
        #endregion EnterText

        #region ReadText
        public string GetText(By element)
        {
            WaitUntilElementIsVisibleExplicitWait(element);
            return _driver.FindElement(element).Text;
        }

        #endregion ReadText


        public bool AreStringsEqual(string currentText, string expectedTest)
        {
            return string.Equals(currentText, expectedTest,
                StringComparison.OrdinalIgnoreCase);
        }

        public void BrowserQuit()
        {
            _driver.Quit();
        }

    }
}

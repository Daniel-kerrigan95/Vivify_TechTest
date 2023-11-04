using Microsoft.Extensions.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Vivify_TechTest.Pages
{
    [Binding]
    public class VivifyPage
    {
        #region Element Locators 
        public readonly IWebDriver _driver;
        public readonly BasePage _basePage;
        public readonly By _header = By.XPath("//h1");
        public readonly By _postcodeField = By.Id("postcode");
        public readonly By _findVenueButton = By.XPath("(//button/span[text()='Find a venue'])[1]");
        public readonly By _resultsList = By.Id("results");
        public readonly By _lancasterClassRoom = By.XPath("(//a[contains(@href, 'lancaster-royal-grammar/lancaster_classroom')])[1]");
        public readonly By _venueTypeHeader = By.XPath("//div[@class='grow -mx-8 px-8 sm:px-0 sm:pr-6 sm:mx-0']//h2");
        public readonly By _venueNameHeader = By.XPath("//div[@class='text-gray-800 font-semibold col-span-2']");
        public readonly By _monthCalendarHeader = By.XPath("//h2[@class='fc-toolbar-title']");
        public readonly By _calendarNextMonth = By.XPath("(//button[@title='Next month'])[1]");
        public readonly By _checkAvailabilityButton = By.XPath("//div[@class='mt-6 gap-y-6 flex flex-col']//button");
        public readonly By _reserveHeader = By.XPath("//h2[@class='text-gray-700 text-2xl font-semibold leading-loose hidden md:block']");
        public readonly By _roomPrice = By.XPath("//div[@class='whitespace-no-wrap text-sm text-gray-500']//span");
        public readonly By _reserveButton = By.XPath("//div[@class='mt-4 sm:ml-16 sm:mt-0 sm:flex-none space-x-4']//button");

        #endregion

        public VivifyPage(IWebDriver driver, BasePage basePage)
        {
            _driver = driver;
            _basePage = basePage;
        }

        public void SelectDateFromCalendar(int day, string monthInFull, int year)
        {
            _basePage.WaitUntilElementIsVisibleExplicitWait(_monthCalendarHeader);
            _basePage.ScrollToView(_monthCalendarHeader);
            _basePage.ClickOn(_calendarNextMonth);
            string monthHeader = _basePage.GetText(_monthCalendarHeader);
            if(monthHeader.Contains(monthInFull + " " + year))
            {
                By date = By.XPath($"//a[@aria-label='{monthInFull} {day}, {year}']");
                _basePage.ClickOn(date);
            }
        }

        public void SelectOneHourSlot()
        {
            string halfHourSlot = "(//td[@class='relative sm:w-12 sm:px-6 px-7'])";

            _basePage.ClickOn(By.XPath(halfHourSlot + "[1]"));
            _basePage.ClickOn(By.XPath(halfHourSlot + "[2]"));
        }

        public void CheckAvailability()
        {
            _basePage.ClickOn(_checkAvailabilityButton);
            _basePage.AreStringsEqual(_basePage.GetText(_reserveHeader), TextResource.ReserveYourBookingHeader);
        }

        public void CheckRoomPrice(string roomPrice)
        {
            Assert.IsTrue(_basePage.AreStringsEqual(_basePage.GetText(_roomPrice), roomPrice));
        }

        public void ReserveSlots()
        {
            _basePage.ClickOn(_reserveButton);
        }
    }
}

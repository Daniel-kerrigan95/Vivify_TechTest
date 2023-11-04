using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vivify_TechTest.Configuration.Constants;
using Vivify_TechTest.Pages;

namespace Vivify_TechTest.Step_Definitions
{
    [Binding]
    public class StepDefinitions
    {
        private readonly BasePage _basePage;
        private readonly VivifyPage _vivifyPage;
        private readonly FeatureContext _featureContext;

        public StepDefinitions(BasePage basePage, VivifyPage vivifyPage, FeatureContext featureContext)
        {
            _basePage = basePage;
            _vivifyPage = vivifyPage;
            _featureContext = featureContext;
        }

        [Given(@"The user has navigated to the webpage")]
        public void GivenTheUserHasNavigatedToTheWebpage()
        {
            _basePage._driver.Navigate().GoToUrl(_featureContext.Get<string>(FeatureContextKeys.BaseUrl));
            _basePage.WaitForTimeOut();
            using (new AssertionScope())
            {
                string header = _basePage.GetText(_vivifyPage._header);
                Assert.IsTrue(_basePage.AreStringsEqual(header, TextResource.GetTogetherHeader));
            }
        }

        [Given(@"The user searches their Post Code (.*)")]
        public void GivenTheUserSearchesTheirPostCodeLAAB(string PostCode)
        {
            _basePage.EnterText(_vivifyPage._postcodeField, PostCode);
            _basePage.ClickOn(_vivifyPage._findVenueButton);
            int resultsList = _basePage._driver.FindElements(By.XPath("(//ul[@id='results'])//li[contains(@class, 'h-full bg-white shadow relative overflow-hidden rounded-lg')]")).Count();
            Assert.IsTrue(resultsList > 0);
        }

        [Given(@"The user has selected Lancaster Royal Grammar School")]
        public void GivenTheUserHasSelectedLancasterRoyalGrammarSchool()
        {
            _basePage.ClickOn(_vivifyPage._lancasterClassRoom);
            using (new AssertionScope())
            {
                //Checks Venue Name 
                Assert.IsTrue(_basePage.AreStringsEqual(_basePage.GetText(_vivifyPage._venueNameHeader), TextResource.LancasterRoyalVenueName));
                //Checks Venue Type 
                Assert.IsTrue(_basePage.AreStringsEqual(_basePage.GetText(_vivifyPage._venueTypeHeader), TextResource.ClassroomHeader));
            }
        }

        [When(@"The user books the venue for one hour on Christmas Day")]
        public void WhenTheUserBooksTheVenueForOneHourOnChristmasDay()
        {
            _vivifyPage.SelectDateFromCalendar(25, "December", 2023);
            _vivifyPage.SelectOneHourSlot();
            _vivifyPage.CheckAvailability();
        }


        [Then(@"the booking price is correct")]
        public void WhenTheBookingPriceIsCorrect()
        {
            _vivifyPage.CheckRoomPrice(TextResource.ClassroomPrice);
        }

    }
}

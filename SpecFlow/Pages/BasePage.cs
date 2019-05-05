using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Helpers;
using TechTalk.SpecFlow;

namespace SpecFlow.Pages
{
    public class BasePage
    {
        public IWebDriver Driver;
        public WebDriverWait Wait;
        public ElementActions ElementActions;
        public LocatorBuilder LocatorBuilder;
        public AssertHelper AssertHelper;

        [FindsBy(How = How.CssSelector, Using = "html[lang]")]
        public IWebElement LanguageElement;

        public BasePage()
        {
            Driver = (IWebDriver)ScenarioContext.Current["Driver"];
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            ElementActions = new ElementActions(new Actions(Driver));
            LocatorBuilder = new LocatorBuilder();
            AssertHelper = new AssertHelper();
        }

        public void PageHasLanguage(string ExpectedLanguage)
        {
            AssertHelper.TwoStringsMatch(ExpectedLanguage, LanguageElement.GetAttribute("lang"));
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }
    }
}

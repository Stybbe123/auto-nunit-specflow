using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SpecFlow.Pages
{
    public class HeaderCookies : BasePage
    {
        //private IWebDriver Driver;

        [FindsBy(How = How.CssSelector, Using = "div.cookiewarning")]
        private IWebElement HeaderCookieBlock;
        [FindsBy(How = How.CssSelector, Using = "div.cookiewarning a.allow_tracking")]
        private IWebElement AcceptButton;

        public HeaderCookies()
        {
          //  this.Driver = (IWebDriver)ScenarioContext.Current["Driver"];
            PageFactory.InitElements(Driver, this);
        }

        public void AcceptCookies()
        {
            if (HeaderCookieBlock.Displayed)
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(AcceptButton)).Click();
            }
        }
            
    }
}

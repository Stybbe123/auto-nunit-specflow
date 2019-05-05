using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlow.Pages
{
    class TopMenu : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "div#header a[href*=par-mums")]
        private IWebElement ParMumsLink;
        [FindsBy(How = How.CssSelector, Using = "div#header a[accesskey]")]
        private IWebElement MainPageLink;

        public TopMenu()
        {   
            PageFactory.InitElements(Driver, this);
        }

        public void OpenParMumsPage()
        {
            ParMumsLink.Click();
        }

        public void OpenMainPage()
        {
            MainPageLink.Click();
        }

    }
}

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlow.Pages
{
    public class VismaHomePage : BasePage
    {
        public HeaderCookies HeaderCookies = new HeaderCookies();
        public BlogTeaserModule BlogTeaser = new BlogTeaserModule();
        public SocialLinksModule SocialLinksModule = new SocialLinksModule();
        public Footer Footer = new Footer();

        private const String PageTitle = "VISMA - Efektivitātes Eksperti - Visma";
        private const String PageUrl = "https://www.visma.lv/";

        [FindsBy(How = How.CssSelector, Using = "div#frontpagebanner a.cta")]
        private IWebElement PresentationFormLink;
        [FindsBy(How = How.CssSelector, Using = "ul.newsticker")]
        private IWebElement BlogElement;

        public VismaHomePage()
        {
            PageFactory.InitElements(Driver, this);
        }
        

        private String GetPageTitle()
        {
            return Driver.Title;
        }

        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }

        public void CheckTitle()
        {
            AssertHelper.TwoStringsMatch(PageTitle, GetPageTitle());
        }

        public void OpenRequestPresentationForm()
        {
            PresentationFormLink.Click();
        }

        public void ScrollToBlogElement()
        {   
            ElementActions.ScrollToElement(BlogElement);
        }

    }
}

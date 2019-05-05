using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ClassLibrary1.Pages
{
    public class VismaHomePage
    {
        private readonly IWebDriver Driver;
        private const String PageTitle = "Business Software and Services - Visma";
        private const String PageUrl = "https://www.visma.com/";

        public VismaHomePage(IWebDriver Driver)
        {
            this.Driver = Driver;
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
            Assert.AreEqual(PageTitle, GetPageTitle());
        }
    }
}

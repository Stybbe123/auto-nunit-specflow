using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace SpecFlow.Hooks
{
    public enum BrowserType
    {
        Chrome,
        Firefox
    }

    [Binding]
    public class Hooks : Base.Base
    {

        private BrowserType browserType;

        [BeforeScenario("web")]
        public void InicializeTest()
        {
            String browserTypeParam = TestContext.Parameters.Get("BrowserName", "Chrome");

            Console.WriteLine("Passed browser name : " + browserTypeParam);

            try
            {
                Enum.IsDefined(typeof(BrowserType), browserTypeParam);
                browserType = (BrowserType)Enum.Parse(typeof(BrowserType), browserTypeParam);
            } catch(Exception)
            {
                throw new Exception("browser type not supported: " + browserTypeParam);
            }

            ChooseDriverInstance(browserType);
        }

        [AfterScenario("web")]
        public void CloseInstance()
        {
            Driver.Close();
            Driver.Quit();
        }

        private void ChooseDriverInstance(BrowserType browserType)
        {
            if (browserType == BrowserType.Chrome)
                Driver = new ChromeDriver();
            else if (browserType == BrowserType.Firefox)
                Driver = new FirefoxDriver();

            ScenarioContext.Current["Driver"] = Driver;
        }
    }
}

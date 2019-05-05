using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SpecFlow.Pages
{

    public class Footer : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "div#footerwrapper select.countries")]
        private IWebElement CountrySelect;

        public Footer()
        {
            PageFactory.InitElements(Driver, this);
        }

        public void CountrySelectOption(string Option)
        {
            SelectElement Select = new SelectElement(CountrySelect);
            Select.SelectByText(Option);
        }

        public void ScrollToCountrySelect()
        {
            ElementActions.ScrollToElement(CountrySelect);
        }
        
    }
}

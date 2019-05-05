using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SpecFlow.Helpers
{
    public class ElementActions
    {
        private Actions Actions;
        public ElementActions(Actions Actions)
        {
            this.Actions = Actions;
        }

        public void ScrollToElement(IWebElement WebElement)
        {
            Actions.MoveToElement(WebElement);
            Actions.Perform();
        }
    }
}

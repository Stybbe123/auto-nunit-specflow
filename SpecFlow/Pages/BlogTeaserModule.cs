using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Models;
using SpecFlow.Helpers;

namespace SpecFlow.Pages
{
    public class BlogTeaserModule : BasePage
    {
        private By BlogElementsLocator = By.CssSelector("ul.newsticker a");
        private string NBlogElementLocator = "ul.newsticker li:nth-of-type(%s)";
        private List<PageLinkModel> BlogRecords = new List<PageLinkModel>();

        private PageLinkModelHelper Helper = new PageLinkModelHelper();

        public BlogTeaserModule()
        {
        }

        public void LoadBlogTeaserElements()
        {
            foreach (IWebElement BlogElement in Driver.FindElements(BlogElementsLocator))
            {
                PageLinkModel PageLinkModel = new PageLinkModel();

                PageLinkModel.PageLinkUrl = BlogElement.GetAttribute("href");
                PageLinkModel.PageLinkTitle = BlogElement.GetAttribute("Title");
                PageLinkModel.PageLinkVisited = false;

                BlogRecords.Add(PageLinkModel);
            }
        }

        public void OpenBlogTeaserElements()
        {   
            for (int i = 1; i <= BlogRecords.Count; i++)
            {
                if (!Helper.ModelIsVisited(BlogRecords))
                {
                    string BlogElementLocator = LocatorBuilder.BuildLocator(NBlogElementLocator, i.ToString());
                    IWebElement BlogToClick = Driver.FindElement(By.CssSelector(BlogElementLocator));
                    Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(BlogElementLocator)));

                    if (BlogToClick.Displayed && !BlogElementIsVisited(BlogToClick))
                    {   
                        Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(BlogElementLocator)));
                        BlogToClick.Click();
                        UpdateModel();
                    }
                }
            }

            Assert.IsTrue(Helper.ModelIsVisited(BlogRecords));
        }

        private bool BlogElementIsVisited(IWebElement BlogRecord)
        {
            bool Result = false;

            foreach (PageLinkModel PageLink in BlogRecords)
            {
                if (!PageLink.PageLinkVisited && PageLink.PageLinkUrl.Equals(BlogRecord.GetAttribute("Title")))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        private void UpdateModel()
        {
            bool result = false;
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);

            for (int i = 0; i <= BlogRecords.Count - 1; i++)
            {
                if (BlogRecords.ElementAt<PageLinkModel>(i).PageLinkUrl.Equals(Driver.Url)
                    && BlogRecords.ElementAt<PageLinkModel>(i).PageLinkVisited == false) {

                    BlogRecords.ElementAt<PageLinkModel>(i).PageLinkVisited = true;
                    result = true;

                    break;
                }
            }

            Assert.IsTrue(result, "Model update was called, but no record was updated");
            
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
        }
    }
}

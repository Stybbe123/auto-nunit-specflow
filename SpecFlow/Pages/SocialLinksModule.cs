using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlow.Helpers;
using SpecFlow.Models;

namespace SpecFlow.Pages
{
    public class SocialLinksModule : BasePage
    {

        private By SocialLinkElementsLocator = By.CssSelector("div#footerwrapper div.col-3:nth-of-type(1) a");
        private List<PageLinkModel> SocialLinks = new List<PageLinkModel>();
        private PageLinkModelHelper Helper = new PageLinkModelHelper();

        public SocialLinksModule()
        {
        }

        public void LoadSocialLinkElements()
        {
            foreach (IWebElement BlogElement in Driver.FindElements(SocialLinkElementsLocator))
            {
                PageLinkModel PageLinkModel = new PageLinkModel
                {
                    PageLinkUrl = BlogElement.GetAttribute("href"),
                    PageLinkTitle = BlogElement.Text,
                    PageLinkVisited = false
                };

                SocialLinks.Add(PageLinkModel);
            }
        }

        public void OpenSocialLinkElements()
        {
            IList<IWebElement> SocialLinkElements = Driver.FindElements(SocialLinkElementsLocator);
            for (int i = 0; i < SocialLinks.Count; i++)
            {
                if (!Helper.ModelIsVisited(SocialLinks))
                {   
                    IWebElement SocialLinkToClick = SocialLinkElements.ElementAt<IWebElement>(i);
                     
                    if (SocialLinkToClick.Displayed && !SocialLinkIsVisited(SocialLinkToClick))
                    {
                        SocialLinkToClick.Click();
                        UpdateModel();
                    }
                }
            }

            Assert.IsTrue(Helper.ModelIsVisited(SocialLinks));
        }

        private bool SocialLinkIsVisited(IWebElement SocialLink)
        {
            bool Result = true;

            foreach (PageLinkModel PageLink in SocialLinks)
            {
                if (!PageLink.PageLinkVisited && PageLink.PageLinkUrl.Equals(SocialLink.GetAttribute("href")))
                {
                    Result = false;
                    break;
                }
            }

            return Result;
        }

        private void UpdateModel()
        {
            bool result = false;
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);

            for (int i = 0; i <= SocialLinks.Count - 1; i++)
            {
                if (SocialLinks.ElementAt<PageLinkModel>(i).PageLinkUrl.Equals(Driver.Url)
                    && SocialLinks.ElementAt<PageLinkModel>(i).PageLinkVisited == false)
                {

                    SocialLinks.ElementAt<PageLinkModel>(i).PageLinkVisited = true;
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

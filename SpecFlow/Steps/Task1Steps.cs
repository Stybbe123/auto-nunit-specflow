using SpecFlow.Models;
using SpecFlow.Pages;
using TechTalk.SpecFlow;

namespace SpecFlow.Steps
{
    [Binding]
    public class Task1Steps
    {
        readonly TopMenu TopMenu = new TopMenu();
        readonly VismaHomePage VismaHomePage = new VismaHomePage();
        readonly PresentationRequestPage PresentationRequestPage = new PresentationRequestPage();
        readonly Footer Footer = new Footer();
        
        [Given(@"I can open visma home page")]
        public void GivenICanOpenVismaHomePage()
        {
            VismaHomePage.OpenHomePage();
        }

        [When(@"home page title check")]
        public void HomePageTitleCheck()
        {
            VismaHomePage.CheckTitle();
        }

        [Then(@"accept cookies")]
        public void ThenAcceptCookies()
        {
            VismaHomePage.HeaderCookies.AcceptCookies();
        }

        [Then(@"open presentation request form")]
        public void ThenOpenPresentationRequestForm()
        {
            VismaHomePage.OpenRequestPresentationForm();
        }


        [Then(@"click form submit button")]
        public void ThenClickFormSubmitButton()
        {
            PresentationRequestPage.SubmitForm();
        }
        
        [Then(@"I fill in the following form")]
        public void ThenTestPresentationFormRequiredFields(Table table)
        {
            string formModelJson = table.Rows[0]["formmodel"];
            PresentationFormModel PresentationFormModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PresentationFormModel>(formModelJson);
            PresentationRequestPage.FillPresentationRequestForm(PresentationFormModel);
        }

        [Then(@"(.*) X errors are present")]
        public void ThenErrorsArePresent(string ExpectedErrorCount)
        {   
            PresentationRequestPage.ExpectedErrorCountMatch(int.Parse(ExpectedErrorCount));
        }


        [Then(@"clear presentation request form")]
        public void ThenClearPresentationRequestForm()
        {
            PresentationRequestPage.ClearForm();
        }

        [Then(@"go to main page")]
        public void ThenGoToMainPage()
        {
            TopMenu.OpenMainPage();
        }

        [Then(@"scroll blog element into view")]
        public void ThenScrollBlogElementIntoView()
        {
            VismaHomePage.ScrollToBlogElement();
        }

        [Then(@"scroll language select element into view")]
        public void ThenScrollLanguageSelectElementIntoView()
        {
            Footer.ScrollToCountrySelect();
        }

        [Then(@"footer country select (.*)")]
        public void ThenFooterCountrySelect(string OptionText)
        {
            Footer.CountrySelectOption(OptionText);
        }

        [Then(@"opened page has (.*) and url contains (.*)")]
        public void ThenOpenedPageHasLanguageAndUrlContainsDomain(string Language, string Domain)
        {
            VismaHomePage.PageHasLanguage(Language);
            VismaHomePage.AssertHelper.StringContainPart(Domain, VismaHomePage.GetCurrentUrl());
        }

        [Then(@"build blog teaser model")]
        public void ThenBuildBlogElemntModel()
        {
            VismaHomePage.BlogTeaser.LoadBlogTeaserElements();
        }

        [Then(@"open every blog record")]
        public void ThenOpenEveryBlogRecords()
        {
            VismaHomePage.BlogTeaser.OpenBlogTeaserElements();
        }

        [Then(@"scroll social link elements into view")]
        public void ThenScrollSocialLinkElementsIntoView()
        {
            
        }

        [Then(@"build social links model")]
        public void ThenBuildSocialLinksModel()
        {
            VismaHomePage.SocialLinksModule.LoadSocialLinkElements();
        }

        [Then(@"visit every social link record")]
        public void ThenVisitEverySocialLinkRecord()
        {
            VismaHomePage.SocialLinksModule.OpenSocialLinkElements();
        }

        [Then(@"(.*) email should not be accepted")]
        public void ThenInvalidEmailShouldNotBeAccepted(string InvalidEmail)
        {
            PresentationRequestPage.TestEmailField(InvalidEmail);
        }



    }

}


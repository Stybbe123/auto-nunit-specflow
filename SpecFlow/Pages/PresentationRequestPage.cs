using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Models;
using TechTalk.SpecFlow;

namespace SpecFlow.Pages
{
    class PresentationRequestPage : BasePage
    {   
        [FindsBy(How = How.CssSelector, Using = "section#__field_ div[data-eloqua-html-field=firstName] > input")]
        private IWebElement NameField;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ div[data-eloqua-html-field=lastName] > input")]
        private IWebElement SurnameField;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ div[data-eloqua-html-field=company] > input")]
        private IWebElement OrganizationField;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ div[data-eloqua-html-field=busPhone] > input")]
        private IWebElement PhoneField;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ div[data-eloqua-html-field=emailAddress] > input")]
        private IWebElement EmailField;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ input[id$=_profiling]")]
        private IWebElement GDPRCheckbox;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ input[id$=_marketing]")]
        private IWebElement SubscribeCheckbox;
        [FindsBy(How = How.CssSelector, Using = "section#__field_ button.FormSubmitButton")]
        private IWebElement FormSubmitButton;
                        
        private By TextErrorLocator = By.CssSelector("div[data-eloqua-html-field] span.Form__Element__ValidationError");
        private By GDPRCheckboxErrorLocator = By.CssSelector("div[data-autofill-key=C_Privacy_Consent1] span.Form__Element__ValidationError");

        public PresentationRequestPage()
        {
            PageFactory.InitElements(Driver, this);
        }

        public void FillPresentationRequestForm(PresentationFormModel model)
        {
            FormLoaded();

            SendKeysToElementFromModel(NameField, model.Name);
            SendKeysToElementFromModel(SurnameField, model.Surname);
            SendKeysToElementFromModel(OrganizationField, model.Company);
            SendKeysToElementFromModel(PhoneField, model.Phone);
            SendKeysToElementFromModel(EmailField, model.Email);
            CheckboxChecked(GDPRCheckbox, model.GDPR);
        }

        private void SendKeysToElementFromModel(IWebElement Element, string value)
        {
            if (value != null)
            {
                Element.Click();
                Element.SendKeys(value);
            }
        }

        public void CheckGDPR(bool Flag)
        {
            CheckboxChecked(GDPRCheckbox, Flag);
        }

        /// <summary>
        /// here are handled 3 cases of GDPR checkbox state
        /// case 0: checkbox is checked from the start, Flag3 is responsible
        /// case 1: it's already checked, Flag2 is responsible
        /// case 2: no need to check it, Flag is responsible
        /// 
        /// to overkill it, i'll assert that Flag = true meaning checkbox is checked 
        /// Assert.IsTrue(Element.GetAttribute("data-consent-accepted-by-click") == "true" ? true : false);
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="Flag"></param>
        private void CheckboxChecked(IWebElement Element, bool Flag)
        {
            bool Flag2 = Element.GetAttribute("data-consent-accepted-by-click") == "true" ? true : false;
            bool Flag3 = Element.GetAttribute("data-start-as-checked") == "false";

            if (Flag && !Flag2 && Flag3)
            {
                Element.Click();
                Assert.IsTrue(Element.GetAttribute("data-consent-accepted-by-click") == "true" ? true : false);
                return;
            }

            Flag2 = Element.GetAttribute("data-consent-accepted-by-click") == "true" ? true : false;

            if (Flag && !Flag2)
            {
                Element.Click();
                Assert.IsTrue(Element.GetAttribute("data-consent-accepted-by-click") == "true" ? true : false);
                return;
            }

            if (Flag)
            {
                Assert.IsTrue(Element.GetAttribute("data-consent-accepted-by-click") == "true" ? true : false);
            }
        }

        private void CheckboxUnchecked(IWebElement Element)
        {
            if (Element.GetAttribute("data-consent-accepted-by-click") == "true")
            {
                Element.Click();
            }
        }

        public void SubmitForm()
        {
            FormSubmitButton.Click();
        }

        private void FormLoaded()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(NameField)).Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(SurnameField)).Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(OrganizationField)).Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(PhoneField)).Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(EmailField)).Click();
        }

        public void ClearForm()
        {
            NameField.Clear();
            SurnameField.Clear();
            OrganizationField.Clear();
            PhoneField.Clear();
            EmailField.Clear();

            CheckboxUnchecked(GDPRCheckbox);
            CheckboxUnchecked(SubscribeCheckbox);
        }

        public int GetErrorCount()
        {
            IReadOnlyCollection<IWebElement> FieldErrors = Driver.FindElements(TextErrorLocator);
            IReadOnlyCollection<IWebElement> CheckBoxErrors = Driver.FindElements(GDPRCheckboxErrorLocator);

            return CountVisibleElements(FieldErrors) + CountVisibleElements(CheckBoxErrors);
        }

        private int CountVisibleElements(IReadOnlyCollection<IWebElement> Elements)
        {
            int Result = 0;

            foreach (IWebElement Element in Elements)
            {
                if (Element.Displayed)
                {
                    Result++;
                }
            }

            return Result;
        }

        public void ExpectedErrorCountMatch(int Expected)
        {
            Assert.AreEqual(Expected, GetErrorCount());
        }

        public void TestEmailField(string InvalidEmail)
        {
            ClearForm();
            FillPresentationRequestForm(new PresentationFormModel()
                {
                    Email = InvalidEmail
                });
            SubmitForm();
            ExpectedErrorCountMatch(6);
        }

    }
}

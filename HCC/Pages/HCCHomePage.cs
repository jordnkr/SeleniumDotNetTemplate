using OpenQA.Selenium;
using SeleniumDotNetTemplate.Shared;
using SeleniumDotNetTemplate.Shared.Enums;
using System;

namespace SeleniumDotNetTemplate.HCC.Pages
{
    public class HCCHomePage : HCCBasePage
    {
        #region elements

        private static readonly By CashToCloseButton = By.XPath("//a[@href='./cashToCloseCalculator.html']");

        #endregion

        public HCCHomePage(IWebDriver driver) : base(driver)
        {
            Driver.WaitForElement(CashToCloseButton);
        }

        public HCCHomePage(IWebDriver driver, TestEnvironment testEnvironment) : base(driver)
        {
            switch (testEnvironment)
            {
                case TestEnvironment.PROD:
                    Driver.Navigate().GoToUrl("https://jordnkr.github.io/homeCostCalculator/index.html");
                    break;
                default:
                    throw new Exception("invalid environment in App.config");
            }
        }

        public CashToClosePage ClickCashToCloseButton()
        {
            Driver.WaitForElement(CashToCloseButton).Click();
            return new CashToClosePage(Driver);
        }
    }
}

using OpenQA.Selenium;
using SeleniumDotNetTemplate.Shared;

namespace SeleniumDotNetTemplate.HCC.Pages
{
    public class CashToClosePage : HCCBasePage
    {
        #region elements

        private static readonly By CashToCloseFinalNumberText = By.Id("cashToClose");

        #endregion

        public CashToClosePage(IWebDriver driver) : base(driver)
        {
            Driver.WaitForElement(CashToCloseFinalNumberText);
        }

        public bool IsTotalCashToCloseTextDisplayed()
        {
            return Driver.IsElementDisplayed(CashToCloseFinalNumberText);
        }
    }
}

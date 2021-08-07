using OpenQA.Selenium;
using SeleniumDotNetTemplate.Shared;

namespace SeleniumDotNetTemplate.HCC.Pages
{
    public abstract class HCCBasePage : BasePage
    {
        #region elements

        private static readonly By Logo = By.XPath("//a[@class='title']/b[text()='Home Cost Calculators']");

        #endregion

        protected HCCBasePage(IWebDriver driver) : base(driver)
        { }

        public HCCHomePage ClickLogo()
        {
            Driver.WaitForElement(Logo).Click();
            return new HCCHomePage(Driver);
        }
    }
}

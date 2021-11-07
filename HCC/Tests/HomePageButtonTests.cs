using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumDotNetTemplate.HCC.Pages;
using SeleniumDotNetTemplate.Shared;

namespace SeleniumDotNetTemplate.HCC.Tests
{
    [TestClass]
    public class HomePageButtonTests : BaseTest
    {
        /// <summary>
        /// Verifies that the 'Cash to Close' button navigates to the correct page
        /// </summary>
        [TestMethod]
        public void CashToCloseButton()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            CashToClosePage cashToClosePage = hccHomePage.ClickCashToCloseButton();
            Assert.IsTrue(cashToClosePage.IsTotalCashToCloseTextDisplayed());
        }

        [TestMethod]
        public void CashToClosePageLighthouse()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            CashToClosePage cashToClosePage = hccHomePage.ClickCashToCloseButton();
            LighthouseAudit audit = cashToClosePage.RunLighthouse(Driver.Url);
            Assert.IsTrue(audit.Accessibility >= 0.9, "Expected: 1, Got: " + audit.Accessibility);
            Assert.IsTrue(audit.BestPractices >= 0.9, "Expected: 1, Got: " + audit.Accessibility);
            Assert.IsTrue(audit.Performance >= 0.9, "Expected: 1, Got: " + audit.Accessibility);
        }
    }
}

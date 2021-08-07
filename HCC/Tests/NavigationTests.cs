using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumDotNetTemplate.HCC.Pages;
using SeleniumDotNetTemplate.Shared;

namespace SeleniumDotNetTemplate.HCC.Tests
{
    [TestClass]
    public class NavigationTests : BaseTest
    {
        [TestMethod]
        public void HomePageCashToCloseButton()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            CashToClosePage cashToClosePage = hccHomePage.ClickCashToCloseButton();
            Assert.IsTrue(cashToClosePage.IsTotalCashToCloseTextDisplayed());
        }
    }
}

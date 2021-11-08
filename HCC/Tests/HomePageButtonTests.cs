using FluentAssertions;
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
            cashToClosePage.IsTotalCashToCloseTextDisplayed().Should().BeTrue();
            //Assert.IsTrue(cashToClosePage.IsTotalCashToCloseTextDisplayed());
        }
    }
}

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Axe;
using SeleniumDotNetTemplate.HCC.Pages;
using SeleniumDotNetTemplate.Shared;

namespace SeleniumDotNetTemplate.HCC.Tests
{
    [TestClass]
    public class CashToCloseAuditTests : BaseTest
    {
        [TestMethod]
        public void CashToCloseLighthouse()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            CashToClosePage cashToClosePage = hccHomePage.ClickCashToCloseButton();
            LighthouseAudit audit = cashToClosePage.RunLighthouse(Driver.Url);
            audit.Accessibility.Should().BeGreaterThan(0.9);
            audit.BestPractices.Should().BeGreaterThan(0.9);
            audit.Performance.Should().BeGreaterThan(0.9);
            //Assert.IsTrue(audit.Accessibility >= 0.9, "Accessibility failed. Expected: 1, Got: " + audit.Accessibility);
            //Assert.IsTrue(audit.BestPractices >= 0.9, "Best Practices failed. Expected: 1, Got: " + audit.Accessibility);
            //Assert.IsTrue(audit.Performance >= 0.9, "Performance failed. Expected: 1, Got: " + audit.Accessibility);
        }

        [TestMethod]
        public void CashToCloseAxeAccessibility()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            CashToClosePage cashToClosePage = hccHomePage.ClickCashToCloseButton();
            AxeResult axeResult = cashToClosePage.RunAxe();
            axeResult.Violations.Length.Should().Be(0);
            //Assert.IsTrue(axeResult.Violations.Length == 0, axeResult.Violations.Length + " accessibility violations were found");
        }
    }
}

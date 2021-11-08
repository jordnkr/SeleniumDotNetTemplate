using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Axe;
using SeleniumDotNetTemplate.HCC.Pages;
using SeleniumDotNetTemplate.Shared;

namespace SeleniumDotNetTemplate.HCC.Tests
{
    [TestClass]
    public class HomePageAuditTests : BaseTest
    {
        [TestMethod]
        public void HomePageLighthouse()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            LighthouseAudit audit = hccHomePage.RunLighthouse(Driver.Url);
            Assert.IsTrue(audit.Accessibility >= 0.9, "Accessibility failed. Expected: 1, Got: " + audit.Accessibility);
            Assert.IsTrue(audit.BestPractices >= 0.9, "Best Practices failed. Expected: 1, Got: " + audit.Accessibility);
            Assert.IsTrue(audit.Performance >= 0.9, "Performance failed. Expected: 1, Got: " + audit.Accessibility);
        }

        [TestMethod]
        public void HomePageAxeAccessibility()
        {
            HCCHomePage hccHomePage = new HCCHomePage(Driver, TestEnvironment);
            AxeResult axeResult = hccHomePage.RunAxe();
            Assert.IsTrue(axeResult.Error == null, "Errors were found:\n" + axeResult.ToString());
        }
    }
}

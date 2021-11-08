using OpenQA.Selenium;
using Newtonsoft.Json;
using System;
using Selenium.Axe;

namespace SeleniumDotNetTemplate.Shared
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Runs Lighthouse via the command line
        /// </summary>
        /// <param name="url">url to target with lighthouse (will open in same browser window, but a new tab)</param>
        /// <param name="port">port the existing test browser is running on. 4444 is Selenium default.</param>
        /// <returns>LighthouseAudit object with Lighthouse scores.</returns>
        public LighthouseAudit RunLighthouse(string url, int port = 4444) // Selenium defaults to 4444. Send in custom number if needed
        {
            // if need to pass auth token (after --output): --extra-headers {"Cookie":""}
            string output = CommandLineUtil.RunCommand("/c lighthouse " + url + " --output=json --form-factor=desktop --preset=desktop --port=" + port);
            string fixedOutput = output.Replace("best-practices", "bestpractices"); // hyphen not easy to deserialize. Replace.
            dynamic jsonObj = JsonConvert.DeserializeObject(fixedOutput);

            LighthouseAudit audit = new LighthouseAudit();
            audit.Accessibility = Convert.ToDouble(jsonObj.categories.accessibility.score.ToString());
            audit.BestPractices = Convert.ToDouble(jsonObj.categories.bestpractices.score.ToString());
            audit.Performance = Convert.ToDouble(jsonObj.categories.performance.score.ToString());
            audit.Seo = Convert.ToDouble(jsonObj.categories.seo.score.ToString());

            return audit;
        }

        /// <summary>
        /// Runs the axe-core accessibility tests agaist the running WebDriver instance
        /// </summary>
        /// <returns>AxeResult object with test results</returns>
        public AxeResult RunAxe()
        {
            return new AxeBuilder(Driver).Analyze();
        }
    }
}

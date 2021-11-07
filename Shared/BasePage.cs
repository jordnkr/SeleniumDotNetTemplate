using OpenQA.Selenium;
using System.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SeleniumDotNetTemplate.Shared
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected LighthouseAudit lighthouseAudit;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

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
    }
}

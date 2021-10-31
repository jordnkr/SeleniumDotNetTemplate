using OpenQA.Selenium;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

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

        public LighthouseAudit RunLighthouse()
        {
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe", "");

            // Configure: no displayed terminal. allow output to be captured.
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;

            Process process = Process.Start(info);
            string output = "";
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => output += e.Data;
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDotNetTemplate.Shared.Enums;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace SeleniumDotNetTemplate.Shared
{
    [TestClass]
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        private string DriverPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        Browser Browser = (Browser)Enum.Parse(typeof(Browser), ConfigurationManager.AppSettings["browser"]);
        protected TestEnvironment TestEnvironment = (TestEnvironment)Enum.Parse(typeof(TestEnvironment), ConfigurationManager.AppSettings["environment"]);

        protected BaseTest()
        { }

        [TestInitialize]
        public void BaseTestInitialize()
        {
            SetupDriver();
            Cursor.Position = new Point(0, 0); // Move cursor to corner to avoid it interfering with tests.
        }

        [TestCleanup]
        public void BaseTestCleanup()
        {
            // TODO: Add screenshot taker later
            Driver.Quit();
        }

        /// <summary>
        /// Setup the IWebDriver object
        /// </summary>
        public void SetupDriver()
        {
            switch (Browser)
            {
                case Browser.Chrome:
                    var chromeOptions = new ChromeOptions();
                    var service = ChromeDriverService.CreateDefaultService(DriverPath);
                    chromeOptions.AddArgument("--start-maximized");

                    // these 2 lines are needed for Lighthouse to use the existing browser window (otherwise opens it's own). Can comment these out if Lighthouse isn't going to be used.
                    chromeOptions.AddArgument("--remote-debugging-address=0.0.0.0");
                    chromeOptions.AddArgument($"--remote-debugging-port=4444");

                    Driver = new ChromeDriver(service, chromeOptions);
                    break;
                default:
                    throw new Exception("Unsupported browser value in App.config");
            }
        }
    }
}

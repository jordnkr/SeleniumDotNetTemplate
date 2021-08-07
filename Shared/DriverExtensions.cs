using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumDotNetTemplate.Shared
{
    public static class DriverExtensions
    {
        /// <summary>
        /// Waits for an element to be displayed on the page before interacting with it
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <param name="timeoutSeconds">amount of time to wait for the element to be displayed. Defaults to 10 secounds</param>
        /// <returns>the element, if and when it's found</returns>
        public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            if (timeoutSeconds > 0)
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(d => d.FindElement(by).Displayed);
            }
            return driver.FindElement(by);
        }

        public static void WaitForElementNotDisplayed(this IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            if (timeoutSeconds > 0)
            {
                if (!driver.FindElement(by).Displayed) return; // return immediately if not displayed
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(d => d.FindElement(by).Displayed == false);
            }
        }

        public static void WaitForElementToBeStale(this IWebDriver driver, IWebElement element, int timeoutSeconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
        }

        public static void ClickElementAndWaitToBeStale(this IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            var element = driver.FindElement(by);
            element.Click();
            WaitForElementToBeStale(driver, element, timeoutSeconds);
        }

        public static void WaitForWindowHandle(this IWebDriver driver, int expectedWindowHandles = 2, int timeoutSeconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(d => d.WindowHandles.Count == expectedWindowHandles);
        }

        public static void SetTextField(this IWebDriver driver, By by, string textValue)
        {
            driver.WaitForElement(by).Clear();
            driver.FindElement(by).SendKeys(textValue);
        }

        public static void SetCheckBox(this IWebDriver driver, By by, bool check)
        {
            var element = driver.FindElement(by);

            if ((check && !element.Selected) || (!check && element.Selected))
            {
                element.Click();
            }
        }

        public static SelectElement GetSelectElement(this IWebDriver driver, By by)
        {
            return new SelectElement(driver.WaitForElement(by));
        }

        public static bool IsElementDisplayed(this IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            try
            {
                WaitForElement(driver, by, timeoutSeconds);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

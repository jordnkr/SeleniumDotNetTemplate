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

        /// <summary>
        /// Waits for an element to not be displayed
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <param name="timeoutSeconds">amount of time to wait for the element to disappear</param>
        public static void WaitForElementNotDisplayed(this IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            if (timeoutSeconds > 0)
            {
                if (!driver.FindElement(by).Displayed) return; // return immediately if not displayed
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(d => d.FindElement(by).Displayed == false);
            }
        }

        /// <summary>
        /// Waits for an element to become stale
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="element">element locator</param>
        /// <param name="timeoutSeconds">amount of time to wait for the element to become stale</param>
        public static void WaitForElementToBeStale(this IWebDriver driver, IWebElement element, int timeoutSeconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
        }

        /// <summary>
        /// Clicks an element and waits for it to become stale. Typically used when clicking an item refreshes the same page without navigating away
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <param name="timeoutSeconds">amount of time to wait for the element to become stale</param>
        public static void ClickElementAndWaitToBeStale(this IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            var element = driver.FindElement(by);
            element.Click();
            WaitForElementToBeStale(driver, element, timeoutSeconds);
        }

        /// <summary>
        /// Waits for a specified amount of window handles to be open
        /// </summary>
        /// <param name="driver">IWebDriverObject</param>
        /// <param name="expectedWindowHandles">amount of window handles to wait for</param>
        /// <param name="timeoutSeconds">amount of time to wait for the window handles to be open</param>
        public static void WaitForWindowHandles(this IWebDriver driver, int expectedWindowHandles = 2, int timeoutSeconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(d => d.WindowHandles.Count == expectedWindowHandles);
        }

        /// <summary>
        /// Sets the value of an input text field. Clears the field before entering the specified text
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <param name="textValue">value to enter into the text field</param>
        public static void SetTextField(this IWebDriver driver, By by, string textValue)
        {
            driver.WaitForElement(by).Clear();
            driver.FindElement(by).SendKeys(textValue);
        }

        /// <summary>
        /// Sets the 'selected' state of a check box
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <param name="check">true if the check box should be checked, false if it should be unchecked</param>
        public static void SetCheckBox(this IWebDriver driver, By by, bool check)
        {
            var element = driver.FindElement(by);

            if ((check && !element.Selected) || (!check && element.Selected))
            {
                element.Click();
            }
        }

        /// <summary>
        /// Gets a select dropdown element. Allows chaining methods in page objects without needing to instantiate a new SelectElement each time
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <returns>SelectElement object</returns>
        public static SelectElement GetSelectElement(this IWebDriver driver, By by)
        {
            return new SelectElement(driver.WaitForElement(by));
        }

        /// <summary>
        /// Checks if an element is displayed. Allows for a timeout value to be set
        /// </summary>
        /// <param name="driver">IWebDriver object</param>
        /// <param name="by">element locator</param>
        /// <param name="timeoutSeconds">time to wait for the element to be displayed</param>
        /// <returns>true if element is displayed, false if not</returns>
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

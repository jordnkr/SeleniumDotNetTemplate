using OpenQA.Selenium;

namespace SeleniumDotNetTemplate.Shared
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}

# Installation
When making new project, choose 'Unit Test Project (.NET Framework)'

> Note: To install nuget dependencies, need to point at correct package source: https://api.nuget.org/v3/index.json

## Dependencies:
- Selenium.WebDriver
- Selenium.Support
- Selenium.Axe
- DotNetSeleniumExtras.WaitHelpers
- HtmlAgilityPack
- MSTest.TestFramework
- MSTest.TestAdapter
- Microsoft.CSharp
- Newtonsoft.Json

## Chromedriver
Add the latest version of [chromedriver.exe](https://chromedriver.chromium.org/downloads) to the root level of the project.
- Make sure your local Chrome install is up to date as well.

## App.config
Browser and Environment are set in the App.config

Can provide different config values here to run in different environments.

# Running Tests
Tests are set up to use the [Page Object Model](https://www.selenium.dev/documentation/guidelines/page_object_models/).

The structure is such that `BasePage.cs` and `BaseTest.cs` are in the `Shared` directory, while the demo page objects and tests are in the `HCC` directory. By having those base classes in the `Shared` directory, we could leverage them for several different websites within this same Selenium project by having a directory for another site alongside the `HCC` directory.

## Lighthouse (accessibility testing)
This project is set up with Lighthouse configured. It can be run via an inherited method found in `BasePage.cs`.

In order to run Lighthouse, you must have the Lighthouse node module installed.
- `npm install -g lighthouse`
- See config options [here](https://github.com/GoogleChrome/lighthouse#cli-options)

## axe-core
This project can run the accessibility tests found in the axe-core library. This is done with the help of [Selenium.Axe](https://github.com/TroyWalshProf/SeleniumAxeDotnet).
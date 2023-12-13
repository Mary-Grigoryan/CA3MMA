using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MMAStatsApp.Tests
{
    [TestClass]
    public class E2ETests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void TestSetup()
        {
            // Initialize the Chrome Driver
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void Test_SearchFunctionality()
        {
            // Replace with the URL where your Blazor app is hosted (localhost if you're running it locally)
            _driver.Navigate().GoToUrl("http://localhost:5000");

            // Implement the steps of your test here...
            // Example:
            _driver.FindElement(By.Id("searchInput")).SendKeys("Test Fighter");
            _driver.FindElement(By.Id("searchButton")).Click();

            // Add assertions here...

            // Cleanup
            _driver.Quit();
        }

        [TestCleanup]
        public void TestTeardown()
        {
            _driver.Dispose();
        }
    }
}

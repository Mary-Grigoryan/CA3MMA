using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MMAStatsApp.Tests
{
    [TestClass]
    public class E2ETests
    {
        private IWebDriver? _driver;

        [TestInitialize]
        public void TestSetup()
        {
            // Initialize the Chrome Driver
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void Test_NavigateToAthletes_And_SearchForConorMcGregor()
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("WebDriver is not initialized.");
            }

            // Navigate to the homepage of your Blazor app
            _driver?.Navigate().GoToUrl("http://localhost:5192/");

            // Wait for the navigation menu to be present
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement athletesNavLink = wait.Until(d => d.FindElement(By.XPath("//a[@href='/fighter-search']")));

            // Click on the "ATHLETES" link in the navigation bar
            athletesNavLink.Click();

            // Wait for the search input to be present and visible
            IWebElement searchInput = wait.Until(d =>
            {
                var element = d.FindElement(By.Id("fighterSearch"));
                if (element.Displayed)
                    return element;
                else
                    throw new NoSuchElementException();
            });

            IWebElement athletesSearchBar = wait.Until(d => d.FindElement(By.Id("fighterSearch")));

            // Enter "Conor McGregor" into the search input
            searchInput.SendKeys("Conor McGregor");

            // Find the search button and click on it
            IWebElement searchButton = _driver?.FindElement(By.Id("searchSubmit"));
            searchButton.Click();

            // Optionally, wait for and verify the search results
            // This will depend on the id or class of the elements that are displayed after search
            // For example, wait until the fighter details are displayed
            wait.Until(d => d.FindElement(By.ClassName("fighter-profile")));

            // Verify if "Conor McGregor" is present on the result page
            IWebElement fighterNameElement = wait.Until(d => d.FindElement(By.ClassName("name")));
            Assert.IsTrue(fighterNameElement.Text.Contains("CONOR MCGREGOR"), "Search results should contain Conor McGregor.");

            // Cleanup
            _driver?.Quit();
        }

        [TestCleanup]
        public void TestTeardown()
        {
            _driver?.Dispose();
        }
    }
}

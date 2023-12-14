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

        [TestMethod]
        public void Test_NavigateToHome_CheckPageLoad()
        {
            _driver = _driver ?? throw new InvalidOperationException("WebDriver is not initialized.");
            _driver.Navigate().GoToUrl("http://localhost:5192/");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Click on the "HOME" link in the navigation bar, ensuring the driver is not null
            var homeNavLink = wait.Until(driver => driver.FindElement(By.CssSelector("nav a.nav-link[href='/']")));
            homeNavLink?.Click();

            // Wait for the main event background to be present, indicating the Home page has loaded
            var mainEventBackground = wait.Until(driver => driver.FindElement(By.CssSelector(".main-event-background")));

            // Assert that the main event title is present and correct
            var eventTitle = _driver.FindElement(By.CssSelector(".event-title"));
            Assert.IsTrue(eventTitle?.Text.Contains("UFC 296"), "The main event title should contain 'UFC 296'.");

            // Assert that the fighter names are present and correct
            var fighterNames = _driver.FindElement(By.CssSelector(".fighter-names h1"));
            string expectedFighterNames = "LEON EDWARDS VS COLBY COVINGTON";
            Assert.IsTrue(fighterNames?.Text.Equals(expectedFighterNames, StringComparison.OrdinalIgnoreCase), "The fighter names should match the expected names.");

            // Cleanup
            _driver.Quit();
        }

        [TestMethod]
        public void Test_SidebarLoadsCorrectly()
        {
            _driver = _driver ?? throw new InvalidOperationException("WebDriver is not initialized.");
            _driver.Navigate().GoToUrl("http://localhost:5192/");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Wait for the sidebar to be present
            IWebElement sidebar = wait.Until(driver => driver.FindElement(By.CssSelector("div.nav-scrollable")));

            // Check that the "HOME" link is present in the sidebar
            IWebElement homeLink = sidebar.FindElement(By.CssSelector("nav a.nav-link[href='/']"));
            Assert.IsNotNull(homeLink, "The HOME link should be present in the sidebar.");

            // Check that the "ATHLETES" link is present in the sidebar
            IWebElement athletesLink = sidebar.FindElement(By.CssSelector("nav a.nav-link[href='/fighter-search']"));
            Assert.IsNotNull(athletesLink, "The ATHLETES link should be present in the sidebar.");
            
            // Cleanup
            _driver.Quit();
        }

        [TestCleanup]
        public void TestTeardown()
        {
            _driver?.Dispose();
        }
    }
}

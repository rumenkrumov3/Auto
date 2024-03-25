using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationExerciseProject.Communication
{
    public class DriverAdapter
    {
        private IWebDriver _webDriver;
        private WebDriverWait _webDriverWait;

        private readonly static Lazy<DriverAdapter> _driverAdapter = new Lazy<DriverAdapter>(() => new DriverAdapter());
        public void Start(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _webDriver = new ChromeDriver();
                    break;
                case Browser.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    _webDriver = new FirefoxDriver();
                    break;
                case Browser.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    _webDriver = new EdgeDriver();
                    break;
                case Browser.Safari:
                    _webDriver = new SafariDriver();
                    break;
                case Browser.InternetExplorer:
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    _webDriver = new InternetExplorerDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }

            _webDriver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        }

        public static DriverAdapter Instance => _driverAdapter.Value;
        public void Quit()
        => _webDriver.Quit();


        public void GoToUrl(string url)
        => _webDriver.Navigate().GoToUrl(url);

        public IWebElement FindElement(By locator)
        => _webDriverWait.Until(ExpectedConditions.ElementExists(locator));

        public List<IWebElement> FindElements(By locator)
        {
            var elements = _webDriverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            var returnedElements = new List<IWebElement>();

            foreach (var element in elements)
            {
                returnedElements.Add(element);
            }

            return returnedElements;
        }
        
        public void Refresh() 
            => _webDriver.Navigate().Refresh();

        public bool ElementExist(By locator)
        {
            try
            {
                _webDriver.FindElement(locator);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteAllCookies()
            => _webDriver.Manage().Cookies.DeleteAllCookies();

        public void ExecuteScript(string script, params object[] args)
        {
            ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, args);
        }
    }
}

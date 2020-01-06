using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace seleniumTests
{
   
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using System;
    using OpenQA.Selenium.Remote;

    namespace SeleniumSample
    {
        [TestClass]
        public class UnitTest1
        {

            public TestContext Testcontext {get;set;}
            [TestMethod]
            //[DataRow("chrome")]
            [DataRow("firefox")]
            public void SearchPageTest(string browser)
            {
                var driver = GetDriver(browser);
                driver.Navigate().GoToUrl("http://www.google.com");
                var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()) + ".png";
                var screenshot = driver.GetScreenshot();
                screenshot.SaveAsFile(filePath);
                TestContext.AddResultFile(filePath);
                screenshot.SaveAsFile(filePath);             
                driver.Quit();
            }

            #region private methods

            private RemoteWebDriver GetDriver(string browser)
            {
                switch (browser)
                {
                    case "chrome":
                        return GetChromeDriver();
                    case "firefox":
                        return GetFirefoxDriver();
                    case "ie":
                    default:
                        return GetIEDriver();
                }
            }

            private RemoteWebDriver GetChromeDriver()
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("headless");
                options.AddArgument("no-sandbox");
                var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return new ChromeDriver(path,options);
                }
                else
                {
                    return new ChromeDriver(options);
                }
            }

            private RemoteWebDriver GetFirefoxDriver()
            {
                var path = Environment.GetEnvironmentVariable("GeckoWebDriver");
                //DesiredCapabilities capabilities = DesiredCapabilities.
                FirefoxOptions options = new FirefoxOptions();
               
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return new FirefoxDriver(path);
                }
                else
                {
                    return new FirefoxDriver();
                }
            }

            private RemoteWebDriver GetIEDriver()
            {
                var path = Environment.GetEnvironmentVariable("IEWebDriver");
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return new InternetExplorerDriver(path);
                }
                else
                {
                    return new InternetExplorerDriver();
                }
            }
            #endregion
            public TestContext TestContext { get; set; }
        }
    }
}

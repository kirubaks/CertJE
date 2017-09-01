using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CertificationAutomation
{
    public class Driver
    {
        private static IWebDriver driver;
       // private static string browser;

        //public static IWebDriver Instance { get; set; }

        public static void Initialize(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    var options = new ChromeOptions();

                    options.AddArguments("chrome.switches", "--disable-extensions --disable-extensions-file-access-check --disable-extensions-http-throttling --disable-infobars --enable-automation --start-maximized");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                   
                    driver = new ChromeDriver(options);
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                   
                    break;

                case "Firefox":
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                    break;

                case "IE":
                    driver = new InternetExplorerDriver();
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                    break;
            }
        }

        internal static IWebElement FindElement(By by)
        {
            return driver.FindElement(by);
        }

        public static void Dispose()
        {
            driver.Quit();
        }

        public static void navigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}

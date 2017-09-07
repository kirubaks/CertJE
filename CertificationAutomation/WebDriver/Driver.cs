using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CertificationAutomation
{
    public class Driver
    {
       
        public static IWebDriver Instance { get; set; }

        public static void Initialize(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    var options = new ChromeOptions();

                    options.AddArguments("chrome.switches", "--disable-extensions --disable-extensions-file-access-check --disable-extensions-http-throttling --disable-infobars --enable-automation --start-maximized");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                   
                    Instance = new ChromeDriver(options);
                    Instance.Manage().Window.Maximize();
                    Instance.Manage().Cookies.DeleteAllCookies();
                    Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                   
                    break;

                case "Firefox":
                    Instance = new FirefoxDriver();
                    Instance.Manage().Window.Maximize();
                    Instance.Manage().Cookies.DeleteAllCookies();
                    Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                    break;

                case "IE":
                    Instance = new InternetExplorerDriver();
                    Instance.Manage().Window.Maximize();
                    Instance.Manage().Cookies.DeleteAllCookies();
                    Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                    break;

                case "Edge":
                    string serverPath = "Microsoft Web Driver";
                    if (System.Environment.Is64BitOperatingSystem)
                    {
                        serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), serverPath);
                    }
                    else
                    {
                        serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles%"), serverPath);
                    }
                    EdgeOptions option = new EdgeOptions();
                    option.PageLoadStrategy = EdgePageLoadStrategy.Eager;
                    Instance = new EdgeDriver(serverPath, option);

                    //Set page load timeout to 5 seconds
                    Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
                    break;
            }
        }
        
        public static void Dispose()
        {
            Instance.Close();
            Instance.Quit();
        }

        public static void NavigateTo(string url)
        {
            Instance.Url = url ?? throw new ArgumentNullException("url", "Url is not defined");
        }
    }
}

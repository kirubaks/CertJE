﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertificationAutomation.Resources;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Data.SqlClient;

namespace CertificationAutomation.Utilities
{
    public static class CommonFunctions
    {
        //private static int WAIT_LONG = 20;
        //private static int WAIT_SHORT = 10;
        private static Random random = new Random();

        public static By GetElement(string locators)
        {

            //string locators = ObjectMap.ResourceManager.GetString("Test");

            //Split the value which contains locator type and locator value
            //Console.WriteLine(locators);
            string[] splitString = locators.Split(':');
            string locatorType = splitString[0];
            string locatorValue = splitString[1];

            //Return the element based on type of locator
            if (locatorType.ToLower().Equals("id"))
                return By.Id(locatorValue);
            else if (locatorType.ToLower().Equals("name"))
                return By.Name(locatorValue);
            else if ((locatorType.ToLower().Equals("classname")) || (locatorType.ToLower().Equals("class")))
                return By.TagName(locatorValue);
            else if ((locatorType.ToLower().Equals("tagname")) || (locatorType.ToLower().Equals("tag")))
                return By.ClassName(locatorValue);
            else if ((locatorType.ToLower().Equals("linktext")) || (locatorType.ToLower().Equals("link")))
                return By.LinkText(locatorValue);
            else if (locatorType.ToLower().Equals("partiallinktext"))
                return By.PartialLinkText(locatorValue);
            else if ((locatorType.ToLower().Equals("cssselector")) || locatorType.ToLower().Equals("css"))
                return By.CssSelector(locatorValue);
            else if (locatorType.ToLower().Equals("xpath"))
                return By.XPath(locatorValue);
            else
                throw new Exception("Invalid LocatorType - Element not found");
            
        }

        public static string GetCookieValue()
        {
            string cookievalue;
            Cookie session = Driver.Instance.Manage().Cookies.GetCookieNamed("OCSessionID");
            string newStr = session.ToString();
            Console.WriteLine(newStr);
            cookievalue = newStr.Substring(0, newStr.IndexOf(" "));
            cookievalue = cookievalue.Substring(cookievalue.IndexOf("=")).Replace("=", "").Replace(";", "");
            Console.WriteLine(cookievalue);
            cookievalue = setdoubleQuote(cookievalue);
            return cookievalue;
        }

        public static string GetTokenValue()
        {
            string tokenvalue;
            //int newStr1;
            Cookie newcookie = Driver.Instance.Manage().Cookies.GetCookieNamed("XSRF-TOKEN");
            
            Console.WriteLine(newcookie);
            string test = newcookie.ToString();
            tokenvalue = test.Substring(0, test.IndexOf(";"));
            string cookietemp = tokenvalue.Substring(11);
            Console.WriteLine(cookietemp);
            return cookietemp;
        }

        public static string setdoubleQuote(string cookievalue)
        {
            string quoteText = "";
            if (!cookievalue.Equals(""))
            {
                quoteText = "\"" + cookievalue + "\"";
            }
            return quoteText;
        }

        public static IWebElement FindElement(string locators)
        {
            return Driver.Instance.FindElement(GetElement(locators));
        }
        public static void EnterText(string locator, string text)
        {
            try
            {
                //new WebDriverWait(Driver.Instance , TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(FindElement(ObjectMap.ResourceManager.GetString(locator))));
                //Console.WriteLine(locator);
                FindElement(ObjectMap.ResourceManager.GetString(locator)).Clear();
                FindElement(ObjectMap.ResourceManager.GetString(locator)).SendKeys(text);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void Click(string locator)
        {
            try
            {
                FindElement(ObjectMap.ResourceManager.GetString(locator)).Click();
                //WebDriverWait wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 5));
                //wait.Until(ExpectedConditions.ElementIsVisible(GetElement(locator)));
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void Hover(string locator)
        {
            try
            {
                Actions builder = new Actions(Driver.Instance);
                IWebElement element = FindElement(ObjectMap.ResourceManager.GetString(locator));
                builder.MoveToElement(element).Build().Perform();
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void ClickbyJSExecutor(string locator)
        {
            try
            {
                // Find element to be clicked
                IWebElement element = FindElement(ObjectMap.ResourceManager.GetString(locator));

                // Declare Java Script Executor and click element
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance;
                js.ExecuteScript("arguments[0].click()", element);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void ClickbyAction(string locator)
        {
            try
            {
                // Find element to be clicked
                IWebElement element = FindElement(ObjectMap.ResourceManager.GetString(locator));

                // Declare Action and click element
                Actions builder = new Actions(Driver.Instance);
                builder.Click(element);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void SwitchtoPopup(string locator)
        {
            try
            {
                // Save main window handle
                string mainWindow = Driver.Instance.CurrentWindowHandle;

                // Initiate pop up and switch to it
                IWebElement element = FindElement(ObjectMap.ResourceManager.GetString(locator));
                PopupWindowFinder finder = new PopupWindowFinder(Driver.Instance);
                string popupWindowHandle = finder.Click(element);
                Driver.Instance.SwitchTo().Window(popupWindowHandle);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void SwitchtoiFrame(string locator)
        {
            try
            {
                Driver.Instance.SwitchTo().Frame(FindElement(ObjectMap.ResourceManager.GetString(locator)));
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void Wait(double number)
        {
            Driver.Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(number);
        }

        public static void WaitUntilElementisVisible(double time, string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(time));
                wait.Until(ExpectedConditions.ElementIsVisible(GetElement(locator)));
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element is not visible : " + e.Message);
                throw e;
            }
        }

        public static void SelectDropDownbyValue(string locator, string value)
        {
            try
            {
                // Create select element
                var dropdown = FindElement(ObjectMap.ResourceManager.GetString(locator));
                var selectElement = new SelectElement(dropdown);

                // Select dropdown option by value
                selectElement.SelectByValue(value);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void SelectDropDownbyIndex(string locator, int index)
        {
            try
            {
                // Create select element
                var dropdown = FindElement(ObjectMap.ResourceManager.GetString(locator));
                var selectElement = new SelectElement(dropdown);

                // Select dropdown option by index
                selectElement.SelectByIndex(index);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static void SelectDropDownbyText(string locator, string text)
        {
            try
            {
                // Create select element
                var dropdown = FindElement(ObjectMap.ResourceManager.GetString(locator));
                var selectElement = new SelectElement(dropdown);

                // Select dropdown option by index
                selectElement.SelectByText(text);
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
        }

        public static string GetTextFromGraphBar(string locator, string barIdentifier)
        {
            string barText = "";
            try
            {
                // Find root element of graph
                IWebElement element = FindElement(ObjectMap.ResourceManager.GetString(locator));

                // Make list of elements from main graph element
                IList<IWebElement> bars = element.FindElements(By.TagName("area"));

                // Iterate through list of elements for desired text
                foreach (IWebElement bar in bars)
                {
                    if (bar.GetAttribute("href").Contains(barIdentifier))
                    {
                        barText = bar.GetAttribute("title").ToString();
                    }
                }
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
            return barText;
        }

        public static void ClickGraphBar(string locator, string barIdentifier)
        {
            
            try
            {
                // Find root element of graph
                IWebElement element = FindElement(ObjectMap.ResourceManager.GetString(locator));

                // Make list of elements from main graph element
                IList<IWebElement> bars = element.FindElements(By.TagName("area"));

                // Iterate through list of elements for desired text
                foreach (IWebElement bar in bars)
                {
                    if (bar.GetAttribute("href").Contains(barIdentifier))
                    {
                        bar.Click();
                    }
                }
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element not found : " + e.Message);
                throw e;
            }
            
        }

        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool ValidateTitle(string locator)
        {
            return (Driver.Instance.Title.Equals(ObjectMap.ResourceManager.GetString(locator), StringComparison.InvariantCultureIgnoreCase));
        }

        public static string DBValidation(string getresult)
        {
            string result = null;
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString =
                "Data Source=ServerName;" +
                "Initial Catalog=DataBaseName;" +
                "User id=UserName;" +
                "Password=Secret;";
                conn.Open();

                SqlCommand runsqltest = new SqlCommand("Command String", conn);
                SqlDataReader reader = runsqltest.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["id"].ToString());
                    Console.WriteLine(reader["name"].ToString());
                    result = reader["id"].ToString();
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }

        public static string CaptureScreenshot(IWebDriver BrowserInstance, string Path, string FileName)
        {
            StringBuilder TimeAndDate = new StringBuilder(DateTime.Now.ToString());
            TimeAndDate.Replace("/", "_");
            TimeAndDate.Replace(":", "_");
            string ScreenshotPath = null;

            DirectoryInfo Validation = new DirectoryInfo(Path);
            Screenshot ss = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
            try
            {
                if (Validation.Exists == true)
                {
                    // ((ITakesScreenshot)BrowserInstance).GetScreenshot().SaveAsFile(Path + "." + FileName + TimeAndDate.ToString() + "." , ScreenshotImageFormat.Png);

                    string screenshot = ss.AsBase64EncodedString;
                    byte[] screenshotAsByteArray = ss.AsByteArray;
                    ss.SaveAsFile(Path + "_" + FileName + TimeAndDate.ToString(), ScreenshotImageFormat.Png); //use any of the built in image formating
                    ScreenshotPath = Path + "_" + FileName + TimeAndDate.ToString();
                    Console.WriteLine(ScreenshotPath);
                }
                else
                {
                    Validation.Create();
                    //Screenshot ss = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
                    string screenshot = ss.AsBase64EncodedString;
                    byte[] screenshotAsByteArray = ss.AsByteArray;
                    ss.SaveAsFile(Path + "_" + FileName + TimeAndDate.ToString(), ScreenshotImageFormat.Jpeg); //use any of the built in image formating
                    ScreenshotPath = Path + "_" + FileName + TimeAndDate.ToString();
                    Console.WriteLine(ScreenshotPath);
                   // return fullPathToReturn;
                }
            }
            catch (System.Runtime.InteropServices.ExternalException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            return ScreenshotPath;
        }
    }
}


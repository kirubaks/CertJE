using System;
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

namespace CertificationAutomation.Utilities
{
    public static class CommonFunctions
    {
        public static By GetElement(string locators)
        {

            //string locators = ObjectMap.ResourceManager.GetString("Test");

            //Split the value which contains locator type and locator value
            Console.WriteLine(locators);
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

        public static IWebElement FindElement(string locators)
        {
            return Driver.Instance.FindElement(GetElement(locators));
        }
        public static void EnterText(string locator, string text)
        {
            try
            {
                //new WebDriverWait(Driver.Instance , TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(FindElement(ObjectMap.ResourceManager.GetString(locator))));
                Console.WriteLine(locator);
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
                WebDriverWait wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 5));
                wait.Until(ExpectedConditions.ElementIsVisible(GetElement(locator)));
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

        public static bool ValidateTitle(string locator)
        {
            return (Driver.Instance.Title.Equals(ObjectMap.ResourceManager.GetString(locator), StringComparison.InvariantCultureIgnoreCase));
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


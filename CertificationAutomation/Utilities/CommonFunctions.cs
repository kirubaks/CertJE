using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CertificationAutomation.Utilities
{
    public static class CommonFunctions
    {
        public static void EnterText(By by, string text)
        {
            Driver.FindElement(by).SendKeys(text);
        }

        public static void Click(By by)
        {
            Driver.FindElement(by).Click();
        }
    }
}

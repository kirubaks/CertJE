using System;
using CertificationAutomation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CertificationAutomation.Utilities;

namespace CertificationTests
{
    [TestFixture]
    public class SmokeTest :TestBase
    {
        [Test]
        public void LoginTest()
        {
            node = parent.CreateNode("Login Test");
            Console.Write("Login Test :");
            string path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "test1");
            
            CommonFunctions.EnterText("Login_Username", "autouser");
            CommonFunctions.EnterText("Login_Password", "User123!@#");
            CommonFunctions.Click("Login_SignInButton");

            Assert.IsTrue(CommonFunctions.ValidateTitle("Dashboard_Title"));
            path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "LoginTest");
            node.Pass("Login Successful").AddScreenCaptureFromPath(path);
        }


    }
}

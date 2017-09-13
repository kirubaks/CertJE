using System;
using CertificationAutomation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CertificationAutomation.Utilities;
using System.Collections;

namespace CertificationTests
{
    [TestFixture]
    public class SmokeTest :TestBase
    {

        [Test]
        public void LoginTest()
        {
            node = parent.CreateNode("Login Test");
            logger.Info("Login Test: Started");
            string path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "test1");
            
            CommonFunctions.EnterText("Login_Username", "autouser");

            Console.WriteLine(table["Username"].ToString());
            CommonFunctions.EnterText("Login_Password", "User123!@#");
            CommonFunctions.Click("Login_SignInButton");

            Assert.IsTrue(CommonFunctions.ValidateTitle("Dashboard_Title"));

            path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "LoginTest");
            node.Pass("Login Successful").AddScreenCaptureFromPath(path);
            logger.Info("Login Test: Ended");
        }


    }
}

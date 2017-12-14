using System;
using CertificationAutomation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CertificationAutomation.Utilities;
using System.Collections;
using RestSharp;
using System.Net;
using System.Net.Cache;
using RestSharp.Deserializers;
using System.Collections.Generic;
using RA;
using Newtonsoft.Json.Linq;

namespace CertificationTests
{
    [TestFixture]
    public class SmokeTest :TestBase
    {
        private string cookieValue=null;

        [Test]
        public void LoginTest()
        {
            node = parent.CreateNode("Login Test");
            logger.Info("Login Test: Started");
            string path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "test1");

            CommonFunctions.EnterText("Login_Username", table["Username"].ToString());

            Console.WriteLine(table["Username"].ToString());
            CommonFunctions.EnterText("Login_Password", table["Password"].ToString());
            CommonFunctions.Click("Login_SignInButton");

            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));
            Assert.IsTrue(CommonFunctions.ValidateTitle("Dashboard_Title"));
            cookieValue = CommonFunctions.GetCookieValue();
            Console.WriteLine(cookieValue);
            path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "test1");
            path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "LoginTest");
            node.Pass("Login Successful").AddScreenCaptureFromPath(path);

            logger.Info("Login Test: Ended");
        }

        [Test]
        public void APITest()
        {
            CommonFunctions.EnterText("Login_Username", "autouser");
            CommonFunctions.EnterText("Login_Password", "User123!@#");
            CommonFunctions.Click("Login_SignInButton");

            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));

            Driver.Instance.FindElement(By.CssSelector(".dashboard_close_container_content>h3>a")).Click();
            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));

            Driver.Instance.FindElement(By.CssSelector("#menu_li_2>a")).Click();
            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(70));

            string cookievalue = CommonFunctions.GetCookieValue();
            string tokenvalue = CommonFunctions.GetTokenValue();
            string token = CommonFunctions.setdoubleQuote(tokenvalue);

            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;

            var client = new RestClient("https://site4console.cadency.trintech.com/closeAPI/api/close/entity");
            client.FollowRedirects = false;
            client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);

            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");

            request.AddCookie("OCSessionID", cookievalue);
            request.AddCookie("XSRF-TOKEN", tokenvalue);

            request.AddHeader("X-XSRF-TOKEN", tokenvalue);
            request.AddHeader("Content-Type", "application/json");

            request.AddParameter("application/json", "{\"parentId\": null, \"number\": \"csharpTest3\", \"name\": \"csharpTest3\", \"description\": \"csharpTest3\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            
            System.Net.HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            Console.WriteLine(numericStatusCode);

            var head = response.Headers;
            string headers = head.ToString();
            //Console.WriteLine(headers);

            // Create a new Deserializer to be able to parse the JSON object
            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            
            //To deserialize into a simple variable, use the <Dictionary<string, string>> type
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);
            string entityId = JSONObj["id"];
            Console.WriteLine("Entity ID: " +entityId);
        }

        [Test]
        public void AddAnAccountTest()
        {
            node = parent.CreateNode("Add an Account Test");
            logger.Info("Add an Account Test: Started");
            string path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "test2");

            CommonFunctions.EnterText("Login_Username", "autouser");

            Console.WriteLine(table["Username"].ToString());
            CommonFunctions.EnterText("Login_Password", "User123!@#");
            CommonFunctions.Click("Login_SignInButton");
            CommonFunctions.Click("Certification_R2R_Header");

            CommonFunctions.Click("Maintenance_Tab");
            CommonFunctions.Click("Add_New_Account");

            CommonFunctions.EnterText("CompanyCode_Textbox", CommonFunctions.RandomNumber(5));
            CommonFunctions.EnterText("Account_Textbox", CommonFunctions.RandomNumber(5));
            CommonFunctions.EnterText("Division_Textbox", CommonFunctions.RandomNumber(5));
            CommonFunctions.EnterText("SetofBooks_Textbox", CommonFunctions.RandomNumber(5));
            CommonFunctions.EnterText("Description_Textbox", "This is an automation test account.");
            CommonFunctions.EnterText("AccountName_Textbox", "AutomationAccount"+ CommonFunctions.RandomNumber(5));
        }

        [Test]
        public void TryingRestAssured()
        {
            CommonFunctions.EnterText("Login_Username", "autouser");
            CommonFunctions.EnterText("Login_Password", "User123!@#");
            CommonFunctions.Click("Login_SignInButton");

            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));

            Driver.Instance.FindElement(By.CssSelector(".dashboard_close_container_content>h3>a")).Click();
            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));

            Driver.Instance.FindElement(By.CssSelector("#menu_li_2>a")).Click();
            Driver.Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(70));

            string cookievalue = CommonFunctions.GetCookieValue();
            string tokenvalue = CommonFunctions.GetTokenValue();
            string token = CommonFunctions.setdoubleQuote(tokenvalue);

          /*  JObject test = new JObject();
            test.Add("parentId", null);
            test.Add("number", "csharptest1");
            test.Add("name", "csharptest1");
            test.Add("description", "testdesc");*/

            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => true;
            // System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            new RestAssured()
                               .Given()
                                .Host("https://site4console.cadency.trintech.com")
                                .Uri("/closeAPI/api/close/entity")
                                .Header("ignore-ssl-errors", "true")
                                .Header("Cookie", "OCSessionID=" + cookievalue + "\"")
                                .Header("Cookie", "XSRF-TOKEN=" + token + "\"")
                                .Header("X-XSRF-TOKEN", tokenvalue)
                                .Header("Content-Type", "application/json")
                                .Body("{ 'parentId' : null, 'number' : 'csharptest', 'name' : 'Brooklyn', 'description' : 'test123'}")
                     .When()
                        .Post()
                        .Then()

                    .Debug();
        }

    }
}

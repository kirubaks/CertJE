using System;
using System.Configuration;
using System.Net;
using CertificationAutomation;
using CertificationAutomation.Utilities;
using CertificationTests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using RA;
using RestSharp;

namespace APITestProj
{
    [TestFixture]
    public class UnitTest1
    {
           [Test]
           public void TestMethod1()
           {
            Driver.Initialize("IE");
            Driver.NavigateTo("https://site4.cadency.trintech.com");
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

            JObject test = new JObject();
            test.Add("parentId", null);
            test.Add("number", "csharptest1");
            test.Add("name", "csharptest1");
            test.Add("description", "testdesc");

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
                //.TestBody("test a", x => x.id != null)
                //.Assert("test a");
            //var client = new RestClient("https://site4console.cadency.trintech.com/closeAPI/api/close/entity");
            /*var client = new RestClient("https://site4console.cadency.trintech.com/closeAPI/api/close/reference/1021");
               var request = new RestRequest(Method.GET);

               var entityModel = new Entity();
            entityModel.parentId = null;
            entityModel.number = "test123123";
            entityModel.name = "test123123";
            entityModel.description = "test123123";

            JObject test = new JObject();
            test.Add("parentId", null);
            test.Add("number", "1231231");
            test.Add("name", "test123123123");
            test.Add("description", "testdesc");

               request.AddParameter("OCSessionID", cookievalue, ParameterType.Cookie);
               request.AddParameter("XSRF-TOKEN", token, ParameterType.Cookie);
            request.AddHeader("X-XSRF-TOKEN", tokenvalue);
               request.RequestFormat = DataFormat.Json;
               request.AddHeader("Accept", "application/json");*/
            // request.AddParameter("application/json", test.ToString(), ParameterType.RequestBody);
            //request.Parameters.Clear();
            //request.AddBody(new { parentId = 19289, number = "test_random213", name = "test_random213", description = "test" });
            //request.AddJsonBody(entityModel);
            //request.AddJsonBody((new { parentId = "", number = "test_random213", name = "test_random213", description = "test" }));
            // request.AddParameter("application/json", "{ \"parentId\": 19288, \"number\": \"testrandom123\", \"name\": \"testrandom123\", \"description\": \"testrandom123\"}", ParameterType.RequestBody);
            /*  Console.WriteLine("posting request");

           IRestResponse response = client.Execute(request);
           Console.WriteLine(response.RawBytes);
           System.Net.HttpStatusCode statusCode = response.StatusCode;
           int numericStatusCode = (int)statusCode;
           Console.WriteLine(numericStatusCode);
           //var obj = JObject.Parse(response.Content);
           var content = response.Content;
           string resp = response.Content.ToString();
           Console.WriteLine(resp);
           var jsontext = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(content);*/
            Driver.Dispose();
            //dynamic json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
            //Console.WriteLine(json);
            /*LoginResult result = new LoginResult
            {
                Status = obj["name"].ToString(),
                //Authority = response.ResponseUri.Authority,
                EntityId = obj["entityId"].ToString()
            };*/
        }

        /* [Test]
         public void MyFirstTest()
         {
             var restClient = new RestClient("https://site4console.cadency.trintech.com");
             var restRequest = new RestRequest("POST");
             restRequest.Resource = "/closeAPI/api/close/entity";
             restRequest.AddCookie("OCSessionID", "1505489118550&demo&2897051104972033417&false&v6pB2mTGN UMIBrDWAT2Knl/ellBOh0p2JsBqNZtwEI=&c80604b4-2489-4c93-a9e6-d336e9e26194&true");
             restRequest.AddCookie("XSRF-TOKEN", "-3469744454860611932");
             restRequest.AddParameter("application/json", "{\"parentId\":\"null\", \"number\":\"\test1234\", \"name\":\"\test1234\", \"description\":\"\test1234\"}", ParameterType.RequestBody);
             var response = restClient.Execute<dynamic>(restRequest);

             //var jPerson = JsonConvert.DeserializeObject<dynamic>(response.Content);

         }*/

       
    }

}

﻿using System;
using System.Configuration;
using System.IO;
using AventStack.ExtentReports;
using CertificationAutomation;
using CertificationAutomation.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using log4net;

namespace CertificationTests
{
    public class TestBase
    {
        public ExtentReports report;
        public ExtentTest parent;
        public ExtentTest node;
        public string ReportLocation = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Reports\");
        public log4net.ILog logger;
        public string TestSuiteFile = "C:\\Test\\Test1.xlsx";

        [OneTimeSetUp]
        public void BeforeSuiteSetup()
        {
            Console.WriteLine(this.GetType().Name);
            logger = log4net.LogManager.GetLogger(this.GetType().Name);
            report = Reporter.GetExtent(ReportLocation + this.GetType().Name + ".html", this.GetType().Name+ " Results", this.GetType().Name+ " Suite");
            parent = report.CreateTest(this.GetType().Name+ " Suite");
        }

        [SetUp]
        public void BeforeTestSetup()
        {
            Console.WriteLine(this.GetType().Name);
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
            Driver.Initialize(ConfigurationManager.AppSettings["Browser"]);
            Driver.NavigateTo(ConfigurationManager.AppSettings["URL"]); 
            
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    string path = CommonFunctions.CaptureScreenshot(Driver.Instance, "C:\\Test\\", "test_failed");
                    node.Fail("Test Failed").AddScreenCaptureFromPath(path);
                    node.Log(logstatus, "Test ended with " + logstatus + stacktrace);
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    node.Log(logstatus, "Test ended with " + logstatus + stacktrace);
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    node.Log(logstatus, "Test skipped " + logstatus + stacktrace);
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            //node.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            Driver.Dispose();
        }

        [OneTimeTearDown]
        public void AfterSuite()
        {
            report.Flush();
        }
    }
}
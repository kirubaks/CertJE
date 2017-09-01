using System;
using System.Configuration;
using System.IO;
using AventStack.ExtentReports;
using CertificationAutomation;
using CertificationAutomation.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CertificationTests
{
    public class TestBase
    {
        public ExtentReports report;
        public ExtentTest parent;
        public ExtentTest node;
        public string ReportLocation = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Reports\");

        [OneTimeSetUp]
        public void BeforeSuiteSetup()
        {
            report = Reporter.GetExtent(ReportLocation + "RegressionResults.html", "Regression Test Results", "Regression Test Suite");
            parent = report.CreateTest("Regression Test Suite_");
        }
        [SetUp]
        public void BeforeTestSetup()
        {
            Driver.Initialize("IE");
            Driver.navigateTo(ConfigurationManager.AppSettings["URL"]); 
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
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            node.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            Driver.Dispose();
        }

        [OneTimeTearDown]
        public void AfterSuite()
        {
            report.Flush();
        }
    }
}
using System;
using CertificationAutomation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CertificationTests
{
    [TestFixture]
    public class SmokeTest :TestBase
    {


        [Test]
        public void FirstTest()
        {
            node = parent.CreateNode("First Test");
            Console.Write("MY First Test");
            Console.Write(ReportLocation);
        }


    }
}

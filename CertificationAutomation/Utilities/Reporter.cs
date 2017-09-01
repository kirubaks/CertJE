using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace CertificationAutomation.Utilities
{
    public class Reporter
    {
        private static ExtentReports extent;
        private static ExtentTest test;
        private static ExtentHtmlReporter htmlReporter;



        public static ExtentReports GetExtent(String path, String docTitle, String reportName)
        {
            extent = new ExtentReports();

            extent.AttachReporter(GetHtmlReporter(path, docTitle, reportName));

            return extent;
        }

        private static ExtentHtmlReporter GetHtmlReporter(String filePath, String title, String name)
        {

            htmlReporter = new AventStack.ExtentReports.Reporter.ExtentHtmlReporter(filePath);


            htmlReporter.Configuration().ChartVisibilityOnOpen=true;
            htmlReporter.Configuration().DocumentTitle = title;
            htmlReporter.Configuration().ReportName = name;

            return htmlReporter;
        }

        public static ExtentTest CreateTest(String name, String description)
        {
            test = extent.CreateTest(name, description);
            return test;
        }
    }
}

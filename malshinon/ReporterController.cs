using MLogic;
using System;

namespace malshinon
{
    internal class ReporterController
    {
        public Logic reportLogic = new Logic();

        public void RunReportFlow()
        {
            string reporter = GetReporterInfo();
            string target = GetTargetInfo();
            string report = GetReporterContent();

            string reporterCode = reportLogic.Login(reporter);
            string targetCode = reportLogic.GetTarget(target);

            reportLogic.InsertReport(reporterCode, targetCode, report);
        }

        public string GetReporterInfo()
        {
            Console.WriteLine("Enter your name or code name:");
            return Console.ReadLine();
        }

        public string GetTargetInfo()
        {
            Console.WriteLine("Enter the target info:");
            return Console.ReadLine();
        }

        public string GetReporterContent()
        {
            Console.WriteLine("Enter your report:");
            return Console.ReadLine();
        }
    }
}

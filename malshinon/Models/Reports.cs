using System;

namespace Models
{
    public class Reports
    {
        public int ReportId;
        public string ReporterCode;
        public string TargetCode;
        public string Text;
        public DateTime DateTime;

        public Reports(string reporterCode, string targetCode, string text)
        {
            this.ReporterCode = reporterCode;
            this.TargetCode = targetCode;
            this.Text = text;
            this.DateTime = DateTime.Now;
        }
    }
}

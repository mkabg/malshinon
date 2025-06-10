namespace Models
{
    internal class Reports
    {
        public int ReportID;
        public int ReporterID;
        public int TargetID;
        public string Text;

        public Reports(int reportID, int reporterID, int targetID, string text)
        {
            this.ReportID = reportID;
            this.ReporterID = reporterID;
            this.TargetID = targetID;
            this.Text = text;
        }
    }
}

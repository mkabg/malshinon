using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.Models
{
    internal class Reporters
    {
        public int ReporterID;
        public string ReporterName;
        public int NumOfReports;
        public int SumCharInAllReports;

        public Reporters(int reporterID, string reporterName, int numOfReports, int sumCharInAllReports,)
        {
            this.ReporterID = reporterID;
            this.ReporterName = reporterName;
            this.NumOfReports = numOfReports;
            this.SumCharInAllReports = sumCharInAllReports;
        }

        public bool Avarge()
        {
            return (this.SumCharInAllReports / this.NumOfReports) > 100;
        }
    }
}

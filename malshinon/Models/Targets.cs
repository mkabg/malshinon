using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.Models
{
    internal class Targets
    {
        public int TargeID;
        public string TargetName;
        public int NumOfReports;

        public Targets(int targetID, string targetName, int numOfReports)
        {
            this.TargeID = targetID;
            this.TargetName = targetName;
            this.NumOfReports = numOfReports;
        }

        public bool Dangerous()
        {
            return this.NumOfReports >= 20;
        }
    }
}

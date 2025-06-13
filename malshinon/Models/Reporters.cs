using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Reporters
    {
        public string ReporterName;
        public string ReporterCode;

        public Reporters(string reporterName)
        {
            this.ReporterName = reporterName;
            People p = new People(reporterName);
            this.ReporterCode = p.CodeName;
        }
    }
}

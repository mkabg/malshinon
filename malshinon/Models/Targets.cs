using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Targets
    {
        public string TargetName;
        public string TargeCode;

        public Targets(string targetName)
        {
            this.TargetName = targetName;
            People p = new People(targetName);
            this.TargeCode = p.CodeName;
        }
    }
}

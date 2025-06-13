using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.Models
{
    internal class Alert
    {
        public int AlertID;
        public string TargetCode;
        public DateTime AlertTime;
        public string AlertType;
        public string Description;

        public Alert(string targetCode, string alertType, string description)
        {
            this.TargetCode = targetCode;
            this.AlertType = alertType;
            this.Description = description;
        }
    }
}

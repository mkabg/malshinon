using DAL;
using alertDAL;

namespace MLogic
{
    public class Logic
    {
        public MalshinonDAL mnd;
        public AlertDAL atd;
        public ReporterDAL rrd;
        public TargetDAL ttd;
        public ReportDAL rtd;

        public Logic()
        {
            mnd = new MalshinonDAL();
            atd = new AlertDAL(mnd);
            rrd = new ReporterDAL(mnd);
            ttd = new TargetDAL(mnd, atd);
            rtd = new ReportDAL(mnd, rrd, ttd);
        }

        public string Login(string input)
        {
            bool exists = mnd.GetCodeName(input) != null;
            if (!exists)
            {
                return rrd.AddReporter(input);
            }
            return mnd.GetCodeName(input);
        }

        public string GetTarget(string input)
        {
            bool exists = mnd.GetCodeName(input) != null;
            if (!exists)
            {
                return ttd.AddTarget(input);
            }
            return input;
        }

        public void InsertReport(string reporterCode, string targetCode, string report)
        {
            rtd.AddReport(reporterCode, targetCode, report);
        }
    }
}
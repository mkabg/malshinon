using Models;
using MySql.Data.MySqlClient;
using System;

namespace DAL
{
    public class ReportDAL
    {
        private readonly MalshinonDAL mnd;

        public ReportDAL(MalshinonDAL mnd)
        {
            this.mnd = mnd;
        }
        public void AddReport(string reporterCode, string targetCode, string text)
        {
            Reports report = new Reports(reporterCode, targetCode, text);
            try
            {
                using (MySqlConnection cone = mnd.CreateConnection())
                {
                    cone.Open();

                    string query = @"INSERT INTO reports (reporterCode, targetCode, text, timestamp)
                                      VALUES (@reporterCode, @targetCode, @Text, @timestamp)";

                    using (MySqlCommand cmd = new MySqlCommand(query, cone))
                    {
                        cmd.Parameters.AddWithValue("@reporterCode", report.ReporterCode);
                        cmd.Parameters.AddWithValue("@targetCode", report.TargetCode);
                        cmd.Parameters.AddWithValue("@text", report.Text);
                        cmd.Parameters.AddWithValue("@timestamp", report.DateTime);


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error in AddReport: " + ex.Message);
            }
            mnd.UpdateReporterAndTarget(report);
        }
    }
}

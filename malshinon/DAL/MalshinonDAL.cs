using MySql.Data.MySqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class MalshinonDAL
    {
        private string connectionString = "" +
            "server=localhost;" +
            "user=root;" +
            "database=malshinon;" +
            "port=3306;";

        private MySqlConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public void AddReport(Reports report)
        {
            try
            {
                using (MySqlConnection cone = CreateConnection())
                {
                    cone.Open();

                    string query = @"INSERT INTO reports (reportID, reporterID, targetID, text)
                                      VALUES (@reportID, @reporterID, @targetID, @Text)";

                    using (MySqlCommand cmd = new MySqlCommand(query, cone))
                    {
                        cmd.Parameters.AddWithValue("@reportID", report.ReportID);
                        cmd.Parameters.AddWithValue("@reporterID", report.ReporterID);
                        cmd.Parameters.AddWithValue("@targetID", report.TargetID);
                        cmd.Parameters.AddWithValue("@text", report.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error in AddReport: " + ex.Message);
                throw;
            }
        }

        public void UpdateReporterOrTarget(Reports report)
        {
            using (MySqlConnection cone = CreateConnection())
            {
                cone.Open();

                string query = "";
            }
        }
    }
}

using Models;
using MySql.Data.MySqlClient;
using System;

namespace DAL
{
    public class MalshinonDAL
    {
        private string connectionString = "server=localhost;user=root;database=malshinon;port=3306;";

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public string GetCodeName(string input)
        {
            try
            {
                using (MySqlConnection cone = CreateConnection())
                {
                    cone.Open();
                    string query = $@"SELECT codeName FROM people WHERE Name = @input OR CodeName = @input";

                    using (MySqlCommand cmd = new MySqlCommand(query, cone))
                    {
                        cmd.Parameters.AddWithValue(@"input", input);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader["CodeName"].ToString();
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error in AddReport: " + ex.Message);
                throw;
            }
            return null;
        }
        
        public void UpdateReporterAndTarget(Reports report)
        {
            try
            {
                using (MySqlConnection cone = CreateConnection())
                {
                    cone.Open();

                    using (MySqlTransaction transaction = cone.BeginTransaction())
                    {
                        // Update reporters
                        string reporterQuery = @"UPDATE reporters 
                                         SET numReports = numReports + 1, sumWords = sumWords + @TextLength 
                                         WHERE codeName = @ReporterCode";

                        using (MySqlCommand reporterCmd = new MySqlCommand(reporterQuery, cone, transaction))
                        {
                            reporterCmd.Parameters.AddWithValue("@TextLength", report.Text.Length);
                            reporterCmd.Parameters.AddWithValue("@ReporterCode", report.ReporterCode);
                            reporterCmd.ExecuteNonQuery();
                        }

                        // Update targets
                        string targetQuery = @"UPDATE targets 
                                       SET numReports = numReports + 1 
                                       WHERE codeName = @TargetCode";

                        using (MySqlCommand targetCmd = new MySqlCommand(targetQuery, cone, transaction))
                        {
                            targetCmd.Parameters.AddWithValue("@TargetCode", report.TargetCode);
                            targetCmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}

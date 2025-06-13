using Models;
using MySql.Data.MySqlClient;
using System;

namespace DAL
{
    public class ReporterDAL
    {
        private readonly MalshinonDAL mnd;

        public ReporterDAL(MalshinonDAL mnd)
        {
            this.mnd = mnd;
        }

        //public string AddReporter(string input)
        //{
        //    Reporters reporter = new Reporters(input);
        //    try
        //    {
        //        using (MySqlConnection cone = mnd.CreateConnection())
        //        {
        //            cone.Open();
        //            string query = @"INSERT INTO users (name, codeName)
        //                            VALUES (@name, @codeName)";

        //            using (MySqlCommand cmd = new MySqlCommand(query, cone))
        //            {
        //                cmd.Parameters.AddWithValue(@"name", reporter.ReporterName);
        //                cmd.Parameters.AddWithValue(@"codeName", reporter.ReporterCode);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        return reporter.ReporterCode;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine("Database error in AddReporter: " + ex.Message);
        //        return null;
        //    }
        //}
        public string AddReporter(string name)
        {
            Reporters reporter = new Reporters(name);

            try
            {
                using (MySqlConnection conn = mnd.CreateConnection())
                {
                    conn.Open();

                    // 1. Insert into people if not exists
                    string peopleQuery = @"
                INSERT IGNORE INTO people (codeName, name)
                VALUES (@codeName, @name);";

                    using (MySqlCommand cmd = new MySqlCommand(peopleQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@codeName", reporter.ReporterCode);
                        cmd.Parameters.AddWithValue("@name", reporter.ReporterName);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Insert into reporters if not exists
                    string reporterQuery = @"
                INSERT IGNORE INTO reporters (codeName)
                VALUES (@codeName);";

                    using (MySqlCommand cmd = new MySqlCommand(reporterQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@codeName", reporter.ReporterCode);
                        cmd.ExecuteNonQuery();
                    }
                }

                return reporter.ReporterCode;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error in AddReporter: " + ex.Message);
                return null;
            }
        }


        public bool CheckIfRecruit(string reporterCode)
        {
            try
            {
                using (MySqlConnection cone = mnd.CreateConnection())
                {
                    cone.Open();

                    string query = @"
                SELECT 
                    COUNT(*) AS ReportCount,
                    AVG(CHAR_LENGTH(text)) AS AvgLength
                FROM reports
                WHERE reporterCode = @code";

                    using (MySqlCommand cmd = new MySqlCommand(query, cone))
                    {
                        cmd.Parameters.AddWithValue("@code", reporterCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int count = Convert.ToInt32(reader["ReportCount"]);
                                double avg = reader["AvgLength"] != DBNull.Value ? Convert.ToDouble(reader["AvgLength"]) : 0;

                                if (count >= 10 && avg >= 100)
                                {
                                    reader.Close(); // Important: close reader before executing another command on same connection

                                    string updateQuery = @"UPDATE reporters SET isAgent = TRUE WHERE codeName = @code";
                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, cone))
                                    {
                                        updateCmd.Parameters.AddWithValue("@code", reporterCode);
                                        updateCmd.ExecuteNonQuery();
                                    }

                                    Console.WriteLine("Thank you very much, we will contact you.");
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in CheckIfRecruit: " + ex.Message);
            }

            return false;
        }
    }
}

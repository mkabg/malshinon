using Models;
using MySql.Data.MySqlClient;
using System;
using alertDAL;

namespace DAL
{
    public class TargetDAL
    {
        private readonly MalshinonDAL mnd;
        private readonly AlertDAL atd;

        public TargetDAL(MalshinonDAL mnd, AlertDAL atd)
        {
            this.mnd = mnd;
            this.atd = atd;
        }

        //public string AddTarget(string input)
        //{
        //    Targets target = new Targets(input);
        //    try
        //    {
        //        using (MySqlConnection cone = mnd.CreateConnection())
        //        {
        //            cone.Open();
        //            string query = @"INSERT INTO targets (name, codeName)
        //                            VALUES (@name, @codeName)";

        //            using (MySqlCommand cmd = new MySqlCommand(query, cone))
        //            {
        //                cmd.Parameters.AddWithValue(@"name", target.TargetName);
        //                cmd.Parameters.AddWithValue(@"codeName", target.TargeCode);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        return target.TargeCode;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine("Database error in AddReporter: " + ex.Message);
        //        return null;
        //    }
        //}
        public string AddTarget(string name)
        {
            Targets target = new Targets(name);

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
                        cmd.Parameters.AddWithValue("@codeName", target.TargeCode);
                        cmd.Parameters.AddWithValue("@name", target.TargetName);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Insert into targets if not exists
                    string targetQuery = @"
                INSERT IGNORE INTO targets (codeName)
                VALUES (@codeName);";

                    using (MySqlCommand cmd = new MySqlCommand(targetQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@codeName", target.TargeCode);
                        cmd.ExecuteNonQuery();
                    }
                }

                return target.TargeCode;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error in AddTarget: " + ex.Message);
                return null;
            }
        }


        public void CheckIfDangerous(string targetCode)
        {
            try
            {
                using (MySqlConnection cone = mnd.CreateConnection())
                {
                    cone.Open();

                    // 1. Check for total number of reports
                    string countQuery = @"SELECT COUNT(*) FROM reports WHERE targetCode = @targetCode";
                    using (MySqlCommand countCmd = new MySqlCommand(countQuery, cone))
                    {
                        countCmd.Parameters.AddWithValue("@targetCode", targetCode);
                        int totalReports = Convert.ToInt32(countCmd.ExecuteScalar());

                        if (totalReports >= 20)
                        {
                            Console.WriteLine("⚠️ WARNING: Target has at least 20 reports.");
                            atd.InsertAlert(targetCode, "many_reports", "Target has 20 or more reports overall.");
                        }
                    }

                    // 2. Check for 3 reports in the last 15 minutes
                    string recentQuery = @"
                SELECT COUNT(*) 
                FROM reports 
                WHERE targetCode = @targetCode 
                AND timestamp >= NOW() - INTERVAL 15 MINUTE";

                    using (MySqlCommand recentCmd = new MySqlCommand(recentQuery, cone))
                    {
                        recentCmd.Parameters.AddWithValue("@targetCode", targetCode);
                        int recentReports = Convert.ToInt32(recentCmd.ExecuteScalar());

                        if (recentReports >= 3)
                        {
                            Console.WriteLine("⚠️ WARNING: Target has 3 or more reports in the last 15 minutes.");
                            atd.InsertAlert(targetCode, "burst_reports", "Target reported 3 or more times in last 15 minutes.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in CheckIfDangerous: " + ex.Message);
            }
        }
    }
}

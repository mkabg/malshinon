using DAL;
using MySql.Data.MySqlClient;
using System;

namespace alertDAL
{
    public class AlertDAL
    {
        private readonly MalshinonDAL mnd;

        public AlertDAL(MalshinonDAL mnd)
        {
            this.mnd = mnd;
        }

        public void InsertAlert(string targetCode, string alertType, string description)
        {
            try
            {
                using (MySqlConnection cone = mnd.CreateConnection())
                {
                    cone.Open();
                    string insertQuery = @"INSERT INTO alerts (targetCode, alertType, description)
                                           VALUES (@targetCode, @alertType, @description)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, cone))
                    {
                        cmd.Parameters.AddWithValue("@targetCode", targetCode);
                        cmd.Parameters.AddWithValue("@alertType", alertType);
                        cmd.Parameters.AddWithValue("@description", description);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in InsertAlert: " + ex.Message);
            }
        }
    }
}
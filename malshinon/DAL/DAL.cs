using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class DAL
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
    }
}

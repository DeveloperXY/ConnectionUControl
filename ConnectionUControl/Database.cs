using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ConnectionUControl
{
    class Database
    {
        private string connectionString;
        private MySqlConnection conn;

        public Database(string connStr)
        {
            connectionString = connStr;
            conn = null;

            openConnection();
        }

        public void openConnection()
        {
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void closeConnection()
        {
            if (conn != null)
                conn.Close();
        }
    }
}

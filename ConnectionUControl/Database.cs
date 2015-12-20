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
                // Display exception message, then bubble it up to the main container to handle it
                Console.WriteLine("From the connection control: " + ex.ToString());
                throw ex;
            }
        }

        public void closeConnection()
        {
            if (conn != null)
                conn.Close();
        }
    }
}

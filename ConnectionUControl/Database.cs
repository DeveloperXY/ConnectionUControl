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
        // The name of the database that we're actually connected to
        public string dbName { get; set; }

        public Database(string connStr)
        {
            connectionString = connStr;
            conn = null;
            dbName = "";
        }

        // Opens a connection to the specified database
        public void openConnection(string databaseName) 
        {
            try
            {
                if(conn == null)
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    dbName = databaseName;
                }
                else
                {
                    // If the database is not open, re-open it
                    if(! isConnectionOpened())
                    {
                        conn.Close();
                        conn.Open();
                        dbName = databaseName;
                    }
                    else
                    {
                        // Connection is already open
                        throw new ConnectionAlreadyOpenedException();
                    }
                }
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

        public Boolean isConnectionOpened()
        {
            return conn.State == ConnectionState.Open;
        }
    }

    class ConnectionAlreadyOpenedException : Exception
    {
        public override string ToString()
        {
            return "Connection to database is already open.";
        }
    }
}

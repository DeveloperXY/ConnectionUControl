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
        public MySqlConnection conn;
        // The name of the database that we're actually connected to
        public string dbName { get; set; }

        // The list of the tables that this database contains
        private List<string> tables;

        public Database(string connStr)
        {
            connectionString = connStr;
            conn = null;
            dbName = "";
            tables = new List<string>();
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
                    getTables();
                }
                else
                {
                    // If the database is not open, re-open it
                    if(! isConnectionOpened())
                    {
                        conn.Close();
                        conn.Open();
                        dbName = databaseName;
                        getTables();
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

        private void getTables()
        {
            string cmd = "SHOW TABLES FROM " + dbName;
            MySqlCommand command = new MySqlCommand(cmd, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                tables.Add(reader.GetString(0));
            }

            reader.Close();
        }

        // Returns a List<string> of the current database's table names
        public List<string> getTableNames()
        {
            return tables;
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

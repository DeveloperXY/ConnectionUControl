using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.VisualBasic;

namespace ConnectionUControl
{
    public partial class UserControl1: UserControl
    {
        private Database database;
        public UserControl1()
        {
            InitializeComponent();

            database = new Database("server=localhost;userid=root;password=;database=connControl");
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connectToDatabase();
        }

        public void connectToDatabase()
        {
            try
            {
                database.openConnection();
                MessageBox.Show("Connection successful",
                    "Connection successfully established to the database.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                 MessageBox.Show("Unable to establish a connection to the database: " + ex.Message,
                    "Connection to database failed",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
            }
            catch(ConnectionAlreadyOpenedException ex)
            {
                 MessageBox.Show(ex.ToString(),
                    "Connection already open",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to establish a connection to the database.",
                    "Connection to database failed",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
            }
        }
    }
}

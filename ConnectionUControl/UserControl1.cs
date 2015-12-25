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

namespace ConnectionUControl
{
    public partial class UserControl1: UserControl
    {
        private Database database;
        public UserControl1()
        {
            InitializeComponent();

            // Hide some UI components at startup
            connectBtn.Enabled = false;
            dbInputPanel.Visible = false;
            connectedLabel.Visible = false;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connectToDatabase();
        }

        public void connectToDatabase()
        {
            try
            {
                // Retrieve the database name from the user's input
                string databaseName = dbTextBox.Text.ToLower();

                if(database != null)
                {
                    // Retrieve the name of the database that we're already connected to
                    string connectedTodb = database.dbName;
                    if (databaseName.Equals(connectedTodb))
                        throw new ConnectionAlreadyOpenedException();
                }

                // Connection to new database, proceed
                database = new Database("server=localhost;userid=root;password=;database=" + databaseName);

                database.openConnection(databaseName);
                
                // UI thing
                dbInputPanel.Visible = false;
                connectLabel.Visible = false;
                connectedLabel.Text =  "Successfully connected to" +
                    "\"" + databaseName + "\"";
                connectedLabel.Visible = true;

                populateTablesCombobox();
            }
            catch (MySqlException ex)
            {
                 MessageBox.Show(ex.Message,
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
                MessageBox.Show("Unknow error occured while trying to connect to database. Reason: " + ex.Message,
                    "Connection to database failed",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
            }
        }

        private void populateTablesCombobox()
        {
            // Empty the tables' combobox
            tablesCombobox.Items.Clear();

            tablesCombobox.Items.AddRange(database.getTableNames().ToArray());
            tablesCombobox.SelectedIndex = 0;
        }

        private void dbTextBox_TextChanged(object sender, EventArgs e)
        {
            if (dbTextBox.Text == "")
                connectBtn.Enabled = false;
            else
                connectBtn.Enabled = true;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectedLabel.Visible = false;
            dbInputPanel.Visible = true;
            connectLabel.Visible = true;
        }

        private void tablesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmd = "SELECT * FROM " + tablesCombobox.SelectedItem;
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd, database.conn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            tableDataGrid.DataSource = dt;
        }
    }
}

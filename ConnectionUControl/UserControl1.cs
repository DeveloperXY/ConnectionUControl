using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try
            {
                database.openConnection();
                MessageBox.Show("Connection successfully established to database.",
                    "Connection successful",
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

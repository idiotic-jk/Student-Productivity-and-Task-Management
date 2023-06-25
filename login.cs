using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace TESTBINFU
{

    public partial class login : Form
    {
        public static string z;
        public login()
        {
            InitializeComponent();
        }

        private void bun_loglog_but_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Table99  where UN='" + bun_logUID_txb.Text + "'and PASS='" + bun_logpass_txb.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
              Form1 f1 = new Form1();
                z = bun_logUID_txb.Text;
              f1.Show();
              this.Hide();
            }
            else
            {
                MessageBox.Show("iv");
            }

        }

        private void bun_logex_but_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

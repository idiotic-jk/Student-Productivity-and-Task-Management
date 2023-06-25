using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTBINFU
{
    public partial class attend : UserControl
    {
        public attend()
        {
            InitializeComponent();
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;");
        }
        private void subtskload()
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Sub_tb", cn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DataRow topItem = dtbl.NewRow();
                gun_sub_cbx.ValueMember = "Sub_no";
                gun_sub_cbx.DisplayMember = "Subject";
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                gun_sub_cbx.DataSource = dtbl;
            }
        }
        
        private void monthload()
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT distinct(Datename(month,[Date])) as Month , (Month([Date])) as ma  from attend;", cn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DataRow topItem = dtbl.NewRow();                
                gun_month_cmbox.ValueMember = "ma";
                gun_month_cmbox.DisplayMember = "Month";
                topItem[0] = "ALL";
                topItem[1] = "0";
                dtbl.Rows.InsertAt(topItem, 0);
                
                gun_month_cmbox.DataSource = dtbl;
                foreach (DataRow dataRow in dtbl.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }

        private void gun_sub_cbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cal_DateChanged(object sender, DateRangeEventArgs e)
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM attend where [Date]=@a", sqlCon);
                cmd.Parameters.AddWithValue("@a", e.Start.Date);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd) ;
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                bun_ttcurtt_dgv.DataSource = dtbl;

            }
        }

        private void gun_sub_cbx_Click(object sender, EventArgs e)
        {
            subtskload();
        }

        private void gun_month_cmbox_Click(object sender, EventArgs e)
        {
            monthload();
        }

        private void gun_month_cmbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bun_chk_btn_Click(object sender, EventArgs e)
        {

        }
    }
}

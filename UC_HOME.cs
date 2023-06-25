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
using System.Globalization;
using Bunifu.UI.WinForms.BunifuButton;

namespace TESTBINFU
{
    public partial class UC_HOME : UserControl
    {
        DateTime dt = DateTime.Now;
        
        SqlCommand cmd = new SqlCommand();
        public UC_HOME()
        {
            InitializeComponent();
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;");
        }

        private SqlConnection GetConnection2()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\source\repos\Sankranti14\Wincat\Wincat\Properties\z.mdf;Integrated Security=True;");
        }
        


        private void LABLEHELLO_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuRating1_ValueChanged(object sender, Bunifu.UI.WinForms.BunifuRating.ValueChangedEventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
           
        }
        private void bindmeet(BunifuButton2 b,int i)
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(" SELECT Name from cl where Id='"+i+"'", cn);
                cmd.Parameters.AddWithValue("@a", System.DateTime.Now.Date.DayOfWeek.ToString());
                cn.Open();
                using (SqlDataReader read = (cmd.ExecuteReader()))
                {
                    if(read.Read())
                    {
                        b.Text = Convert.ToString(read.GetValue(0));                       

                    }
                }
            }
        }
        private void btnamw()
        {
            bindmeet(bunifuButton21,1);
            bindmeet(bunifuButton23,2);
            bindmeet(bunifuButton24,3);
            bindmeet(bunifuButton26,4);
            bindmeet(bunifuButton27,5);
            bindmeet(bunifuButton28,6);
        }
        public void classlink(BunifuButton2 b)        
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(" SELECT link from cl where Name=@a", cn);
                cmd.Parameters.AddWithValue("@a", b.Text);
                cn.Open();
                using (SqlDataReader read = (cmd.ExecuteReader()))
                {
                    if (read.Read())
                        if(Convert.ToString(read.GetValue(0))!="")
                        System.Diagnostics.Process.Start(Convert.ToString(read.GetValue(0)));                        
                }
            }
        }
        private void bunifuButton21_Click_1(object sender, EventArgs e)
        {
            classlink(bunifuButton21);
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            classlink(bunifuButton23);

        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            classlink(bunifuButton24);
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            classlink(bunifuButton26);
        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            classlink(bunifuButton27);
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            classlink(bunifuButton28);
        }

        private void bunifuLabel14_Click(object sender, EventArgs e)
        {
            bunifuLabel14.Text = System.DateTime.Now.ToString();
        }

        private void UC_HOME_Load(object sender, EventArgs e)
        {
            bunifuLabel14.Text = System.DateTime.Now.ToString();
            bunifuPanel3.Visible = false;
            loadatt();ttload();hometskload();
            bindstate();loadsub();btnamw();
        }
        private void tskper()
        {
            using (SqlConnection sqlCon = GetConnection2())
            {
                SqlCommand cmd = new SqlCommand("select count([done]) tasktbl set [Done]=@a where Task_id=@b", sqlCon);


            }
        }
        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = GetConnection2())
            {
                if(bun_hometsk_dgv.CurrentRow!=null)
                {
                    SqlCommand cmd = new SqlCommand("update tasktbl set [Done]=@a where Task_id=@b", sqlCon);
                    cmd.Parameters.AddWithValue("a", Convert.ToBoolean(bun_hometsk_dgv.CurrentRow.Cells["chkdone"].Value));
                    cmd.Parameters.AddWithValue("b", Convert.ToInt32((bun_hometsk_dgv.CurrentRow.Cells["Task_id"].Value)));
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    
                }
                
            }


            //bunifuPanel3.Visible = true;
            //bunifuPanel3.BringToFront();

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            bunifuPanel3 .Visible = false;
            bunifuPanel3.SendToBack ();
        }

        public bool checkexist(SqlCommand cmd)
        {
            SqlConnection con = GetConnection();
            cmd.Connection = con;
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                con.Close(); return true;
            }
            else
            {
                con.Close(); return false;
            }

        }
        private void loadatt()
        {

            using (SqlConnection cn = GetConnection())
            {
                cmd.CommandText = ("Select * From attend Where [Date] = @a ");
                cmd.Parameters.AddWithValue("@a", System.DateTime.Now.Date);


                if (checkexist(cmd) == false)
                {
                    SqlCommand cmd = new SqlCommand("Insert into attend  values(@a,0,0,0,0,0,0)", cn);
                    
                    cmd.Parameters.AddWithValue("@a", System.DateTime.Now.Date);
                    cn.Open();
                    cmd.ExecuteNonQuery();cn.Close();

                }
                else
                {
                    
                }
            }
        }
        private void bindstate()
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("select * from attend where [Date]=@a", cn);
                cmd.Parameters.AddWithValue("@a", System.DateTime.Now.Date);cn.Open();
                using (SqlDataReader read = (cmd.ExecuteReader()))
                {
                    if(read.Read())
                    {
                        gun_cb1.Checked = Convert.ToBoolean(read.GetValue(1));
                        gun_cb2.Checked = Convert.ToBoolean(read.GetValue(2));
                        gun_cb3.Checked = Convert.ToBoolean(read.GetValue(3));
                        gun_cb4.Checked = Convert.ToBoolean(read.GetValue(4));
                        gun_cb5.Checked = Convert.ToBoolean(read.GetValue(5));
                        gun_cb6.Checked = Convert.ToBoolean(read.GetValue(6));

                    }
                }
            }

        }
        private void upatt()
        {
            using (SqlConnection cn = GetConnection())
            {
                
                SqlCommand cmd = new SqlCommand("update attend set  [9:45]= @x, [10:45]= @b , [11:45]= @c , [1:30]=@d , [2:30]=@e , [3:30]=@f where [Date]= (@g)", cn);
                cmd.Parameters.AddWithValue("@x", gun_cb1.CheckState);
                cmd.Parameters.AddWithValue("@b", gun_cb2.CheckState);
                cmd.Parameters.AddWithValue("@c", gun_cb3.CheckState);
                cmd.Parameters.AddWithValue("@d", gun_cb4.CheckState);
                cmd.Parameters.AddWithValue("@e", gun_cb5.CheckState);
                cmd.Parameters.AddWithValue("@f", gun_cb6.CheckState);
                cmd.Parameters.AddWithValue("@g", System.DateTime.Now.Date);
                cn.Open();                
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

            }
        }

        private void loadsub()
        {
            using (SqlConnection cn = GetConnection())
            {
                //SqlCommand cmd = new SqlCommand(" SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMN WHERE TABLE_NAME = N'attend'", cn);
                SqlCommand cmd = new SqlCommand(" SELECT * from ttexp where [day]=@a", cn);
                cmd.Parameters.AddWithValue("@a", System.DateTime.Now.Date.DayOfWeek.ToString());
                cn.Open();
                using (SqlDataReader read = (cmd.ExecuteReader()))
                {
                    if (read.Read())
                    {
                        gun_cb1.Text = Convert.ToString(read.GetValue(2));
                        gun_cb2.Text = Convert.ToString(read.GetValue(3));
                        gun_cb3.Text = Convert.ToString(read.GetValue(4));
                        gun_cb4.Text = Convert.ToString(read.GetValue(5));
                        gun_cb5.Text = Convert.ToString(read.GetValue(6));
                        gun_cb6.Text = Convert.ToString(read.GetValue(7));

                    }
                }
            }

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            upatt(); MessageBox.Show("Attendence saved");
        }

        private void hometskload()
        {
            using (SqlConnection cn = GetConnection2())
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from cat", cn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DataRow topItem = dtbl.NewRow();
                gun_hometsk_cmbox.ValueMember = "Cat_ID";
                gun_hometsk_cmbox.DisplayMember = "Category";
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                gun_hometsk_cmbox.DataSource = dtbl;
            }
        }

        private void gun_hometsk_cmbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gun_hometsk_cmbox.SelectedIndex != 0)
            {
                using (SqlConnection sqlCon = GetConnection2())
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT [Task_id],[Desc],[Done],End_date from tasktbl  where Category_ID='" + gun_hometsk_cmbox.SelectedValue + "'", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    bun_hometsk_dgv.DataSource = dtbl;

                }
            }

        }
        private void ttload()
        {
            using (SqlConnection cn = GetConnection())
            {          
                SqlDataAdapter sqlDa = new SqlDataAdapter("select day  from ttexp", cn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DataRow topItem = dtbl.NewRow();
                gun_tt_cmbx.ValueMember = "day";
                gun_tt_cmbx.DisplayMember = "day";
                topItem[0] = null;

                dtbl.Rows.InsertAt(topItem, 0);
                gun_tt_cmbx.DataSource = dtbl;
            }
        }

        private void gun_tt_cmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
           if( gun_tt_cmbx.SelectedIndex!=0)
            {
                using (SqlConnection sqlCon = GetConnection())
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ttexp where day='"+gun_tt_cmbx.SelectedValue+"'", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    bun_ttday_dgc.DataSource = dtbl;

                }
            }
        }
        private void loadtskcmp()
        {

        }

        private void bun_ref_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = GetConnection2())
            {
                SqlCommand cmd = new SqlCommand("(select count([Done]) from tasktbl where [Done]=1)", cn); cn.Open();
                SqlCommand cmd1 = new SqlCommand("(select count([Done]) from tasktbl)", cn);

                using (SqlDataReader read = (cmd.ExecuteReader()))
                {
                    if (read.Read())
                    {
                        bunifuCircleProgress2.Value = Convert.ToInt32(read.GetValue(0));
                    }
                }
                using (SqlDataReader read = (cmd1.ExecuteReader()))
                {
                    if (read.Read())
                    {
                        bunifuCircleProgress1.Value = Convert.ToInt32(read.GetValue(0));
                    }
                }
                //loadatt(); ttload(); hometskload();
                //bindstate(); loadsub(); btnamw();
            }

        }
    }
}

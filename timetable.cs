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
    public partial class timetable : UserControl
    {
        public timetable()
        {
            InitializeComponent();
            
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;");
        }
        private void distt( )
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ttexp", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                bun_ttcurtt_dgv.DataSource = dtbl;
                
            }
        }
        private void disuptt()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ttexp  ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                bun_updtt_dgv.DataSource = dtbl;
            }
        }
        //private void loadsub(DataGridViewComboBoxColumn dc)
        //{
        //    using (SqlConnection sqlCon = GetConnection())
        //    {
               
        //        sqlCon.Open();
        //        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Sub_tb", sqlCon);
        //        DataTable dtbl = new DataTable();
        //        sqlDa.Fill(dtbl);
        //        dc.ValueMember = "Subject";
        //        dc.DisplayMember = "Subject";
        //        DataRow topItem = dtbl.NewRow();
        //        topItem[0] = 0;
        //        topItem[1] = "-Select-";
        //        dtbl.Rows.InsertAt(topItem, 0);
        //        dc.DataSource = dtbl;
        //    }
        //}
        //private void subload()
        //{
        //    loadsub(up_9);loadsub(up_10);loadsub(up_11);loadsub(up_1);loadsub(up_2);loadsub(up_3);
        //}
        private void loaddaycmx()
        {
            using (SqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT day FROM ttexp", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                gun_hometsk_cmbox.ValueMember = "day";
                gun_hometsk_cmbox.DisplayMember = "day";
                DataRow topItem = dtbl.NewRow();
                topItem[0] = "-Select-";
               // topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                gun_hometsk_cmbox.DataSource = dtbl;
            }
        }
        private void loadsubcmx(ComboBox dc)
        {
            using (SqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Sub_tb", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dc.ValueMember = "Subject";
                dc.DisplayMember = "Subject";
                DataRow topItem = dtbl.NewRow();
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                dc.DataSource = dtbl;
            }
        }
        private void subloadcmdx()
        {
            loaddaycmx(); loadsubcmx(gunacbx1); loadsubcmx(gunacbx2); loadsubcmx(gunacbx3); loadsubcmx(gunacbx4); loadsubcmx(gunacbx5); loadsubcmx(gunacbx6);
        }
        private void bunifuBtnsave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                if(gunacbx1.SelectedIndex!=0 && gunacbx2.SelectedIndex != 0 && gunacbx3.SelectedIndex != 0 && gunacbx4.SelectedIndex != 0 && gunacbx5.SelectedIndex != 0 && gunacbx6.SelectedIndex != 0 && gun_hometsk_cmbox.SelectedIndex!=0)
                {

                    try
                    {
                        using (SqlCommand command = new SqlCommand("update  ttexp set [9:45]= @a, [10:45]= @b,[11:45]= @c, [1:30]=@d,[2:30]=@e,[3:30]=@f where day='"+gun_hometsk_cmbox.SelectedValue+ "'", sqlCon))
                        {
                            command.Parameters.AddWithValue("@a", gunacbx1.SelectedValue);
                            command.Parameters.AddWithValue("@b", gunacbx2.SelectedValue);
                            command.Parameters.AddWithValue("@c", gunacbx3.SelectedValue);
                            command.Parameters.AddWithValue("@d", gunacbx4.SelectedValue);
                            command.Parameters.AddWithValue("@e", gunacbx5.SelectedValue);
                            command.Parameters.AddWithValue("@f", gunacbx6.SelectedValue);
                            command.CommandType = CommandType.Text;
                            MessageBox.Show("updated");
                            sqlCon.Open();
                            int result = command.ExecuteNonQuery();

                            // Check Error
                            if (result < 0)
                                Console.WriteLine("Error inserting data into Database!");
                        }

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "not saved");
                    }
                }
                
               
            }
        }

        //private void updtt()
        //{
        //    using (SqlConnection sqlCon = GetConnection())
        //    {
                
        //        DataGridViewRow dgvRow = bun_updtt_dgv.CurrentRow;
        //        String query = "update  ttexp set [9:45]= @a, [10:45]= @b,[11:45]= @c, [1:30]=@d,[2:30]=@e,[3:30]=@f where ttid='" + Convert.ToInt32(dgvRow.Cells["upttid"].Value) + "' ";

        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand(query, sqlCon))
        //            {


        //                command.Parameters.AddWithValue("@a", dgvRow.Cells["up_9"].Value);
        //                command.Parameters.AddWithValue("@b", dgvRow.Cells["up_10"].Value);
        //                command.Parameters.AddWithValue("@c", dgvRow.Cells["up_11"].Value);
        //                command.Parameters.AddWithValue("@d", dgvRow.Cells["up_1"].Value);
        //                command.Parameters.AddWithValue("@e", dgvRow.Cells["up_2"].Value);
        //                command.Parameters.AddWithValue("@f", dgvRow.Cells["up_3"].Value);
        //                command.CommandType = CommandType.Text;

        //                sqlCon.Open();
        //                int result = command.ExecuteNonQuery();

        //                // Check Error
        //                if (result < 0)
        //                    Console.WriteLine("Error inserting data into Database!");
        //            }

        //        }

        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "er");
        //        }
        //    }
        //}
        private void bun_updtt_dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(bun_ttcurtt_dgv!=null)
            {
                //updtt();MessageBox.Show("er");
            }
            
        }
        private void bun_ttsave_but_Click(object sender, EventArgs e)
        {
            //updtt();
        }


        private void bun_ttupd_dgv_Click(object sender, EventArgs e)
        {
            distt();
        }
        private void discat()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Sub_tb ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                bun_ttsub_dgv.DataSource = dtbl;
            }
        }
        private void bun_ttsub_dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (bun_ttsub_dgv.CurrentRow != null)
            {
                using (SqlConnection sqlCon = GetConnection())
                {

                    sqlCon.Open();
                    DataGridViewRow dgvRow = bun_ttsub_dgv.CurrentRow;
                    SqlCommand sqlCmd = new SqlCommand("subio", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure; //MessageBox.Show("eeee");
                    try
                    {
                        if (dgvRow.Cells["add_SUBid"].Value == DBNull.Value)//Insert
                        {
                            sqlCmd.Parameters.AddWithValue("@Sub_no", 0);
                           // MessageBox.Show("eeee");                            
                        }                       

                        else//update
                            sqlCmd.Parameters.AddWithValue("@Sub_no", Convert.ToInt32(dgvRow.Cells["add_SUBid"].Value));
                        sqlCmd.Parameters.AddWithValue("@Subject", dgvRow.Cells["add_SUBname"].Value == DBNull.Value ? "" : dgvRow.Cells["add_SUBname"].Value.ToString());
                        sqlCmd.ExecuteNonQuery();
                        discat();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ex");
                    }
                }
            }
        }

        private void bun_ttsub_dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (bun_ttsub_dgv.CurrentRow.Cells["add_SUBid"].Value != DBNull.Value)
            {
                if (MessageBox.Show("Are You Sure to Delete this Record ?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = GetConnection())
                    {
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("Delete from Sub_tb where Sub_no= '" + Convert.ToInt32(bun_ttsub_dgv.CurrentRow.Cells["add_SUBid"].Value) + "' ", sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            //bunifuPanel1.BringToFront(); subload();disuptt();
            bunifuPanel4.BringToFront(); subloadcmdx();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            distt();
            bunifuPanel2.BringToFront();
        }

        private void timetable_Load(object sender, EventArgs e)
        {
            distt();
            discat();
            disuptt();subloadcmdx();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            bunifuPanel3.BringToFront();
        }

        private void bun_updtt_dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.Exception.Message=="DataGridViewComboBoxCell value is not valid")
            {
                object v = bun_updtt_dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)bun_updtt_dgv.Columns[e.ColumnIndex]).Items.Contains(v))
                {
                    ((DataGridViewComboBoxColumn)bun_updtt_dgv.Columns[e.ColumnIndex]).Items.Add(v);
                    e.ThrowException = false;

                }
            }
        }

        
    }
}

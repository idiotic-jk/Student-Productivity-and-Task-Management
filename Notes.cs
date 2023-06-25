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
using System.IO;

namespace TESTBINFU
{
    public partial class Notes : UserControl
    {
        public Notes()
        {
            InitializeComponent();
        }
        private void Notes_Load(object sender, EventArgs e)
        {
            loaddata();
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;");
        }

        private void funsavefile(string filepath)
        {
            try
            { using (Stream stream = File.OpenRead(filepath))
             {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                                        

                var fi = new FileInfo(filepath);
                string extn = fi.Extension;
                string name = fi.Name;
                string query = "INSERT INTO DOC (Filename,Data,Extension)VALUES(@name,@data,@extn)";

                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@extn", SqlDbType.Char).Value = extn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

              }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "msg");
            }
            
        }
        private void loaddata()
        {
            using (SqlConnection cn = GetConnection())
            {
                string query = "SELECT ID,Filename,Extension FROM doc";
                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    guna_notes_dgv.DataSource = dt;
                }

            }

        }
        private void OpenFile(int id)
        {
            using (SqlConnection cn = GetConnection())
            {
                string query = "SELECT Data,Filename,Extension FROM Doc WHERE ID=@id";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                cn.Open();
                var reader = cmd.ExecuteReader();
                try
                {
                   if (reader.Read())
                    {
                    var name = reader["FileName"].ToString();
                    var data = (byte[])reader["data"];
                    var extn = reader["Extension"].ToString();
                    var newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss")) + extn;
                    File.WriteAllBytes(newFileName, data);
                    System.Diagnostics.Process.Start(newFileName);

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "msg");
                }
                
            }

        }
        private void searchfun()
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    string query = "select ID,Filename,Extension from DOC where Filename like '" + guna_not_txt.Text.Trim() + "%'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    guna_notes_dgv.DataSource = dt;
                    cn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "er");
            }

        }

        private void bun_not_brow_but_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.ShowDialog();
                guna_not_txt.Text = dlg.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }

        }

        private void bun_not_save_but_Click(object sender, EventArgs e)
        {
            funsavefile(guna_not_txt.Text );
            MessageBox.Show("saved");
            loaddata();
        }

        private void bun_not_open_but_Click(object sender, EventArgs e)
        {
            var selectedrow = guna_notes_dgv.SelectedRows;
            foreach (var row in selectedrow)
            {
                int id = (int)((DataGridViewRow)row).Cells[0].Value;
                OpenFile(id);
            }
        }

        private void bun_not_search_but_Click(object sender, EventArgs e)
        {
            searchfun();
        }

        private void guna_not_txt_TextChanged(object sender, EventArgs e)
        {
            searchfun();
        }

        private void guna_notes_dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (guna_notes_dgv.CurrentRow.Cells["Id"].Value != DBNull.Value)
            {
                if (MessageBox.Show("Are You Sure to Delete this Record ?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = GetConnection())
                    {
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("Delete from DOC where Id= '" + Convert.ToInt32(guna_notes_dgv.CurrentRow.Cells[0].Value) + "' ", sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        //sqlCmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(dgvEmployee.CurrentRow.Cells["txtEmployeeID"].Value));
                        sqlCmd.ExecuteNonQuery();
                        loaddata();
                    }
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;
        }
    }
}

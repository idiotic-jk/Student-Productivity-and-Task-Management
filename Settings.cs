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
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;");
        }

        private void passreset()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                try
                { 
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Table99 where PASS ='" +bun_oldpass_txb.Text + "'", sqlCon);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);//MessageBox.Show("e");
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        SqlCommand cmd = new SqlCommand("update Table99 set Pass='"+bun_newpass_txb.Text+"' where Pass='"+ bun_oldpass_txb.Text + "' ",sqlCon);
                        sqlCon.Open(); cmd.ExecuteNonQuery();
                        MessageBox.Show("reset done");
                    }
                    else
                    {
                        MessageBox.Show("No matching password found");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "er pass reset");
                }
            }
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            passreset();
            
        }

        private void bun_loglog_but_Click(object sender, EventArgs e)
        {
            passwordPanel.BringToFront();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            ppPanel.BringToFront();

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            classlinkspanel.BringToFront();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            aboutPanel1.BringToFront();
        }
        string s;
        private void funsavefile(string filepath)
        {
            try
            {
                using (Stream stream = File.OpenRead(filepath))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    var fi = new FileInfo(filepath);
                    string extn = fi.Extension;
                    string name = fi.Name;
                    
                    string query = "update Table99 set  [img_name]= @a, [imgf]= @c  ";
                    using (SqlConnection cn = GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.Add("@a", SqlDbType.VarChar).Value = name;
                        cmd.Parameters.Add("@c", SqlDbType.VarBinary).Value = buffer;
                       // cmd.Parameters.Add("@e", SqlDbType.Char);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "msg");
            }

        }
        private void button_browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg,*.jpeg,*.gif,*.png)|*.jpg,*.jpeg,*.gif,*.png|All files(*.*)|*.*", Multiselect = false })
            {
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    s = ofd.FileName;
                    bunifuPictureBox1.Image = Image.FromFile(s);

                }
               
            }
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            funsavefile(s);
            MessageBox.Show("saved");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadlink()
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("select Id from cl", cn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DataRow topItem = dtbl.NewRow();
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Id";
                topItem[0] = DBNull.Value;
                
                dtbl.Rows.InsertAt(topItem, 0);
                comboBox1.DataSource = dtbl;
            }
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text != "" || bunifuTextBox2.Text != "")
            {
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("update cl set  [Name]= @x, [link]= @b  where [Id]= (@g)", cn);
                    cmd.Parameters.AddWithValue("@x", bunifuTextBox1.Text);
                    cmd.Parameters.AddWithValue("@b", bunifuTextBox2.Text);
                    cmd.Parameters.AddWithValue("@g", comboBox1.SelectedValue);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    bunifuTextBox1.Clear();
                    bunifuTextBox2.Clear();
                }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            loadlink();
        }

       
    }
}

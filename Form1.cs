using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTBINFU
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            //Loadfrom(new TASKS());
            tasks1.Visible = true;
            tasks1.BringToFront();
            uC_HOME1.Visible = false;
            notes1.Visible = false;
        }
        //#region hello
        //public void Loadfrom(object Form)
        //{
        //    if(this.bunifuPanel1.Controls .Count > 0)
        //        this.bunifuPanel1 .Controls.RemoveAt(0);
        //    Form f = Form as Form;
        //    f.TopLevel = false ;
        //    f.Dock =DockStyle.Fill;
        //    this.bunifuPanel1.Controls.Add(f);
        //    this.bunifuPanel1.Tag = f;
        //    f.Show();

        //}
        //#endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qDataSet.Table99' table. You can move, or remove it, as needed.
            this.table99TableAdapter.Fill(this.qDataSet.Table99);

            uC_HOME1.BringToFront();
            notes1.Visible = false;
            tasks1.Visible = false;
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            // Loadfrom(new UC_HOME());
            uC_HOME1.Visible = true;
            uC_HOME1.BringToFront();
            notes1.Visible = false;
            tasks1.Visible = false;

        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\OneDrive\Documents\q.mdf;Integrated Security=True;");
        }

        private void uC_HOME1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("select imgf from Table99", cn);cn.Open();
                SqlDataReader da = cmd.ExecuteReader();
                if(da.Read())
                {
                    byte[] img = (byte[])(da[0]);
                    if (img == null)
                        bunifuPictureBox1.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        bunifuPictureBox1.Image = Image.FromStream(ms);

                    }
                }
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        { }
            public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            notes1.Visible = true;
            notes1.BringToFront();
            uC_HOME1 .Visible = false;
            tasks1.Visible = false;
        }

        private void login1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            settings1.BringToFront();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            timetable1.BringToFront();
        }

        private void bun_cancle_but_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            attend1.BringToFront();

        }
    } 
    }
        
    


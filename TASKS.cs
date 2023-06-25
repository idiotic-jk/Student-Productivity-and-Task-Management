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
    public partial class TASKS : UserControl
    {
        public TASKS()
        {
            InitializeComponent();
        }

        private void TASKS_Load(object sender, EventArgs e)
        {
      
            disdgvcat();
            statup();           
            disdgvtsk();
            PopulatePositionComboBox();
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bharath\source\repos\Sankranti14\Wincat\Wincat\Properties\z.mdf;Integrated Security=True");
        }
        private void disdgvtsk()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT tasktbl.Task_id , tasktbl.[Desc] , tasktbl.Start_date , tasktbl.End_date ,Cat.Category ,tasktbl.Status FROM tasktbl ,Cat where Category_ID=Cat_id", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                bun_tskdis_dgv.DataSource = dtbl;
            }
        }
        public void statup()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                String c = "due", b = "pending";
                //MessageBox.Show("err");
                SqlCommand com = new SqlCommand("update tasktbl set [Status] = CASE WHEN @a >= [End_date] THEN @c  ELSE @b END", sqlCon);
                com.Parameters.AddWithValue("@a", System.DateTime.Now.Date);
                com.Parameters.AddWithValue("@b", b.ToString()) ;
                com.Parameters.AddWithValue("@c", c.ToString()) ;

                sqlCon.Open();
                com.ExecuteNonQuery();
            }
        }
        
        private void bun_tskdis_dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (bun_tskdis_dgv.CurrentRow.Cells["Task_id"].Value != DBNull.Value)
            {
                if (MessageBox.Show("Are You Sure to Delete this Record ?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = GetConnection())
                    {
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("Delete from tasktbl where Task_id= '" + Convert.ToInt32(bun_tskdis_dgv.CurrentRow.Cells["Task_id"].Value) + "' ", sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        //sqlCmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(dgvEmployee.CurrentRow.Cells["txtEmployeeID"].Value));
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;
        }
        private void PopulatePositionComboBox()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Cat", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                add_Category.ValueMember = "Cat_Id";
                add_Category.DisplayMember = "Category";
                DataRow topItem = dtbl.NewRow();
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                add_Category.DataSource = dtbl;
            }
        }
        private void savefuntsk()
        {
            using (SqlConnection sqlCon = GetConnection())
            {

                String query = "insert into tasktbl ( [Desc] , Start_date,End_date, [Category_ID],[Done]) values (@Desc,@Start_date,@End_date, @Category,'"+0+"')";
                DataGridViewRow dgvRow = bun_tskadd_dgv.CurrentRow;
                try
                {
                   using(SqlCommand command = new SqlCommand(query, sqlCon))
                    {  
                        command.Parameters.AddWithValue("@Desc", dgvRow.Cells["add_Desc"].Value.ToString());
                        command.Parameters.AddWithValue("@Start_date", Convert.ToDateTime(dgvRow.Cells["add_Start_Date"].Value).Date);
                        command.Parameters.AddWithValue("@End_date", Convert.ToDateTime(dgvRow.Cells["add_End_Date"].Value).Date);
                        command.Parameters.AddWithValue("@Category", dgvRow.Cells["add_Category"].Value);
                        command.CommandType = CommandType.Text;                        
                        sqlCon.Open();
                        int result = command.ExecuteNonQuery();
                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                }               
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "er");
                }
            }
        }

        private void bun_tsksavetsk_but_Click(object sender, EventArgs e)
        {
            savefuntsk();
        }


        private void disdgvcat()
        {
            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * from Cat", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                bun_tskcat_dgv.DataSource = dtbl;
            }
        }
        private void bun_tskcat_dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (bun_tskcat_dgv.CurrentRow.Cells["Cat_Id"].Value != DBNull.Value)
            {
                if (MessageBox.Show("Are You Sure to Delete this Record ?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = GetConnection())
                    {
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("Delete from Cat where Cat_Id= '" + Convert.ToInt32(bun_tskcat_dgv.CurrentRow.Cells["Cat_Id"].Value) + "' ", sqlCon);
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
        private void bun_tskcat_dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (bun_tskcat_dgv.CurrentRow != null)
            {
                using (SqlConnection sqlCon = GetConnection())
                {

                    sqlCon.Open();
                    DataGridViewRow dgvRow = bun_tskcat_dgv.CurrentRow;
                    SqlCommand sqlCmd = new SqlCommand("Procatiu", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        if (dgvRow.Cells["Cat_Id"].Value == DBNull.Value)//Insert
                        {
                            sqlCmd.Parameters.AddWithValue("@Cat_Id",0);
                           // MessageBox.Show("eeee");                            
                        }                       

                        else//update
                            sqlCmd.Parameters.AddWithValue("@Cat_Id", Convert.ToInt32(dgvRow.Cells["Cat_Id"].Value));
                        sqlCmd.Parameters.AddWithValue("@Category", dgvRow.Cells["Category"].Value == DBNull.Value ? "" : dgvRow.Cells["Category"].Value.ToString());
                        sqlCmd.ExecuteNonQuery();
                        disdgvcat();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ex");
                    }
                }
            }
        }


        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            disdgvtsk();
            panel1.Visible = true;
            panel1.BringToFront();
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel2.BringToFront();
            panel1.Visible = false;
            panel3.Visible = false;
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel3.BringToFront();
            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel4.BringToFront();
            panel2.Visible = false;
            panel3.Visible = false;
            panel1.Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        DateTimePicker dtp = new DateTimePicker();
        private void dtpclose(object sender, EventArgs e)
        {
            dtp.Visible = false;
        }
        private void dtptxt(object sender, EventArgs e)
        {
            bun_tskadd_dgv.CurrentCell.Value = dtp.Text.ToString();
        }
        private void bun_tskadd_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 2)
                {
                   // DateTimePicker dtp = new DateTimePicker();
                    bun_tskadd_dgv.Controls.Add(dtp);
                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.CustomFormat = "dd-MM-yyyy";
                    dtp.Visible = true;
                    Rectangle dc = bun_tskadd_dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtp.Size = new Size(dc.Width, dc.Height);
                    dtp.Location = new Point(dc.X, dc.Y);
                    dtp.TextChanged += new EventHandler(dtptxt);
                    dtp.CloseUp += new EventHandler(dtpclose);

                }
            }
            
        }
    }
}

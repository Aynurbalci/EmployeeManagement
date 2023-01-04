using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;



namespace EmployeeManagement
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {

        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt;
        string imgLocation = "";

        

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'rentCarDataSet.Employees' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.employeesTableAdapter.Fill(this.rentCarDataSet.Employees);
            // TODO: Bu kod satırı 'rentCarDataSet.Employees' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
           
            // TODO: Bu kod satırı 'rentCarDataSet.Employees' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
         

        }
        public void insert(string fileName, byte[] img)
        {
            try
            {
                
                using (conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \"C:\\Users\\aynur balcı\\Documents\\Calisanlar.mdb\""))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (cmd = new OleDbCommand("insert into employees(FullName,Email,Phone,Gender,Address,Photo)Values(@Name,@Email,@Phone,@Gender,@Address,@Picture)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", metroTextBox1.Text);
                        cmd.Parameters.AddWithValue("@Email", metroTextBox5.Text);
                        cmd.Parameters.AddWithValue("PhoneNumber", Convert.ToInt32(metroTextBox2.Text));
                        cmd.Parameters.AddWithValue("@Gender,", metroComboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Address", metroTextBox3.Text);
                        cmd.Parameters.AddWithValue("@Picture", img);
                        cmd.ExecuteNonQuery();
                    }
                }
                employeesTableAdapter.Update(this.rentCarDataSet.Employees);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            try
            {
                insert(imgLocation, ConvertImageToByte(pictureBox1.Image));

                employeesBindingSource.EndEdit();
                employeesTableAdapter.Update(this.rentCarDataSet.Employees);
                metroPanel1.Enabled = false;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                employeesBindingSource.ResetBindings(false);
            }
        }
        private void GetData()
        {

            conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \"C:\\Users\\aynur balcı\\Source\\Repos\\EmployeeManagement\\EmployeeManagement\\RentCar.mdb\"");
            dt = new DataTable();
            adapter = new OleDbDataAdapter("Select * from employees", conn);
            conn.Open();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

        }
        byte[] ConvertImageToByte(Image img)
        {
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;


        }
        public Image ConvertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "JPEG Dosyası |*.jpeg| JPEG Dosyası|*.jpg";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = false;
                ofd.Title = "JPEG Dosyası Seçiniz..";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string DosyaYolu = ofd.FileName;
                    // string DosyaAdi = ofd.SafeFileName;
                    pictureBox1.ImageLocation = DosyaYolu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void metroTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(metroTextBox4.Text))

                {
                    this.employeesTableAdapter.Fill(this.rentCarDataSet.Employees);
                    employeesBindingSource.DataSource = this.rentCarDataSet.Employees;
                    //dataGridView.DataSource = employeesBindingSource;
                }
                else
                {

                    var query = from o in this.rentCarDataSet.Employees
                                where o.Name.Contains(metroTextBox4.Text) || o.PhoneNumber.Contains(metroTextBox4.Text) || o.Email.Contains(metroTextBox4.Text) || o.Address.Contains(metroTextBox4.Text)
                                select o;
                    employeesBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();

                }

            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                metroPanel1.Enabled = true;
                metroTextBox1.Focus();
                this.rentCarDataSet.Employees.AddEmployeesRow(this.rentCarDataSet.Employees.NewEmployeesRow());
                employeesBindingSource.MoveLast();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                employeesBindingSource.ResetBindings(false);
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            metroPanel1.Enabled = true;
            metroTextBox1.Focus();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            metroPanel1.Enabled = false;
            employeesBindingSource.ResetBindings(false);
        }
        RentCarDataSetTableAdapters.EmployeesTableAdapter emp = new RentCarDataSetTableAdapters.EmployeesTableAdapter();
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            int empID = Convert.ToInt32(row.Cells[0].Value);
            emp.DeleteQuery(empID);
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you want to delete this row?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    employeesBindingSource.RemoveCurrent();

            }
        }

        //????
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }
        private void metroButton7_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.male = Convert.ToInt32(emp.CountGender("Male"));
            f3.female = Convert.ToInt32(emp.CountGender("Female"));
            f3.ShowDialog();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    metroTextBox1.Text = row.Cells[1].Value.ToString();
                    metroTextBox2.Text = row.Cells[2].Value.ToString();
                    metroTextBox3.Text = row.Cells[3].Value.ToString();
                    metroTextBox5.Text = row.Cells[4].Value.ToString();
                    metroComboBox1.Text = row.Cells[5].Value.ToString();
                }
            }
        }

        private void metroTextBox4_TextChanged(object sender, EventArgs e)
        {
            this.employeesBindingSource.Filter="Name like '%" + metroTextBox4.Text + "%'";
           
        }

        private void employeesBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            BindingSource bm =new BindingSource();
           // this.bm.Add(this.viewmode);
            
           /* BindingManagerBase bm = (BindingManagerBase) sender;
            if (bm.Current.GetType() != typeof(DataRowView)) return;

            DataRowView drv = (DataRowView)bm.Current;
            Console.Write("CurrentChanged");
            Console.Write(drv["Name"]);
            Console.WriteLine();*/
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
           
        }
    }
}

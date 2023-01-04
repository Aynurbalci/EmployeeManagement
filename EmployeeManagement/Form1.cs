using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'rentCarDataSet.Login' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.loginTableAdapter.Fill(this.rentCarDataSet.Login);
            // TODO: Bu kod satırı 'rentCarDataSet.Login' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.loginTableAdapter.Fill(this.rentCarDataSet.Login);

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(metroTextBox1.Text))
            {
                MessageBox.Show("Please Enter your name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                metroTextBox1.Focus();
                return;
            }
            try
            {
                RentCarDataSetTableAdapters.LoginTableAdapter user = new RentCarDataSetTableAdapters.LoginTableAdapter();
                RentCarDataSet.LoginDataTable dt = user.NamePassword(metroTextBox1.Text, metroTextBox2.Text);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(" You have been succesfully logged in", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 frm2 = new Form2();
                    frm2.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show(" Your name or password is incorrect.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                metroTextBox1.Focus();
        }

        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                metroButton1.PerformClick();
        }
    }
}

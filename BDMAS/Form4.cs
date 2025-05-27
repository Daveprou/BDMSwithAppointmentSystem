using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_donation.Resources
{
    public partial class Form4: Form
    {
        private int donorId;
        public Form4(int donorId)
        {
            InitializeComponent();
            this.donorId = donorId;
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5(donorId);
            frm5.Show();

            this.Hide();
        }

        private void btnEditProf1_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6(donorId);
            frm6.Show();

            this.Hide();
        }

        private void btnDonate1_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7(donorId);
            frm7.Show();

            this.Hide();
        }

        private void btnAbout1_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8(donorId);
            frm8.Show();

            this.Hide();
        }

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            this.Close();


            using (var loginForm = new LogIn())
            {

                loginForm.ClearLoginFields();



                var result = loginForm.ShowDialog();



                if (result == DialogResult.OK)
                {
                    this.Show();
                    return;
                }
            }
        }
    }
}

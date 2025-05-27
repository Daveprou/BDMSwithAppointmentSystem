using Blood_donation.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_donation
{
    public partial class Form8: Form
    {
        private int donorId;
        public Form8(int donorId)
        {
            InitializeComponent();
            this.donorId = donorId;
        }

        private void btnHome4_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4(donorId);
            frm4.Show();

            this.Hide();
        }

        private void btnProfile4_Click(object sender, EventArgs e)
        {
            Form5 home = new Form5(donorId);
            this.Hide();
            home.Show();
        }

        private void btnEditProf4_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6(donorId);
            frm6.Show();

            this.Hide();
        }

        private void btnDonate4_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7(donorId);
            frm7.Show();

            this.Hide();
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            LogIn loginForm = new LogIn();
            loginForm.Show();
            this.Close();
        }
    }
}

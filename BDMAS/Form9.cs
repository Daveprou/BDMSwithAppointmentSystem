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
    public partial class Form9: Form
    {
        public Form9()
        {
            InitializeComponent();
        }

 

        private void btnHomeAdmin_Click(object sender, EventArgs e)
        {
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();


            homeA1.Show();
            homeA1.BringToFront();
        }

        private void btnAddDonor_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();


            addDonorA1.Show();
            addDonorA1.BringToFront();
        }

        private void btnUpdateDonor_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();

            updateDonorA1.Show();
            updateDonorA1.BringToFront();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            updateDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();

            viewDonorA1.Show();
            viewDonorA1.BringToFront();
        }

        private void btnDonateBlood_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();

            donateBloodA1.Show();
            donateBloodA1.BringToFront();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();

            bloodStockA1.Show();
            bloodStockA1.BringToFront();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();

            bloodTransferA1.Show();
            bloodTransferA1.BringToFront();
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            viewPatientsA1.Hide();

            patientsA1.Show();
            patientsA1.BringToFront();
        }

        private void btnViewPatients_Click(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();

            viewPatientsA1.Show();
            viewPatientsA1.BringToFront();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LogIn Login = new LogIn();
            Login.Show();
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            addDonorA1.Hide();
            updateDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();



            homeA1.Show();
            homeA1.BringToFront();
        }

        private void updateDonorA1_Load(object sender, EventArgs e)
        {
            homeA1.Hide();
            addDonorA1.Hide();
            viewDonorA1.Hide();
            donateBloodA1.Hide();
            bloodStockA1.Hide();
            bloodTransferA1.Hide();
            patientsA1.Hide();
            viewPatientsA1.Hide();


            updateDonorA1.Show();
            updateDonorA1.BringToFront();
        }
    }
}

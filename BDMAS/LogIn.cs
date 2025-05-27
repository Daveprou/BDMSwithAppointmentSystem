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
using MySql.Data.MySqlClient;

namespace Blood_donation
{
    public partial class LogIn: Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Username and password are required!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {

                    string adminQuery = @"SELECT * FROM tblAdmins 
                                WHERE username = @username 
                                AND password = @password";

                    conn.Open();

                    // Check admin credentials first
                    using (MySqlCommand adminCmd = new MySqlCommand(adminQuery, conn))
                    {
                        adminCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        adminCmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        using (MySqlDataReader adminReader = adminCmd.ExecuteReader())
                        {
                            if (adminReader.HasRows)
                            {
                                // Admin login successful
                                this.Hide();
                                new Form9().Show(); // Admin dashboard
                                return;
                            }
                        }
                    }

                    string userQuery = @"SELECT donor_id, password FROM tbldonoraccounts 
                           WHERE username = @username";


                    using (MySqlCommand cmd = new MySqlCommand(userQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if any data exists
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Invalid username!");
                                return;
                            }

                            // Read the first row
                            reader.Read();

                            // Verify password (replace with hashing in production)
                            string storedPassword = reader.GetString("password");
                            if (txtPassword.Text != storedPassword)
                            {
                                MessageBox.Show("Invalid password!");
                                return;
                            }

                            // Get donor ID
                            int donorId = reader.GetInt32("donor_id");

                            // Open Profile Form
                            this.Hide();
                            Form4 profileForm = new Form4(donorId);
                            profileForm.Show();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login Error: {ex.Message}");
            }
        }

        public void ClearLoginFields()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lnk_SignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();

            this.Hide();
        }

        private void LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }


        }
    }
}

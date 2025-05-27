using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Blood_donation.Resources
{
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private bool UsernameExists(string username)
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tbldonoraccounts WHERE username = @uname";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@uname", username);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch
            {
                return true; // Assume exists to prevent duplicates on error
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername2.Text) ||
        string.IsNullOrWhiteSpace(txtFname.Text) ||
        string.IsNullOrWhiteSpace(txtLname.Text) ||
        string.IsNullOrWhiteSpace(txtEmail.Text) ||
        string.IsNullOrWhiteSpace(txtPassword2.Text) ||
        string.IsNullOrWhiteSpace(txtConfirmPw.Text) ||
        string.IsNullOrWhiteSpace(txtAddress.Text) ||
        string.IsNullOrWhiteSpace(txtZip.Text) ||
        cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            // Check if passwords match
            if (txtPassword2.Text != txtConfirmPw.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            if (txtPassword2.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return;
            }

            // Calculate age
            DateTime birthDate = dtpBirthDate.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            // Age validation
            if (age < 18 || age > 65)
            {
                MessageBox.Show("You must be between 18 to 65 years old to register!");
                return;
            }

            if (UsernameExists(txtUsername2.Text))
            {
                MessageBox.Show("Username already exists! Please enter a different one.");
                return;
            }

            // Add confirmation dialog
            var confirmResult = MessageBox.Show("Are you sure all information is correct?",
                                              "Confirm Registration",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                return; // User canceled registration
            }

            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    string query = @"INSERT INTO tbldonoraccounts 
                            (username, first_name, last_name, email, password, 
                             birthdate, address, zip_code, gender, age)
                            VALUES
                            (@username, @firstname, @lastname, @email, @password, 
                             @birthdate, @address, @zipcode, @gender, @age)";

                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername2.Text);
                        cmd.Parameters.AddWithValue("@firstname", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lastname", txtLname.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword2.Text);
                        cmd.Parameters.AddWithValue("@birthdate", birthDate);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@zipcode", txtZip.Text);
                        cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@age", age);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");
                        ClearFields();

                        // Auto-redirect to login
                        LogIn loginForm = new LogIn();
                        loginForm.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
            }
        }
        
        private void ClearFields()
        {
            txtUsername2.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtEmail.Text = "";
            txtPassword2.Text = "";
            txtConfirmPw.Text = "";
            dtpBirthDate.Value = DateTime.Today;
            txtAddress.Text = "";
            txtZip.Text = "";
            cmbGender.SelectedIndex = -1;
            txtAge.Text = "";
        }
        private void lnk_LogIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogIn frm2 = new LogIn();
            frm2.Show();

            this.Hide();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            
        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime birthDate = dtpBirthDate.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            txtAge.Text = age.ToString(); // Add a read-only txtAgeSU TextBox
        }

        private void txtUsername2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using Blood_donation.Resources;
using MySql.Data.MySqlClient;
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
    public partial class Form6: Form
    {
        private int donorId;
        private string originalPassword;
        private string originalGender;
        private DateTime originalBirthdate;
        private int currentAge;
        public Form6(int donorId)
        {
            InitializeComponent();
            this.donorId = donorId;
            LoadUserData();
            txtPassEP.Enabled = false;
            txtCPEP.Enabled = false;

        }

        private void LoadUserData()
        {
            using (MySqlConnection conn = DatabasePub.GetConnection())
            {
                string query = @"SELECT username, email, address, zip_code, 
                password, birthdate, gender, age 
                FROM tbldonoraccounts 
                WHERE donor_id = @donorId";
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@donorId", donorId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtDonorIDEP.Text = donorId.ToString();
                        txtUsernameEP.Text = reader["username"].ToString();

                        // Store original email, address, zip in Tag
                        txtEmailEP.Text = reader["email"].ToString();
                        txtEmailEP.Tag = reader["email"].ToString();

                        txtAddressEP.Text = reader["address"].ToString();
                        txtAddressEP.Tag = reader["address"].ToString();

                        txtZipEP.Text = reader["zip_code"].ToString();
                        txtZipEP.Tag = reader["zip_code"].ToString();

                        dtpBirthdate4.Value = Convert.ToDateTime(reader["birthdate"]);
                        originalPassword = reader["password"].ToString();
                        originalBirthdate = dtpBirthdate4.Value;

                        // Store original gender
                        originalGender = reader["gender"].ToString();
                        cmbGender.SelectedItem = originalGender;

                        txtAge.Text = reader["age"].ToString();
                    }
                }
            }
        }


        private void btnHome2_Click(object sender, EventArgs e)
        {
            Form4 home = new Form4(donorId);
            this.Hide();
            home.Show();
        }

        private void btnProfile2_Click(object sender, EventArgs e)
        {
            Form5 home = new Form5(donorId);
            this.Hide();
            home.Show();
        }

        private void btnDonate3_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7(donorId);
            frm7.Show();

            this.Hide();
        }

        private void btnAbout3_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8(donorId);
            frm8.Show();

            this.Hide();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (!HasChanges())
            {
                MessageBox.Show("No changes detected.");
                return;
            }

            // Calculate current age
            DateTime newBirthdate = dtpBirthdate4.Value;
            DateTime today = DateTime.Today;
            currentAge = today.Year - newBirthdate.Year;
            if (newBirthdate.Date > today.AddYears(-currentAge)) currentAge--;

            // Age validation
            if (currentAge < 18 || currentAge > 65)
            {
                MessageBox.Show("You must be between 18 and 65 years old to maintain an account!",
                              "Age Restriction",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }


            // Password change validation (plain text comparison)
            if (!string.IsNullOrEmpty(txtPassEP.Text))
            {
                if (string.IsNullOrEmpty(txtCPassEP.Text))
                {
                    MessageBox.Show("Please enter current password!");
                    return;
                }

                if (txtCPassEP.Text != originalPassword) // Direct text comparison
                {
                    MessageBox.Show("Current password is incorrect!");
                    return;
                }

                //new add
                if (txtPassEP.Text == originalPassword)
                {
                    MessageBox.Show("New password must be different from current password!");
                    return;
                }

                if (txtPassEP.Text.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long.");
                    return;
                }

                if (txtPassEP.Text != txtCPEP.Text)
                {
                    MessageBox.Show("New passwords don't match!");
                    return;
                }
            }
            var confirmResult = MessageBox.Show($"Are you sure you want to update your information?\n\n",
                                      "Confirm Update",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                UpdateUserInformation();
            }

            ClearPasswordFields();
        }

        private bool HasChanges()
        {
            
            bool passwordChanged = !string.IsNullOrEmpty(txtPassEP.Text) && txtPassEP.Text != originalPassword;

            
            bool emailChanged = txtEmailEP.Text != (txtEmailEP.Tag?.ToString() ?? "");
            bool addressChanged = txtAddressEP.Text != (txtAddressEP.Tag?.ToString() ?? "");
            bool zipChanged = txtZipEP.Text != (txtZipEP.Tag?.ToString() ?? "");

            
            bool birthdateChanged = dtpBirthdate4.Value != originalBirthdate;

            
            string currentGender = cmbGender.SelectedItem?.ToString() ?? "";
            bool genderChanged = currentGender != originalGender;

            
            return passwordChanged || emailChanged || addressChanged || zipChanged || birthdateChanged || genderChanged;
        }
        

        private void UpdateUserInformation()
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    string query = @"UPDATE tbldonoraccounts SET
                                    password = COALESCE(@password, password),
                                    email = @email,
                                    address = @address,
                                    zip_code = @zipCode,
                                    birthdate = @birthdate,
                                    gender = @gender,
                                    age = @age
                                    WHERE donor_id = @donorId";

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@password",
                       string.IsNullOrEmpty(txtPassEP.Text) ? DBNull.Value :
                       (object)txtPassEP.Text);

                        cmd.Parameters.AddWithValue("@email", txtEmailEP.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddressEP.Text);
                        cmd.Parameters.AddWithValue("@zipCode", txtZipEP.Text);
                        cmd.Parameters.AddWithValue("@birthdate", dtpBirthdate4.Value.Date);
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@age", int.Parse(txtAge.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information updated successfully!");
                        ClearPasswordFields();
                        LoadUserData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message);
            }
        }

        private void ClearPasswordFields()
        {
            txtCPassEP.Text = "";
            txtPassEP.Text = "";
            txtCPEP.Text = "";
        }

        private void txtCPassEP_TextChanged(object sender, EventArgs e)
        {
            bool enableFields = !string.IsNullOrEmpty(txtCPassEP.Text);
            txtPassEP.Enabled = enableFields;
            txtCPEP.Enabled = enableFields;

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            txtCPassEP.Text = "";
            txtPassEP.Text = "";
            txtCPEP.Text = "";
            LoadUserData();
            
        }

        private void dtpBirthdate4_ValueChanged(object sender, EventArgs e)
        {
            DateTime birthDate = dtpBirthdate4.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            txtAge.Text = age.ToString();
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            LogIn loginForm = new LogIn();
            loginForm.Show();
            this.Close();
        }
    }
}

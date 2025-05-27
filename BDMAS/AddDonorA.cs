using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Blood_donation
{
    public partial class AddDonorA: UserControl
    {
        public AddDonorA()
        {
            InitializeComponent();
            dtpBirthdateA2.MaxDate = DateTime.Today;
            cmbGenderA2.SelectedIndex = -1;
            UpdateAgeDisplay();
        }

        private void btnSaveAD_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            if (!ValidateAge(dtpBirthdateA2.Value))
            {
                MessageBox.Show("Donor must be 18-65 years old.", "Validation Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPasswordAD.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPasswordAD.Text != txtCPasswordAD.Text)
            {
                MessageBox.Show("Password and confirmation do not match!", "Validation Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UsernameExists(txtUsernameAD.Text))
            {
                MessageBox.Show("Username already exists!", "Validation Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation dialog
            var confirmResult = MessageBox.Show(
                "Are you sure you want to save this donor information?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (confirmResult != DialogResult.Yes)
                return;


            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO tbldonoraccounts 
                               (username, first_name, last_name, email, password, 
                                birthdate, address, zip_code, gender, age)
                               VALUES
                               (@uname, @fname, @lname, @email, @pass, 
                                @bdate, @addr, @zip, @gender, @age)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@uname", txtUsernameAD.Text);
                    cmd.Parameters.AddWithValue("@fname", txtFnameAD.Text);
                    cmd.Parameters.AddWithValue("@lname", txtLnameAD.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmailAd.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPasswordAD.Text); 
                    cmd.Parameters.AddWithValue("@bdate", dtpBirthdateA2.Value);
                    cmd.Parameters.AddWithValue("@addr", txtAddressAD.Text);
                    cmd.Parameters.AddWithValue("@zip", txtZipCodeAD.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbGenderA2.SelectedItem?.ToString());
                    cmd.Parameters.AddWithValue("@age", CalculateAge(dtpBirthdateA2.Value));


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donor saved successfully!", "Success",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving donor: {ex.Message}");
            }
        }

        private bool ValidateForm()
        {
            var controls = new[] {
            txtUsernameAD, txtFnameAD, txtLnameAD, txtEmailAd,
            txtPasswordAD, txtCPasswordAD, txtAddressAD, txtZipCodeAD
        };

            foreach (var control in controls)
            {
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    MessageBox.Show("All fields are required!");
                    return false;
                }
            }

            if (cmbGenderA2.SelectedIndex == -1)
            {
                MessageBox.Show("Gender is required!");
                return false;
            }
            return true;
        }

        private int CalculateAge(DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate > DateTime.Today.AddYears(-age)) age--;
            return age;
        }

        private bool ValidateAge(DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate > DateTime.Today.AddYears(-age)) age--;
            return age >= 18 && age <= 65;
        }

        private void UpdateAgeDisplay()
        {
            int age = CalculateAge(dtpBirthdateA2.Value);
            txtAgeAd.Text = age.ToString();
        }

        private bool UsernameExists(string username)
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

        private void btnClearAD_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox txt)
                    txt.Clear();
            }
            cmbGenderA2.SelectedIndex = -1;
            dtpBirthdateA2.Value = DateTime.Today;
            UpdateAgeDisplay(); // Ensure age is updated after reset
        }

        private void dtpBirthdateA2_ValueChanged(object sender, EventArgs e)
        {
            UpdateAgeDisplay();
        }
    }
}

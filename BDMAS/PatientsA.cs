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
    public partial class PatientsA: UserControl
    {
        public PatientsA()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Configure DateTimePicker
            dtpBDAP.Format = DateTimePickerFormat.Short;
            dtpBDAP.Value = DateTime.Today;

            // Initialize ComboBoxes
            cmbGenderAP.Items.AddRange(new[] { "Male", "Female", "Other" });
            cmbBGAP.Items.AddRange(new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", "N/A" });

        }

        private void CalculateAge()
        {
            DateTime birthDate = dtpBDAP.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // Subtract 1 if birthday hasn't occurred yet this year
            if (birthDate.Date > today.AddYears(-age)) age--;

            txtAgeAP.Text = age.ToString();
        }

        private void btnSaveAP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFNameAP.Text))
            {
                MessageBox.Show("First Name is required!", "Validation Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFNameAP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLNameAP.Text))
            {
                MessageBox.Show("Last Name is required!", "Validation Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLNameAP.Focus();
                return;
            }

            if (cmbGenderAP.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGenderAP.Focus();
                return;
            }

            if (cmbBGAP.SelectedIndex == -1)
            {
                MessageBox.Show("Please select blood group!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBGAP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneAP.Text))
            {
                MessageBox.Show("Phone Number is required!", "Validation Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneAP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddressAP.Text))
            {
                MessageBox.Show("Address is required!", "Validation Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddressAP.Focus();
                return;
            }

            // Confirmation dialog
            var patientInfo = $"{txtFNameAP.Text.Trim()} {txtLNameAP.Text.Trim()}\n" +
                             $"Blood Group: {cmbBGAP.SelectedItem}\n" +
                             $"Gender: {cmbGenderAP.SelectedItem}";

            var result = MessageBox.Show(
                $"Are you sure you want to save this new patient?\n\n{patientInfo}",
                "Confirm New Patient",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result != DialogResult.Yes)
                return;


            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    var query = @"INSERT INTO tblpatients 
                                (P_FName, P_LName, P_Gender, P_BG, P_BirthDate, P_Age, P_Phone, P_Address)
                                VALUES (@fname, @lname, @gender, @bg, @bdate, @age, @phone, @address)";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@fname", txtFNameAP.Text.Trim());
                        cmd.Parameters.AddWithValue("@lname", txtLNameAP.Text.Trim());
                        cmd.Parameters.AddWithValue("@gender", cmbGenderAP.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@bg", cmbBGAP.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@bdate", dtpBDAP.Value.Date);
                        cmd.Parameters.AddWithValue("@age", int.Parse(txtAgeAP.Text));
                        cmd.Parameters.AddWithValue("@phone", txtPhoneAP.Text.Trim());
                        cmd.Parameters.AddWithValue("@address", txtAddressAP.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Patient saved successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient: {ex.Message}");
            }
        }

        private void dtpBirthDateAP_ValueChanged(object sender, EventArgs e)
        {
            CalculateAge();
        }

        private void btnClearAP_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtFNameAP.Clear();
            txtLNameAP.Clear();
            cmbGenderAP.SelectedIndex = -1;
            cmbBGAP.SelectedIndex = -1;
            dtpBDAP.Value = DateTime.Today;
            txtAgeAP.Clear();
            txtPhoneAP.Clear();
            txtAddressAP.Clear();
        }
    }
}

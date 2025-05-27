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
    public partial class ViewPatientsA: UserControl
    {
        private int selectedPatientId = -1;
        private bool IsInDesignMode => DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        public ViewPatientsA()
        {
            InitializeComponent();
            if (!IsInDesignMode)
            {
                InitializeControls();
                LoadPatients();
            }
        }

        private void InitializeControls()
        {
            dtpBDPL.Format = DateTimePickerFormat.Short;
            cmbGenderPL.Items.AddRange(new[] { "Male", "Female", "Other" });
            cmbBGPL.Items.AddRange(new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", "N/A" });

        }

        private void LoadPatients(string search = "")
        {
            if (IsInDesignMode) return;
            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    var query = @"SELECT P_ID, P_FName, P_LName, P_Gender, 
                                  P_BirthDate, P_BG, P_Phone, P_Address 
                                  FROM tblpatients 
                                  WHERE P_ID LIKE @search 
                                     OR P_FName LIKE @search 
                                     OR P_LName LIKE @search";

                    var cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@search", $"%{search}%");

                    var adapter = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dgvPatientsPL.DataSource = dt;
                    dgvPatientsPL.ClearSelection();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error loading patients: {ex.Message}");
            }
        }

        

        private void CheckTransferRestrictions()
        {
            if (IsInDesignMode) return;
            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    var query = "SELECT COUNT(*) FROM tblbtransfers WHERE Patient_ID = @id";
                    var cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", selectedPatientId);

                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    cmbBGPL.Enabled = count == 0;
                }
            }
            catch
            {
                if (!DesignMode)
                    MessageBox.Show("Error checking transfer restrictions");
            }
        }

        private void btnUpdatePL_Click(object sender, EventArgs e)
        {
            if (selectedPatientId == -1)
            {
                MessageBox.Show("Please select a patient first.", "Selection Required",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get patient info for confirmation
            string patientInfo = $"{txtFNamePL.Text.Trim()} {txtLNamePL.Text.Trim()} (ID: {selectedPatientId})";

            // Confirmation dialog
            var result = MessageBox.Show(
                $"Are you sure you want to update details for:\n{patientInfo}?",
                "Confirm Patient Update",
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
                    var query = @"UPDATE tblpatients SET 
                                P_FName = @fname,
                                P_LName = @lname,
                                P_Gender = @gender,
                                P_BG = @bg,
                                P_BirthDate = @bdate,
                                P_Age = @age,
                                P_Phone = @phone,
                                P_Address = @address 
                                WHERE P_ID = @id";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@fname", txtFNamePL.Text);
                        cmd.Parameters.AddWithValue("@lname", txtLNamePL.Text);
                        cmd.Parameters.AddWithValue("@gender", cmbGenderPL.SelectedItem);
                        cmd.Parameters.AddWithValue("@bg", cmbBGPL.SelectedItem);
                        cmd.Parameters.AddWithValue("@bdate", dtpBDPL.Value);
                        cmd.Parameters.AddWithValue("@age", txtAgePL.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhonePL.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddressPL.Text);
                        cmd.Parameters.AddWithValue("@id", selectedPatientId);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Patient updated successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPatients();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating patient: {ex.Message}");
            }
        }

        private void dtpBDatePL_ValueChanged(object sender, EventArgs e)
        {
            CalculateAge();
        }

        private void CalculateAge()
        {
            var birthDate = dtpBDPL.Value;
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            txtAgePL.Text = age.ToString();
        }

        private void btnDeletePL_Click(object sender, EventArgs e)
        {
            if (IsInDesignMode) return;
            if (selectedPatientId == -1)
            {
                MessageBox.Show("Please select a patient first.", "Selection Required",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();

                    // Check for existing transfers first
                    var checkQuery = "SELECT COUNT(*) FROM tblbtransfers WHERE Patient_ID = @id";
                    var checkCmd = new MySqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@id", selectedPatientId);
                    var transferCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (transferCount > 0)
                    {
                        MessageBox.Show("Cannot delete patient with existing blood transfer records!",
                                      "Deletion Restricted",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                        return;
                    }

                    // Proceed with deletion confirmation
                    // Get patient info for confirmation
                    string patientInfo = $"{txtFNamePL.Text.Trim()} {txtLNamePL.Text.Trim()} (ID: {selectedPatientId})";

                    // Confirmation dialog
                    var confirmResult = MessageBox.Show(
                        $"Permanently delete patient record:\n{patientInfo}\nThis cannot be undone!",
                        "Confirm Patient Deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2
                    );

                    if (confirmResult != DialogResult.Yes) return;
                    // Proceed with deletion
                    var deleteQuery = "DELETE FROM tblpatients WHERE P_ID = @id";
                    using (var deleteCmd = new MySqlCommand(deleteQuery, con))
                    {
                        deleteCmd.Parameters.AddWithValue("@id", selectedPatientId);

                        if (deleteCmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Patient record deleted successfully!", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPatients();
                            ClearForm();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting patient: {ex.Message}");
            }
        }

        private void txtSearchPL_TextChanged(object sender, EventArgs e)
        {
            if (IsInDesignMode) return;
            LoadPatients(txtSearchPL.Text);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (IsInDesignMode) return;

            if (Visible)
            {
                LoadPatients();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            selectedPatientId = -1;
            txtFNamePL.Clear();
            txtLNamePL.Clear();
            cmbGenderPL.SelectedIndex = -1;
            cmbBGPL.SelectedIndex = -1;
            dtpBDPL.Value = DateTime.Today;
            txtAgePL.Clear();
            txtPhonePL.Clear();
            txtAddressPL.Clear();
            dgvPatientsPL.ClearSelection();
        }

        private void dgvPatientsPL_SelectionChanged(object sender, EventArgs e)
        {
            if (IsInDesignMode) return;
            if (IsInDesignMode || dgvPatientsPL.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvPatientsPL.SelectedRows[0];

            // Safe value handling
            selectedPatientId = row.Cells["P_ID"].Value is int id ? id : -1;
            txtFNamePL.Text = row.Cells["P_FName"].Value?.ToString() ?? "";
            txtLNamePL.Text = row.Cells["P_LName"].Value?.ToString() ?? "";
            cmbGenderPL.SelectedItem = row.Cells["P_Gender"].Value?.ToString() ?? "";
            cmbBGPL.SelectedItem = row.Cells["P_BG"].Value?.ToString() ?? "";

            if (DateTime.TryParse(row.Cells["P_BirthDate"].Value?.ToString(), out DateTime bdate))
                dtpBDPL.Value = bdate;

            txtPhonePL.Text = row.Cells["P_Phone"].Value?.ToString() ?? "";
            txtAddressPL.Text = row.Cells["P_Address"].Value?.ToString() ?? "";

            CheckTransferRestrictions();
            CalculateAge();
        }
    }
}

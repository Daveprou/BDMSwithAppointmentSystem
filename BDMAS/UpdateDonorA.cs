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
    public partial class UpdateDonorA: UserControl
    {
        private int selectedDonorId = -1;
        private string originalUsername;
        private string originalEmail;
        private DateTime originalBirthdate;
        private string originalAddress;
        private string originalZipCode;
        private string originalGender;
        private int originalAge;
        private string originalFirstName;
        private string originalLastName;
        public UpdateDonorA()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDonors();
                dtpBirthdateA3.MaxDate = DateTime.Today;
                cmbGenderA3.SelectedIndex = -1;
                UpdateAgeDisplay();
            }
        }

        private void LoadDonors(string searchId = "")
        {
            if (this.DesignMode) return;
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT donor_id, username, first_name, last_name,
                                     email, birthdate, 
                                     address, zip_code, gender, age
                                      FROM tbldonoraccounts";

                    if (!string.IsNullOrEmpty(searchId))
                    {
                        query += " WHERE donor_id = @id";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    if (!string.IsNullOrEmpty(searchId))
                    {
                        cmd.Parameters.AddWithValue("@id", searchId);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Console.WriteLine($"Loaded {dt.Rows.Count} rows");
                    dgvDonorsA3.DataSource = dt;
                    ResetSelection();
                    dgvDonorsA3.ClearSelection();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error loading donors: {ex.Message}");

            }
        }
        private void ResetSelection()
        {
            // Clear grid selection and controls
            dgvDonorsA3.ClearSelection();
            dgvDonorsA3.CurrentCell = null;

            // Clear form controls
            ClearForm();

            // Reset tracking variables
            selectedDonorId = -1;

        }

        private void btnUpdateUD_Click(object sender, EventArgs e)
        {
            if (selectedDonorId == -1)
            {
                MessageBox.Show("Please select a donor first.", "Selection Required",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbGenderA3.SelectedIndex == -1)
            {
                MessageBox.Show("Gender is required!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool hasChanges =
            txtFNameA3.Text != originalFirstName ||
            txtLNameA3.Text != originalLastName ||
            txtUsernameA3.Text != originalUsername ||
            txtEmailA3.Text != originalEmail ||
            dtpBirthdateA3.Value != originalBirthdate ||
            txtAddressA3.Text != originalAddress ||
            txtZipA3.Text != originalZipCode ||
            dtpBirthdateA3.Value != originalBirthdate ||
            cmbGenderA3.SelectedItem.ToString() != originalGender ||
            !string.IsNullOrEmpty(txtNPassA3.Text);


            if (!hasChanges)
            {
                MessageBox.Show("No changes to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFNameA3.Text) ||
            string.IsNullOrWhiteSpace(txtLNameA3.Text))
            {
                MessageBox.Show("First Name and Last Name are required.");
                return;
            }

            // Validate only absolute requirements
            if (string.IsNullOrWhiteSpace(txtUsernameA3.Text) ||
               string.IsNullOrWhiteSpace(txtEmailA3.Text) ||
               string.IsNullOrWhiteSpace(txtAddressA3.Text) ||
               string.IsNullOrWhiteSpace(txtZipA3.Text))
            {
                MessageBox.Show("Username, Email, Address, and Zip Code are required.");
                return;
            }

            if (!ValidateAge(dtpBirthdateA3.Value))
            {
                MessageBox.Show("Donor must be 18-65 years old.");
                return;
            }

            if (!string.IsNullOrEmpty(txtNPassA3.Text))
            {
                if (string.IsNullOrEmpty(txtCurrentPA3.Text))
                {
                    MessageBox.Show("Current password is required to change password.");
                    return;
                }

                if (txtNPassA3.Text != txtConfirmPA3.Text)
                {
                    MessageBox.Show("New password and confirmation do not match!");
                    return;
                }

                if (txtNPassA3.Text.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long.");
                    return;
                }

                // Verify current password
                string currentPasswordFromDB = GetCurrentPassword(selectedDonorId);
                if (currentPasswordFromDB != txtCurrentPA3.Text)
                {
                    MessageBox.Show("Current password is incorrect!");
                    return;
                }

                if (txtNPassA3.Text == currentPasswordFromDB)
                {
                    MessageBox.Show("New password must be different from current password!");
                    return;
                }
            }

            // Add confirmation dialog
            var confirmResult = MessageBox.Show(
                "Are you sure you want to update this donor's information?",
                "Confirm Update",
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
                    var query = new StringBuilder("UPDATE tbldonoraccounts SET ");

                    // Build dynamic parameters
                    var parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@fname", txtFNameA3.Text),
                new MySqlParameter("@lname", txtLNameA3.Text),
                new MySqlParameter("@uname", txtUsernameA3.Text),
                new MySqlParameter("@email", txtEmailA3.Text),
                new MySqlParameter("@bdate", dtpBirthdateA3.Value),
                new MySqlParameter("@addr", txtAddressA3.Text),
                new MySqlParameter("@zip", txtZipA3.Text),
                new MySqlParameter("@gender", cmbGenderA3.SelectedItem.ToString()),
                new MySqlParameter("@age", CalculateAge(dtpBirthdateA3.Value))
            };

                    query.Append("first_name = @fname, last_name = @lname, username = @uname, email = @email, birthdate = @bdate, address = @addr, zip_code = @zip, gender = @gender, age = @age");

                    // Add password update if provided
                    if (!string.IsNullOrEmpty(txtNPassA3.Text))
                    {
                        query.Append(", password = @pass");
                        parameters.Add(new MySqlParameter("@pass", txtNPassA3.Text));
                    }

                    query.Append(" WHERE donor_id = @id");
                    parameters.Add(new MySqlParameter("@id", selectedDonorId));

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conn);
                    cmd.Parameters.AddRange(parameters.ToArray());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update successful!", "Success",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDonors();
                    ResetSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private string GetCurrentPassword(int donorId)
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT password FROM tbldonoraccounts WHERE donor_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", donorId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "";
                }
            }
            catch
            {
                return ""; // Handle error appropriately
            }
        }

        

        private void btnDeleteUD_Click(object sender, EventArgs e)
        {
            if (selectedDonorId == -1)
            {
                MessageBox.Show("Please select a donor to delete.", "Selection Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
               "Are you sure you want to permanently delete this donor?\nThis action cannot be undone!",
               "Confirm Delete",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning,
               MessageBoxDefaultButton.Button2
   );

            if (confirmResult != DialogResult.Yes)
                return;
            
                try
                {
                    using (MySqlConnection conn = DatabasePub.GetConnection())
                    {
                        conn.Open();
                        var cmd = new MySqlCommand("DELETE FROM tbldonoraccounts WHERE donor_id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", selectedDonorId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Donor deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonors();
                            ClearForm();
                            ResetSelection();
                        }
                    }
                }

                    catch (MySqlException ex) when (ex.Number == 1451)
                    {
                    MessageBox.Show("Unable to delete a donor that has an appointment history.",
                    "Deletion Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    }

                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting donor: {ex.Message}");
                }
            }
        

        private bool ValidateForm()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox txt && txt != txtSearchA3 && txt != txtNPassA3 &&
                    txt != txtConfirmPA3 && string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("All required fields must be filled!");
                    return false;
                }
            }
            return true;
        }
        private bool ValidateAge(DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate.Date > DateTime.Today.AddYears(-age)) age--;

            // Corrected condition: use AND (&&) instead of OR (||)
            return age >= 18 && age <= 65;

        }

        // Check username uniqueness
        private bool UsernameExists(string username, int excludeId)
        {
            using (MySqlConnection conn = DatabasePub.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tblAccounts WHERE username = @uname AND donor_id != @excludeId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@uname", username);
                cmd.Parameters.AddWithValue("@excludeId", excludeId);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Clear form after delete
        private void ClearForm()
        {
            selectedDonorId = -1;
            txtDonorIDA3.Clear();
            txtUsernameA3.Clear();
            txtFNameA3.Clear();
            txtLNameA3.Clear();
            txtEmailA3.Clear();
            dtpBirthdateA3.Value = DateTime.Today;
            txtAddressA3.Clear();
            txtZipA3.Clear();
            txtCurrentPA3.Clear();
            txtNPassA3.Clear();
            txtConfirmPA3.Clear();
            cmbGenderA3.SelectedIndex = -1;
            txtAgeA3.Clear();
        }

        private void dgvDonors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonorsA3.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDonorsA3.SelectedRows[0];

                selectedDonorId = Convert.ToInt32(row.Cells["donor_id"].Value);
                // Store original values
                txtDonorIDA3.Text = row.Cells["donor_id"].Value.ToString();
                originalUsername = row.Cells["username"].Value.ToString();
                originalFirstName = row.Cells["first_name"].Value?.ToString();
                originalLastName = row.Cells["last_name"].Value?.ToString();
                originalEmail = row.Cells["email"].Value.ToString();
                originalBirthdate = Convert.ToDateTime(row.Cells["birthdate"].Value);
                originalGender = row.Cells["gender"].Value?.ToString();
                originalAge = Convert.ToInt32(row.Cells["age"].Value);
                originalAddress = row.Cells["address"].Value.ToString();
                originalZipCode = row.Cells["zip_code"].Value.ToString();


                // Populate controls
                txtUsernameA3.Text = originalUsername;
                txtEmailA3.Text = originalEmail;
                txtFNameA3.Text = originalFirstName;
                txtLNameA3.Text = originalLastName;
                dtpBirthdateA3.Value = originalBirthdate;
                txtAddressA3.Text = originalAddress;
                txtZipA3.Text = originalZipCode;
                cmbGenderA3.SelectedItem = originalGender;
                txtAgeA3.Text = originalAge.ToString();
            }
        }

        private void UpdateDonorA_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                LoadDonors();
                ResetSelection();
            }
        }

        private void UpdateDonorA_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !this.DesignMode)
            {
                LoadDonors();
                ResetSelection();
            }
        }

        private void UpdateAgeDisplay()
        {
            txtAgeA3.Text = CalculateAge(dtpBirthdateA3.Value).ToString();
        }

        private int CalculateAge(DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate > DateTime.Today.AddYears(-age)) age--;
            return age;
        }

        private void dtpBirthdateA2_ValueChanged(object sender, EventArgs e)
        {
            UpdateAgeDisplay();
        }

        private void txtSearchA3_TextChanged(object sender, EventArgs e)
        {
            LoadDonors(txtSearchA3.Text.Trim());
        }
    }
}

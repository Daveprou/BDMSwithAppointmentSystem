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
    public partial class ViewDonorA: UserControl
    {
        private int selectedAppointmentId = -1;
        private string currentStatus = "";
        private int isTransferred = 0;
        public ViewDonorA()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadAppointments();
                SetupComboBox();
                DisableButtons();
            }
        }

        private void LoadAppointments(string donorIdFilter = "", string statusFilter = "")
        {
            if (this.DesignMode)
                return;
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT appointment_id, donor_id, blood_type, 
                               appointment_date, time_slot, status, is_transferred, created_at 
                               FROM tblappointments";

                    var whereClauses = new List<string>();
                    var parameters = new List<MySqlParameter>();

                    if (!string.IsNullOrEmpty(donorIdFilter))
                    {
                        if (int.TryParse(donorIdFilter, out int donorId))
                        {
                            whereClauses.Add("donor_id = @donorId");
                            parameters.Add(new MySqlParameter("@donorId", donorId));
                        }
                        else
                        {
                            // Clear results for invalid input
                            dgvAppointmentsVD.DataSource = new DataTable();
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
                    {
                        whereClauses.Add("status = @status");
                        parameters.Add(new MySqlParameter("@status", statusFilter));
                    }





                    if (whereClauses.Count > 0)
                    {
                        query += " WHERE " + string.Join(" AND ", whereClauses);
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddRange(parameters.ToArray());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvAppointmentsVD.DataSource = dt;
                    dgvAppointmentsVD.ClearSelection();
                }
            }
            catch (Exception ex)
            {


                MessageBox.Show($"Error loading appointments: {ex.Message}");

            }
        }

        private void SetupComboBox()
        {
            cmbStatusVD.Items.AddRange(new object[] { "All", "Scheduled", "Cancelled", "Completed" });
            cmbStatusVD.SelectedIndex = 0;
        }

        private void btnScheduledVD_Click(object sender, EventArgs e) => UpdateStatus("Scheduled");

        private void dgvAppointmentsVD_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppointmentsVD.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvAppointmentsVD.SelectedRows[0];
                selectedAppointmentId = Convert.ToInt32(row.Cells["appointment_id"].Value);
                currentStatus = row.Cells["status"].Value.ToString();
                isTransferred = row.Cells["is_transferred"] != null ? Convert.ToInt32(row.Cells["is_transferred"].Value) : 0;
                UpdateButtonStates();
            }
            else
            {
                selectedAppointmentId = -1;
                isTransferred = 0;
                DisableButtons();
            }
        }


        private void UpdateButtonStates()
        {
            if (isTransferred == 1)
            {
                btnScheduledVD.Enabled = false;
                btnCancelledVD.Enabled = false;
                btnCompletedVD.Enabled = false;
                return;
            }
            btnScheduledVD.Enabled = currentStatus != "Scheduled";
            btnCancelledVD.Enabled = currentStatus != "Cancelled";
            btnCompletedVD.Enabled = currentStatus != "Completed";
        }

        private void DisableButtons()
        {
            btnScheduledVD.Enabled = false;
            btnCancelledVD.Enabled = false;
            btnCompletedVD.Enabled = false;
        }

        private void UpdateStatus(string newStatus)
        {
            if (selectedAppointmentId == -1)
            {
                MessageBox.Show("Please select an appointment first!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE tblappointments SET status = @status WHERE appointment_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", selectedAppointmentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Status updated successfully!");
                        LoadAppointments(txtSearchBoxVD.Text, cmbStatusVD.SelectedItem.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}");
            }
        }

        private void txtSearchIDVD_TextChanged(object sender, EventArgs e)
        {
            string currentStatus = cmbStatusVD.SelectedItem?.ToString() ?? "All";

            if (!string.IsNullOrEmpty(txtSearchBoxVD.Text) &&
                !int.TryParse(txtSearchBoxVD.Text, out _))
            {
                dgvAppointmentsVD.DataSource = new DataTable();
                return;
            }

            LoadAppointments(txtSearchBoxVD.Text, currentStatus);
        }

        private void cmbStatusVD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentSearch = txtSearchBoxVD.Text;
            string newStatus = cmbStatusVD.SelectedItem?.ToString() ?? "All";
            LoadAppointments(currentSearch, newStatus);
        }

        private void btnCanceledVD_Click(object sender, EventArgs e) => UpdateStatus("Cancelled");

        private void btnCompletedVD_Click(object sender, EventArgs e) => UpdateStatus("Completed");
    }
}

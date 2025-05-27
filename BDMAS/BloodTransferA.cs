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
    public partial class BloodTransferA: UserControl
    {
        private string currentBloodGroup = string.Empty;
        public BloodTransferA()
        {
            InitializeComponent();
            if (!IsInDesignMode)
            {
                InitializeControls();
            }
        }


        private bool IsInDesignMode =>
        DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        private void InitializeControls()
        {
            txtNameBT.ReadOnly = true;
            txtBGBT.ReadOnly = true;
            btnTransfer.Visible = false;

            // Delay initial load until actually needed
            this.Load += (s, e) =>
            {
                if (!IsInDesignMode && Visible)
                {
                    LoadInitialData();
                }
            };
        }

        private void LoadInitialData()
        {
            LoadPatients();
            LoadTransfers();
        }

        private void LoadPatients()
        {
            if (IsInDesignMode) return;
            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    var query = "SELECT P_ID, CONCAT(P_FName, ' ', P_LName) AS FullName, P_BG FROM tblpatients";
                    var cmd = new MySqlCommand(query, con);
                    var adapter = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    cmbPatientsIDBT.DataSource = dt;
                    cmbPatientsIDBT.DisplayMember = "P_ID";
                    cmbPatientsIDBT.ValueMember = "P_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}");
            }
        }

        private void BloodTransferA_Load(object sender, EventArgs e)
        {

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (IsInDesignMode) return;

            // Confirmation dialog
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to transfer 1 unit of {currentBloodGroup} blood to patient {cmbPatientsIDBT.SelectedValue}?",
                "Confirm Blood Transfer",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    using (var transaction = con.BeginTransaction())
                    {
                        try
                        {
                            // 1. Insert transfer record
                            var insertQuery = @"INSERT INTO tblbtransfers 
                                  (Patient_ID, TransferDate)
                                  VALUES (@pid, @date)";
                            var insertCmd = new MySqlCommand(insertQuery, con, transaction);
                            insertCmd.Parameters.AddWithValue("@pid", cmbPatientsIDBT.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@date", DateTime.Today);
                            insertCmd.ExecuteNonQuery();

                            // 2. Update blood stock with validation
                            var updateQuery = @"UPDATE tblbstock 
                                  SET BloodStock = GREATEST(BloodStock - 1, 0)
                                  WHERE BloodGroup = @bg";
                            var updateCmd = new MySqlCommand(updateQuery, con, transaction);
                            updateCmd.Parameters.AddWithValue("@bg", currentBloodGroup);
                            int affectedRows = updateCmd.ExecuteNonQuery();

                            if (affectedRows == 0)
                            {
                                // Insert zero stock entry if missing
                                var insertStockQuery = @"INSERT INTO tblbstock 
                                           (BloodGroup, BloodStock)
                                           VALUES (@bg, 0)";
                                var stockCmd = new MySqlCommand(insertStockQuery, con, transaction);
                                stockCmd.Parameters.AddWithValue("@bg", currentBloodGroup);
                                stockCmd.ExecuteNonQuery();
                            }

                            var updateAppointment = @"UPDATE tblappointments
                                SET is_transferred = 1
                                WHERE blood_type = @bg AND status = 'Completed' AND is_transferred = 0
                                ORDER BY appointment_date, time_slot
                                LIMIT 1";
                            var updateAppCmd = new MySqlCommand(updateAppointment, con, transaction);
                            updateAppCmd.Parameters.AddWithValue("@bg", currentBloodGroup);
                            updateAppCmd.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show($"Blood transfer completed successfully!\n{currentBloodGroup} stock updated.",
                                    "Transfer Complete",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                            // Force UI refresh
                            CheckBloodAvailability();
                            LoadTransfers();
                            ClearSelection();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Transfer failed: {ex.Message}");
            }
        }

        private void cmbPatientsIDBT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInDesignMode) return;
            if (cmbPatientsIDBT.SelectedValue == null) return;

            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    var query = @"SELECT CONCAT(P_FName, ' ', P_LName) AS FullName, 
                        P_BG FROM tblpatients WHERE P_ID = @id";
                    var cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", cmbPatientsIDBT.SelectedValue);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Update UI controls first
                            txtNameBT.Text = reader["FullName"].ToString();
                            currentBloodGroup = reader["P_BG"].ToString().Trim().ToUpper();
                            txtBGBT.Text = currentBloodGroup;

                            // Force immediate UI update
                            this.Update();

                            // Check availability AFTER UI elements are updated
                            CheckBloodAvailability();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient details: {ex.Message}");
            }
        }

        private void CheckBloodAvailability()
        {
            if (IsInDesignMode) return;
            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();

                    // 1. Check if blood group exists in stock table
                    var checkQuery = @"SELECT COUNT(*) 
                             FROM tblbstock 
                             WHERE BloodGroup = @bg";
                    var checkCmd = new MySqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@bg", currentBloodGroup);

                    // 2. Insert missing blood groups with 0 stock
                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
                    {
                        var insertQuery = @"INSERT INTO tblbstock 
                                   (BloodGroup, BloodStock)
                                   VALUES (@bg, 0)";
                        var cstockCmd = new MySqlCommand(insertQuery, con);
                        cstockCmd.Parameters.AddWithValue("@bg", currentBloodGroup);
                        cstockCmd.ExecuteNonQuery();
                    }

                    // 3. Get current stock
                    var stockQuery = @"SELECT BloodStock 
                             FROM tblbstock 
                             WHERE BloodGroup = @bg";
                    var stockCmd = new MySqlCommand(stockQuery, con);
                    stockCmd.Parameters.AddWithValue("@bg", currentBloodGroup);

                    int stock = Convert.ToInt32(stockCmd.ExecuteScalar());

                    // 4. Update UI controls directly
                    lblAvailLabel.Text = stock > 0 ? "Available" : "Not Available";
                    lblAvailLabel.ForeColor = stock > 0 ? Color.Green : Color.Red;
                    lblAvailLabel.Visible = true; // Ensure visibility
                    btnTransfer.Visible = stock > 0;

                    // 5. Force immediate UI refresh
                    lblAvailLabel.Refresh();
                    btnTransfer.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking stock: {ex.Message}");
            }
        }

        private void LoadTransfers()
        {
            if (IsInDesignMode) return;
            try
            {
                using (var con = DatabasePub.GetConnection())
                {
                    con.Open();
                    var query = @"SELECT t.Transfer_ID, 
                                p.P_ID, 
                                CONCAT(p.P_FName, ' ', p.P_LName) AS PatientName, 
                                p.P_BG AS BloodGroup,
                                t.TransferDate 
                         FROM tblbtransfers t
                         INNER JOIN tblpatients p 
                         ON t.Patient_ID = p.P_ID";
                    var cmd = new MySqlCommand(query, con);
                    var adapter = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    if (dgvTransfers.IsHandleCreated)
                    {
                        dgvTransfers.DataSource = dt;
                        dgvTransfers.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transfers: {ex.Message}");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                // Delay initial load until handle is created
                this.HandleCreated += (s, ev) =>
                {
                    if (Visible) LoadInitialData();
                };
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (IsInDesignMode) return;

            if (Visible)
            {
                LoadInitialData();
                ClearSelection();
            }
        }

        private void ClearSelection()
        {
            cmbPatientsIDBT.SelectedIndex = -1;
            txtNameBT.Clear();
            txtBGBT.Clear();
            lblAvailLabel.Visible = false;
            btnTransfer.Visible = false;
        }
    }
}

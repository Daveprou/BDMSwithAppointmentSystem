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
using System.Globalization;

namespace Blood_donation
{
    public partial class DonateBloodA: UserControl
    {
        private int selectedDonorId = -1;
        private string originalBloodType = "";
        private DateTime originalDate;
        private TimeSpan originalTime;
        private bool isProgrammaticDateChange = false;
        private bool isInitializing = true;
        private bool appointmentAutoAdvanced = false;
        public DonateBloodA()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                isInitializing = true;

                isProgrammaticDateChange = true;
                dtpDateDB.Value = DateTime.Today;
                isProgrammaticDateChange = false;
                DisableButtons();
                InitializeComponents();
                InitializeTimeSlots();
                LoadDonors();

                isInitializing = false;
            }
        }

        private void InitializeComponents()
        {
            // Initialize blood type combo box
            cmbBloodTypeDB.Items.AddRange(new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", "N/A" });

            cmbBloodTypeDB.SelectedIndexChanged += cmbBloodTypeDB_SelectedIndexChanged;
            cmbBloodTypeDB.DropDownStyle = ComboBoxStyle.DropDownList;

            // Set date restrictions
            dtpDateDB.MinDate = DateTime.Today;
            dtpDateDB.Value = DateTime.Today;
        }

        private void LoadDonors(string searchTerm = "")
        {
            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    var query = @"SELECT donor_id, CONCAT(first_name, ' ', last_name) AS full_name, blood_type 
                            FROM tbldonoraccounts 
                            WHERE donor_id LIKE @search 
                               OR first_name LIKE @search 
                               OR last_name LIKE @search";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                        var adapter = new MySqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        dgvAccountsDB.DataSource = dt;
                        dgvAccountsDB.Columns["donor_id"].HeaderText = "Donor ID";
                        dgvAccountsDB.Columns["full_name"].HeaderText = "Name";
                        dgvAccountsDB.Columns["blood_type"].HeaderText = "Blood Type";


                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error loading donors: {ex.Message}");

            }
        }

        private List<TimeSpan> GetBookedTimeSlots(DateTime date)
        {
            List<TimeSpan> bookedSlots = new List<TimeSpan>();
            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    var query = @"SELECT time_slot FROM tblAppointments 
                        WHERE appointment_date = @selectedDate AND status = 'Scheduled'";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@selectedDate", date.Date);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bookedSlots.Add(reader.GetTimeSpan("time_slot"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving booked slots: {ex.Message}");
            }
            return bookedSlots;
        }

        private bool HasAvailableSlots(DateTime date)
        {
            List<TimeSpan> bookedSlots = GetBookedTimeSlots(date);
            TimeSpan start = new TimeSpan(9, 0, 0);
            TimeSpan end = new TimeSpan(17, 0, 0);
            int totalPossible = 0;

            DateTime now = DateTime.Now;
            bool isToday = date.Date == now.Date;

            while (start <= end)
            {
                if (isToday)
                {
                    if (start >= now.TimeOfDay)
                    {
                        totalPossible++;
                    }
                }
                else
                {
                    totalPossible++;
                }
                start = start.Add(new TimeSpan(0, 30, 0));
            }

            return (totalPossible - bookedSlots.Count) > 0;
        }

        private void InitializeTimeSlots()
        {
            cmbTimeSlotDB.Items.Clear();
            TimeSpan startTime = new TimeSpan(9, 0, 0); // 9:00 AM
            TimeSpan endTime = new TimeSpan(17, 0, 0); // 5:00 PM

            DateTime selectedDate = dtpDateDB.Value.Date;
            DateTime now = DateTime.Now;

            List<TimeSpan> bookedSlots = GetBookedTimeSlots(selectedDate);

            while (startTime <= endTime)
            {
                // Skip past times if selected date is today
                if (selectedDate == now.Date)
                {
                    TimeSpan currentTimeOfDay = now.TimeOfDay;
                    if (startTime < currentTimeOfDay)
                    {
                        startTime = startTime.Add(new TimeSpan(0, 30, 0));
                        continue;
                    }

                }

                if (bookedSlots.Contains(startTime))
                {
                    startTime = startTime.Add(new TimeSpan(0, 30, 0));
                    continue;
                }

                string displayTime = DateTime.Today.Add(startTime).ToString("HH:mm");
                cmbTimeSlotDB.Items.Add(displayTime);
                startTime = startTime.Add(new TimeSpan(0, 30, 0));
            }

            if (cmbTimeSlotDB.Items.Count == 0)
            {
                cmbTimeSlotDB.Text = "";

            }
            else
            {
                cmbTimeSlotDB.SelectedIndex = 0;
            }
        }



        private void dgvAccountsDB_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccountsDB.SelectedRows.Count > 0)
            {
                var row = dgvAccountsDB.SelectedRows[0];
                selectedDonorId = Convert.ToInt32(row.Cells["donor_id"].Value);
                UpdateDonorInfo(row);
                CheckTransferredStatus();
                CheckExistingAppointment();
                LoadAppointmentDetails();
            }
            else
            {
                ClearSelection();
            }
        }

        private void CheckTransferredStatus()
        {
            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    var query = @"SELECT COUNT(*) FROM tblAppointments 
                          WHERE donor_id = @donorId AND is_transferred = 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        cmbBloodTypeDB.Enabled = (count == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking transfer status: {ex.Message}");
                cmbBloodTypeDB.Enabled = true; // fallback to enabled
            }
        }

        private void UpdateDonorInfo(DataGridViewRow row)
        {
            txtDonorIDDB.Text = selectedDonorId.ToString();

            // 1) Pull the raw cell value
            object cellValue = row.Cells["blood_type"].Value;

            // 2) If it's DBNull (or empty), treat as "N/A"
            string bloodType = (cellValue == DBNull.Value ||
                                string.IsNullOrWhiteSpace(cellValue?.ToString()))
                               ? "N/A"
                               : cellValue
                                   .ToString()
                                   .Trim()
                                   .ToUpperInvariant();

            // 3) Find that string in the combo
            int idx = cmbBloodTypeDB.FindStringExact(bloodType);
            if (idx >= 0)
            {
                cmbBloodTypeDB.SelectedIndex = idx;
            }
            else
            {
                // fallback to N/A
                cmbBloodTypeDB.SelectedIndex =
                    cmbBloodTypeDB.FindStringExact("N/A");
            }

            originalBloodType = bloodType;
            Console.WriteLine($"Blood Type from DGV: {bloodType}");
        }

        private void CheckExistingAppointment()
        {
            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    var query = @"SELECT COUNT(*) FROM tblAppointments 
                             WHERE donor_id = @donorId 
                               AND status = 'Scheduled'";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                        var count = Convert.ToInt32(cmd.ExecuteScalar());

                        btnAddDB.Enabled = count == 0;
                        AutoMoveDateIfWeekendAndNoAppointment();
                        btnCancelDB.Enabled = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking appointments: {ex.Message}");
            }
        }

        private void AutoMoveDateIfWeekendAndNoAppointment()
        {
            if (btnAddDB.Enabled && IsWeekend(dtpDateDB.Value))
            {
                isProgrammaticDateChange = true;
                dtpDateDB.Value = GetNextWeekday(dtpDateDB.Value);
                isProgrammaticDateChange = false;
            }
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        private void dtpDateDB_ValueChanged(object sender, EventArgs e)
        {
            if (isInitializing || isProgrammaticDateChange) return;
            AutoMoveDateIfWeekendAndNoAppointment();




            InitializeTimeSlots();

            // Check if no slots available
            if (cmbTimeSlotDB.Items.Count == 0)
            {
                MessageBox.Show("No available slots. Adjusting to the next available date.");
                DateTime nextDate = dtpDateDB.Value.AddDays(1);
                while (true)
                {
                    nextDate = GetNextWeekday(nextDate); // Skip weekends
                    if (HasAvailableSlots(nextDate))
                    {
                        isProgrammaticDateChange = true;
                        dtpDateDB.Value = nextDate;
                        isProgrammaticDateChange = false;
                        InitializeTimeSlots();
                        break;
                    }
                    nextDate = nextDate.AddDays(1);
                }
            }

            EnableUpdateButton();
        }

        private DateTime GetNextWeekday(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        private void btnAddDB_Click(object sender, EventArgs e)
        {
            if (IsWeekend(dtpDateDB.Value.Date))
            {
                MessageBox.Show("Weekend appointments are not allowed.", "Invalid Date",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTimeSlotDB.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a time slot!", "Selection Required",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate time format
            DateTime parsedTime;
            if (!DateTime.TryParseExact(
                cmbTimeSlotDB.Text,
                "HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedTime))
            {
                MessageBox.Show("Invalid time format. Please select a valid time slot.");
                return;
            }
            TimeSpan time = parsedTime.TimeOfDay;

            // Confirmation dialog
            var confirmResult = MessageBox.Show(
                "Are you sure you want to create this appointment?",
                "Confirm New Appointment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {

                        try
                        {
                            // Update blood type if changed
                            if (cmbBloodTypeDB.SelectedItem.ToString() != originalBloodType)
                            {
                                UpdateBloodType(conn, transaction);
                            }

                            // Insert new appointment
                            var insertQuery = @"INSERT INTO tblAppointments 
                                          (donor_id, blood_type, appointment_date, time_slot, status)
                                          VALUES (@donorId, @bloodType, @date, @time, 'Scheduled')";

                            using (var cmd = new MySqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                                cmd.Parameters.AddWithValue("@bloodType", GetBloodTypeValue());
                                cmd.Parameters.AddWithValue("@date", dtpDateDB.Value.Date);
                                cmd.Parameters.AddWithValue("@time", time);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Appointment added successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonors();
                            CheckExistingAppointment();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error adding appointment: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
            }
        }

        private void EnableUpdateButton()
        {
            if (isInitializing) return;

            // 1) Detect whether they actually have an appointment
            bool hasAppointment = btnCancelDB.Enabled;

            // 2) Recompute “blood type changed?”
            string currentBT = cmbBloodTypeDB.SelectedItem?.ToString().Trim() ?? "N/A";
            bool bloodTypeChanged =
                !currentBT.Equals(originalBloodType, StringComparison.OrdinalIgnoreCase);

            // 3) If there’s no appointment, only BT matters
            if (!hasAppointment)
            {
                btnUpdateDB.Enabled = bloodTypeChanged;
                btnAddDB.Enabled = !bloodTypeChanged; // Add button disabled if blood type changed
                return;
            }

            // 4) Otherwise (they do have an appt) also check date/time
            bool dateChanged = dtpDateDB.Value.Date != originalDate;

            bool timeChanged = false;
            if (DateTime.TryParseExact(cmbTimeSlotDB.Text, "HH:mm",
                  CultureInfo.InvariantCulture, DateTimeStyles.None,
                  out DateTime parsed))
            {
                timeChanged = parsed.TimeOfDay != originalTime;
            }

            // **Enable if auto-advanced**
            btnUpdateDB.Enabled = bloodTypeChanged || dateChanged || timeChanged || appointmentAutoAdvanced;
        }


        private void LoadAppointmentDetails()
        {
            isInitializing = true;
            appointmentAutoAdvanced = false;
            isProgrammaticDateChange = true;
            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    var query = @"SELECT appointment_date, time_slot, blood_type, status 
                    FROM tblAppointments 
                    WHERE donor_id = @donorId 
                      AND status = 'Scheduled'";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                originalDate = reader.GetDateTime("appointment_date");
                                originalTime = reader.GetTimeSpan("time_slot");
                                string status = reader["status"].ToString();

                                // Check for passed appointments
                                DateTime appointmentDateTime = originalDate.Add(originalTime);
                                if (originalDate.Date < DateTime.Today && status == "Scheduled")
                                {
                                    // Find the next allowed date and time, but DO NOT update originalDate/originalTime!
                                    DateTime nextDate = DateTime.Today;
                                    if (IsWeekend(nextDate))
                                        nextDate = GetNextWeekday(nextDate);

                                    TimeSpan nextTime = GetNextAvailableTime(nextDate);

                                    // Set the UI controls to the new values
                                    try
                                    {
                                        dtpDateDB.MinDate = new DateTime(1900, 1, 1);
                                        dtpDateDB.Value = nextDate;
                                    }
                                    finally
                                    {
                                        // restore min date after
                                        dtpDateDB.MinDate = DateTime.Today;
                                    }
                                    InitializeTimeSlots();
                                    cmbTimeSlotDB.Text = nextTime.ToString(@"hh\:mm");

                                    appointmentAutoAdvanced = true;
                                }
                                else
                                {
                                    appointmentAutoAdvanced = false;
                                    // Set UI controls to original values
                                    try
                                    {
                                        dtpDateDB.MinDate = new DateTime(1900, 1, 1);
                                        dtpDateDB.Value = originalDate;
                                    }
                                    finally
                                    {
                                        dtpDateDB.MinDate = DateTime.Today;
                                    }
                                    InitializeTimeSlots();
                                    cmbTimeSlotDB.Text = originalTime.ToString(@"hh\:mm");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment details: {ex.Message}");
            }
            finally
            {
                // Handle datepicker limitations
                DateTime currentMinDate = dtpDateDB.MinDate;
                try
                {
                    dtpDateDB.MinDate = new DateTime(1900, 1, 1);
                    dtpDateDB.Value = originalDate;
                }
                finally
                {
                    dtpDateDB.MinDate = currentMinDate;
                }

                InitializeTimeSlots();
                cmbTimeSlotDB.Text = originalTime.ToString(@"hh\:mm");
                isInitializing = false;
                isProgrammaticDateChange = false;
                EnableUpdateButton();
            }
        }


        private TimeSpan GetNextAvailableTime(DateTime date)
        {
            var bookedSlots = GetBookedTimeSlots(date);
            TimeSpan start = new TimeSpan(9, 0, 0);
            DateTime now = DateTime.Now;

            while (start <= new TimeSpan(17, 0, 0))
            {
                if (date.Date == now.Date && start < now.TimeOfDay)
                {
                    start = start.Add(TimeSpan.FromMinutes(30));
                    continue;
                }
                if (!bookedSlots.Contains(start))
                {
                    return start;
                }
                start = start.Add(TimeSpan.FromMinutes(30));
            }
            return start; // Fallback
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            bool hasAppointment = btnCancelDB.Enabled;
            bool changesMade = false;

            string selectedBT = cmbBloodTypeDB.SelectedItem.ToString();
            bool btChanged = !selectedBT.Equals(
        originalBloodType,
        StringComparison.OrdinalIgnoreCase);


            // 2) Figure out if date/time changed (only relevant if an appt exists)
            bool dateChanged = hasAppointment && dtpDateDB.Value.Date != originalDate;
            bool timeChanged = false;
            if (hasAppointment &&
                DateTime.TryParseExact(cmbTimeSlotDB.Text, "HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsed))
            {
                timeChanged = parsed.TimeOfDay != originalTime;
            }



            // Only check for weekend if updating appointment (date/time)
            if ((dateChanged || timeChanged) && IsWeekend(dtpDateDB.Value.Date))
            {
                MessageBox.Show("Weekend appointments are not allowed.", "Invalid Date",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((dateChanged || timeChanged) && cmbTimeSlotDB.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid time slot!", "Selection Required",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string confirmationMessage = "Are you sure you want to update?";

            if (hasAppointment)
            {
                bool updatingAppointment = dateChanged || timeChanged;
                bool updatingBloodType = btChanged;

                if (updatingAppointment && updatingBloodType)
                {
                    confirmationMessage = "Are you sure you want to update the donor's appointment and blood type?";
                }
                else if (updatingAppointment)
                {
                    confirmationMessage = "Are you sure you want to update this appointment?";
                }
                else if (updatingBloodType)
                {
                    confirmationMessage = "Are you sure you want to update the donor's blood type?";
                }
            }
            else
            {
                confirmationMessage = btChanged
                    ? "Are you sure you want to update the donor's blood type?"
                    : "Are you sure you want to save changes?";
            }

            var confirmResult = MessageBox.Show(
                confirmationMessage,
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {


                            // Update blood type if changed
                            if (btChanged)
                            {
                                UpdateBloodType(conn, transaction);
                                changesMade = true;
                            }

                            if (hasAppointment)
                            {
                                // Update appointment details if changed
                                if (dtpDateDB.Value != originalDate ||
                                DateTime.ParseExact(cmbTimeSlotDB.Text, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay != originalTime)
                                {
                                    var updateQuery = @"UPDATE tblAppointments 
                                              SET appointment_date = @date, time_slot = @time 
                                              WHERE donor_id = @donorId 
                                                AND status = 'Scheduled'";

                                    using (var cmd = new MySqlCommand(updateQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@date", dtpDateDB.Value.Date);
                                        cmd.Parameters.AddWithValue(
                                            "@time",
                                            DateTime.ParseExact(
                                                cmbTimeSlotDB.Text,
                                                "HH:mm",
                                                CultureInfo.InvariantCulture)
                                                .TimeOfDay
                                        );
                                        cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                                        cmd.ExecuteNonQuery();
                                        changesMade = true;
                                    }
                                }


                            }

                            if (changesMade)
                            {
                                MessageBox.Show("Update successful!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAppointmentDetails();
                                EnableUpdateButton();
                                LoadDonors();
                                appointmentAutoAdvanced = false;
                                transaction.Commit();
                            }
                            else
                            {
                                MessageBox.Show("No changes detected.", "Information",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                                transaction.Rollback();
                            }
                            LoadAppointmentDetails();
                            LoadDonors();
                            CheckExistingAppointment();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Update failed: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
            }
        }

        private void btnCancelDB_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
            "Are you sure you want to cancel this appointment?\nThis action cannot be undone!",
            "Confirm Cancellation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
            );

            if (confirmResult != DialogResult.Yes)
                return;
            try
            {
                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    var query = @"UPDATE tblAppointments 
                             SET status = 'Cancelled' 
                             WHERE donor_id = @donorId 
                               AND status = 'Scheduled'";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                        var rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment cancelled!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CheckExistingAppointment();
                            ClearSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling appointment: {ex.Message}");
            }
        }

        private void UpdateBloodType(MySqlConnection conn, MySqlTransaction transaction)
        {
            var updateQuery = @"UPDATE tbldonoraccounts 
                          SET blood_type = @bloodType 
                          WHERE donor_id = @donorId";

            using (var cmd = new MySqlCommand(updateQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@bloodType", GetBloodTypeValue());
                cmd.Parameters.AddWithValue("@donorId", selectedDonorId);
                cmd.ExecuteNonQuery();
            }
        }

        private object GetBloodTypeValue()
        {
            return cmbBloodTypeDB.SelectedItem?.ToString() ?? "N/A";
        }

        private void txtSearchBoxDB_TextChanged(object sender, EventArgs e)
        {
            LoadDonors(txtSearchBoxDB.Text);
        }

        private void ClearSelection()
        {
            appointmentAutoAdvanced = false;
            isProgrammaticDateChange = true;
            txtDonorIDDB.Clear();
            cmbBloodTypeDB.SelectedIndex = 8;
            dtpDateDB.Value = DateTime.Today;
            cmbTimeSlotDB.SelectedIndex = -1;
            selectedDonorId = -1;
            originalBloodType = "N/A";
            originalDate = DateTime.Today;
            originalTime = TimeSpan.Zero;
            DisableButtons();
            isProgrammaticDateChange = false;
        }

        private void DisableButtons()
        {
            btnAddDB.Enabled = false;
            btnUpdateDB.Enabled = false;
            btnCancelDB.Enabled = false;
        }

        private void cmbTimeSlotDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableUpdateButton();
        }

        private void cmbBloodTypeDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"Selected Blood Type: {cmbBloodTypeDB.SelectedItem}");
            Console.WriteLine($"Original Blood Type: {originalBloodType}");
            EnableUpdateButton();
        }

        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                || System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
        }

        private void DonateBloodA_VisibleChanged(object sender, EventArgs e)
        {
            // Prevent running in design mode or when not visible
            if (!this.Visible || IsInDesignMode())
                return;

            // Reload donors and reset UI
            LoadDonors();
            ClearSelection();
            // Optionally, reload other data or reset controls as needed
        }
    }
}

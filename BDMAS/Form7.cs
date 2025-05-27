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
using Mysqlx.Crud;

namespace Blood_donation
{
    public partial class Form7: Form
    {
        private int donorId;
        private string currentBloodType;
        private int? existingAppointmentId = null;
        private DateTime originalDate;
        private TimeSpan originalTime;
        public Form7(int donorId)
        {
            InitializeComponent();
            this.donorId = donorId;
            LoadInitialData();
            InitializeControls();
            dtpDate.ValueChanged += DtpDate_ValueChanged;
            cmbTimeSlot.SelectedIndexChanged += cmbTimeSlot_SelectedIndexChanged;
        }

        private void InitializeControls()
        {

            dtpDate.MinDate = DateTime.Today;
            dtpDate.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
        }

        private void LoadInitialData()
        {
            txtDonorID7.Text = donorId.ToString();
            try
            {
                existingAppointmentId = null;

                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    // Get blood type from accounts
                    string bloodTypeQuery = "SELECT blood_type FROM tbldonoraccounts WHERE donor_id = @donorId";
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(bloodTypeQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        currentBloodType = cmd.ExecuteScalar()?.ToString();
                    }

                    // Get existing appointment
                    string appointmentQuery = @"SELECT appointment_id, blood_type, appointment_date, time_slot 
                                  FROM tblappointments 
                                  WHERE donor_id = @donorId 
                                  AND status = 'Scheduled'";
                    using (MySqlCommand cmd = new MySqlCommand(appointmentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime appointmentDate = reader.GetDateTime("appointment_date");

                                // Temporarily extend MaxDate if needed
                                if (appointmentDate > dtpDate.MaxDate)
                                {
                                    dtpDate.MaxDate = appointmentDate.AddDays(1);
                                }

                                dtpDate.Value = appointmentDate;
                                existingAppointmentId = reader.GetInt32("appointment_id");
                                originalDate = reader.GetDateTime("appointment_date");
                                originalTime = (TimeSpan)reader["time_slot"];
                                cboBloodType.Text = reader["blood_type"].ToString();
                                dtpDate.Value = originalDate;
                                // Load time slots for the appointment date
                                LoadTimeSlots(); // Add this line

                                // Find and select the existing time slot
                                var ts = (TimeSpan)reader["time_slot"];
                                string fullSlot = $"{ts:hh\\:mm} - {ts.Add(TimeSpan.FromMinutes(30)):hh\\:mm}";

                                if (!cmbTimeSlot.Items.Contains(fullSlot))
                                    cmbTimeSlot.Items.Insert(0, fullSlot);

                                cmbTimeSlot.SelectedIndex = cmbTimeSlot.Items.IndexOf(fullSlot);
                                ToggleButtons(false);
                            }
                            else
                            {
                                existingAppointmentId = null;
                                ToggleButtons(true);
                                ResetForm();
                            }
                        }
                    }
                    ToggleButtons(!existingAppointmentId.HasValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data load error: " + ex.Message);
            }
            HandleBloodType();
        }

        private void CheckForChanges()
        {
            if (!existingAppointmentId.HasValue) return;

            // Get the currently selected time in the correct format for comparison
            string currentTimeSelection = cmbTimeSlot.Text;
            TimeSpan selectedTime;

            // Parse the selected time, handling both formats (HH:MM and HH:MM - HH:MM)
            if (currentTimeSelection.Contains("-"))
            {
                selectedTime = TimeSpan.Parse(currentTimeSelection.Split('-')[0].Trim());
            }
            else
            {
                // If it's already in HH:MM format
                try
                {
                    selectedTime = TimeSpan.Parse(currentTimeSelection);
                }
                catch
                {
                    // If parse fails, don't enable the button
                    btnResched7.Enabled = false;
                    return;
                }
            }

            bool dateChanged = dtpDate.Value.Date != originalDate.Date;
            bool timeChanged = selectedTime != originalTime;

            // Only enable the button if something actually changed
            btnResched7.Enabled = dateChanged || timeChanged;
        }

        private void DtpDate_ValueChanged(object sender, EventArgs e) => CheckForChanges();
        private void cmbTimeSlot_SelectedIndexChanged(object sender, EventArgs e) => CheckForChanges();

        private void HandleBloodType()
        {
            if (!string.IsNullOrEmpty(currentBloodType))
            {
                cboBloodType.Items.Add(currentBloodType);
                cboBloodType.Text = currentBloodType;
                cboBloodType.Enabled = false;
            }
            else
            {
                cboBloodType.Items.AddRange(new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", "N/A" });
            }
        }

        private void LoadTimeSlots()
        {
            cmbTimeSlot.Items.Clear();
            var baseTime = new TimeSpan(9, 0, 0);
            var allSlots = new List<string>();
            for (int i = 0; i < 17; i++)
            {
                var start = baseTime.Add(TimeSpan.FromMinutes(30 * i));
                var end = start.Add(TimeSpan.FromMinutes(30));
                allSlots.Add($"{start.ToString(@"hh\:mm")} - {end.ToString(@"hh\:mm")}");
            }

            var bookedSlots = new List<string>();
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    string query = @"SELECT time_slot FROM tblappointments 
                           WHERE appointment_date = @selectedDate 
                           AND status = 'Scheduled'
                           AND donor_id != @donorId"; // Key change

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@selectedDate", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@donorId", donorId); // Pass donorId

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var time = (TimeSpan)reader["time_slot"];
                                bookedSlots.Add(time.ToString(@"hh\:mm"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading slots: " + ex.Message);
            }

            // Check if selected date is a weekend
            bool isWeekend = dtpDate.Value.DayOfWeek == DayOfWeek.Saturday ||
                             dtpDate.Value.DayOfWeek == DayOfWeek.Sunday;

            List<string> availableSlots;

            if (isWeekend)
            {
                // No slots on weekends
                availableSlots = new List<string> { "No Time Slots" };
            }
            else
            {
                // Filter out booked slots
                availableSlots = allSlots.Where(slot =>
                    !bookedSlots.Contains(slot.Split('-')[0].Trim())).ToList();

                // Remove past time slots on the current day
                if (dtpDate.Value.Date == DateTime.Today)
                {
                    TimeSpan now = DateTime.Now.TimeOfDay;
                    availableSlots = availableSlots
                        .Where(slot => {
                            TimeSpan slotTime = TimeSpan.Parse(slot.Split('-')[0].Trim());
                            return slotTime >= now;
                        })
                        .ToList();
                }
            }

            // Add available slots to combo box
            cmbTimeSlot.Items.AddRange(availableSlots.ToArray());

            if (isWeekend)
            {
                cmbTimeSlot.SelectedIndex = 0;
            }
            else
            {
                cmbTimeSlot.SelectedIndex = -1;
            }
                
        }

        private void LoadExistingAppointment()
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    string query = @"SELECT date, time_slot 
                           FROM tblAppointments 
                           WHERE appointment_id = @appId";
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appId", existingAppointmentId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update original values for change detection
                                originalDate = reader.GetDateTime("date");
                                originalTime = (TimeSpan)reader["time_slot"];

                                // Update UI without clearing state
                                dtpDate.Value = originalDate;
                                cmbTimeSlot.Text = originalTime.ToString(@"hh\:mm");
                            }
                        }
                    }
                }
                CheckForChanges(); // Update button states
            }
            catch (Exception ex)
            {
                MessageBox.Show("Refresh error: " + ex.Message);
            }
        }
        private bool ValidateTimeSlot()
        {
            if (string.IsNullOrEmpty(cmbTimeSlot.Text))
            {
                MessageBox.Show("Please select a valid time slot!");
                return false;
            }

            try
            {
                TimeSpan.Parse(cmbTimeSlot.Text.Split('-')[0].Trim());
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid time slot format!");
                return false;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(cboBloodType.Text) ||
           string.IsNullOrEmpty(cmbTimeSlot.Text))
            {
                MessageBox.Show("Please fill all fields!");
                return false;
            }

            if (dtpDate.Value.DayOfWeek == DayOfWeek.Saturday ||
                dtpDate.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Weekend bookings not allowed!");
                return false;
            }

            return true;
        }

        private void ToggleButtons(bool isNewAppointment)
        {

            bool hasAppointment = existingAppointmentId.HasValue;

            btnBook7.Enabled = !hasAppointment;
            btnResched7.Enabled = false; // Disabled until changes detected
            btnCancel7.Enabled = hasAppointment;
        }


        private void ResetForm()
        {
            if (!existingAppointmentId.HasValue)
            {
                dtpDate.Value = DateTime.Today;
                cmbTimeSlot.SelectedIndex = -1;
                cmbTimeSlot.Items.Clear();

            }
            LoadTimeSlots();
        }

        private void RefreshForm()
        {
            LoadInitialData();
            ResetForm();
            this.Refresh();
        }












        private void btnHome3_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4(donorId);
            frm4.Show();

            this.Hide();
        }

        private void btnProfile3_Click(object sender, EventArgs e)
        {
            Form5 home = new Form5(donorId);
            this.Hide();
            home.Show();
        }

        private void btnEditProf3_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6(donorId);
            frm6.Show();

            this.Hide();
        }

        private void btnAbout4_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8(donorId);
            frm8.Show();

            this.Hide();
        }

        private void btnCancel7_Click(object sender, EventArgs e)
        {
            if (existingAppointmentId.HasValue)

            {
                // Warning dialog for cancellation
                var result = MessageBox.Show(
                    "Are you sure you want to cancel your appointment?",
                    "Confirm Cancellation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result != DialogResult.Yes)
                    return;

                try
                {
                    using (MySqlConnection conn = DatabasePub.GetConnection())
                    {
                        string query = @"UPDATE tblappointments 
                       SET status = 'Cancelled'
                       WHERE appointment_id = @appId";
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@appId", existingAppointmentId);
                            cmd.ExecuteNonQuery();

                            // Reset the existingAppointmentId
                            existingAppointmentId = null;

                            // Clear the time slot selection
                            cmbTimeSlot.SelectedIndex = -1;
                            cmbTimeSlot.Text = "";

                            // Reset the form and update button states
                            LoadInitialData();
                            ResetForm();
                            ToggleButtons(true);

                            MessageBox.Show("Appointment cancelled!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cancellation error: " + ex.Message);
                }
            }
        }

        private void InjectAndSelectSlot(TimeSpan ts)
        {
            string fullSlot = $"{ts:hh\\:mm} - {ts.Add(TimeSpan.FromMinutes(30)):hh\\:mm}";
            // If DropDownStyle=DropDownList, we must ensure the item exists:
            if (!cmbTimeSlot.Items.Contains(fullSlot))
                cmbTimeSlot.Items.Insert(0, fullSlot);
            cmbTimeSlot.SelectedIndex = cmbTimeSlot.Items.IndexOf(fullSlot);
        }

        private void btnBook7_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            // Confirmation dialog with icon and custom buttons
            var result = MessageBox.Show(
                "Are you sure you want to book this appointment?",
                "Confirm Booking",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result != DialogResult.Yes)
                return;
            try
            {
                // 1) pull the TimeSpan out of the selected item
                var slot = cmbTimeSlot.SelectedItem as string;
                var parts = slot.Split(new[] { " - " }, StringSplitOptions.None);
                TimeSpan bookedTime = TimeSpan.Parse(parts[0]);

                using (var conn = DatabasePub.GetConnection())
                {
                    conn.Open();
                    // 2) INSERT + get ID in one go
                    string sql = @"
                INSERT INTO tblappointments 
                  (donor_id, blood_type, appointment_date, time_slot, status)
                VALUES 
                  (@donorId, @bloodType, @date, @timeSlot, 'Scheduled');
                SELECT LAST_INSERT_ID();";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        cmd.Parameters.AddWithValue("@bloodType", cboBloodType.Text);
                        cmd.Parameters.AddWithValue("@date", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@timeSlot", bookedTime);
                        existingAppointmentId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3) update the accounts table
                    string upd = @"
                UPDATE tbldonoraccounts 
                   SET blood_type = @bloodType 
                 WHERE donor_id = @donorId";
                    using (var cmd2 = new MySqlCommand(upd, conn))
                    {
                        cmd2.Parameters.AddWithValue("@bloodType", cboBloodType.Text);
                        cmd2.Parameters.AddWithValue("@donorId", donorId);
                        cmd2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Appointment booked!", "Success",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToggleButtons(false);

                // 4) refresh the slot list, then inject & select this exact slot
                LoadTimeSlots();
                InjectAndSelectSlot(bookedTime);

                // 5) finally reload everything else if needed
                LoadInitialData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Booking error: " + ex.Message);
            }
        }

        private void btnResched7_Click(object sender, EventArgs e)
        {
            // First verify changes exist before attempting to reschedule
            string currentTimeSelection = cmbTimeSlot.Text;
            TimeSpan selectedTime;

            if (currentTimeSelection.Contains("-"))
            {
                selectedTime = TimeSpan.Parse(currentTimeSelection.Split('-')[0].Trim());
            }
            else
            {
                try
                {
                    selectedTime = TimeSpan.Parse(currentTimeSelection);
                }
                catch
                {
                    MessageBox.Show("Please select a valid time slot.");
                    return;
                }
            }

            bool dateChanged = dtpDate.Value.Date != originalDate.Date;
            bool timeChanged = selectedTime != originalTime;

            // If nothing changed, don't allow rescheduling
            if (!dateChanged && !timeChanged)
            {
                MessageBox.Show("No changes detected. Please select a different date or time to reschedule.");
                btnResched7.Enabled = false;
                return;
            }

            var result = MessageBox.Show(
            "Are you sure you want to reschedule your appointment?",
            "Confirm Reschedule",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2
            );

            if (result != DialogResult.Yes)
                return;

            // Continue with existing validation and rescheduling logic
            if (ValidateInputs())
            {
                try
                {
                    using (MySqlConnection conn = DatabasePub.GetConnection())
                    {
                        string query = @"UPDATE tblAppointments SET
                        appointment_date = @newDate,
                        time_slot = @newTime
                        WHERE appointment_id = @appId";
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@newDate", dtpDate.Value.Date);
                            cmd.Parameters.AddWithValue("@newTime", selectedTime);
                            cmd.Parameters.AddWithValue("@appId", existingAppointmentId);

                            cmd.ExecuteNonQuery();
                            // Update original date/time
                            originalDate = dtpDate.Value.Date;
                            originalTime = selectedTime;
                            MessageBox.Show("Appointment rescheduled!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh UI
                            LoadTimeSlots();
                            InjectAndSelectSlot(selectedTime);
                            LoadInitialData(); // Reload slots for the current date
                            btnResched7.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Reschedule error: " + ex.Message);
                }
            }
        }

        private void cmbTimeSlot_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CheckForChanges();
        }

        private void dtpDate_ValueChanged_1(object sender, EventArgs e)
        {
            // Save the current selected time if any
            string currentSelection = cmbTimeSlot.Text;
            TimeSpan selectedTime = ParseTimeSlot(currentSelection);

            // Reload available time slots for the new date
            LoadTimeSlots();

            if (existingAppointmentId.HasValue)
            {
                // If we're viewing same day as original appointment, show the original time
                if (dtpDate.Value.Date == originalDate.Date)
                {
                    // Find and select the item containing the original time
                    string originalTimeString = originalTime.ToString(@"hh\:mm");
                    for (int i = 0; i < cmbTimeSlot.Items.Count; i++)
                    {
                        string item = cmbTimeSlot.Items[i].ToString();
                        if (item.StartsWith(originalTimeString))
                        {
                            cmbTimeSlot.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    // For a different date, clear selection
                    cmbTimeSlot.SelectedIndex = -1;
                }
            }
            else
            {
                // If not rescheduling, check for existing appointments
                CheckForAppointment();
            }

            // Update button states
            CheckForChanges();
        }


        private void CheckForAppointment()
        {
            using (MySqlConnection conn = DatabasePub.GetConnection())
            {
                string query = @"SELECT time_slot FROM tblappointments 
                         WHERE donor_id = @donorId AND appointment_date = @selectedDate AND status = 'Scheduled'";
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@donorId", donorId);
                    cmd.Parameters.AddWithValue("@selectedDate", dtpDate.Value.Date);
                    var timeSlot = cmd.ExecuteScalar();
                    if (timeSlot != null)
                    {
                        // Set the time slot if an appointment exists for the selected date
                        var ts = (TimeSpan)timeSlot;
                        string fullSlot = $"{ts:hh\\:mm} - {ts.Add(TimeSpan.FromMinutes(30)):hh\\:mm}";
                        int idx = cmbTimeSlot.Items.IndexOf(fullSlot);
                        cmbTimeSlot.SelectedIndex = (idx >= 0) ? idx : -1;
                    }
                    else
                    {
                        // If no appointment exists, clear the time slot
                        cmbTimeSlot.SelectedIndex = -1;
                    }
                }
            }
        }

        private TimeSpan ParseTimeSlot(string timeSlotText)
        {
            // Handle both formats: "HH:MM" and "HH:MM - HH:MM"
            if (string.IsNullOrEmpty(timeSlotText))
            {
                return TimeSpan.Zero;
            }

            string timePart = timeSlotText;
            if (timeSlotText.Contains("-"))
            {
                timePart = timeSlotText.Split('-')[0].Trim();
            }

            try
            {
                return TimeSpan.Parse(timePart);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            LogIn loginForm = new LogIn();
            loginForm.Show();
            this.Close();
        }
    }
}

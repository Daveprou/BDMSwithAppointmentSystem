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

namespace Blood_donation
{
    public partial class Form5: Form
    {
        private int donorId;
        
        public Form5(int donorId)
        {
            InitializeComponent();
            this.donorId = donorId;
            LoadProfileData(donorId);
            LoadCompletedAppointments();
            this.Activated += (s, e) => LoadCompletedAppointments();
        }

        private void LoadProfileData(int donorId)
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {

                    // Modified query to include gender and age
                    string userQuery = @"SELECT donor_id, first_name, last_name, email,
                           birthdate, address, zip_code, gender, age 
                           FROM tbldonoraccounts WHERE donor_id = @donorId";

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(userQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            lblDonorID5.Text = reader["donor_id"].ToString();
                            lblHelloName.Text = reader["first_name"].ToString();
                            lblFName.Text = reader["first_name"].ToString();
                            lblLName.Text = reader["last_name"].ToString();
                            lblEmail.Text = reader["email"].ToString();
                            lblDoB.Text = Convert.ToDateTime(reader["birthdate"]).ToString("MM/dd/yyyy");
                            string rawAddress = reader["address"].ToString();
                            lblAddress.Text = FormatAddress(rawAddress);
                            lblZipCode.Text = reader["zip_code"].ToString();
                            lblBloodType.Text = GetDonorBloodType(donorId);
                            lblGender.Text = reader["gender"].ToString();
                            lblAge.Text = reader["age"].ToString();
                        }
                        reader.Close();
                    }

                    // Load donation stats
                    string donationQuery = @"SELECT 
                    COUNT(CASE WHEN status = 'Completed' THEN 1 END) AS total_donations,
                    COUNT(CASE WHEN status = 'Completed' AND YEAR(appointment_date) = 2025 THEN 1 END) AS 2025_donations
                    FROM tblappointments 
                    WHERE donor_id = @donorId";

                    using (MySqlCommand cmd = new MySqlCommand(donationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            lblAllDon.Text = reader["total_donations"].ToString();
                            lblTYearDon.Text = reader["2025_donations"].ToString();
                        }
                        reader.Close();
                    }

                    // Load upcoming appointment
                    string appointmentQuery = @"SELECT appointment_date, time_slot 
                                          FROM tblappointments 
                                          WHERE donor_id = @donorId 
                                          AND status = 'Scheduled' 
                                          ORDER BY appointment_date ASC 
                                          LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(appointmentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            lblDate.Text = Convert.ToDateTime(reader["appointment_date"]).ToString("MM/dd/yyyy");
                            lblTime.Text = ((TimeSpan)reader["time_slot"]).ToString(@"hh\:mm");
                        }
                        else
                        {
                            lblDate.Text = "No upcoming";
                            lblTime.Text = "appointments";
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private string FormatAddress(string rawAddress)
        {
            // Split address components separated by commas
            string[] parts = rawAddress.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Trim whitespace and join with newlines
            return string.Join(Environment.NewLine, parts.Select(p => p.Trim()));
        }

        private void LoadCompletedAppointments()
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    string query = @"SELECT 
                    appointment_id AS 'Appointment ID',
                    appointment_date AS 'Date',
                    time_slot AS 'Time',
                    status AS 'Status',
                    is_transferred AS 'Transferred'
                    FROM tblappointments 
                    WHERE donor_id = @donorId 
                    AND status = 'Completed'
                    ORDER BY appointment_date DESC";

                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@donorId", donorId);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvDonationH.DataSource = dt;

                    if (dgvDonationH.Columns["Transferred"] != null)
                    {
                        dgvDonationH.Columns["Transferred"].ReadOnly = true;
                        dgvDonationH.Columns["Transferred"].CellTemplate = new DataGridViewCheckBoxCell();
                    }

                    // Make it look nice
                    dgvDonationH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvDonationH.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message);
            }
        }

        private string GetDonorBloodType(int donorId)
        {
            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    string query = @"SELECT blood_type FROM tblappointments 
                    WHERE donor_id = @donorId 
                    ORDER BY appointment_date DESC 
                    LIMIT 1";

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@donorId", donorId);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? "Not Available";
                    }
                }
            }
            catch
            {
                return "Error";
            }
        }

        private void btnHome1_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4(donorId);
            frm4.Show();

            this.Hide();
        }

        private void btnEditProf2_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6(donorId);
            frm6.Show();

            this.Hide();
        }

        private void btnDonate2_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7(donorId);
            frm7.Show();

            this.Hide();
        }

        private void btnAbout2_Click_1(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8(donorId);
            frm8.Show();

            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblHelloName_Click(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            
            this.Close();

            
            using (var loginForm = new LogIn())
            {
                
                loginForm.ClearLoginFields();

                
                
                var result = loginForm.ShowDialog();

                
                
                if (result == DialogResult.OK)
                {
                    this.Show();
                    return;
                }
            }
        }

        private void Form5_Shown(object sender, EventArgs e)
        {
            dgvDonationH.ClearSelection();
            dgvDonationH.CurrentCell = null;
        }
    }
}

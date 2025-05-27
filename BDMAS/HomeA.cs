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
    public partial class HomeA: UserControl
    {
        private Timer refreshTimer;
        public HomeA()
        {
            InitializeComponent();
            LoadStatistics();

            if (!DesignMode)
            {
                refreshTimer = new Timer { Interval = 5000 };
                refreshTimer.Tick += RefreshTimer_Tick;
                SetPlaceholderValues();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (!DesignMode)
            {
                if (this.Visible)
                {
                    // Start operations when control becomes visible
                    refreshTimer?.Start();
                    LoadStatistics();
                }
                else
                {
                    // Stop operations when control is hidden
                    refreshTimer?.Stop();
                }
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode && this.Visible)
            {
                LoadStatistics();
            }
        }

        private void LoadStatistics()
        {
            if (DesignMode)
            {
                SetPlaceholderValues();
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabasePub.GetConnection())
                {
                    conn.Open();

                    // 1. Donors (Completed appointments)
                    int donors = GetCount(conn,
                        "SELECT COUNT(DISTINCT donor_id) FROM tblAppointments WHERE status = 'Completed'");
                    lblDonors1.Text = donors.ToString();

                    // 2. Users (Total accounts)
                    int users = GetCount(conn,
                        "SELECT COUNT(*) FROM tbldonoraccounts");
                    lblUsers1.Text = users.ToString();

                    // 3. Receivers (Patients who will receive blood)
                    int receivers = GetCount(conn,
                        "SELECT COUNT(*) FROM tblPatients");
                    lblReceivers1.Text = receivers.ToString();

                    // 4. Transfers (Blood transfers)
                    int transfers = GetCount(conn,
                        "SELECT COUNT(*) FROM tblBTransfers");
                    lblTransfers1.Text = transfers.ToString();

                    // 5. All Donated Blood (Total blood stock)
                    int totalBlood = GetSum(conn,
                        "SELECT SUM(BloodStock) FROM tblBStock");
                    lblTotalBlood1.Text = totalBlood.ToString();

                    lblST1.Text = GetCount(conn,
                    "SELECT COUNT(*) FROM tblAppointments " +
                    "WHERE status = 'Scheduled' " +
                    "AND appointment_date = CURDATE()").ToString();

                }
            }
            catch (Exception ex)
            {
                if (!DesignMode)
                {
                    SetPlaceholderValues();
                    Console.WriteLine($"Database error: {ex.Message}"); // Silent log
                }
            }
        }

        private void SetPlaceholderValues()
        {
            if (lblDonors1.InvokeRequired)
            {
                lblDonors1.Invoke((MethodInvoker)delegate {
                    lblDonors1.Text = "--";
                    lblUsers1.Text = "--";
                    lblReceivers1.Text = "--";
                    lblTransfers1.Text = "--";
                    lblTotalBlood1.Text = "---";
                    lblST1.Text = "---";
                });
            }
            else
            {
                lblDonors1.Text = "--";
                lblUsers1.Text = "--";
                lblReceivers1.Text = "--";
                lblTransfers1.Text = "--";
                lblTotalBlood1.Text = "---";
                lblST1.Text = "---";
            }
        }

        private int GetCount(MySqlConnection conn, string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int GetSum(MySqlConnection conn, string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        private void lblPatientsCA_Click(object sender, EventArgs e)
        {

        }
    }
}

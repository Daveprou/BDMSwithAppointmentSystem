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
    public partial class BloodStockA: UserControl
    {
        private Timer refreshTimer;
        public BloodStockA()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                InitializeRefreshTimer();
                bloodStock();
                getData();

            }
        }



        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 5000; // 5 seconds
            refreshTimer.Tick += (s, e) =>
            {
                bloodStock();
                getData();
            };
            refreshTimer.Start();
        }

        private void bloodStock()
        {
            try
            {
                using (var Con = DatabasePub.GetConnection())
                {
                    Con.Open();
                    string Query = "SELECT * FROM tblbstock";
                    MySqlDataAdapter sda = new MySqlDataAdapter(Query, Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgvBS6.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading blood stock: {ex.Message}");
            }
        }

        private void getData()
        {
            try
            {
                using (var Con = DatabasePub.GetConnection())
                {
                    Con.Open();

                    // Get total blood stock
                    MySqlCommand totalCmd = new MySqlCommand("SELECT SUM(BloodStock) FROM tblbstock", Con);
                    int BS = Convert.ToInt32(totalCmd.ExecuteScalar());

                    // Helper function remains the same but needs connection parameter
                    Func<MySqlConnection, string, int> getStock = (connection, bloodType) =>
                    {
                        MySqlCommand cmd = new MySqlCommand(
                            "SELECT BloodStock FROM tblbstock WHERE BloodGroup = @type",
                            connection
                        );
                        cmd.Parameters.AddWithValue("@type", bloodType);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    };

                    // Update A+ 
                    int ap = getStock(Con, "A+");
                    lblAPBS.Text = ap.ToString();
                    APCircleProgressBar.Percentage = (ap * 100) / BS;

                    // Update A-
                    int an = getStock(Con, "A-");
                    lblAMBS.Text = an.ToString();
                    AMCircleProgressBar.Percentage = (an * 100) / BS;

                    // Update AB+
                    int abp = getStock(Con, "AB+");
                    lblABPBS.Text = abp.ToString();
                    ABPCircleProgressBar.Percentage = (abp * 100) / BS;

                    // Update AB-
                    int abn = getStock(Con, "AB-");
                    lblABMBS.Text = abn.ToString();
                    ABMCircleProgressBar.Percentage = (abn * 100) / BS;

                    // Update B+
                    int bp = getStock(Con, "B+");
                    lblBPBS.Text = bp.ToString();
                    BPCircleProgressBar.Percentage = (bp * 100) / BS;

                    // Update B-
                    int bn = getStock(Con, "B-");
                    lblBMBS.Text = bn.ToString();
                    BMCircleProgressBar.Percentage = (bn * 100) / BS;

                    // Update O+
                    int op = getStock(Con, "O+");
                    lblOPBS.Text = op.ToString();
                    OPCircleProgressBar.Percentage = (op * 100) / BS;

                    // Update O-
                    int on = getStock(Con, "O-");
                    lblOMBS.Text = on.ToString();
                    OMCircleProgressBar.Percentage = (on * 100) / BS;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}");
            }
        }
    }
}

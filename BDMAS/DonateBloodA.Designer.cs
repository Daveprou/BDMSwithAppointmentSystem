namespace Blood_donation
{
    partial class DonateBloodA
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonateBloodA));
            this.dgvAccountsDB = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDonorIDDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBloodTypeDB = new System.Windows.Forms.ComboBox();
            this.cmbTimeSlotDB = new System.Windows.Forms.ComboBox();
            this.btnAddDB = new Blood_donation.RJButton();
            this.btnUpdateDB = new Blood_donation.RJButton();
            this.btnCancelDB = new Blood_donation.RJButton();
            this.dtpDateDB = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearchBoxDB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAccountsDB
            // 
            this.dgvAccountsDB.AllowUserToAddRows = false;
            this.dgvAccountsDB.AllowUserToDeleteRows = false;
            this.dgvAccountsDB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccountsDB.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAccountsDB.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.dgvAccountsDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountsDB.Location = new System.Drawing.Point(93, 551);
            this.dgvAccountsDB.Name = "dgvAccountsDB";
            this.dgvAccountsDB.ReadOnly = true;
            this.dgvAccountsDB.Size = new System.Drawing.Size(1276, 239);
            this.dgvAccountsDB.TabIndex = 0;
            this.dgvAccountsDB.SelectionChanged += new System.EventHandler(this.dgvAccountsDB_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bell MT", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(547, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(422, 74);
            this.label2.TabIndex = 16;
            this.label2.Text = "Donate Blood";
            // 
            // txtDonorIDDB
            // 
            this.txtDonorIDDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonorIDDB.Location = new System.Drawing.Point(207, 165);
            this.txtDonorIDDB.Name = "txtDonorIDDB";
            this.txtDonorIDDB.ReadOnly = true;
            this.txtDonorIDDB.Size = new System.Drawing.Size(258, 38);
            this.txtDonorIDDB.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(202, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 37;
            this.label1.Text = "DonorID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(202, 358);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 25);
            this.label3.TabIndex = 38;
            this.label3.Text = "Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(829, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 25);
            this.label4.TabIndex = 39;
            this.label4.Text = "Blood Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(829, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 25);
            this.label5.TabIndex = 40;
            this.label5.Text = "Time Slot:";
            // 
            // cmbBloodTypeDB
            // 
            this.cmbBloodTypeDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBloodTypeDB.FormattingEnabled = true;
            this.cmbBloodTypeDB.Location = new System.Drawing.Point(834, 165);
            this.cmbBloodTypeDB.Name = "cmbBloodTypeDB";
            this.cmbBloodTypeDB.Size = new System.Drawing.Size(399, 39);
            this.cmbBloodTypeDB.TabIndex = 45;
            this.cmbBloodTypeDB.SelectedIndexChanged += new System.EventHandler(this.cmbBloodTypeDB_SelectedIndexChanged);
            // 
            // cmbTimeSlotDB
            // 
            this.cmbTimeSlotDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTimeSlotDB.FormattingEnabled = true;
            this.cmbTimeSlotDB.Location = new System.Drawing.Point(834, 316);
            this.cmbTimeSlotDB.Name = "cmbTimeSlotDB";
            this.cmbTimeSlotDB.Size = new System.Drawing.Size(399, 39);
            this.cmbTimeSlotDB.TabIndex = 46;
            this.cmbTimeSlotDB.SelectedIndexChanged += new System.EventHandler(this.cmbTimeSlotDB_SelectedIndexChanged);
            // 
            // btnAddDB
            // 
            this.btnAddDB.BackColor = System.Drawing.Color.DarkGreen;
            this.btnAddDB.FlatAppearance.BorderSize = 0;
            this.btnAddDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDB.ForeColor = System.Drawing.Color.White;
            this.btnAddDB.Location = new System.Drawing.Point(370, 421);
            this.btnAddDB.Name = "btnAddDB";
            this.btnAddDB.Size = new System.Drawing.Size(144, 57);
            this.btnAddDB.TabIndex = 47;
            this.btnAddDB.Text = "ADD";
            this.btnAddDB.UseVisualStyleBackColor = false;
            this.btnAddDB.Click += new System.EventHandler(this.btnAddDB_Click);
            // 
            // btnUpdateDB
            // 
            this.btnUpdateDB.BackColor = System.Drawing.Color.DarkGreen;
            this.btnUpdateDB.FlatAppearance.BorderSize = 0;
            this.btnUpdateDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDB.ForeColor = System.Drawing.Color.White;
            this.btnUpdateDB.Location = new System.Drawing.Point(655, 421);
            this.btnUpdateDB.Name = "btnUpdateDB";
            this.btnUpdateDB.Size = new System.Drawing.Size(144, 57);
            this.btnUpdateDB.TabIndex = 48;
            this.btnUpdateDB.Text = "UPDATE";
            this.btnUpdateDB.UseVisualStyleBackColor = false;
            this.btnUpdateDB.Click += new System.EventHandler(this.btnUpdateDB_Click);
            // 
            // btnCancelDB
            // 
            this.btnCancelDB.BackColor = System.Drawing.Color.DarkGreen;
            this.btnCancelDB.FlatAppearance.BorderSize = 0;
            this.btnCancelDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelDB.ForeColor = System.Drawing.Color.White;
            this.btnCancelDB.Location = new System.Drawing.Point(939, 421);
            this.btnCancelDB.Name = "btnCancelDB";
            this.btnCancelDB.Size = new System.Drawing.Size(144, 57);
            this.btnCancelDB.TabIndex = 49;
            this.btnCancelDB.Text = "CANCEL";
            this.btnCancelDB.UseVisualStyleBackColor = false;
            this.btnCancelDB.Click += new System.EventHandler(this.btnCancelDB_Click);
            // 
            // dtpDateDB
            // 
            this.dtpDateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateDB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDB.Location = new System.Drawing.Point(207, 317);
            this.dtpDateDB.Name = "dtpDateDB";
            this.dtpDateDB.Size = new System.Drawing.Size(406, 38);
            this.dtpDateDB.TabIndex = 50;
            this.dtpDateDB.ValueChanged += new System.EventHandler(this.dtpDateDB_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(111, 514);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 25);
            this.label6.TabIndex = 51;
            this.label6.Text = "Search:";
            // 
            // txtSearchBoxDB
            // 
            this.txtSearchBoxDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchBoxDB.Location = new System.Drawing.Point(203, 505);
            this.txtSearchBoxDB.Name = "txtSearchBoxDB";
            this.txtSearchBoxDB.Size = new System.Drawing.Size(258, 38);
            this.txtSearchBoxDB.TabIndex = 52;
            this.txtSearchBoxDB.TextChanged += new System.EventHandler(this.txtSearchBoxDB_TextChanged);
            // 
            // DonateBloodA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.txtSearchBoxDB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDateDB);
            this.Controls.Add(this.btnCancelDB);
            this.Controls.Add(this.btnUpdateDB);
            this.Controls.Add(this.btnAddDB);
            this.Controls.Add(this.cmbTimeSlotDB);
            this.Controls.Add(this.cmbBloodTypeDB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDonorIDDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAccountsDB);
            this.Name = "DonateBloodA";
            this.Size = new System.Drawing.Size(1476, 810);
            this.VisibleChanged += new System.EventHandler(this.DonateBloodA_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountsDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAccountsDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDonorIDDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbBloodTypeDB;
        private System.Windows.Forms.ComboBox cmbTimeSlotDB;
        private RJButton btnAddDB;
        private RJButton btnUpdateDB;
        private RJButton btnCancelDB;
        private System.Windows.Forms.DateTimePicker dtpDateDB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearchBoxDB;
    }
}

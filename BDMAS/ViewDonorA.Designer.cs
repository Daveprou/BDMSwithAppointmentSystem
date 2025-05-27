namespace Blood_donation
{
    partial class ViewDonorA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewDonorA));
            this.dgvAppointmentsVD = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchBoxVD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStatusVD = new System.Windows.Forms.ComboBox();
            this.btnScheduledVD = new Blood_donation.RJButton();
            this.btnCancelledVD = new Blood_donation.RJButton();
            this.btnCompletedVD = new Blood_donation.RJButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsVD)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAppointmentsVD
            // 
            this.dgvAppointmentsVD.AllowUserToAddRows = false;
            this.dgvAppointmentsVD.AllowUserToDeleteRows = false;
            this.dgvAppointmentsVD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointmentsVD.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAppointmentsVD.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.dgvAppointmentsVD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentsVD.Location = new System.Drawing.Point(95, 201);
            this.dgvAppointmentsVD.Name = "dgvAppointmentsVD";
            this.dgvAppointmentsVD.ReadOnly = true;
            this.dgvAppointmentsVD.Size = new System.Drawing.Size(1274, 374);
            this.dgvAppointmentsVD.TabIndex = 1;
            this.dgvAppointmentsVD.SelectionChanged += new System.EventHandler(this.dgvAppointmentsVD_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bell MT", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(563, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(372, 74);
            this.label2.TabIndex = 3;
            this.label2.Text = "View Donor";
            // 
            // txtSearchBoxVD
            // 
            this.txtSearchBoxVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchBoxVD.Location = new System.Drawing.Point(235, 148);
            this.txtSearchBoxVD.Name = "txtSearchBoxVD";
            this.txtSearchBoxVD.Size = new System.Drawing.Size(192, 38);
            this.txtSearchBoxVD.TabIndex = 14;
            this.txtSearchBoxVD.TextChanged += new System.EventHandler(this.txtSearchIDVD_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(117, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 25);
            this.label1.TabIndex = 37;
            this.label1.Text = "Search ID:";
            // 
            // cmbStatusVD
            // 
            this.cmbStatusVD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.cmbStatusVD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusVD.FormattingEnabled = true;
            this.cmbStatusVD.Location = new System.Drawing.Point(1149, 143);
            this.cmbStatusVD.Name = "cmbStatusVD";
            this.cmbStatusVD.Size = new System.Drawing.Size(199, 39);
            this.cmbStatusVD.TabIndex = 45;
            this.cmbStatusVD.SelectedIndexChanged += new System.EventHandler(this.cmbStatusVD_SelectedIndexChanged);
            // 
            // btnScheduledVD
            // 
            this.btnScheduledVD.BackColor = System.Drawing.Color.DarkGreen;
            this.btnScheduledVD.FlatAppearance.BorderSize = 0;
            this.btnScheduledVD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduledVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScheduledVD.ForeColor = System.Drawing.Color.White;
            this.btnScheduledVD.Location = new System.Drawing.Point(274, 621);
            this.btnScheduledVD.Name = "btnScheduledVD";
            this.btnScheduledVD.Size = new System.Drawing.Size(201, 76);
            this.btnScheduledVD.TabIndex = 46;
            this.btnScheduledVD.Text = "SCHEDULED";
            this.btnScheduledVD.UseVisualStyleBackColor = false;
            this.btnScheduledVD.Click += new System.EventHandler(this.btnScheduledVD_Click);
            // 
            // btnCancelledVD
            // 
            this.btnCancelledVD.BackColor = System.Drawing.Color.DarkGreen;
            this.btnCancelledVD.FlatAppearance.BorderSize = 0;
            this.btnCancelledVD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelledVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelledVD.ForeColor = System.Drawing.Color.White;
            this.btnCancelledVD.Location = new System.Drawing.Point(650, 621);
            this.btnCancelledVD.Name = "btnCancelledVD";
            this.btnCancelledVD.Size = new System.Drawing.Size(201, 76);
            this.btnCancelledVD.TabIndex = 47;
            this.btnCancelledVD.Text = "CANCELLED";
            this.btnCancelledVD.UseVisualStyleBackColor = false;
            this.btnCancelledVD.Click += new System.EventHandler(this.btnCanceledVD_Click);
            // 
            // btnCompletedVD
            // 
            this.btnCompletedVD.BackColor = System.Drawing.Color.DarkGreen;
            this.btnCompletedVD.FlatAppearance.BorderSize = 0;
            this.btnCompletedVD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompletedVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompletedVD.ForeColor = System.Drawing.Color.White;
            this.btnCompletedVD.Location = new System.Drawing.Point(1048, 621);
            this.btnCompletedVD.Name = "btnCompletedVD";
            this.btnCompletedVD.Size = new System.Drawing.Size(201, 76);
            this.btnCompletedVD.TabIndex = 48;
            this.btnCompletedVD.Text = "COMPLETED";
            this.btnCompletedVD.UseVisualStyleBackColor = false;
            this.btnCompletedVD.Click += new System.EventHandler(this.btnCompletedVD_Click);
            // 
            // ViewDonorA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnCompletedVD);
            this.Controls.Add(this.btnCancelledVD);
            this.Controls.Add(this.btnScheduledVD);
            this.Controls.Add(this.cmbStatusVD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearchBoxVD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAppointmentsVD);
            this.Name = "ViewDonorA";
            this.Size = new System.Drawing.Size(1476, 810);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsVD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAppointmentsVD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchBoxVD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatusVD;
        private RJButton btnScheduledVD;
        private RJButton btnCancelledVD;
        private RJButton btnCompletedVD;
    }
}

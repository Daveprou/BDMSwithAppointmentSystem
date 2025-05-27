namespace Blood_donation
{
    partial class BloodTransferA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BloodTransferA));
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPatientsIDBT = new System.Windows.Forms.ComboBox();
            this.txtNameBT = new System.Windows.Forms.TextBox();
            this.txtBGBT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvTransfers = new System.Windows.Forms.DataGridView();
            this.lblAvailLabel = new System.Windows.Forms.Label();
            this.btnTransfer = new Blood_donation.RJButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfers)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bell MT", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(532, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(459, 74);
            this.label2.TabIndex = 4;
            this.label2.Text = "Blood Transfer";
            // 
            // cmbPatientsIDBT
            // 
            this.cmbPatientsIDBT.BackColor = System.Drawing.Color.White;
            this.cmbPatientsIDBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientsIDBT.FormattingEnabled = true;
            this.cmbPatientsIDBT.Location = new System.Drawing.Point(206, 179);
            this.cmbPatientsIDBT.Name = "cmbPatientsIDBT";
            this.cmbPatientsIDBT.Size = new System.Drawing.Size(228, 39);
            this.cmbPatientsIDBT.TabIndex = 46;
            this.cmbPatientsIDBT.SelectedIndexChanged += new System.EventHandler(this.cmbPatientsIDBT_SelectedIndexChanged);
            // 
            // txtNameBT
            // 
            this.txtNameBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameBT.Location = new System.Drawing.Point(638, 179);
            this.txtNameBT.Name = "txtNameBT";
            this.txtNameBT.ReadOnly = true;
            this.txtNameBT.Size = new System.Drawing.Size(223, 38);
            this.txtNameBT.TabIndex = 47;
            // 
            // txtBGBT
            // 
            this.txtBGBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBGBT.Location = new System.Drawing.Point(1039, 179);
            this.txtBGBT.Name = "txtBGBT";
            this.txtBGBT.ReadOnly = true;
            this.txtBGBT.Size = new System.Drawing.Size(210, 38);
            this.txtBGBT.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(201, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 25);
            this.label1.TabIndex = 49;
            this.label1.Text = "PatientID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(633, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 50;
            this.label3.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1034, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 51;
            this.label4.Text = "Blood Group:";
            // 
            // dgvTransfers
            // 
            this.dgvTransfers.AllowUserToAddRows = false;
            this.dgvTransfers.AllowUserToDeleteRows = false;
            this.dgvTransfers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransfers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTransfers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransfers.Location = new System.Drawing.Point(97, 522);
            this.dgvTransfers.Name = "dgvTransfers";
            this.dgvTransfers.ReadOnly = true;
            this.dgvTransfers.Size = new System.Drawing.Size(1272, 222);
            this.dgvTransfers.TabIndex = 52;
            // 
            // lblAvailLabel
            // 
            this.lblAvailLabel.AutoSize = true;
            this.lblAvailLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblAvailLabel.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAvailLabel.Location = new System.Drawing.Point(641, 275);
            this.lblAvailLabel.Name = "lblAvailLabel";
            this.lblAvailLabel.Size = new System.Drawing.Size(175, 34);
            this.lblAvailLabel.TabIndex = 53;
            this.lblAvailLabel.Text = "AvailOrNot";
            this.lblAvailLabel.Visible = false;
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.Color.DarkGreen;
            this.btnTransfer.FlatAppearance.BorderSize = 0;
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.ForeColor = System.Drawing.Color.White;
            this.btnTransfer.Location = new System.Drawing.Point(648, 376);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(207, 54);
            this.btnTransfer.TabIndex = 54;
            this.btnTransfer.Text = "TRANSFER";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // BloodTransferA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.lblAvailLabel);
            this.Controls.Add(this.dgvTransfers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBGBT);
            this.Controls.Add(this.txtNameBT);
            this.Controls.Add(this.cmbPatientsIDBT);
            this.Controls.Add(this.label2);
            this.Name = "BloodTransferA";
            this.Size = new System.Drawing.Size(1476, 810);
            this.Load += new System.EventHandler(this.BloodTransferA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPatientsIDBT;
        private System.Windows.Forms.TextBox txtNameBT;
        private System.Windows.Forms.TextBox txtBGBT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvTransfers;
        private System.Windows.Forms.Label lblAvailLabel;
        private RJButton btnTransfer;
    }
}

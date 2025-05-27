namespace Blood_donation
{
    partial class ViewPatientsA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewPatientsA));
            this.dgvPatientsPL = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSearchPL = new System.Windows.Forms.TextBox();
            this.btnDeletePL = new Blood_donation.RJButton();
            this.btnUpdatePL = new Blood_donation.RJButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddressPL = new System.Windows.Forms.TextBox();
            this.txtAgePL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLNamePL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFNamePL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpBDPL = new System.Windows.Forms.DateTimePicker();
            this.cmbGenderPL = new System.Windows.Forms.ComboBox();
            this.cmbBGPL = new System.Windows.Forms.ComboBox();
            this.txtPhonePL = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientsPL)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPatientsPL
            // 
            this.dgvPatientsPL.AllowUserToAddRows = false;
            this.dgvPatientsPL.AllowUserToDeleteRows = false;
            this.dgvPatientsPL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatientsPL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPatientsPL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatientsPL.Location = new System.Drawing.Point(94, 587);
            this.dgvPatientsPL.Name = "dgvPatientsPL";
            this.dgvPatientsPL.ReadOnly = true;
            this.dgvPatientsPL.Size = new System.Drawing.Size(1275, 197);
            this.dgvPatientsPL.TabIndex = 53;
            this.dgvPatientsPL.SelectionChanged += new System.EventHandler(this.dgvPatientsPL_SelectionChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(130, 552);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 25);
            this.label15.TabIndex = 109;
            this.label15.Text = "Search:";
            // 
            // txtSearchPL
            // 
            this.txtSearchPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchPL.Location = new System.Drawing.Point(222, 543);
            this.txtSearchPL.Name = "txtSearchPL";
            this.txtSearchPL.Size = new System.Drawing.Size(154, 38);
            this.txtSearchPL.TabIndex = 108;
            this.txtSearchPL.TextChanged += new System.EventHandler(this.txtSearchPL_TextChanged);
            // 
            // btnDeletePL
            // 
            this.btnDeletePL.BackColor = System.Drawing.Color.DarkGreen;
            this.btnDeletePL.FlatAppearance.BorderSize = 0;
            this.btnDeletePL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePL.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeletePL.ForeColor = System.Drawing.Color.White;
            this.btnDeletePL.Location = new System.Drawing.Point(756, 512);
            this.btnDeletePL.Name = "btnDeletePL";
            this.btnDeletePL.Size = new System.Drawing.Size(155, 51);
            this.btnDeletePL.TabIndex = 107;
            this.btnDeletePL.Text = "DELETE";
            this.btnDeletePL.UseVisualStyleBackColor = false;
            this.btnDeletePL.Click += new System.EventHandler(this.btnDeletePL_Click);
            // 
            // btnUpdatePL
            // 
            this.btnUpdatePL.BackColor = System.Drawing.Color.DarkGreen;
            this.btnUpdatePL.FlatAppearance.BorderSize = 0;
            this.btnUpdatePL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdatePL.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePL.ForeColor = System.Drawing.Color.White;
            this.btnUpdatePL.Location = new System.Drawing.Point(534, 512);
            this.btnUpdatePL.Name = "btnUpdatePL";
            this.btnUpdatePL.Size = new System.Drawing.Size(155, 51);
            this.btnUpdatePL.TabIndex = 106;
            this.btnUpdatePL.Text = "UPDATE";
            this.btnUpdatePL.UseVisualStyleBackColor = false;
            this.btnUpdatePL.Click += new System.EventHandler(this.btnUpdatePL_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(932, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 25);
            this.label12.TabIndex = 104;
            this.label12.Text = "Phone No:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(929, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 25);
            this.label6.TabIndex = 103;
            this.label6.Text = "Age:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(927, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 25);
            this.label5.TabIndex = 102;
            this.label5.Text = "Birth Date:";
            // 
            // txtAddressPL
            // 
            this.txtAddressPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressPL.Location = new System.Drawing.Point(932, 361);
            this.txtAddressPL.Multiline = true;
            this.txtAddressPL.Name = "txtAddressPL";
            this.txtAddressPL.Size = new System.Drawing.Size(266, 115);
            this.txtAddressPL.TabIndex = 101;
            // 
            // txtAgePL
            // 
            this.txtAgePL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAgePL.Location = new System.Drawing.Point(932, 191);
            this.txtAgePL.Name = "txtAgePL";
            this.txtAgePL.ReadOnly = true;
            this.txtAgePL.Size = new System.Drawing.Size(266, 38);
            this.txtAgePL.TabIndex = 100;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(274, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 25);
            this.label7.TabIndex = 98;
            this.label7.Text = "Last Name:";
            // 
            // txtLNamePL
            // 
            this.txtLNamePL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLNamePL.Location = new System.Drawing.Point(277, 204);
            this.txtLNamePL.Name = "txtLNamePL";
            this.txtLNamePL.Size = new System.Drawing.Size(266, 38);
            this.txtLNamePL.TabIndex = 97;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(273, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 96;
            this.label4.Text = "Blood Group:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(272, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 25);
            this.label3.TabIndex = 95;
            this.label3.Text = "Gender:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(275, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 94;
            this.label1.Text = "First Name:";
            // 
            // txtFNamePL
            // 
            this.txtFNamePL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFNamePL.Location = new System.Drawing.Point(277, 117);
            this.txtFNamePL.Name = "txtFNamePL";
            this.txtFNamePL.Size = new System.Drawing.Size(266, 38);
            this.txtFNamePL.TabIndex = 91;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(932, 479);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 25);
            this.label2.TabIndex = 110;
            this.label2.Text = "Address:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Bell MT", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(557, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(369, 56);
            this.label8.TabIndex = 111;
            this.label8.Text = "PATIENT LIST";
            // 
            // dtpBDPL
            // 
            this.dtpBDPL.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBDPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBDPL.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBDPL.Location = new System.Drawing.Point(932, 113);
            this.dtpBDPL.Name = "dtpBDPL";
            this.dtpBDPL.Size = new System.Drawing.Size(266, 38);
            this.dtpBDPL.TabIndex = 112;
            this.dtpBDPL.ValueChanged += new System.EventHandler(this.dtpBDatePL_ValueChanged);
            // 
            // cmbGenderPL
            // 
            this.cmbGenderPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenderPL.FormattingEnabled = true;
            this.cmbGenderPL.Location = new System.Drawing.Point(277, 294);
            this.cmbGenderPL.Name = "cmbGenderPL";
            this.cmbGenderPL.Size = new System.Drawing.Size(266, 39);
            this.cmbGenderPL.TabIndex = 113;
            // 
            // cmbBGPL
            // 
            this.cmbBGPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBGPL.FormattingEnabled = true;
            this.cmbBGPL.Location = new System.Drawing.Point(280, 396);
            this.cmbBGPL.Name = "cmbBGPL";
            this.cmbBGPL.Size = new System.Drawing.Size(263, 39);
            this.cmbBGPL.TabIndex = 114;
            // 
            // txtPhonePL
            // 
            this.txtPhonePL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhonePL.Location = new System.Drawing.Point(932, 275);
            this.txtPhonePL.Name = "txtPhonePL";
            this.txtPhonePL.Size = new System.Drawing.Size(266, 38);
            this.txtPhonePL.TabIndex = 115;
            // 
            // ViewPatientsA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.txtPhonePL);
            this.Controls.Add(this.cmbBGPL);
            this.Controls.Add(this.cmbGenderPL);
            this.Controls.Add(this.dtpBDPL);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtSearchPL);
            this.Controls.Add(this.btnDeletePL);
            this.Controls.Add(this.btnUpdatePL);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAddressPL);
            this.Controls.Add(this.txtAgePL);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLNamePL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFNamePL);
            this.Controls.Add(this.dgvPatientsPL);
            this.Name = "ViewPatientsA";
            this.Size = new System.Drawing.Size(1476, 810);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientsPL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPatientsPL;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSearchPL;
        private RJButton btnDeletePL;
        private RJButton btnUpdatePL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddressPL;
        private System.Windows.Forms.TextBox txtAgePL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLNamePL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFNamePL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpBDPL;
        private System.Windows.Forms.ComboBox cmbGenderPL;
        private System.Windows.Forms.ComboBox cmbBGPL;
        private System.Windows.Forms.TextBox txtPhonePL;
    }
}

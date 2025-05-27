namespace Blood_donation
{
    partial class FrontPage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDonate1 = new Blood_donation.RJButton();
            this.SuspendLayout();
            // 
            // btnDonate1
            // 
            this.btnDonate1.BackColor = System.Drawing.Color.Firebrick;
            this.btnDonate1.FlatAppearance.BorderSize = 0;
            this.btnDonate1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDonate1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonate1.ForeColor = System.Drawing.Color.White;
            this.btnDonate1.Location = new System.Drawing.Point(511, 787);
            this.btnDonate1.Name = "btnDonate1";
            this.btnDonate1.Size = new System.Drawing.Size(314, 65);
            this.btnDonate1.TabIndex = 4;
            this.btnDonate1.Text = "DONATE NOW";
            this.btnDonate1.UseVisualStyleBackColor = false;
            this.btnDonate1.Click += new System.EventHandler(this.btn_Donate1_Click);
            // 
            // FrontPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Blood_donation.Properties.Resources.BIRINGAN_GENERAL_HOSPITAL__1920_x_1080_px___4_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.btnDonate1);
            this.DoubleBuffered = true;
            this.Name = "FrontPage";
            this.Text = "7";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrontPage_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private RJButton btnDonate1;
    }
}


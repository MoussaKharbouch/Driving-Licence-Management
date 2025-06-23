namespace DVLDPresentationLayer.Tests
{
    partial class frmTestAppointments
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbTestType = new System.Windows.Forms.PictureBox();
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlDrivingLicenseDLApplicationInfo1 = new DVLDPresentationLayer.Controls.ctrlDrivingLicenseDLApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(197, 111);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(266, 24);
            this.lblTitle.TabIndex = 47;
            this.lblTitle.Text = "Vision Test Appointments";
            // 
            // pbTestType
            // 
            this.pbTestType.Image = global::DVLDPresentationLayer.Properties.Resources.Vision_512;
            this.pbTestType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbTestType.Location = new System.Drawing.Point(282, 1);
            this.pbTestType.Name = "pbTestType";
            this.pbTestType.Size = new System.Drawing.Size(110, 107);
            this.pbTestType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestType.TabIndex = 48;
            this.pbTestType.TabStop = false;
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.AllowUserToDeleteRows = false;
            this.dgvPeople.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeople.Location = new System.Drawing.Point(26, 470);
            this.dgvPeople.MultiSelect = false;
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.RowTemplate.ReadOnly = true;
            this.dgvPeople.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPeople.Size = new System.Drawing.Size(613, 186);
            this.dgvPeople.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(23, 450);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 51;
            this.label3.Text = "Appointments:";
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.BackColor = System.Drawing.Color.White;
            this.btnAddAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAppointment.Image = global::DVLDPresentationLayer.Properties.Resources.AddAppointment_32;
            this.btnAddAppointment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddAppointment.Location = new System.Drawing.Point(600, 431);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(39, 33);
            this.btnAddAppointment.TabIndex = 52;
            this.btnAddAppointment.UseVisualStyleBackColor = false;
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblRecords.ForeColor = System.Drawing.Color.Black;
            this.lblRecords.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRecords.Location = new System.Drawing.Point(363, 659);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(17, 17);
            this.lblRecords.TabIndex = 54;
            this.lblRecords.Text = "0";
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(288, 659);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 53;
            this.label1.Text = "Records:";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(553, 662);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 29);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlDrivingLicenseDLApplicationInfo1
            // 
            this.ctrlDrivingLicenseDLApplicationInfo1.Location = new System.Drawing.Point(26, 138);
            this.ctrlDrivingLicenseDLApplicationInfo1.Name = "ctrlDrivingLicenseDLApplicationInfo1";
            this.ctrlDrivingLicenseDLApplicationInfo1.Size = new System.Drawing.Size(624, 287);
            this.ctrlDrivingLicenseDLApplicationInfo1.TabIndex = 49;
            // 
            // frmTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 700);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvPeople);
            this.Controls.Add(this.ctrlDrivingLicenseDLApplicationInfo1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbTestType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision Test Appointments";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbTestType;
        private Controls.ctrlDrivingLicenseDLApplicationInfo ctrlDrivingLicenseDLApplicationInfo1;
        private System.Windows.Forms.DataGridView dgvPeople;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;

    }
}
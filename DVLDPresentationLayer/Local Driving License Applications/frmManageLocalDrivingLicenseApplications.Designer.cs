namespace DVLDPresentationLayer.Local_Driving_License_Applications
{
    partial class frmManageLocalDrivingLicenseApplications
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddLDLApplication = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLDLApplications = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cmsLocalDrivingLicenseApplication = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsScheduleTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsIssueDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.cmsLocalDrivingLicenseApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(189, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(439, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "Manage Local Driving License Applications";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDPresentationLayer.Properties.Resources.Manage_Applications_64;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(351, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 107);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(224, 211);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(100, 20);
            this.tbValue.TabIndex = 29;
            this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // cbFilters
            // 
            this.cbFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilters.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Location = new System.Drawing.Point(97, 211);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(121, 21);
            this.cbFilters.TabIndex = 28;
            this.cbFilters.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(22, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "Filter By:";
            // 
            // btnAddLDLApplication
            // 
            this.btnAddLDLApplication.BackColor = System.Drawing.Color.White;
            this.btnAddLDLApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLDLApplication.Image = global::DVLDPresentationLayer.Properties.Resources.New_Application_64;
            this.btnAddLDLApplication.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddLDLApplication.Location = new System.Drawing.Point(695, 160);
            this.btnAddLDLApplication.Name = "btnAddLDLApplication";
            this.btnAddLDLApplication.Size = new System.Drawing.Size(71, 69);
            this.btnAddLDLApplication.TabIndex = 26;
            this.btnAddLDLApplication.UseVisualStyleBackColor = false;
            this.btnAddLDLApplication.Click += new System.EventHandler(this.AddLocalDrivingLicenseApplication_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblRecords.ForeColor = System.Drawing.Color.Black;
            this.lblRecords.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRecords.Location = new System.Drawing.Point(431, 498);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(17, 17);
            this.lblRecords.TabIndex = 25;
            this.lblRecords.Text = "0";
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(356, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Records:";
            // 
            // dgvLDLApplications
            // 
            this.dgvLDLApplications.AllowUserToAddRows = false;
            this.dgvLDLApplications.AllowUserToDeleteRows = false;
            this.dgvLDLApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLDLApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLDLApplications.Location = new System.Drawing.Point(25, 235);
            this.dgvLDLApplications.MultiSelect = false;
            this.dgvLDLApplications.Name = "dgvLDLApplications";
            this.dgvLDLApplications.RowTemplate.ReadOnly = true;
            this.dgvLDLApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLDLApplications.Size = new System.Drawing.Size(741, 261);
            this.dgvLDLApplications.TabIndex = 23;
            this.dgvLDLApplications.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLDLApplications_CellMouseClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLDPresentationLayer.Properties.Resources.Local_32;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(445, 55);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 29);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 31;
            this.pictureBox2.TabStop = false;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "All",
            "New",
            "Canceled",
            "Completed"});
            this.cbStatus.Location = new System.Drawing.Point(224, 212);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(100, 21);
            this.cbStatus.TabIndex = 33;
            this.cbStatus.Visible = false;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // cmsLocalDrivingLicenseApplication
            // 
            this.cmsLocalDrivingLicenseApplication.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsShowDetails,
            this.toolStripSeparator2,
            this.tsEdit,
            this.tsDelete,
            this.toolStripSeparator3,
            this.tsCancel,
            this.tsScheduleTest,
            this.toolStripSeparator4,
            this.tsIssueDrivingLicense,
            this.toolStripSeparator1,
            this.tsShowLicense,
            this.toolStripSeparator5,
            this.toolStripMenuItem4});
            this.cmsLocalDrivingLicenseApplication.Name = "cmsPerson";
            this.cmsLocalDrivingLicenseApplication.Size = new System.Drawing.Size(247, 232);
            this.cmsLocalDrivingLicenseApplication.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLocalDrivingLicenseApplication_Opening);
            // 
            // tsShowDetails
            // 
            this.tsShowDetails.Image = global::DVLDPresentationLayer.Properties.Resources.PersonDetails_32;
            this.tsShowDetails.Name = "tsShowDetails";
            this.tsShowDetails.Size = new System.Drawing.Size(246, 22);
            this.tsShowDetails.Text = "Show Application Details";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(243, 6);
            // 
            // tsEdit
            // 
            this.tsEdit.Image = global::DVLDPresentationLayer.Properties.Resources.edit_32;
            this.tsEdit.Name = "tsEdit";
            this.tsEdit.Size = new System.Drawing.Size(246, 22);
            this.tsEdit.Text = "Edit Application";
            // 
            // tsDelete
            // 
            this.tsDelete.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32_2;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(246, 22);
            this.tsDelete.Text = "Delete Application";
            this.tsDelete.Click += new System.EventHandler(this.DeleteLocalDrivingLicenseApplication);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(243, 6);
            // 
            // tsCancel
            // 
            this.tsCancel.Enabled = false;
            this.tsCancel.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32;
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(246, 22);
            this.tsCancel.Text = "Cancel Application";
            this.tsCancel.Click += new System.EventHandler(this.tsCancel_Click);
            // 
            // tsScheduleTest
            // 
            this.tsScheduleTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsVisionTest,
            this.tsWrittenTest,
            this.tsStreetTest});
            this.tsScheduleTest.Enabled = false;
            this.tsScheduleTest.Image = global::DVLDPresentationLayer.Properties.Resources.Schedule_Test_512;
            this.tsScheduleTest.Name = "tsScheduleTest";
            this.tsScheduleTest.Size = new System.Drawing.Size(246, 22);
            this.tsScheduleTest.Text = "Schedule Test";
            this.tsScheduleTest.DropDownOpening += new System.EventHandler(this.tsScheduleTest_DropDownOpening);
            // 
            // tsVisionTest
            // 
            this.tsVisionTest.Enabled = false;
            this.tsVisionTest.Image = global::DVLDPresentationLayer.Properties.Resources.Vision_512;
            this.tsVisionTest.Name = "tsVisionTest";
            this.tsVisionTest.Size = new System.Drawing.Size(152, 22);
            this.tsVisionTest.Text = "Vision Test";
            this.tsVisionTest.Click += new System.EventHandler(this.tsVisionTest_Click);
            // 
            // tsWrittenTest
            // 
            this.tsWrittenTest.Enabled = false;
            this.tsWrittenTest.Image = global::DVLDPresentationLayer.Properties.Resources.Written_Test_32;
            this.tsWrittenTest.Name = "tsWrittenTest";
            this.tsWrittenTest.Size = new System.Drawing.Size(152, 22);
            this.tsWrittenTest.Text = "Written Test";
            this.tsWrittenTest.Click += new System.EventHandler(this.tsWrittenTest_Click);
            // 
            // tsStreetTest
            // 
            this.tsStreetTest.Enabled = false;
            this.tsStreetTest.Image = global::DVLDPresentationLayer.Properties.Resources.Street_Test_32;
            this.tsStreetTest.Name = "tsStreetTest";
            this.tsStreetTest.Size = new System.Drawing.Size(152, 22);
            this.tsStreetTest.Text = "Street Test";
            this.tsStreetTest.Click += new System.EventHandler(this.tsStreetTest_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(243, 6);
            // 
            // tsIssueDrivingLicense
            // 
            this.tsIssueDrivingLicense.Enabled = false;
            this.tsIssueDrivingLicense.Image = global::DVLDPresentationLayer.Properties.Resources.IssueDrivingLicense_32;
            this.tsIssueDrivingLicense.Name = "tsIssueDrivingLicense";
            this.tsIssueDrivingLicense.Size = new System.Drawing.Size(246, 22);
            this.tsIssueDrivingLicense.Text = "Issue Driving License (First Time)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(243, 6);
            // 
            // tsShowLicense
            // 
            this.tsShowLicense.Enabled = false;
            this.tsShowLicense.Image = global::DVLDPresentationLayer.Properties.Resources.LicenseView_400;
            this.tsShowLicense.Name = "tsShowLicense";
            this.tsShowLicense.Size = new System.Drawing.Size(246, 22);
            this.tsShowLicense.Text = "Show License";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(243, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = global::DVLDPresentationLayer.Properties.Resources.PersonLicenseHistory_32;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(246, 22);
            this.toolStripMenuItem4.Text = "Show Person License History";
            // 
            // frmManageLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 529);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddLDLApplication);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvLDLApplications);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmManageLocalDrivingLicenseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Local Driving License Applications";
            this.Load += new System.EventHandler(this.frmManageLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.cmsLocalDrivingLicenseApplication.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddLDLApplication;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLDLApplications;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ContextMenuStrip cmsLocalDrivingLicenseApplication;
        private System.Windows.Forms.ToolStripMenuItem tsShowDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsEdit;
        private System.Windows.Forms.ToolStripMenuItem tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsScheduleTest;
        private System.Windows.Forms.ToolStripMenuItem tsVisionTest;
        private System.Windows.Forms.ToolStripMenuItem tsWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsStreetTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsIssueDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem tsShowLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    }
}
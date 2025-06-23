namespace DVLDPresentationLayer
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAccountSettings = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnDrivers = new System.Windows.Forms.Button();
            this.btnPeople = new System.Windows.Forms.Button();
            this.btnApplications = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmsAccountSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsDrivingLicenceServices = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replacementForLostOrDamagedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDeatinedDeivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsManageApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenseApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDetainLicenses = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsManageApplicationTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsManageTestTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsAccountSettings.SuspendLayout();
            this.cmsApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnAccountSettings);
            this.panel1.Controls.Add(this.btnUsers);
            this.panel1.Controls.Add(this.btnDrivers);
            this.panel1.Controls.Add(this.btnPeople);
            this.panel1.Controls.Add(this.btnApplications);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 65);
            this.panel1.TabIndex = 0;
            // 
            // btnAccountSettings
            // 
            this.btnAccountSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnAccountSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAccountSettings.FlatAppearance.BorderSize = 0;
            this.btnAccountSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccountSettings.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccountSettings.ForeColor = System.Drawing.Color.Black;
            this.btnAccountSettings.Image = global::DVLDPresentationLayer.Properties.Resources.account_settings_64;
            this.btnAccountSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccountSettings.Location = new System.Drawing.Point(523, 0);
            this.btnAccountSettings.Name = "btnAccountSettings";
            this.btnAccountSettings.Size = new System.Drawing.Size(190, 65);
            this.btnAccountSettings.TabIndex = 4;
            this.btnAccountSettings.Text = "Account Settings";
            this.btnAccountSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccountSettings.UseVisualStyleBackColor = false;
            this.btnAccountSettings.Click += new System.EventHandler(this.btnAccountSettings_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.Transparent;
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnUsers.ForeColor = System.Drawing.Color.Black;
            this.btnUsers.Image = global::DVLDPresentationLayer.Properties.Resources.Users_2_64;
            this.btnUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.Location = new System.Drawing.Point(406, 0);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(117, 65);
            this.btnUsers.TabIndex = 3;
            this.btnUsers.Text = "Users";
            this.btnUsers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnDrivers
            // 
            this.btnDrivers.BackColor = System.Drawing.Color.Transparent;
            this.btnDrivers.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDrivers.FlatAppearance.BorderSize = 0;
            this.btnDrivers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrivers.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnDrivers.ForeColor = System.Drawing.Color.Black;
            this.btnDrivers.Image = global::DVLDPresentationLayer.Properties.Resources.Drivers_64;
            this.btnDrivers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDrivers.Location = new System.Drawing.Point(282, 0);
            this.btnDrivers.Name = "btnDrivers";
            this.btnDrivers.Size = new System.Drawing.Size(124, 65);
            this.btnDrivers.TabIndex = 2;
            this.btnDrivers.Text = "Drivers";
            this.btnDrivers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDrivers.UseVisualStyleBackColor = false;
            // 
            // btnPeople
            // 
            this.btnPeople.BackColor = System.Drawing.Color.Transparent;
            this.btnPeople.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPeople.FlatAppearance.BorderSize = 0;
            this.btnPeople.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeople.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnPeople.ForeColor = System.Drawing.Color.Black;
            this.btnPeople.Image = ((System.Drawing.Image)(resources.GetObject("btnPeople.Image")));
            this.btnPeople.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPeople.Location = new System.Drawing.Point(156, 0);
            this.btnPeople.Name = "btnPeople";
            this.btnPeople.Size = new System.Drawing.Size(126, 65);
            this.btnPeople.TabIndex = 1;
            this.btnPeople.Text = "People";
            this.btnPeople.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPeople.UseVisualStyleBackColor = false;
            this.btnPeople.Click += new System.EventHandler(this.btnPeople_Click);
            // 
            // btnApplications
            // 
            this.btnApplications.BackColor = System.Drawing.Color.Transparent;
            this.btnApplications.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnApplications.FlatAppearance.BorderSize = 0;
            this.btnApplications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplications.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnApplications.ForeColor = System.Drawing.Color.Black;
            this.btnApplications.Image = global::DVLDPresentationLayer.Properties.Resources.Applications_64;
            this.btnApplications.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApplications.Location = new System.Drawing.Point(0, 0);
            this.btnApplications.Name = "btnApplications";
            this.btnApplications.Size = new System.Drawing.Size(156, 65);
            this.btnApplications.TabIndex = 0;
            this.btnApplications.Text = "Applications";
            this.btnApplications.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnApplications.UseVisualStyleBackColor = false;
            this.btnApplications.Click += new System.EventHandler(this.btnApplications_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUsername);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 497);
            this.panel2.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(169, 470);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(149, 17);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Logged in with user:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDPresentationLayer.Properties.Resources.DVLD_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(351, 111);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 274);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // cmsAccountSettings
            // 
            this.cmsAccountSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCurrentUserInfo,
            this.tsChangePassword,
            this.toolStripSeparator1,
            this.tsSignOut});
            this.cmsAccountSettings.Name = "cmsAccountSettings";
            this.cmsAccountSettings.Size = new System.Drawing.Size(185, 124);
            // 
            // tsCurrentUserInfo
            // 
            this.tsCurrentUserInfo.Image = global::DVLDPresentationLayer.Properties.Resources.PersonDetails_32;
            this.tsCurrentUserInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsCurrentUserInfo.Name = "tsCurrentUserInfo";
            this.tsCurrentUserInfo.Size = new System.Drawing.Size(184, 38);
            this.tsCurrentUserInfo.Text = "Current User Info";
            this.tsCurrentUserInfo.Click += new System.EventHandler(this.tsCurrentUserInfo_Click);
            // 
            // tsChangePassword
            // 
            this.tsChangePassword.Image = global::DVLDPresentationLayer.Properties.Resources.Password_32;
            this.tsChangePassword.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsChangePassword.Name = "tsChangePassword";
            this.tsChangePassword.Size = new System.Drawing.Size(184, 38);
            this.tsChangePassword.Text = "Change Password";
            this.tsChangePassword.Click += new System.EventHandler(this.tsChangePassword_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // tsSignOut
            // 
            this.tsSignOut.Image = global::DVLDPresentationLayer.Properties.Resources.sign_out_32__2;
            this.tsSignOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsSignOut.Name = "tsSignOut";
            this.tsSignOut.Size = new System.Drawing.Size(184, 38);
            this.tsSignOut.Text = "Sign Out";
            this.tsSignOut.Click += new System.EventHandler(this.tsSignOut_Click);
            // 
            // cmsApplications
            // 
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDrivingLicenceServices,
            this.toolStripSeparator2,
            this.tsManageApplications,
            this.toolStripSeparator3,
            this.tsDetainLicenses,
            this.toolStripSeparator4,
            this.tsManageApplicationTypes,
            this.tsManageTestTypes});
            this.cmsApplications.Name = "cmsAccountSettings";
            this.cmsApplications.Size = new System.Drawing.Size(265, 394);
            // 
            // tsDrivingLicenceServices
            // 
            this.tsDrivingLicenceServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivingLicenseToolStripMenuItem,
            this.renewDrivingLicenseToolStripMenuItem,
            this.replacementForLostOrDamagedLicenseToolStripMenuItem,
            this.releaseDeatinedDeivingLicenseToolStripMenuItem,
            this.retakeTestToolStripMenuItem});
            this.tsDrivingLicenceServices.Image = global::DVLDPresentationLayer.Properties.Resources.Driver_License_48;
            this.tsDrivingLicenceServices.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsDrivingLicenceServices.Name = "tsDrivingLicenceServices";
            this.tsDrivingLicenceServices.Size = new System.Drawing.Size(264, 70);
            this.tsDrivingLicenceServices.Text = "Driving Licence Services";
            // 
            // newDrivingLicenseToolStripMenuItem
            // 
            this.newDrivingLicenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localLicenseToolStripMenuItem,
            this.internationalLicenseToolStripMenuItem});
            this.newDrivingLicenseToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.New_Driving_License_32;
            this.newDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newDrivingLicenseToolStripMenuItem.Name = "newDrivingLicenseToolStripMenuItem";
            this.newDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(316, 38);
            this.newDrivingLicenseToolStripMenuItem.Text = "New Driving License";
            // 
            // localLicenseToolStripMenuItem
            // 
            this.localLicenseToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.Local_32;
            this.localLicenseToolStripMenuItem.Name = "localLicenseToolStripMenuItem";
            this.localLicenseToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.localLicenseToolStripMenuItem.Text = "Local License";
            this.localLicenseToolStripMenuItem.Click += new System.EventHandler(this.localLicenseToolStripMenuItem_Click);
            // 
            // internationalLicenseToolStripMenuItem
            // 
            this.internationalLicenseToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.International_32;
            this.internationalLicenseToolStripMenuItem.Name = "internationalLicenseToolStripMenuItem";
            this.internationalLicenseToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.internationalLicenseToolStripMenuItem.Text = "International License";
            // 
            // renewDrivingLicenseToolStripMenuItem
            // 
            this.renewDrivingLicenseToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.Renew_Driving_License_32;
            this.renewDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.renewDrivingLicenseToolStripMenuItem.Name = "renewDrivingLicenseToolStripMenuItem";
            this.renewDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(316, 38);
            this.renewDrivingLicenseToolStripMenuItem.Text = "Renew Driving License";
            // 
            // replacementForLostOrDamagedLicenseToolStripMenuItem
            // 
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.Damaged_Driving_License_32;
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Name = "replacementForLostOrDamagedLicenseToolStripMenuItem";
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Size = new System.Drawing.Size(316, 38);
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Text = "Replacement For Lost Or Damaged License";
            // 
            // releaseDeatinedDeivingLicenseToolStripMenuItem
            // 
            this.releaseDeatinedDeivingLicenseToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.Detained_Driving_License_32;
            this.releaseDeatinedDeivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.releaseDeatinedDeivingLicenseToolStripMenuItem.Name = "releaseDeatinedDeivingLicenseToolStripMenuItem";
            this.releaseDeatinedDeivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(316, 38);
            this.releaseDeatinedDeivingLicenseToolStripMenuItem.Text = "Release Deatined Driving License";
            // 
            // retakeTestToolStripMenuItem
            // 
            this.retakeTestToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.Retake_Test_32;
            this.retakeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.retakeTestToolStripMenuItem.Name = "retakeTestToolStripMenuItem";
            this.retakeTestToolStripMenuItem.Size = new System.Drawing.Size(316, 38);
            this.retakeTestToolStripMenuItem.Text = "Retake Test";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
            // 
            // tsManageApplications
            // 
            this.tsManageApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenseApplicationToolStripMenuItem,
            this.internationalLicenseApplicationsToolStripMenuItem});
            this.tsManageApplications.Image = global::DVLDPresentationLayer.Properties.Resources.Manage_Applications_64;
            this.tsManageApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsManageApplications.Name = "tsManageApplications";
            this.tsManageApplications.Size = new System.Drawing.Size(264, 70);
            this.tsManageApplications.Text = "Manage Applications";
            // 
            // localDrivingLicenseApplicationToolStripMenuItem
            // 
            this.localDrivingLicenseApplicationToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.Driver_License_48;
            this.localDrivingLicenseApplicationToolStripMenuItem.Name = "localDrivingLicenseApplicationToolStripMenuItem";
            this.localDrivingLicenseApplicationToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.localDrivingLicenseApplicationToolStripMenuItem.Text = "Local Driving License Applications";
            this.localDrivingLicenseApplicationToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLicenseApplicationToolStripMenuItem_Click);
            // 
            // internationalLicenseApplicationsToolStripMenuItem
            // 
            this.internationalLicenseApplicationsToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.International_32;
            this.internationalLicenseApplicationsToolStripMenuItem.Name = "internationalLicenseApplicationsToolStripMenuItem";
            this.internationalLicenseApplicationsToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.internationalLicenseApplicationsToolStripMenuItem.Text = "International License Applications";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
            // 
            // tsDetainLicenses
            // 
            this.tsDetainLicenses.Image = global::DVLDPresentationLayer.Properties.Resources.Detain_64;
            this.tsDetainLicenses.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsDetainLicenses.Name = "tsDetainLicenses";
            this.tsDetainLicenses.Size = new System.Drawing.Size(264, 70);
            this.tsDetainLicenses.Text = "Detain Licenses";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(261, 6);
            // 
            // tsManageApplicationTypes
            // 
            this.tsManageApplicationTypes.Image = global::DVLDPresentationLayer.Properties.Resources.Application_Types_64;
            this.tsManageApplicationTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsManageApplicationTypes.Name = "tsManageApplicationTypes";
            this.tsManageApplicationTypes.Size = new System.Drawing.Size(264, 70);
            this.tsManageApplicationTypes.Text = "Manage Application Types";
            this.tsManageApplicationTypes.Click += new System.EventHandler(this.tsManageApplicationTypes_Click);
            // 
            // tsManageTestTypes
            // 
            this.tsManageTestTypes.Image = global::DVLDPresentationLayer.Properties.Resources.Test_Type_64;
            this.tsManageTestTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsManageTestTypes.Name = "tsManageTestTypes";
            this.tsManageTestTypes.Size = new System.Drawing.Size(264, 70);
            this.tsManageTestTypes.Text = "Manage Test Types";
            this.tsManageTestTypes.Click += new System.EventHandler(this.tsManageTestTypes_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driving Licence Management";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsAccountSettings.ResumeLayout(false);
            this.cmsApplications.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAccountSettings;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnDrivers;
        private System.Windows.Forms.Button btnPeople;
        private System.Windows.Forms.Button btnApplications;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem tsCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsSignOut;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem tsDrivingLicenceServices;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsManageApplications;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsDetainLicenses;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsManageApplicationTypes;
        private System.Windows.Forms.ToolStripMenuItem tsManageTestTypes;
        private System.Windows.Forms.ToolStripMenuItem newDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replacementForLostOrDamagedLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDeatinedDeivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retakeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenseApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenseApplicationsToolStripMenuItem;
    }
}


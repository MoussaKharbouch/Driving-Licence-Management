namespace DVLDPresentationLayer.Licenses
{
    partial class frmDriverLicensesHistory
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
            this.ctrlPersonCardWithFilter1 = new DVLDPresentationLayer.User_Controls.ctrlPersonCardWithFilter();
            this.ctrlDriverLicenseHistory1 = new DVLDPresentationLayer.Controls.ctrlDriverLicenseHistory();
            this.SuspendLayout();
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(0, 0);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(744, 529);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            // 
            // ctrlDriverLicenseHistory1
            // 
            this.ctrlDriverLicenseHistory1.Location = new System.Drawing.Point(733, 126);
            this.ctrlDriverLicenseHistory1.Name = "ctrlDriverLicenseHistory1";
            this.ctrlDriverLicenseHistory1.Size = new System.Drawing.Size(592, 331);
            this.ctrlDriverLicenseHistory1.TabIndex = 1;
            // 
            // frmDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 533);
            this.Controls.Add(this.ctrlDriverLicenseHistory1);
            this.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDriverLicensesHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Licenses History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDriverLicensesHistory_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private User_Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private Controls.ctrlDriverLicenseHistory ctrlDriverLicenseHistory1;
    }
}
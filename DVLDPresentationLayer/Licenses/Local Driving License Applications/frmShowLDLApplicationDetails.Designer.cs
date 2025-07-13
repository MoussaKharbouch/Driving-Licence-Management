namespace DVLDPresentationLayer.Local_Driving_License_Applications
{
    partial class frmShowLDLApplicationDetails
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
            this.ctrlDrivingLicenseDLApplicationInfo1 = new DVLDPresentationLayer.Controls.ctrlDrivingLicenseDLApplicationInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlDrivingLicenseDLApplicationInfo1
            // 
            this.ctrlDrivingLicenseDLApplicationInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlDrivingLicenseDLApplicationInfo1.Name = "ctrlDrivingLicenseDLApplicationInfo1";
            this.ctrlDrivingLicenseDLApplicationInfo1.Size = new System.Drawing.Size(624, 287);
            this.ctrlDrivingLicenseDLApplicationInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(537, 303);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 29);
            this.btnClose.TabIndex = 157;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowLDLApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 344);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlDrivingLicenseDLApplicationInfo1);
            this.Name = "frmShowLDLApplicationDetails";
            this.Text = "Show Local Driving License Application Info";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlDrivingLicenseDLApplicationInfo ctrlDrivingLicenseDLApplicationInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}
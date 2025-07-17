using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer.Licenses.Detain_Licenses
{

    public partial class frmDetainLicense : Form
    {

        public clsDetainedLicense DetainedLicense { get; private set; }

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmDetainLicense()
        {

            InitializeComponent();

            DetainedLicense = new clsDetainedLicense();
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += ShowInformation;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesHistory.Enabled = true; };
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesInfo.Enabled = true; };

        }

        private void ShowInformation()
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License == null || ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null)
                return;

            lblDetainDate.Text = DateTime.Now.ToShortDateString();

            if (Global.user != null)
                lblCreatedByUser.Text = Global.user.Username;

        }

        private bool ValidateInformation(clsLicense License)
        {

            if (License == null)
                return false;

            return License.IsActive;

        }

        private bool DetainLicense(ref clsDetainedLicense DetainedLicense)
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null)
                return false;

            if (Global.user == null)
                return false;
            
            clsLicense License = ctrlDrivingLicenseInfoWithFilter1.License;

            DetainedLicense.LicenseID = License.LicenseID;
            DetainedLicense.DetainDate = DateTime.Now;

            decimal FineFees = 0;

            if (!decimal.TryParse(tbFineFees.Text, out FineFees))
            {

                MessageBox.Show("Please enter a valid fine fee.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                
            }

            DetainedLicense.FineFees = FineFees;

            DetainedLicense.CreatedByUserID = Global.user.UserID;

            return true;

        }

        private void lnklblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmDriverLicensesHistory DriverLicensesHistory = new frmDriverLicensesHistory(ctrlDrivingLicenseInfoWithFilter1.License.DriverID);
            DriverLicensesHistory.ShowDialog();

        }

        private void lnklblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowDrivingLicenseInfo ShowDriverLicenseInfo = new frmShowDrivingLicenseInfo(ctrlDrivingLicenseInfoWithFilter1.License.LicenseID);
            ShowDriverLicenseInfo.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            this.Close();

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {

            if (!ValidateInformation(ctrlDrivingLicenseInfoWithFilter1.License))
            {

                MessageBox.Show("Some data are not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsDetainedLicense DetainedLicense = new clsDetainedLicense();

            if (!DetainLicense(ref DetainedLicense))
            {

                MessageBox.Show("Failed to prepare detained license data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            this.DetainedLicense = DetainedLicense;

            if (DetainedLicense.Save() && ctrlDrivingLicenseInfoWithFilter1.License.Save())
            {

                MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = DetainedLicense.LicenseID.ToString();

            lnklblShowLicensesInfo.Enabled = true;

            ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
            btnDetain.Enabled = false;

        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utils.UI.StopEnteringCharacters(e);

        }

    }

}

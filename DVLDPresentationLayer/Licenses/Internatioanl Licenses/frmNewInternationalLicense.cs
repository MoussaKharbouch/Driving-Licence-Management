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

namespace DVLDPresentationLayer.Licenses.Internatioanl_Licenses
{

    public partial class frmNewInternationalLicense : Form
    {

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public clsInternationalLicense InternationalLicense { get; private set; }

        public frmNewInternationalLicense()
        {

            InitializeComponent();

            InternationalLicense = new clsInternationalLicense();

            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += ShowInformation;

        }

        private void ShowInformation()
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License == null || ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null)
                return;

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass.ClassFees.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();

            if (Global.user != null)
                lblCreatedByUser.Text = Global.user.Username;

        }

        private bool ValidateInformation(clsLicense License)
        {

            return (License.LicenseClassID == 3 && License.IsActive == true && DateTime.Now < License.ExpirationDate && License != null);

        }

        private bool FillInternationalLicense(clsInternationalLicense InternationalLicense, clsLicense License)
        {

            InternationalLicense.ApplicationID = License.ApplicationID;

            if(Global.user == null)
                return false;

            InternationalLicense.CreatedByUserID = Global.user.UserID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.IsActive = true;
            InternationalLicense.IssuedUsingLocalLicenseID = License.LicenseID;
            InternationalLicense.DriverID = License.DriverID;

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

            this.Close();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (!ValidateInformation(ctrlDrivingLicenseInfoWithFilter1.License))
            {

                MessageBox.Show("Some data are not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FillInternationalLicense(InternationalLicense, ctrlDrivingLicenseInfoWithFilter1.License);

            if (InternationalLicense.Save())
            {

                MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicense.LicenseID.ToString();
            lblLocalLicenseID.Text = InternationalLicense.IssuedUsingLocalLicense.LicenseID.ToString();

            ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
            btnIssue.Enabled = false;

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

    }

}

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

namespace DVLDPresentationLayer.Licenses.Renew_Licenses
{

    public partial class frmRenewLicense : Form
    {

        public clsLicense RenewedLicense { get; private set; }
        public clsApplication Application { get; private set; }

        public frmRenewLicense()
        {

            InitializeComponent();

            RenewedLicense = new clsLicense();

            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += ShowInformation;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += CheckIsExpired;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesHistory.Enabled = true; };

        }

        private void CheckIsExpired()
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License == null)
                return;

            if (DateTime.Now < ctrlDrivingLicenseInfoWithFilter1.License.ExpirationDate)
            {

                MessageBox.Show("This license has not been expired.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;

            }
            else
            {

                btnRenew.Enabled = true;
                return;

            }

        }

        private void ShowInformation()
        {

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(2);

            if (ctrlDrivingLicenseInfoWithFilter1.License == null || ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null || ApplicationType == null)
                return;

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseFees.Text = ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass.ClassFees.ToString();
            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblTotalFees.Text = (ApplicationType.ApplicationFees + ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass.ClassFees).ToString();

            if (Global.user != null)
                lblCreatedByUser.Text = Global.user.Username;

        }

        private bool ValidateInformation(clsLicense License)
        {

            if (License == null)
                return false;

            return (License.IsActive && DateTime.Now > License.ExpirationDate);

        }

        private bool FillApplication(ref clsApplication Application, clsLicense License)
        {

            if (License.Driver == null)
            {

                MessageBox.Show("Driver object is not assigned in the License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if(License == null)
                return false;

            if (Global.user == null)
                return false;

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(2);

            if(ApplicationType == null)
                return false;

            if (Application == null)
                Application = new clsApplication();

            Application.ApplicantPersonID = License.Driver.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enStatus.Completed;
            Application.ApplicationTypeID = 2;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = ApplicationType.ApplicationFees;
            Application.CreatedByUserID = Global.user.UserID;

            return true;

        }

        private bool FillLicense(ref clsLicense License)
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null)
                return false;

            if (Global.user == null)
                return false;

            if (License == null)
                License = new clsLicense();

            clsApplication Application = new clsApplication();

            if (!FillApplication(ref Application, ctrlDrivingLicenseInfoWithFilter1.License))
                return false;

            this.Application = Application;

            if (!Application.Save())
                return false;
            
            License.ApplicationID = Application.ApplicationID;
            License.IssueReason = clsLicense.enIssueReason.Renew;
            License.LicenseClassID = ctrlDrivingLicenseInfoWithFilter1.License.LicenseClassID;
            License.Notes = tbNotes.Text;
            License.PaidFees = ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass.ClassFees;
            License.CreatedByUserID = Global.user.UserID;
            License.IssueDate = DateTime.Now;
            License.IsActive = true;
            License.DriverID = ctrlDrivingLicenseInfoWithFilter1.License.DriverID;

            return true;

        }

        private void lnklblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmDriverLicensesHistory DriverLicensesHistory = new frmDriverLicensesHistory(ctrlDrivingLicenseInfoWithFilter1.License.DriverID);
            DriverLicensesHistory.ShowDialog();

        }

        private void lnklblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowDrivingLicenseInfo ShowDriverLicenseInfo = new frmShowDrivingLicenseInfo(RenewedLicense.LicenseID);
            ShowDriverLicenseInfo.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (DateTime.Now < ctrlDrivingLicenseInfoWithFilter1.License.ExpirationDate)
                return;

            if (!ValidateInformation(ctrlDrivingLicenseInfoWithFilter1.License))
            {

                MessageBox.Show("Some data are not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsLicense RenewedLicense = new clsLicense();

            if (!FillLicense(ref RenewedLicense))
            {

                MessageBox.Show("Failed to prepare license data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ctrlDrivingLicenseInfoWithFilter1.License.IsActive = false;

            this.RenewedLicense = RenewedLicense;

            if (RenewedLicense.Save() && ctrlDrivingLicenseInfoWithFilter1.License.Save())
            {

                MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblApplicationID.Text = Application.ApplicationID.ToString();
            lblRenewedLicenseID.Text = RenewedLicense.LicenseID.ToString();
            lblOldLicenseID.Text = ctrlDrivingLicenseInfoWithFilter1.License.LicenseID.ToString();

            lnklblShowNewLicensesInfo.Enabled = true;

            ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
            btnRenew.Enabled = false;

        }

    }

}

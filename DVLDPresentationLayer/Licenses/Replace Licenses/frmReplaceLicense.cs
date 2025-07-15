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

namespace DVLDPresentationLayer.Licenses.Replace_Licenses
{

    public partial class frmReplaceLicense : Form
    {

        public clsLicense ReplacedLicense { get; private set; }
        public clsApplication Application { get; private set; }

        public enum enReplacementReason { Lost = 3, Damaged = 4 }
        enReplacementReason ReplacementReason;

        public frmReplaceLicense()
        {

            InitializeComponent();

            ReplacedLicense = new clsLicense();

            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += ShowInformation;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += CheckIsExpired;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesHistory.Enabled = true; };

            ReplacementReason = rbLostLicense.Checked ? enReplacementReason.Lost : enReplacementReason.Damaged;

        }

        private void CheckIsExpired()
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License == null)
                return;

            if (DateTime.Now > ctrlDrivingLicenseInfoWithFilter1.License.ExpirationDate)
            {

                MessageBox.Show("This license has already been expired.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;

            }
            else
            {

                btnIssue.Enabled = true;
                return;

            }

        }

        private void ShowInformation()
        {

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType((int) ReplacementReason);

            if (ctrlDrivingLicenseInfoWithFilter1.License == null || ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null || ApplicationType == null)
                return;

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblOldLicenseID.Text = ctrlDrivingLicenseInfoWithFilter1.License.LicenseID.ToString();
            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString();

            if (Global.user != null)
                lblCreatedByUser.Text = Global.user.Username;

        }

        private bool ValidateInformation(clsLicense License)
        {

            if (License == null)
                return false;

            return (License.IsActive && DateTime.Now < License.ExpirationDate);

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
            Application.ApplicationTypeID = (int)ReplacementReason;
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
            License.IssueReason = ReplacementReason == enReplacementReason.Lost ? clsLicense.enIssueReason.LostReplacement : clsLicense.enIssueReason.DamagedReplacement;
            License.LicenseClassID = ctrlDrivingLicenseInfoWithFilter1.License.LicenseClassID;
            License.Notes = string.Empty;
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

            frmShowDrivingLicenseInfo ShowDriverLicenseInfo = new frmShowDrivingLicenseInfo(ReplacedLicense.LicenseID);
            ShowDriverLicenseInfo.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (DateTime.Now > ctrlDrivingLicenseInfoWithFilter1.License.ExpirationDate)
                return;

            if (!ValidateInformation(ctrlDrivingLicenseInfoWithFilter1.License))
            {

                MessageBox.Show("Some data are not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsLicense ReplacedLicense = new clsLicense();

            if (!FillLicense(ref ReplacedLicense))
            {

                MessageBox.Show("Failed to prepare license data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ctrlDrivingLicenseInfoWithFilter1.License.IsActive = false;

            this.ReplacedLicense = ReplacedLicense;

            if (ReplacedLicense.Save() && ctrlDrivingLicenseInfoWithFilter1.License.Save())
            {

                MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblApplicationID.Text = Application.ApplicationID.ToString();
            lblReplacedLicenseID.Text = ReplacedLicense.LicenseID.ToString();
            lblOldLicenseID.Text = ctrlDrivingLicenseInfoWithFilter1.License.LicenseID.ToString();

            lnklblShowNewLicensesInfo.Enabled = true;

            ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
            btnIssue.Enabled = false;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {

            ReplacementReason = enReplacementReason.Damaged;

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType((int)ReplacementReason);
            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString();

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {

            ReplacementReason = enReplacementReason.Lost;

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType((int)ReplacementReason);
            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString();

        }

    }

}

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

namespace DVLDPresentationLayer.Licenses.Release_Detained_Licenses
{

    public partial class frmReleaseDetainedLicense : Form
    {

        public clsDetainedLicense DetainedLicense { get; private set; }
        public clsApplication Application { get; private set; }

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmReleaseDetainedLicense()
        {

            InitializeComponent();

            DetainedLicense = new clsDetainedLicense();
            Application = new clsApplication();

            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += ShowInformation;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesHistory.Enabled = true; };
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesInfo.Enabled = true; };

        }

        public frmReleaseDetainedLicense(int DetainID)
        {

            InitializeComponent();

            DetainedLicense = clsDetainedLicense.Find(DetainID);
            Application = new clsApplication();

            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += ShowInformation;
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesHistory.Enabled = true; };
            ctrlDrivingLicenseInfoWithFilter1.OnFilterEventHandler += () => { lnklblShowLicensesInfo.Enabled = true; };

            if (DetainedLicense == null)
                this.Close();

            ctrlDrivingLicenseInfoWithFilter1.Filter("LicenseID", DetainedLicense.LicenseID.ToString());

        }

        private bool GetDetainedLicense()
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License == null)
                return false;

            DetainedLicense = clsDetainedLicense.FindByLicenseID(ctrlDrivingLicenseInfoWithFilter1.License.LicenseID);

            if (DetainedLicense == null)
            {

                MessageBox.Show("This license is not detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            else
                return true;

        }

        private void ShowInformation()
        {

            if (!GetDetainedLicense())
                return;

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(5);

            if (ctrlDrivingLicenseInfoWithFilter1.License == null || ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null || ApplicationType == null)
                return;

            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString();
            lblTotalFees.Text = (ApplicationType.ApplicationFees + DetainedLicense.FineFees).ToString();
            lblFineFees.Text = (DetainedLicense.FineFees).ToString();

            if (Global.user != null)
                lblCreatedByUser.Text = Global.user.Username;

        }

        private bool ValidateInformation(clsLicense License)
        {

            if (License == null)
                return false;

            return (License.IsActive && clsDetainedLicense.IsDetained(License.LicenseID));

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

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(5);

            if(ApplicationType == null)
                return false;

            if (Application == null)
                Application = new clsApplication();

            Application.ApplicantPersonID = License.Driver.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enStatus.Completed;
            Application.ApplicationTypeID = 5;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = ApplicationType.ApplicationFees;
            Application.CreatedByUserID = Global.user.UserID;

            return true;

        }

        private bool ReleaseLicense(ref clsDetainedLicense DetainedLicense)
        {

            if (ctrlDrivingLicenseInfoWithFilter1.License.LicenseClass == null)
                return false;

            if (Global.user == null)
                return false;
            
            clsLicense License = ctrlDrivingLicenseInfoWithFilter1.License;

            clsApplication Application = new clsApplication();

            if (!FillApplication(ref Application, ctrlDrivingLicenseInfoWithFilter1.License))
                return false;

            this.Application = Application;

            if (!Application.Save())
                return false;

            DetainedLicense.ReleaseApplicationID = Application.ApplicationID;
            DetainedLicense.ReleaseDate = DateTime.Now;
            DetainedLicense.ReleasedByUserID = Global.user.UserID;
            DetainedLicense.IsReleased = true;

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

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (!ValidateInformation(ctrlDrivingLicenseInfoWithFilter1.License))
            {

                MessageBox.Show("Some data are not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsDetainedLicense DetainedLicense = this.DetainedLicense;

            if (!ReleaseLicense(ref DetainedLicense))
            {

                MessageBox.Show("Failed to prepare release detained license data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            lblApplicationID.Text = Application.ApplicationID.ToString();

            lnklblShowLicensesInfo.Enabled = true;

            ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
            btnRelease.Enabled = false;

        }

    }

}

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

namespace DVLDPresentationLayer.Licenses
{

    public partial class frmIssueDrivingLicense : Form
    {

        public clsLocalDrivingLicenseApplication LDLApplication { get; private set; }
        public clsApplication Application { get; private set; }
        public clsLicense License { get; private set; }
        public clsDriver Driver { get; private set; }
        public clsLicenseClass LicenseClass { get; private set; }

        public event Action OnSaveEventHandler;

        public frmIssueDrivingLicense(int LDLApplicationID)
        {

            InitializeComponent();

            LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(LDLApplicationID);

            if (LDLApplication == null)
            {

                MessageBox.Show("Local driving license application is not found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            Application = clsApplication.FindApplication(LDLApplication.ApplicationID);

            if (Application == null)
            {

                MessageBox.Show("Application is not found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }

            LicenseClass = clsLicenseClass.FindLicenseClass(LDLApplication.LicenseClassID);

            if (LicenseClass == null)
            {

                MessageBox.Show("License Class is not found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
                
            }

            License = new clsLicense();
            Driver = new clsDriver();

            if (clsTest.GetPassedTests(LDLApplication.LocalDrivingLicenseApplicationID) != 3)
            {

                MessageBox.Show("This person did'nt passed all of his test!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }

            ctrlDrivingLicenseDLApplicationInfo1.Refresh(LDLApplicationID);

        }

        private bool FillDriver(clsDriver Driver)
        {

            Driver.PersonID = Application.ApplicantPersonID;

            if (Global.user != null)
                Driver.CreatedByUserID = Global.user.UserID;
            else
            {

                MessageBox.Show("No user is logged in!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            Driver.CreatedDate = DateTime.Now;

            return true;

        }

        private bool FillDrivingLicense(clsDriver Driver, clsLicense License)
        {

            License.ApplicationID = Application.ApplicationID;
            License.DriverID = Driver.DriverID;
            License.LicenseClassID = LDLApplication.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.Notes = tbNotes.Text;

            License.PaidFees = LicenseClass.ClassFees;
            License.IsActive = true;
            License.IssueReason = (clsLicense.enIssueReason)1;

            if (Global.user != null)
                License.CreatedByUserID = Global.user.UserID;
            else
            {

                MessageBox.Show("No user is logged in!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return true;

        }

        private bool IssueLicense()
        {

            Driver = clsDriver.FindDriverByPersonID(Application.ApplicantPersonID);

            if (Driver == null)
            {

                Driver = new clsDriver();

                if (!FillDriver(Driver))
                    return false;

                if (!Driver.Save())
                    return false;
            }

            if (clsLicense.HasLicenseInSameClass(Driver.DriverID, LicenseClass.LicenseClassID))
            {
                MessageBox.Show("This person already has a license in the same class!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!FillDrivingLicense(Driver, License))
                return false;

            Application.ApplicationStatus = clsApplication.enStatus.Completed;

            return (License.Save() && Application.Save());

        }


        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (IssueLicense())
            {

                MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            
            }
            else
            {

                MessageBox.Show("Data has not been saved successfully!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

    }

}

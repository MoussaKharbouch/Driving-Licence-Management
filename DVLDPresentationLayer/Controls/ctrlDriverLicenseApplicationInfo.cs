using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Licenses;

namespace DVLDPresentationLayer.Controls
{

    public partial class ctrlDrivingLicenseDLApplicationInfo : UserControl
    {

        public clsLocalDrivingLicenseApplication DLApplication { get; private set; }

        public ctrlDrivingLicenseDLApplicationInfo()
        {

            InitializeComponent();

        }

        public ctrlDrivingLicenseDLApplicationInfo(int LocalDrivingLicenseApplicationID = -1)
        {

            InitializeComponent();

            Refresh(LocalDrivingLicenseApplicationID);
            
        }

        public void LoadDLApplication(int LocalDrivingLicenseApplicationID)
        {

            DLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

        }

        public void ShowItem(clsLocalDrivingLicenseApplication DLApplication)
        {

            if (DLApplication == null)
                return;

            ctrlApplicationBasicInfo1.Refresh(DLApplication.ApplicationID);

            lblDLAppID.Text = DLApplication.LocalDrivingLicenseApplicationID.ToString();

            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClass(DLApplication.LicenseClassID);

            if (LicenseClass == null)
                return;

            lblPassedTests.Text = clsTest.GetPassedTests(DLApplication.LocalDrivingLicenseApplicationID).ToString() + "/3";
            lblLicenseClass.Text = LicenseClass.ClassName.ToString();

        }

        public void RefreshInformation()
        {

            if (DLApplication == null)
            {

                MessageBox.Show("DLApplication data not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            DLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(DLApplication.LocalDrivingLicenseApplicationID);

            if (DLApplication == null)
            {

                MessageBox.Show("DLApplication data not found on refresh!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ShowItem(DLApplication);

        }

        public void Refresh(int LocalDrivingLicenseApplicationID)
        {

            if (LocalDrivingLicenseApplicationID != -1)
            {

                LoadDLApplication(LocalDrivingLicenseApplicationID);
                RefreshInformation();

                clsApplication Application = clsApplication.FindApplication(DLApplication.ApplicationID);
                if (Application == null)
                    return;

                if (Application.ApplicationStatus == clsApplication.enStatus.Completed)
                    lnkShowLicenseInfo.Visible = true;

            }

        }

        private void ctrlDrivingLicenseDLApplicationInfo_Load(object sender, EventArgs e)
        {

            if (DLApplication != null)
                RefreshInformation();

        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            clsApplication Application = clsApplication.FindApplication(DLApplication.ApplicationID);
            if (Application == null)
                return;

            int PersonID = Application.ApplicantPersonID;
            clsDriver Driver = clsDriver.FindDriverByPersonID(PersonID);

            if (Driver == null)
            {

                MessageBox.Show("Driver not found for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (PersonID != -1)
            {

                clsLicense License = clsLicense.FindLicenseByApplicationID(Application.ApplicationID);

                if (License != null)
                {

                    frmShowDriverLicenseInfo ShowDriverLicenseInfo = new frmShowDriverLicenseInfo(License.LicenseID);
                    ShowDriverLicenseInfo.ShowDialog();

                }
                else
                {
                    MessageBox.Show("No license found for this driver.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {

                MessageBox.Show("Driver not found for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

    }

}
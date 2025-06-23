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

            DataTable DLFullInfo = clsLocalDrivingLicenseApplication.GetFullInfo();

            if (DLFullInfo != null)
            {

                Utils.Filtering.FilterDataTable("LDL AppID", DLApplication.LocalDrivingLicenseApplicationID.ToString(), DLFullInfo);

                if (DLFullInfo.Rows.Count == 1)
                {

                    lblPassedTests.Text = DLFullInfo.DefaultView[0]["Passed Tests"].ToString() + "/3";
                    lblLicenseClass.Text = DLFullInfo.DefaultView[0]["Driving Class"].ToString();

                }

            }

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

            }

        }

        private void ctrlDLApplicationBasicInfo_Load(object sender, EventArgs e)
        {

            if (DLApplication != null)
                Refresh(DLApplication.LocalDrivingLicenseApplicationID);

        }

        private void ctrlDrivingLicenseDLApplicationInfo_Load(object sender, EventArgs e)
        {

            if (DLApplication != null)
                RefreshInformation();

        }

    }

}
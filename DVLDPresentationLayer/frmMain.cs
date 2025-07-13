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
using DVLDPresentationLayer.People;
using DVLDPresentationLayer.Users;
using DVLDPresentationLayer.Application_Types;
using DVLDPresentationLayer.Test_Types;
using DVLDPresentationLayer.Local_Driving_License_Applications;
using DVLDPresentationLayer.Drivers;
using DVLDPresentationLayer.Licenses.Internatioanl_Licenses;

namespace DVLDPresentationLayer
{

    public partial class frmMain : Form
    {

        public frmMain(clsUser user)
        {

            InitializeComponent();

            if (user != null)
            {

                Global.user = user;
                lblUsername.Text = Global.user.Username;

            }

        }

        private void btnPeople_Click(object sender, EventArgs e)
        {

            frmManagePeople ManagePeople = new frmManagePeople();
            ManagePeople.ShowDialog();

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {

            frmManageUsers ManageUsers = new frmManageUsers();
            ManageUsers.Show();

        }

        private void tsCurrentUserInfo_Click(object sender, EventArgs e)
        {

            if (Global.user != null)
            {

                frmShowUserDetails CurrentUserInfo = new frmShowUserDetails(Global.user.UserID);
                CurrentUserInfo.ShowDialog();

            }

        }

        private void tsChangePassword_Click(object sender, EventArgs e)
        {

            if (Global.user != null)
            {

                frmChangePassword ChangePassword = new frmChangePassword(Global.user.UserID);
                ChangePassword.ShowDialog();

            }

        }

        private void tsSignOut_Click(object sender, EventArgs e)
        {

            Global.user = null;

            this.DialogResult = DialogResult.Retry;
            this.Close();


        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {
            
            Point location = btnAccountSettings.PointToScreen(new Point(0, btnAccountSettings.Height));

            cmsAccountSettings.Show(location);

        }

        private void tsManageApplicationTypes_Click(object sender, EventArgs e)
        {

            frmManageApplicationTypes ManageApplicationTypes = new frmManageApplicationTypes();
            ManageApplicationTypes.ShowDialog();

        }

        private void btnApplications_Click(object sender, EventArgs e)
        {

            Point menuLocation = btnApplications.PointToScreen(new Point(0, btnApplications.Height));
            cmsApplications.Show(menuLocation);

        }

        private void tsManageTestTypes_Click(object sender, EventArgs e)
        {

            frmManageTestTypes ManageTestTypes = new frmManageTestTypes();
            ManageTestTypes.ShowDialog();

        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmManageLocalDrivingLicenseApplications ManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            ManageLocalDrivingLicenseApplications.ShowDialog();

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmNewLDLApplication NewLDLApplication = new frmNewLDLApplication();
            NewLDLApplication.ShowDialog();

        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {

            frmManageDrivers ManageDrivers = new frmManageDrivers();
            ManageDrivers.ShowDialog();

        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmNewInternationalLicense NewInternationalLicense = new frmNewInternationalLicense();
            NewInternationalLicense.ShowDialog();

        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            (new frmManageInternationalLicenses()).ShowDialog();

        }

    }

}

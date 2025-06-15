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
using DVLDPresentationLayer.Users;

namespace DVLDPresentationLayer
{

    public partial class frmMain : Form
    {

        public frmMain(User user)
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

                frmShowDetails CurrentUserInfo = new frmShowDetails(Global.user.UserID);
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


    }

}

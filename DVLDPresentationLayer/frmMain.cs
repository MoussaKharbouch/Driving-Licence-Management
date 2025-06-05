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

    }

}

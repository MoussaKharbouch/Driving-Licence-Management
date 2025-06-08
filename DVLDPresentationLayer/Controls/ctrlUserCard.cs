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

namespace DVLDPresentationLayer.Users
{
    public partial class ctrlUserInfoCard : UserControl
    {

        private User user;

        public ctrlUserInfoCard()
        {

            InitializeComponent();

        }

        public void ShowInformation(User user)
        {

            if (user == null)
                return;

            this.user = user;

            ctrlPersonCard1.Refresh(user.PersonID);

            lblUserID.Text = user.UserID.ToString();
            lblUsername.Text = user.Username;

            if (user.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

        }

    }

}

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

namespace DVLDPresentationLayer.Users
{

    public partial class frmShowUserDetails : Form
    {

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmShowUserDetails(int UserID)
        {

            InitializeComponent();
            ctrlUserInfoCard1.ShowInformation(clsUser.FindUser(UserID));

        }

        private void frmShowDetails_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

    }

}

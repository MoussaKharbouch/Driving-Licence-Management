﻿using System;
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

    public partial class frmShowDetails : Form
    {

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmShowDetails(int UserID)
        {

            InitializeComponent();
            ctrlUserInfoCard1.ShowInformation(User.FindUser(UserID));

        }

        private void frmShowDetails_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

    }

}

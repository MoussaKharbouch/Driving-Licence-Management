﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{

    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {

            frmManagePeople ManagePeople = new frmManagePeople();
            ManagePeople.ShowDialog();

        }

    }

}

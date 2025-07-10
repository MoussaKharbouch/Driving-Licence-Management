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

    public partial class frmDriverLicensesHistory : Form
    {

        public clsDriver Driver { get; private set; }

        public frmDriverLicensesHistory(int DriverID)
        {

            InitializeComponent();

            Driver = clsDriver.FindDriver(DriverID);

            ctrlPersonCardWithFilter1.Initialize();

            ctrlPersonCardWithFilter1.Filter("PersonID", Driver.PersonID.ToString());
            ctrlDriverLicenseHistory1.Refresh(Driver.DriverID);

        }

    }

}

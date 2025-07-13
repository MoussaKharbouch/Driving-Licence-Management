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

    public partial class ctrlDriverLicenseHistory : UserControl
    {

        public ctrlDriverLicenseHistory()
        {
            InitializeComponent();
        }

        public clsDriver Driver { get; private set; }

        public ctrlDriverLicenseHistory(int DriverID = -1)
        {

            InitializeComponent();

            Refresh(DriverID);
            
        }

        public void LoadDriver(int DriverID)
        {

            Driver = clsDriver.FindDriver(DriverID);

        }

        public void LoadDriverLicensesHistory(clsDriver Driver)
        {

            if (Driver == null)
                return;

            dgvLocalLicensesHistory.DataSource = clsLicense.GetDriverLocalLicensesHistory(Driver.DriverID);
            lblLocalLicensesHistoryRecord.Text = ((DataTable)dgvLocalLicensesHistory.DataSource).Rows.Count.ToString();

            dgvInternationalLicensesHistory.DataSource = clsInternationalLicense.GetDriverInternationalLicensesHistory(Driver.DriverID);
            lblInternationalLicensesRecords.Text = ((DataTable)dgvInternationalLicensesHistory.DataSource).Rows.Count.ToString();

        }

        public void RefreshInformation()
        {

            if (Driver == null)
            {

                MessageBox.Show("Driving Driver data not found on refresh!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            LoadDriverLicensesHistory(Driver);

        }

        public void Refresh(int DriverID)
        {

            if (DriverID != -1)
            {

                LoadDriver(DriverID);
                RefreshInformation();

            }

        }

        private void ctrlDriverCard_Load(object sender, EventArgs e)
        {

            if(Driver != null)
                RefreshInformation();

        }

    }

}

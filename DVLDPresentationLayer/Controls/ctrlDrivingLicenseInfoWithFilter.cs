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

    public partial class ctrlDrivingLicenseInfoWithFilter : UserControl
    {

        public clsLicense License { get; private set; }
        private DataTable dtItems;

        public delegate void OnFilter();
        public event OnFilter OnFilterEventHandler;

        public ctrlDrivingLicenseInfoWithFilter()
        {

            InitializeComponent();

            License = new clsLicense();

        }

        public ctrlDrivingLicenseInfoWithFilter(int LicenseID)
        {

            InitializeComponent();

            clsLicense SearchedLicense = clsLicense.FindLicense(LicenseID);
            License = SearchedLicense != null ? SearchedLicense : new clsLicense();

            ctrlDrivingLicenseInfo1.Refresh(LicenseID);

        }

        public void ApplyFilter(string FilterName, string Value)
        {

            if (dtItems == null)
                dtItems = clsLicense.GetLicenses();

            if (!Utils.Filtering.FilterDataTable(FilterName, Value, dtItems))
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (dtItems.DefaultView.Count == 1)
                License.LicenseID = Convert.ToInt32(dtItems.DefaultView[0][FilterName]);
            else if (dtItems.DefaultView.Count != 1)
                MessageBox.Show("No matching License found.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void ctrlDrivingLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

            dtItems = clsLicense.GetLicenses();

        }

        public void Filter(string FilterName, string Value)
        {

            ApplyFilter(FilterName, Value);

            tbValue.Text = Value;
            tbValue.Enabled = false;

            if (dtItems.DefaultView.Count == 1)
            {

                ctrlDrivingLicenseInfo1.Refresh((int)dtItems.DefaultView[0]["LicenseID"]);

                License = clsLicense.FindLicense((int)dtItems.DefaultView[0]["LicenseID"]);

                if (OnFilterEventHandler != null)
                    OnFilterEventHandler();

            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            ApplyFilter("LicenseID", tbValue.Text);

            if (dtItems.DefaultView.Count == 1)
            {

                ctrlDrivingLicenseInfo1.Refresh((int)dtItems.DefaultView[0]["LicenseID"]);

                License = clsLicense.FindLicense((int)dtItems.DefaultView[0]["LicenseID"]);

                if (OnFilterEventHandler != null)
                    OnFilterEventHandler();

            }

        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            Utils.UI.StopEnteringCharacters(e);

        }

    }

}
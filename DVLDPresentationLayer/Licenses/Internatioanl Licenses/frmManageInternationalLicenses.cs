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

namespace DVLDPresentationLayer.Licenses.Internatioanl_Licenses
{

    public partial class frmManageInternationalLicenses : Form
    {

        public frmManageInternationalLicenses()
        {

            InitializeComponent();

        }

        public void ApplyFilter(string filterName, string value)
        {

            DataTable dtItems = (DataTable)dgvInternationalLicenses.DataSource;
            bool succeeded = false;

            if (dtItems.Columns.Count == 0)
                return;

            succeeded = Utils.Filtering.FilterDataTable(filterName, value, dtItems);

            if (!succeeded)
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lblRecords.Text = dtItems.DefaultView.Count.ToString();

        }

        public void LoadItems()
        {

            DataTable dtLocalDrivingLicenseApplications = clsInternationalLicense.GetLicensesMainInfo();

            dgvInternationalLicenses.DataSource = dtLocalDrivingLicenseApplications;

            lblRecords.Text = dtLocalDrivingLicenseApplications.Rows.Count.ToString();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            string filterName = cbFilters.SelectedItem != null ? cbFilters.SelectedItem.ToString() : string.Empty;

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
            {

                tbValue.Enabled = true;
                ApplyFilter(filterName, tbValue.Text);  

            }  

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

        }

        private void dgvInternationalLicenses_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvInternationalLicenses, e, cmsInternationalLicense);

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() != "None")
            {

                Utils.UI.StopEnteringCharacters(e);

            }

        }

        private void btnAddInternationalLicense_Click(object sender, EventArgs e)
        {

            frmNewInternationalLicense NewInternationalLicense = new frmNewInternationalLicense();
            NewInternationalLicense.OnSaveEventHandler += LoadItems;

            NewInternationalLicense.ShowDialog();

        }

        private void frmManageInternationalLicenses_Load(object sender, EventArgs e)
        {

            LoadItems();

            cbFilters.Items.Add("None");

            cbFilters.Items.Add("InternationalLicenseID");
            cbFilters.Items.Add("ApplicationID");

            cbFilters.SelectedIndex = 0;

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsDriver Driver = clsDriver.FindDriver(Convert.ToInt32(dgvInternationalLicenses.SelectedRows[0].Cells["DriverID"].Value));

            if (Driver == null)
                return;

            (new frmShowPersonDetails(Driver.PersonID)).ShowDialog();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            (new frmShowDrivingLicenseInfo(Convert.ToInt32(dgvInternationalLicenses.SelectedRows[0].Cells["IssuedUsingLocalLicenseID"].Value))).ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            (new frmDriverLicensesHistory(Convert.ToInt32(dgvInternationalLicenses.SelectedRows[0].Cells["DriverID"].Value))).ShowDialog();

        }

    }

}
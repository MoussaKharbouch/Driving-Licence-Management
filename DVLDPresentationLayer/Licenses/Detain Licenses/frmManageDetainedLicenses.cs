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
using DVLDPresentationLayer.Licenses.Release_Detained_Licenses;
using DVLDPresentationLayer.Licenses;
using DVLDPresentationLayer.People;

namespace DVLDPresentationLayer.Licenses.Detain_Licenses
{

    public partial class frmManageDetainedLicenses : Form
    {

        public frmManageDetainedLicenses()
        {

            InitializeComponent();

            LoadItems();
            cbFilters.SelectedIndex = 0;

        }

        public void ApplyFilter(string filterName, string value)
        {

            DataTable dtItems = (DataTable)dgvDetainedLicenses.DataSource;
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

            DataTable dtDetainedLicenses = clsDetainedLicense.GetDetainedLicensesMainInfo();

            dgvDetainedLicenses.DataSource = dtDetainedLicenses;

            lblRecords.Text = dtDetainedLicenses.Rows.Count.ToString();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            string filterName = cbFilters.SelectedItem != null ? cbFilters.SelectedItem.ToString() : string.Empty;

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
                tbValue.Enabled = true;

            ApplyFilter(filterName, tbValue.Text);

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

        }

        private void dgvDetainedLicenses_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvDetainedLicenses, e, cmsDetainedLicense);

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() == "Detain ID" || cbFilters.SelectedItem.ToString() == "License ID")
            {

                Utils.UI.StopEnteringCharacters(e);

            }

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {

            frmDetainLicense DetainLicense = new frmDetainLicense();
            DetainLicense.OnSaveEventHandler += LoadItems;

            DetainLicense.ShowDialog();

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            frmReleaseDetainedLicense ReleaseDetainedLicense = new frmReleaseDetainedLicense();
            ReleaseDetainedLicense.OnSaveEventHandler += LoadItems;

            ReleaseDetainedLicense.ShowDialog();

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsLicense License = clsLicense.FindLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells["License ID"].Value);

            if (License == null)
                return;

            frmShowPersonDetails ShowPersonDetails = new frmShowPersonDetails(License.Driver.PersonID);
            ShowPersonDetails.OnSaveEventHandler += LoadItems;

            ShowPersonDetails.ShowDialog();

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmShowDrivingLicenseInfo ShowDrivingLicenseInfo = new frmShowDrivingLicenseInfo((int)dgvDetainedLicenses.SelectedRows[0].Cells["License ID"].Value);
            ShowDrivingLicenseInfo.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsLicense License = clsLicense.FindLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells["License ID"].Value);

            if (License == null)
                return;

            frmDriverLicensesHistory DriverLicensesHistory = new frmDriverLicensesHistory(License.DriverID);
            DriverLicensesHistory.ShowDialog();

        }

        private void tsReleaseDetainedLicense_Click(object sender, EventArgs e)
        {

            frmReleaseDetainedLicense ReleaseDetainedLicense = new frmReleaseDetainedLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells["Detain ID"].Value);
            ReleaseDetainedLicense.OnSaveEventHandler += LoadItems;

            ReleaseDetainedLicense.ShowDialog();

        }

        private void cmsDetainedLicense_Opening(object sender, CancelEventArgs e)
        {

            if (clsDetainedLicense.IsDetained((int)dgvDetainedLicenses.SelectedRows[0].Cells["License ID"].Value))
                tsReleaseDetainedLicense.Enabled = true;
            else
                tsReleaseDetainedLicense.Enabled = false;

        }

    }

}

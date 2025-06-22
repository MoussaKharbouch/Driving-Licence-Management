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

namespace DVLDPresentationLayer.Local_Driving_License_Applications
{

    public partial class frmManageLocalDrivingLicenseApplications : Form
    {

        public frmManageLocalDrivingLicenseApplications()
        {

            InitializeComponent();

        }

        //Check filter and apply it on LocalDrivingLicenseApplications's data (it can be None, or Status...)
        public void ApplyFilter(string filterName, string value)
        {

            DataTable dtItems = (DataTable)dgvLDLApplications.DataSource;

            if (!Utils.Filtering.FilterDataTable(filterName, value, dtItems))
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lblRecords.Text = dtItems.DefaultView.Count.ToString();

        }

        public void LoadItems()
        {

            DataTable dtLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetFullInfo();

            if (dtLocalDrivingLicenseApplications.Rows.Count > 0)
                dgvLDLApplications.DataSource = dtLocalDrivingLicenseApplications;
            else
                dgvLDLApplications.DataSource = null;

            lblRecords.Text = dtLocalDrivingLicenseApplications.Rows.Count.ToString();

        }

        private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {

            LoadItems();

            if (dgvLDLApplications.Columns.Count > 0)
            {

                cbFilters.Items.Add("None");

                cbFilters.Items.Add("LDLAppID");
                cbFilters.Items.Add("NationalNo");
                cbFilters.Items.Add("FullName");
                cbFilters.Items.Add("Status");

                cbStatus.SelectedIndex = 0;

                cbFilters.SelectedIndex = 0;

            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            string filterName = cbFilters.SelectedItem != null ? cbFilters.SelectedItem.ToString() : string.Empty;

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
                tbValue.Enabled = true;

            if (filterName == "Status")
            {

                tbValue.Visible = false;
                cbStatus.Visible = true;

                DataTable dtItems = (DataTable)dgvLDLApplications.DataSource;

                cbStatus_SelectedIndexChanged(cbStatus, EventArgs.Empty);

            }
            else
            {

                tbValue.Visible = true;
                cbStatus.Visible = false;

                ApplyFilter(filterName, tbValue.Text);

            }

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

        }

        private void dgvLDLApplications_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvLDLApplications, e, cmsLocalDrivingLicenseApplication);

        }

        private void DeleteLocalDrivingLicenseApplication(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count == 0)
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (MessageBox.Show("Are you sure you want delete this Local Driving License Application?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

                try
                {

                    bool succeeded = DeleteItem(Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDLAppID"].Value));

                    if (succeeded)
                    {

                        MessageBox.Show("Local Driving License Application deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItems();

                    }
                    else
                        MessageBox.Show("Failed to delete Local Driving License Application", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch
                {

                    MessageBox.Show("Data has not been saved succeesfully. Local Driving License Application has data linked to it.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
                MessageBox.Show("Failed to delete Local Driving License Application", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        public bool DeleteItem(int LocalDrivingLicenseApplicationID)
        {

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null)
                return false;

            try
            {

                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID) && clsApplication.DeleteApplication(LocalDrivingLicenseApplication.ApplicationID)){

                    LoadItems();
                    return true;

                }
                else
                    return false;

            }
            catch
            {

                throw;

            }

        }

        private void tsEditLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {

            /*if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                //Getting LocalDrivingLicenseApplicationID from row selected
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDLAppID"].Value);

                frmAddEditLDLApplication EditLocalDrivingLicenseApplication = new frmAddEditLDLApplication(LocalDrivingLicenseApplicationID);
                EditLocalDrivingLicenseApplication.OnSaveEventHandler += LoadItems;

                EditLocalDrivingLicenseApplication.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }*/

        }

        private void AddLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {

            frmAddEditLDLApplication AddLocalDrivingLicenseApplication = new frmAddEditLDLApplication();
            AddLocalDrivingLicenseApplication.OnSaveEventHandler += LoadItems;

            AddLocalDrivingLicenseApplication.ShowDialog();

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() == "LocalDrivingLicenseApplicationID")
            {

                Utils.UI.StopEnteringCharacters(e);

            }

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbStatus.SelectedItem != null)
            {

                if(cbStatus.SelectedItem.ToString() != "All")
                    ApplyFilter("Status", cbStatus.SelectedItem.ToString());
                else
                    ApplyFilter("Status", "");

            }
            else
                ApplyFilter("Status", "");

        }

        private void tsCancel_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDLAppID"].Value));
                clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);

                if (Application.ApplicationStatus == clsApplication.enStatus.New)
                {

                    Application.ApplicationStatus = clsApplication.enStatus.Canceled;
                    Application.LastStatusDate = DateTime.Now;

                    Application.Save();

                    LoadItems();

                }
                else
                {

                    MessageBox.Show("You can't cancel this application!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

    }

}

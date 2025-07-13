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
using DVLDPresentationLayer.Tests;
using DVLDPresentationLayer.Licenses;

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

            DataTable dtLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetFullInfo();

            dgvLDLApplications.DataSource = dtLocalDrivingLicenseApplications;

            lblRecords.Text = dtLocalDrivingLicenseApplications.Rows.Count.ToString();

        }

        private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {

            LoadItems();

            cbFilters.Items.Add("None");

            cbFilters.Items.Add("LDL AppID");
            cbFilters.Items.Add("National No");
            cbFilters.Items.Add("Full Name");
            cbFilters.Items.Add("Status");

            cbStatus.SelectedIndex = 0;

            cbFilters.SelectedIndex = 0;

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

                    bool succeeded = DeleteItem(Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value));

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

        private void AddLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {

            frmNewLDLApplication AddLocalDrivingLicenseApplication = new frmNewLDLApplication();
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

                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value));
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

        private void cmsLocalDrivingLicenseApplication_Opening(object sender, CancelEventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value));
                clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);

                int PassedTests = (int)dgvLDLApplications.SelectedRows[0].Cells["Passed Tests"].Value;

                clsDriver Driver = clsDriver.FindDriverByPersonID(Application.ApplicantPersonID);

                if (Application.ApplicationStatus == clsApplication.enStatus.Completed)
                {

                    if (Driver != null)
                    {

                        if (clsLicense.HasLicenseInSameClass(Driver.DriverID, LDLApplication.LicenseClassID))
                        {

                            tsShowLicense.Enabled = true;

                        }

                    }
                    else
                        tsShowLicense.Enabled = false;

                    tsCancel.Enabled = false;
                    tsScheduleTest.Enabled = false;
                    tsIssueDrivingLicense.Enabled = false;

                }
                else if (Application.ApplicationStatus == clsApplication.enStatus.New)
                {

                    tsCancel.Enabled = true;

                    if (PassedTests < 3)
                        tsScheduleTest.Enabled = true;
                    else
                    {

                        if (PassedTests >= 1)
                            tsDelete.Enabled = false;

                        if(PassedTests == 3)
                            tsIssueDrivingLicense.Enabled = true;

                        tsScheduleTest.Enabled = false;
                        
                    }

                }
                else
                {

                    tsCancel.Enabled = false;
                    tsScheduleTest.Enabled = false;
                    tsIssueDrivingLicense.Enabled = false;
                    tsShowLicense.Enabled = false;

                }

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsScheduleTest_DropDownOpening(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                List<ToolStripMenuItem> TestsMenuStrips = new List<ToolStripMenuItem>();

                TestsMenuStrips.Add(tsVisionTest);
                TestsMenuStrips.Add(tsWrittenTest);
                TestsMenuStrips.Add(tsStreetTest);

                TestsMenuStrips[0].Enabled = false;
                TestsMenuStrips[1].Enabled = false;
                TestsMenuStrips[2].Enabled = false;

                int PassedTests = (int)dgvLDLApplications.SelectedRows[0].Cells["Passed Tests"].Value;

                if(PassedTests < 3)
                    TestsMenuStrips[Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["Passed Tests"].Value)].Enabled = true;

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsVisionTest_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                frmTestAppointments TestAppointments = new frmTestAppointments(frmTestAppointments.enTestType.Vision, Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value));
                TestAppointments.OnSaveEventHandler += LoadItems;

                TestAppointments.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsWrittenTest_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                frmTestAppointments TestAppointments = new frmTestAppointments(frmTestAppointments.enTestType.Written, Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value));
                TestAppointments.OnSaveEventHandler += LoadItems;

                TestAppointments.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsStreetTest_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                frmTestAppointments TestAppointments = new frmTestAppointments(frmTestAppointments.enTestType.Street, Convert.ToInt32(dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value));
                TestAppointments.OnSaveEventHandler += LoadItems;

                TestAppointments.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsShowDetails_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                frmShowLDLApplicationDetails ShowLDLApplicationDetails = new frmShowLDLApplicationDetails((int)dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value);
                ShowLDLApplicationDetails.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsIssueDrivingLicense_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                frmIssueDrivingLicense IssueDrivingLicense = new frmIssueDrivingLicense((int)dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value);
                IssueDrivingLicense.OnSaveEventHandler += LoadItems;

                IssueDrivingLicense.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsShowLicense_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication((int)dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value);
                if (LDLApplication == null)
                    return;

                clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);
                if (Application == null)
                    return;

                int PersonID = Application.ApplicantPersonID;
                clsDriver Driver = clsDriver.FindDriverByPersonID(PersonID);

                if (PersonID != -1)
                {

                    clsLicense License = clsLicense.FindLicenseByApplicationID(Application.ApplicationID);

                    if (License != null)
                    {

                        frmShowDrivingLicenseInfo ShowDriverLicenseInfo = new frmShowDrivingLicenseInfo(License.LicenseID);
                        ShowDriverLicenseInfo.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("No license found for this driver.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {

                    MessageBox.Show("Driver not found for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {

            if (dgvLDLApplications.SelectedRows.Count > 0)
            {

                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication((int)dgvLDLApplications.SelectedRows[0].Cells["LDL AppID"].Value);
                if (LDLApplication == null)
                    return;

                clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);
                if (Application == null)
                    return;

                int PersonID = Application.ApplicantPersonID;
                clsDriver Driver = clsDriver.FindDriverByPersonID(PersonID);

                if (PersonID != -1)
                {

                    frmDriverLicensesHistory DriverLicensesHistory = new frmDriverLicensesHistory(Driver.DriverID);
                    DriverLicensesHistory.ShowDialog();

                }
                else
                {

                    MessageBox.Show("Driver not found for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

    }

}

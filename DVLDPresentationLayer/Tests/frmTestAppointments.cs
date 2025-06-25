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

namespace DVLDPresentationLayer.Tests
{
    public partial class frmTestAppointments : Form
    {

        public enum enTestType{Vision = 1, Written = 2, Street = 3}
        enTestType TestType;

        clsLocalDrivingLicenseApplication LDLApplication { get; set; }

        public frmTestAppointments(enTestType TestType, int LDLApplicationID)
        {

            InitializeComponent();

            this.TestType = TestType;
            this.LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(LDLApplicationID);

        }

        public void RefreshWindowInfo(enTestType TestType)
        {

            switch (TestType)
            {

                case enTestType.Vision:
                    pbTestType.Image = Properties.Resources.Vision_512;
                    lblTitle.Text = "Vision Test Appointments";
                    this.Text = "Vision Test Appointments";
                    break;

                case enTestType.Written:
                    pbTestType.Image = Properties.Resources.Written_Test_512;
                    lblTitle.Text = "Written Test Appointments";
                    this.Text = "Written Test Appointments";
                    break;

                case enTestType.Street:
                    pbTestType.Image = Properties.Resources.Street_Test_32;
                    lblTitle.Text = "Street Test Appointments";
                    this.Text = "Street Test Appointments";
                    break;

            }

        }

        private void RefreshAppointments()
        {

            if (LDLApplication == null)
            {

                MessageBox.Show("This LDLApplication is unavailable", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);

            if (Application == null)
            {

                MessageBox.Show("This Application is unavailable", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            dgvAppointments.DataSource = clsTestAppointment.GetTestAppointmentsMainInfoForPersonTestType(Application.ApplicantPersonID, (int)TestType);
            lblRecords.Text = ((DataTable)dgvAppointments.DataSource).Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void dgvAppointments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvAppointments, e, cmsAppointment);

        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvAppointments.SelectedRows.Count > 0)
            {

                int AppointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["TestAppointmentID"].Value);

                frmScheduleTest ScheduleTest = new frmScheduleTest(AppointmentID);
                ScheduleTest.OnSaveEventHandler += RefreshAppointments;

                ScheduleTest.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {

            clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);

            if (Application == null)
            {

                MessageBox.Show("This Application is unavailable", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (clsTestAppointment.HasActiveAppointmentInTestType(Application.ApplicantPersonID, (int)TestType))
            {

                MessageBox.Show("This person has an appointment to same test!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {

                frmScheduleTest ScheduleTest = new frmScheduleTest((frmScheduleTest.enTestType)TestType, LDLApplication.LocalDrivingLicenseApplicationID);
                ScheduleTest.OnSaveEventHandler += RefreshAppointments;

                ScheduleTest.ShowDialog();

            }

        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {

            RefreshWindowInfo(TestType);
            ctrlDrivingLicenseDLApplicationInfo1.Refresh(LDLApplication.LocalDrivingLicenseApplicationID);

            RefreshAppointments();

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }

}

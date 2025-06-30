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

    public partial class frmTakeTest : Form
    {

        public clsTest Test { get; private set; }
        public clsTestAppointment Appointment { get; private set; }

        public event Action OnSaveEventHandler;

        public frmTakeTest()
        {

            InitializeComponent();
            Test = new clsTest();
            
        }

        public frmTakeTest(int AppointmentID)
        {

            InitializeComponent();
            ctrlTestInfo1.Refresh(AppointmentID);

            Test = new clsTest();
            Appointment = clsTestAppointment.FindTestAppointment(AppointmentID);

        }

        private bool TakeTest()
        {

            if(Appointment == null)
            {

                MessageBox.Show("Appointment is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);          
                this.Close();

                return false;

            }

            Appointment.IsLocked = true;
            Test.TestAppointmentID = Appointment.TestAppointmentID;
            Test.Notes = tbNotes.Text;
            Test.TestResult = rbPass.Checked;

            if (Global.user != null)
                Test.CreatedByUserID = Global.user.UserID;
            else
            {

                MessageBox.Show("User is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return (Appointment.Save() && Test.Save());

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (TakeTest())
                MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {

                MessageBox.Show("Data has not been saved successfully!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            this.Close();

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {

            if (Appointment != null)
            {

                if (Appointment.IsLocked)
                {

                    clsTest Test = clsTest.FindTestByAppointmentID(Appointment.TestAppointmentID);

                    if (Test == null)
                    {

                        MessageBox.Show("This appointment is locked. But cannot find test!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }

                    if (Test.TestResult)
                    {

                        MessageBox.Show("You cannot edit test result. This Test is already passed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }

                    clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Appointment.LocalDrivingLicenseApplicationID);

                    if (LDLApp == null)
                    {

                        MessageBox.Show("Driving license application is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }

                    clsApplication Application = clsApplication.FindApplication(LDLApp.ApplicationID);

                    if (Application == null)
                    {

                        MessageBox.Show("Application data is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }

                    if (clsTest.HasPassedTest(Application.ApplicantPersonID, Appointment.TestTypeID))
                    {

                        MessageBox.Show("This person has already passed this test!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }


                    MessageBox.Show("This test is failed. So retake test window will be shown.", "Failed test", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmScheduleTest RetakeTest = new frmScheduleTest(Appointment.TestAppointmentID);

                    if (OnSaveEventHandler != null)
                        RetakeTest.OnSaveEventHandler += OnSaveEventHandler;

                    RetakeTest.ShowDialog();

                    this.Close();

                    return;

                }

            }
            else
            {

                MessageBox.Show("Appointment is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
        }

    }

}

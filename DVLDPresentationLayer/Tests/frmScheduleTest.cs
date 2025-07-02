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

    public partial class frmScheduleTest : Form
    {

        public enum enMode{Add, Edit, RetakeTest}
        public enMode Mode;

        public clsTestAppointment Appointment { get; private set; }
        public clsTest PrevTest { get; private set; }

        private int LDLApplicationID;

        public enum enTestType { Vision = 1, Written = 2, Street = 3 }
        enTestType TestType;

        public event Action OnSaveEventHandler;

        public frmScheduleTest(enTestType TestType, int LDLApplicationID)
        {

            InitializeComponent();

            Appointment = new clsTestAppointment();

            this.TestType = TestType;
            this.LDLApplicationID = LDLApplicationID;

            Mode = enMode.Add;

        }

        public frmScheduleTest(int AppointmentID)
        {

            InitializeComponent();

            Appointment = clsTestAppointment.FindTestAppointment(AppointmentID);

            if (Appointment != null)
            {

                TestType = (enTestType)Appointment.TestTypeID;

                if (Appointment.IsLocked)
                {

                    Mode = enMode.RetakeTest;

                    //Find Test By Appointment Here
                    PrevTest = clsTest.FindTestByAppointmentID(Appointment.TestAppointmentID);

                    if (PrevTest == null)
                    {

                        MessageBox.Show("Cannot find original test linked to this appointment!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }

                    if (PrevTest.TestResult == true)
                    {

                        MessageBox.Show("The test result is pass. You can't retake passed test!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                        return;

                    }
                    else
                    {

                        gbRetakeTestInfo.Enabled = true;
                        ShowRetakeTestInfo();

                    }

                }
                else
                {

                    Mode = enMode.Edit;

                }

            }

        }

        public void RefreshWindowInfo(enTestType TestType)
        {

            switch (TestType)
            {

                case enTestType.Vision:
                    pbTestType.Image = Properties.Resources.Vision_512;
                    lblTitle.Text = "Schedule Vision Test";
                    lblErrorMessage.Text = "Cannot schedule. Vision test must be passed first.";
                    break;

                case enTestType.Written:
                    pbTestType.Image = Properties.Resources.Written_Test_512;
                    lblTitle.Text = "Written Test Appointments";
                    lblErrorMessage.Text = "Cannot schedule. Written test must be passed first.";
                    break;

                case enTestType.Street:
                    pbTestType.Image = Properties.Resources.driving_test_512;
                    lblTitle.Text = "Street Test Appointments";
                    lblErrorMessage.Text = "Cannot schedule. Street test must be passed first.";
                    break;

            }

        }

        private void ShowAppointmentData(clsTestAppointment Appointment)
        {

            if (Appointment.TestAppointmentID != -1)
                lblLDLAppID.Text = Appointment.TestAppointmentID.ToString();

            lblTrial.Text = "0";

            if (Appointment.AppointmentDate < dtpTestDate.MinDate)
                dtpTestDate.Value = dtpTestDate.MinDate;
            else if (Appointment.AppointmentDate > dtpTestDate.MaxDate)
                dtpTestDate.Value = dtpTestDate.MaxDate;

            lblFees.Text = ((int)Appointment.PaidFees).ToString();

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Appointment.LocalDrivingLicenseApplicationID);

            if(LocalDrivingLicenseApplication == null)
                return;

            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClass(LocalDrivingLicenseApplication.LicenseClassID);
            clsApplication Application = clsApplication.FindApplication(LocalDrivingLicenseApplication.ApplicationID);
            
            if(Application == null)
                return;

            if(LicenseClass != null)
                lblDrivingClass.Text = LicenseClass.ClassName;

            clsPerson Person = clsPerson.FindPerson(Application.ApplicantPersonID);

            if (Person != null && LicenseClass != null)
            {

                lblName.Text = Person.FullName();
                lblTrial.Text = clsTestAppointment.GetTestAppointmentsMainInfoForPersonTestType(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, (int)TestType).Rows.Count.ToString() + "/3";

            }

        }

        private void FillAppointment(clsTestAppointment Appointment)
        {

            Appointment.AppointmentDate = dtpTestDate.Value;

            if (Mode == enMode.Add || Mode == enMode.RetakeTest)
            {

                LDLApplicationID = (Mode == enMode.RetakeTest) ? Appointment.LocalDrivingLicenseApplicationID : LDLApplicationID;

                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(LDLApplicationID);               

                if (LocalDrivingLicenseApplication == null)
                {

                    MessageBox.Show("Local Driving License Application is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                clsTestType TestType = clsTestType.FindTestType((int)this.TestType);

                if (TestType == null)
                {

                    MessageBox.Show("Test Type is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                Appointment.LocalDrivingLicenseApplicationID = LDLApplicationID;

                if (Global.user != null)
                    Appointment.CreatedByUserID = Global.user.UserID;

                Appointment.IsLocked = false;
                Appointment.TestTypeID = (int)this.TestType;
                Appointment.PaidFees = TestType.TestTypeFees;

                return;

            }

        }

        private void FillRetakeTestApplication(clsApplication RetakeTestApplication)
        {

            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Appointment.LocalDrivingLicenseApplicationID);

            if (LDLApplication == null)
            {

                MessageBox.Show("Local Driving License Application is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsApplication Application = clsApplication.FindApplication(LDLApplication.ApplicationID);

            if (Application == null)
            {

                MessageBox.Show("Application is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            RetakeTestApplication.ApplicantPersonID = Application.ApplicantPersonID;
            RetakeTestApplication.ApplicationDate = DateTime.Now;
            RetakeTestApplication.ApplicationTypeID = 7;
            RetakeTestApplication.ApplicationStatus = clsApplication.enStatus.New;
            RetakeTestApplication.LastStatusDate = DateTime.Now;

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(7);

            if (ApplicationType != null)
                RetakeTestApplication.PaidFees = ApplicationType.ApplicationFees;

            if (Global.user != null)
                RetakeTestApplication.CreatedByUserID = Global.user.UserID;
            else
                MessageBox.Show("No user is logged in. You cannot do this action!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

            RefreshWindowInfo(TestType);

            if (Mode != enMode.Edit)
                dtpTestDate.MinDate = DateTime.Now;

            FillAppointment(Appointment);

            ShowAppointmentData(Appointment);

        }


        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }
        
        private void ShowRetakeTestInfo()
        {

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(7);

            if (ApplicationType == null)
            {

                MessageBox.Show("Application Type is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblRetakeTestAppFees.Text = ApplicationType.ApplicationFees.ToString();
            lblTotalFees.Text = (Appointment.PaidFees + ApplicationType.ApplicationFees).ToString();

        }

        private bool RetakeTestMethod()
        {

            clsLocalDrivingLicenseApplication originalLDLApp = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Appointment.LocalDrivingLicenseApplicationID);

            if (originalLDLApp == null)
            {
                MessageBox.Show("Local Driving License Application is unavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            clsApplication RetakeTestApplication = new clsApplication();
            FillRetakeTestApplication(RetakeTestApplication);

            bool IsRetakeTestApplicationSaved = RetakeTestApplication.Save();

            clsTestAppointment NewAppointment = new clsTestAppointment();
            NewAppointment.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
            NewAppointment.LocalDrivingLicenseApplicationID = originalLDLApp.LocalDrivingLicenseApplicationID;
            FillAppointment(NewAppointment);

            bool IsNewAppointmentSaved = NewAppointment.Save();

            return (IsNewAppointmentSaved && IsRetakeTestApplicationSaved);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            FillAppointment(Appointment);
            bool succeeded = false;

            if (Mode == enMode.RetakeTest)
                succeeded = RetakeTestMethod();
            else
            {

                FillAppointment(Appointment);
                succeeded = Appointment.Save();

            }

            if (succeeded)
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

    }

}

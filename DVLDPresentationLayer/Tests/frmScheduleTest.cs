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
        public clsTest RetakeTest { get; private set; }

        private int LDLApplicationID;

        public enum enTestType { Vision = 1, Written = 2, Street = 3 }
        enTestType TestType;

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

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
                    PrevTest = new clsTest();
                    RetakeTest = new clsTest();

                    if (PrevTest.TestResult == true)
                    {

                        MessageBox.Show("The test result is pass. You can't reatake passed test!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();

                    }
                    else
                    {

                        gbRetakeTestInfo.Enabled = true;

                    }

                }

            }

            Mode = enMode.Edit;

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
                    pbTestType.Image = Properties.Resources.Street_Test_32;
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
            dtpTestDate.Value = Appointment.AppointmentDate;
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

            if (Person != null)
                lblName.Text = Person.FullName();

        }

        private void FillAppointment()
        {

            Appointment.AppointmentDate = dtpTestDate.Value;

            if (Mode == enMode.Add)
            {

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

            }

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

            RefreshWindowInfo(TestType);

            if (Mode == enMode.Add)
            {

                dtpTestDate.MinDate = DateTime.Now;
                FillAppointment();

            }
                
            ShowAppointmentData(Appointment);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void FillRetakeTest(clsTest RetakeTest)
        {


        }

        private void RetakeTestMethod(clsTestAppointment Appointment, clsTest PrevTest, clsTest RetakeTest)
        {

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            FillAppointment();

            if (Mode == enMode.RetakeTest)
                RetakeTestMethod(Appointment, PrevTest, RetakeTest);
            else
            {

                if (!Appointment.Save())
                    return;

            }

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            this.Close();

        }

    }

}

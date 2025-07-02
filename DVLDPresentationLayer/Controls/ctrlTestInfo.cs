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

    public partial class ctrlTestInfo : UserControl
    {

        public clsTestAppointment Appointment { get; private set; }

        public enum enTestType { Vision = 1, Written = 2, Street = 3 }
        enTestType TestType;

        public ctrlTestInfo()
        {

            InitializeComponent();

        }

        public ctrlTestInfo(int AppointmentID)
        {

            InitializeComponent();

            Refresh(AppointmentID);

        }

        public void RefreshWindowInfo(enTestType TestType)
        {

            switch (TestType)
            {

                case enTestType.Vision:
                    pbTestType.Image = Properties.Resources.Vision_512;
                    break;

                case enTestType.Written:
                    pbTestType.Image = Properties.Resources.Written_Test_512;
                    break;

                case enTestType.Street:
                    pbTestType.Image = Properties.Resources.driving_test_512;
                    break;

            }

        }

        public void LoadAppointment(int AppointmentID)
        {

            Appointment = clsTestAppointment.FindTestAppointment(AppointmentID);

        }

        public void ShowItem(clsTestAppointment Appointment)
        {

            if (Appointment == null)
                return;

            if (Appointment.TestAppointmentID != -1)
                lblLDLAppID.Text = Appointment.TestAppointmentID.ToString();

            lblDate.Text = Appointment.AppointmentDate.ToShortDateString();
            lblFees.Text = ((int)Appointment.PaidFees).ToString();

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(Appointment.LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null)
                return;

            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClass(LocalDrivingLicenseApplication.LicenseClassID);
            clsApplication Application = clsApplication.FindApplication(LocalDrivingLicenseApplication.ApplicationID);

            if (Application == null)
                return;

            if (LicenseClass != null)
                lblDrivingClass.Text = LicenseClass.ClassName;

            clsPerson Person = clsPerson.FindPerson(Application.ApplicantPersonID);

            if (Person != null && LicenseClass != null)
            {

                lblName.Text = Person.FullName();
                lblTrial.Text = clsTestAppointment.GetTestAppointmentsMainInfoForPersonTestType(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, (int)TestType).Rows.Count.ToString() + "/3";

            }

        }

        public void RefreshInformation()
        {

            if (Appointment == null)
            {

                MessageBox.Show("Appointment data not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            Appointment = clsTestAppointment.FindTestAppointment(Appointment.TestAppointmentID);

            if (Appointment == null)
            {

                MessageBox.Show("Appointment data not found on refresh!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ShowItem(Appointment);

        }

        public void Refresh(int AppointmentID)
        {

            if (AppointmentID != -1)
            {

                LoadAppointment(AppointmentID);
                TestType = (enTestType)Appointment.TestTypeID;

                RefreshInformation();
                RefreshWindowInfo(TestType);

            }

        }

    }

}

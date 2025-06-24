using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsTestAppointment
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int TestAppointmentID { get; set; }

        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }

        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public clsTestAppointment()
        {

            TestAppointmentID = -1;

            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;

            AppointmentDate = DateTime.Now;
            PaidFees = 0;

            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;

            Mode = enMode.Add;

        }

        public clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
                                  decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {

            this.TestAppointmentID = TestAppointmentID;

            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;

            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            Mode = enMode.Update;

        }
        
        public static clsTestAppointment FindTestAppointment(int TestAppointmentID)
        {

            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;

            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            TestAppointmentsData.FindTestAppointment(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate,
                                                     ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID);

            if (TestTypeID == -1)
                return null;

            return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                                          PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

        }
        
        private bool Add()
        {

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
            clsApplication Application = new clsApplication();

            if(LocalDrivingLicenseApplication == null)
                return false;

            Application = clsApplication.FindApplication(LocalDrivingLicenseApplication.ApplicationID);

            if(Application == null)
                return false;

            if (HasActiveAppointmentInTestType(Application.ApplicantPersonID, TestTypeID))
                return false;

            int TestAppointmentID = this.TestAppointmentID;

            bool succeeded = TestAppointmentsData.AddTestAppointment(ref TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                                                                     PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

            this.TestAppointmentID = TestAppointmentID;

            return succeeded;

        }

        private bool Update()
        {

            return TestAppointmentsData.UpdateTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                                                              PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

        }

        public bool Save()
        {

            bool succeeded = false;

            switch(Mode)
            {

                case enMode.Add:
                    succeeded = Add();
                    Mode = enMode.Update;
                    break;

                case enMode.Update:
                    succeeded = Update();
                    break;

                default:
                    break;

            }

            return succeeded;

        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {

            if (!TestAppointmentsData.DoesTestAppointmentExist(TestAppointmentID))
                return false;

            return TestAppointmentsData.DeleteTestAppointment(TestAppointmentID);

        }

        public static bool DoesTestAppointmentExist(int TestAppointmentID)
        {

            return TestAppointmentsData.DoesTestAppointmentExist(TestAppointmentID);

        }

        public static bool HasActiveAppointmentInTestType(int ApplicantPersonID, int TestTypeID)
        {

            return TestAppointmentsData.HasActiveAppointmentInTestType(ApplicantPersonID, TestTypeID);

        }

        static public DataTable GetTestAppointments()
        {

            return TestAppointmentsData.GetTestAppointments();

        }

        static public DataTable GetTestAppointmentsMainInfo()
        {

            return TestAppointmentsData.GetTestAppointmentsMainInfo();

        }

    }

}

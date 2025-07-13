using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsInternationalLicense
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int LicenseID { get; set; }

        private int _ApplicationID;
        public int ApplicationID
        {

            get { return _ApplicationID; }
            set
            {

                _ApplicationID = value;
                Application = clsApplication.FindApplication(_ApplicationID);

                if (Application == null)
                    Application = new clsApplication();

            }

        }
        public clsApplication Application { get; set; }

        private int _DriverID;
        public int DriverID
        {

            get { return _DriverID; }
            set
            {

                _DriverID = value;
                Driver = clsDriver.FindDriver(_DriverID);

                if (Driver == null)
                    Driver = new clsDriver();

            }

        }
        public clsDriver Driver { get; set; }

        private int _IssuedUsingLocalLicenseID;
        public int IssuedUsingLocalLicenseID
        {

            get { return _IssuedUsingLocalLicenseID; }
            set
            {

                _IssuedUsingLocalLicenseID = value;
                IssuedUsingLocalLicense = clsLicense.FindLicense(_IssuedUsingLocalLicenseID);

                if (IssuedUsingLocalLicense == null)
                    IssuedUsingLocalLicense = new clsLicense();

            }

        }
        public clsLicense IssuedUsingLocalLicense { get; set; }

        private DateTime _IssueDate;
        public DateTime IssueDate
        {

            get { return _IssueDate; }
            set
            {

                _IssueDate = value;
                ExpirationDate = _IssueDate.AddYears(1);

            }

        }

        public DateTime ExpirationDate { get; private set; }

        public bool IsActive { get; set; }

        public int CreatedByUserID { get; set; }

        public clsInternationalLicense()
        {

            LicenseID = -1;
            _ApplicationID = -1;
            _DriverID = -1;
            _IssuedUsingLocalLicenseID = -1;

            IssueDate = DateTime.Now;
            ExpirationDate = IssueDate.AddYears(1);

            IsActive = false;
            CreatedByUserID = -1;

            Mode = enMode.Add;

        }

        public clsInternationalLicense(int LicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
                                       DateTime IssueDate, bool IsActive, int CreatedByUserID)
        {

            this.LicenseID = LicenseID;

            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;

            this.IssueDate = IssueDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;

        }

        public static clsInternationalLicense FindLicense(int LicenseID)
        {

            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (!InternationalLicensesData.FindLicense(LicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                                                       ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
                return null;

            return new clsInternationalLicense(LicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                                               IssueDate, IsActive, CreatedByUserID);

        }

        public static clsInternationalLicense FindLicenseByApplicationID(int ApplicationID)
        {

            int LicenseID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (!InternationalLicensesData.FindLicenseByApplicationID(ApplicationID, ref LicenseID, ref DriverID, ref IssuedUsingLocalLicenseID,
                                                                      ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
                return null;

            return new clsInternationalLicense(LicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                                               IssueDate, IsActive, CreatedByUserID);

        }

        private bool Add()
        {

            int LicenseID = this.LicenseID;

            bool succeeded = InternationalLicensesData.AddLicense(ref LicenseID, _ApplicationID, _DriverID, _IssuedUsingLocalLicenseID,
                                                                  IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            this.LicenseID = LicenseID;

            return succeeded;

        }

        private bool Update()
        {

            return InternationalLicensesData.UpdateLicense(LicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                                                           IssueDate, ExpirationDate, IsActive, CreatedByUserID);

        }

        public bool Save()
        {

            bool succeeded = false;

            switch (Mode)
            {

                case enMode.Add:
                    succeeded = Add();

                    if (succeeded)
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

        public static bool DeleteLicense(int LicenseID)
        {

            if (!DoesLicenseExist(LicenseID))
                return false;

            return InternationalLicensesData.DeleteLicense(LicenseID);

        }

        public static bool DoesLicenseExist(int LicenseID)
        {

            return InternationalLicensesData.DoesLicenseExist(LicenseID);

        }

        public static DataTable GetLicenses()
        {

            return InternationalLicensesData.GetLicenses();

        }

        public static DataTable GetDriverInternationalLicensesHistory(int DriverID)
        {

            return InternationalLicensesData.GetDriverInternationalLicensesHistory(DriverID);

        }

        public static DataTable GetLicensesMainInfo()
        {

            return InternationalLicensesData.GetLicensesMainInfo();

        }


    }

}
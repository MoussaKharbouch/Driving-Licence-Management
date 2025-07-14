using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsLicense
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

        private int _LicenseClassID;
        public int LicenseClassID
        {

            get { return _LicenseClassID; }
            set
            {

                _LicenseClassID = value;
                LicenseClass = clsLicenseClass.FindLicenseClass(_LicenseClassID);

                if (LicenseClass == null)
                    LicenseClass = new clsLicenseClass();

                ExpirationDate = IssueDate.AddYears(LicenseClass.DefaultValidityLength);

            }

        }
        public clsLicenseClass LicenseClass { get; set; }

        private DateTime _IssueDate;
        public DateTime IssueDate
        {

            get { return _IssueDate; }
            set
            {
                _IssueDate = value;

                if (LicenseClass != null)
                    ExpirationDate = _IssueDate.AddYears(LicenseClass.DefaultValidityLength);
                else
                    ExpirationDate = _IssueDate;
            }

        }

        public DateTime ExpirationDate { get; private set; }

        public string Notes { get; set; }

        public decimal PaidFees { get; set; }

        public bool IsActive { get; set; }

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public enIssueReason IssueReason { get; set; }

        public string IssueReasonText
        {

            get
            {

                switch (IssueReason)
                {

                    case enIssueReason.FirstTime:
                        return "First Time";

                    case enIssueReason.Renew:
                        return "Renew";

                    case enIssueReason.DamagedReplacement:
                        return "Replacement for Damaged";

                    case enIssueReason.LostReplacement:
                        return "Replacement for Lost";

                    default:
                        return "First Time";

                }

            }

        }

        public int CreatedByUserID { get; set; }

        public clsLicense()
        {

            LicenseID = -1;
            _ApplicationID = -1;
            _DriverID = -1;
            _LicenseClassID = -1;

            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;

            Notes = string.Empty;
            PaidFees = 0;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = -1;

            Mode = enMode.Add;

        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
                          DateTime IssueDate, DateTime ExpirationDate, string Notes,
                          decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {

            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;

            _LicenseClassID = LicenseClassID;
            LicenseClass = clsLicenseClass.FindLicenseClass(LicenseClassID);

            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;

            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;

        }

        public static clsLicense FindLicense(int LicenseID)
        {

            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = string.Empty;
            decimal PaidFees = 0;
            bool IsActive = false;
            short IssueReason = 0;
            int CreatedByUserID = -1;

            if (!LicensesData.FindLicense(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                                          ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                                          ref IsActive, ref IssueReason, ref CreatedByUserID))
                return null;

            return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID,
                                  IssueDate, ExpirationDate, Notes, PaidFees,
                                  IsActive, (enIssueReason)IssueReason, CreatedByUserID);

        }

        public static clsLicense FindLicenseByApplicationID(int ApplicationID)
        {

            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = string.Empty;
            decimal PaidFees = 0;
            bool IsActive = false;
            short IssueReason = 0;
            int CreatedByUserID = -1;

            if (!LicensesData.FindLicenseByApplicationID(ApplicationID, ref LicenseID, ref DriverID, ref LicenseClassID,
                                                         ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                                                         ref IsActive, ref IssueReason, ref CreatedByUserID))
                return null;

            return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID,
                                  IssueDate, ExpirationDate, Notes, PaidFees,
                                  IsActive, (enIssueReason)IssueReason, CreatedByUserID);


        }

        private bool Add()
        {

            int LicenseID = this.LicenseID;

            bool succeeded = LicensesData.AddLicense(ref LicenseID, _ApplicationID, _DriverID, _LicenseClassID,
                                                     IssueDate, ExpirationDate, Notes, PaidFees,
                                                     IsActive, (short)IssueReason, CreatedByUserID);

            this.LicenseID = LicenseID;

            return succeeded;

        }

        private bool Update()
        {

            return LicensesData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClassID,
                                              IssueDate, ExpirationDate, Notes, PaidFees,
                                              IsActive, (short)IssueReason, CreatedByUserID);

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

            return LicensesData.DeleteLicense(LicenseID);

        }

        public static bool DoesLicenseExist(int LicenseID)
        {

            return LicensesData.DoesLicenseExist(LicenseID);

        }

        public static bool HasLicenseInSameClass(int DriverID, int LicenseClassID)
        {

            return LicensesData.HasLicenseInSameClass(DriverID, LicenseClassID);

        }

        public static DataTable GetLicenses()
        {

            return LicensesData.GetLicenses();

        }

        public static DataTable GetDriverLocalLicensesHistory(int DriverID)
        {

            return LicensesData.GetDriverLocalLicensesHistory(DriverID);

        }

    }

}

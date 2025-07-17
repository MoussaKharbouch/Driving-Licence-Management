using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsDetainedLicense
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int DetainID { get; set; }

        private int _LicenseID;
        public int LicenseID
        {

            get { return _LicenseID; }
            set
            {

                _LicenseID = value;
                License = clsLicense.FindLicense(_LicenseID);

                if (License == null)
                    License = new clsLicense();

            }

        }

        public clsLicense License { get; set; }

        public DateTime DetainDate { get; set; }

        public decimal FineFees { get; set; }

        private int _CreatedByUserID;
        public int CreatedByUserID
        {
            get { return _CreatedByUserID; }
            set
            {
                _CreatedByUserID = value;
                CreatedByUser = clsUser.FindUser(_CreatedByUserID);

                if (CreatedByUser == null)
                    CreatedByUser = new clsUser();
            }
        }
        public clsUser CreatedByUser { get; set; }

        public bool IsReleased { get; set; }

        private int _ReleasedByUserID;
        public int ReleasedByUserID
        {

            get { return _ReleasedByUserID; }
            set
            {

                _ReleasedByUserID = value;
                ReleasedByUser = (_ReleasedByUserID != -1 ? clsUser.FindUser(_ReleasedByUserID) : null);

            }

        }

        public clsUser ReleasedByUser { get; set; }

        private int _ReleaseApplicationID;
        public int ReleaseApplicationID
        {

            get { return _ReleaseApplicationID; }
            set
            {

                _ReleaseApplicationID = value;
                ReleaseApplication = (_ReleaseApplicationID != -1 ? clsApplication.FindApplication(_ReleaseApplicationID) : null);

            }

        }

        public clsApplication ReleaseApplication { get; set; }

        private DateTime _ReleaseDate;
        public DateTime ReleaseDate
        {

            get { return _ReleaseDate; }
            set { _ReleaseDate = value; }

        }

        public clsDetainedLicense()
        {

            DetainID = -1;
            _LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            _CreatedByUserID = -1;
            IsReleased = false;
            _ReleasedByUserID = -1;
            _ReleaseApplicationID = -1;
            _ReleaseDate = DateTime.MinValue;
            Mode = enMode.Add;

        }

        public clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees,
                                   int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
                                   int ReleasedByUserID, int ReleaseApplicationID)
        {

            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            Mode = enMode.Update;

        }

        public static clsDetainedLicense Find(int DetainID)
        {

            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (!DetainedLicensesData.FindDetainedLicense(DetainID, ref LicenseID, ref DetainDate,
                                                           ref FineFees, ref CreatedByUserID, ref IsReleased,
                                                           ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return null;

            return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                                           CreatedByUserID, IsReleased, ReleaseDate,
                                           ReleasedByUserID, ReleaseApplicationID);

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {

            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (!DetainedLicensesData.FindDetainedLicenseByLicenseID(LicenseID, ref DetainID, ref DetainDate,
                                                                     ref FineFees, ref CreatedByUserID,
                                                                     ref IsReleased, ref ReleaseDate,
                                                                     ref ReleasedByUserID, ref ReleaseApplicationID))
                return null;

            return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                                          CreatedByUserID, IsReleased, ReleaseDate,
                                          ReleasedByUserID, ReleaseApplicationID);

        }


        private bool Add()
        {

            int DetainID = this.DetainID;

            bool result = DetainedLicensesData.AddDetainedLicense(ref DetainID, _LicenseID, DetainDate,
                                                                  FineFees, _CreatedByUserID, IsReleased,
                                                                  _ReleaseDate, _ReleasedByUserID, _ReleaseApplicationID);

            this.DetainID = DetainID;
            return result;

        }

        private bool Update()
        {

            return DetainedLicensesData.UpdateDetainedLicense(DetainID, _LicenseID, DetainDate,
                                                              FineFees, _CreatedByUserID, IsReleased,
                                                              _ReleaseDate, _ReleasedByUserID, _ReleaseApplicationID);

        }

        public bool Save()
        {

            bool result = false;

            switch (Mode)
            {

                case enMode.Add:
                    result = Add();
                    if (result)
                        Mode = enMode.Update;
                    break;

                case enMode.Update:
                    result = Update();
                    break;

            }

            return result;

        }

        public static bool IsDetained(int LicenseID)
        {

            return DetainedLicensesData.IsDetained(LicenseID);

        }

        public static bool Delete(int DetainID)
        {

            return DetainedLicensesData.DeleteDetainedLicense(DetainID);

        }

        public static bool DoesDetainedLicenseExist(int DetainID)
        {

            return DetainedLicensesData.DoesDetainedLicenseExist(DetainID);

        }

        public static DataTable GetAll()
        {

            return DetainedLicensesData.GetAllDetainedLicenses();

        }

        public static DataTable GetDetainedLicensesMainInfo()
        {

            return DetainedLicensesData.GetDetainedLicensesMainInfo();

        }


    }

}

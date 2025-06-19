using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsLocalDrivingLicenseApplication
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int LocalDrivingLicenseApplicationID { get; set; }

        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplication()
        {

            LocalDrivingLicenseApplicationID = -1;

            ApplicationID = -1;
            LicenseClassID = -1;

            Mode = enMode.Add;

        }

        public clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {

            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;

            Mode = enMode.Update;

        }
        
        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            int ApplicationID = -1;
            int LicenseClassID = -1;

            LocalDrivingLicenseApplicationsData.FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (ApplicationID == -1)
                return null;

            return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);

        }

        private bool Add()
        {

            int LocalDrivingLicenseApplicationID = this.LocalDrivingLicenseApplicationID;

            bool succeeded = LocalDrivingLicenseApplicationsData.AddLocalDrivingLicenseApplication(ref LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);

            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            return succeeded;

        }

        private bool Update()
        {

            return LocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            return LocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

        }

        public static bool DoesLocalDrivingLicenseApplicationExist(int LocalDrivingLicenseApplicationID)
        {

            return LocalDrivingLicenseApplicationsData.DoesLocalDrivingLicenseApplicationExist(LocalDrivingLicenseApplicationID);

        }

        static public DataTable GetLocalDrivingLicenseApplications()
        { 

            return LocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplications();

        }

        static public DataTable GetFullInfo()
        {

            return LocalDrivingLicenseApplicationsData.GetFullInfo();

        }

    }

}

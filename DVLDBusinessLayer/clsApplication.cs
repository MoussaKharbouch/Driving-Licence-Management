using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsApplication
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }

        public enum enStatus { New = 1, Canceled = 2, Completed = 3}
        public enStatus ApplicationStatus;

        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public clsApplication()
        {

            ApplicationID = -1;
            ApplicantPersonID = -1;

            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;

            ApplicationStatus = enStatus.New;

            LastStatusDate = DateTime.Now;
            PaidFees = -1;

            CreatedByUserID = -1;

            Mode = enMode.Add;

        }

        public clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
                              enStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {

            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;

            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;

            this.ApplicationStatus = ApplicationStatus;

            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;

            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;

        }
        
        public static clsApplication FindApplication(int ApplicationID)
        {

            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            short ApplicationStatus = -1;
            DateTime LastStatusDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;

            ApplicationsData.FindApplication(ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                                             ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate,
                                             ref PaidFees, ref CreatedByUserID);

            if (ApplicantPersonID == -1)
                return null;

            return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                                      ApplicationTypeID, (enStatus)ApplicationStatus, LastStatusDate,
                                      PaidFees, CreatedByUserID);

        }

        private bool Add()
        {

            int ApplicationID = this.ApplicationID;

            bool succeeded = ApplicationsData.AddApplication(ref ApplicationID, ApplicantPersonID, ApplicationDate,
                                                             ApplicationTypeID, (short)ApplicationStatus, LastStatusDate,
                                                             PaidFees, CreatedByUserID);

            this.ApplicationID = ApplicationID;

            return succeeded;

        }

        private bool Update()
        {

            return ApplicationsData.UpdateApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                                                      ApplicationTypeID, (short)ApplicationStatus, LastStatusDate,
                                                      PaidFees, CreatedByUserID);

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

        public static bool DeleteApplication(int ApplicationID)
        {

            return ApplicationsData.DeleteApplication(ApplicationID);

        }

        public static bool DoesApplicationExist(int ApplicationID)
        {

            return ApplicationsData.DoesApplicationExist(ApplicationID);

        }

        static public DataTable GetApplications()
        { 

            return ApplicationsData.GetApplications();

        }

    }

}

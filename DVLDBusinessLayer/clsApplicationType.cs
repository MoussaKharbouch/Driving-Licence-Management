using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsApplicationType
    {

        public int ApplicationTypeID { get; set; }

        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        public clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {

            this.ApplicationTypeID = ApplicationTypeID;

            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;

        }

        public static clsApplicationType FindApplicationType(int ApplicationTypeID)
        {

            string ApplicationTypeTitle = string.Empty;
            decimal ApplicationFees = 0;

            ApplicationTypesData.FindApplicationType(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees);

            if (ApplicationTypeTitle == string.Empty)
                return null;

            return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);

        }

        public static bool DoesApplicationTypeTitleExist(string ApplicationTypeTitle)
        {

            return ApplicationTypesData.DoesApplicationTypeTitleExist(ApplicationTypeTitle);

        }

        public bool Update()
        {

            return ApplicationTypesData.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);

        }

        static public DataTable GetApplicationTypes()
        {

            return ApplicationTypesData.GetApplicationTypes();

        }

    }

}

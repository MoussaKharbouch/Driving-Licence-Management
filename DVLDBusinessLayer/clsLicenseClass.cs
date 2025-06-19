using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    class clsLicenseClass
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int LicenseClassID { get; set; }

        public string ClassName { get; set; }
        public string ClassDescription { get; set; }

        public short MinimumAllowedAge { get; set; }
        public short DefaultValidityLength { get; set; }

        public decimal ClassFees { get; set; }

        public clsLicenseClass()
        {

            LicenseClassID = -1;

            ClassName = string.Empty;
            ClassDescription = string.Empty;

            MinimumAllowedAge = -1;
            DefaultValidityLength = -1;

            ClassFees = -1;

            Mode = enMode.Add;

        }

        public clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
                               short MinimumAllowedAge, short DefaultValidityLength, decimal ClassFees)
        {

            this.LicenseClassID = LicenseClassID;

            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;

            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;

            this.ClassFees = ClassFees;

            Mode = enMode.Update;

        }
        
        public static clsLicenseClass FindLicenseClass(int LicenseClassID)
        {

            string ClassName = string.Empty;
            string ClassDescription = string.Empty;
            short MinimumAllowedAge = -1;
            short DefaultValidityLength = -1;
            decimal ClassFees = -1;

            LicenseClassesData.FindLicenseClass(LicenseClassID, ref ClassName, ref ClassDescription,
                                                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees);

            if (MinimumAllowedAge == -1)
                return null;

            return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
                                       MinimumAllowedAge, DefaultValidityLength, ClassFees);

        }

        private bool Add()
        {

            int LicenseClassID = this.LicenseClassID;

            bool succeeded = LicenseClassesData.AddLicenseClass(ref LicenseClassID, ClassName, ClassDescription,
                                                                MinimumAllowedAge, DefaultValidityLength, ClassFees);

            this.LicenseClassID = LicenseClassID;

            return succeeded;

        }

        private bool Update()
        {

            return LicenseClassesData.UpdateLicenseClass(LicenseClassID, ClassName, ClassDescription,
                                                         MinimumAllowedAge, DefaultValidityLength, ClassFees);

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

        public static bool DeleteLicenseClass(int LicenseClassID)
        {

            return LicenseClassesData.DeleteLicenseClass(LicenseClassID);

        }

        public static bool DoesLicenseClassExist(int LicenseClassID)
        {

            return LicenseClassesData.DoesLicenseClassExist(LicenseClassID);

        }

        static public DataTable GetLicenseClasses()
        { 

            return LicenseClassesData.GetLicenseClasses();

        }

    }

}
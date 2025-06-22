using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsTestType
    {

        public int TestTypeID { get; set; }

        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {

            this.TestTypeID = TestTypeID;

            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

        }

        public static clsTestType FindTestType(int TestTypeID)
        {

            string TestTypeTitle = string.Empty;
            string TestTypeDescription = string.Empty;
            decimal TestTypeFees = 0;

            TestTypesData.FindTestType(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);

            if (TestTypeTitle == string.Empty)
                return null;

            return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

        }

        public static bool DoesTestTypeTitleExist(string TestTypeTitle)
        {

            return TestTypesData.DoesTestTypeTitleExist(TestTypeTitle);

        }

        public bool Update()
        {

            if (TestTypesData.DoesTestTypeTitleExist(TestTypeTitle, TestTypeID))
                return false;

            return TestTypesData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

        }

        static public DataTable GetTestTypes()
        {

            return TestTypesData.GetTestTypes();

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsTest
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public bool TestResult { get; set; }
        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }

        public clsTest()
        {

            TestID = -1;
            TestAppointmentID = -1;

            TestResult = false;
            Notes = string.Empty;

            CreatedByUserID = -1;

            Mode = enMode.Add;

        }

        public clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;

            this.TestResult = TestResult;
            this.Notes = Notes;

            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;

        }

        public static clsTest FindTest(int TestID)
        {

            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            TestsData.FindTest(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);

            if (TestAppointmentID == -1)
                return null;

            return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

        }

        private bool Add()
        {

            int TestID = this.TestID;

            bool succeeded = TestsData.AddTest(ref TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

            this.TestID = TestID;

            return succeeded;

        }

        private bool Update()
        {

            return TestsData.UpdateTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

        }

        public bool Save()
        {

            bool succeeded = false;

            switch (Mode)
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

        public static bool DeleteTest(int TestID)
        {

            if (!TestsData.DoesTestExist(TestID))
                return false;

            return TestsData.DeleteTest(TestID);

        }

        public static bool DoesTestExist(int TestID)
        {

            return TestsData.DoesTestExist(TestID);

        }

        public static DataTable GetTests()
        {

            return TestsData.GetTests();

        }

    }

}

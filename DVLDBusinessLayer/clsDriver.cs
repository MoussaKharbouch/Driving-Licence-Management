using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsDriver
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int DriverID { get; set; }

        private int _PersonID;
        public int PersonID
        {

            get
            {

                return _PersonID;

            }
            set
            {

                _PersonID = value;
                Person = clsPerson.FindPerson(_PersonID);

                if (Person == null)
                    Person = new clsPerson();

            }

        }

        public clsPerson Person { get; set; }

        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {

            DriverID = -1;
            _PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;

            Mode = enMode.Add;

        }

        public clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;

            Mode = enMode.Update;

        }

        public static clsDriver FindDriver(int DriverID)
        {

            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (!DriversData.FindDriver(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return null;

            return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);

        }

        public static clsDriver FindDriverByPersonID(int PersonID)
        {

            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (!DriversData.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return null;

            return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);

        }

        private bool Add()
        {

            int DriverID = this.DriverID;

            bool succeeded = DriversData.AddDriver(ref DriverID, PersonID, CreatedByUserID, CreatedDate);

            this.DriverID = DriverID;

            return succeeded;

        }

        private bool Update()
        {

            return DriversData.UpdateDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);

        }

        public bool Save()
        {

            bool succeeded = false;

            switch (Mode)
            {

                case enMode.Add:
                    succeeded = Add();

                    if(succeeded)
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

        public static bool DeleteDriver(int DriverID)
        {

            if (!DoesDriverExist(DriverID))
                return false;

            return DriversData.DeleteDriver(DriverID);

        }

        public static bool DoesDriverExist(int DriverID)
        {

            return DriversData.DoesDriverExist(DriverID);

        }

        public static DataTable GetDrivers()
        {

            return DriversData.GetDrivers();

        }

        public static DataTable GetDriversMainInfo()
        {

            return DriversData.GetDriversMainInfo();

        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsPerson
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public enum enGender { Male, Female }
        public enGender Gender;

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public string ImagePath { get; set; }

        public clsPerson()
        {

            PersonID = -1;
            NationalNo = string.Empty;

            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;

            DateOfBirth = DateTime.Now;
            Gender = enGender.Male;

            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            NationalityCountryID = -1;

            ImagePath = string.Empty;

            Mode = enMode.Add;

        }

        public clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName,
                      string ThirdName, string LastName, DateTime DateOfBirth, enGender Gender,
                      string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {

            this.PersonID = PersonID;
            this.NationalNo = NationalNo;

            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;

            this.DateOfBirth = DateOfBirth;

            this.Gender = Gender;

            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;

            this.ImagePath = ImagePath;

            Mode = enMode.Update;

        }

        public string FullName()
        {

            return (FirstName + " " + SecondName + " " + ThirdName + " " + LastName).Trim();

        } 
        
        public static clsPerson FindPerson(int PersonID)
        {

            string NationalNo = string.Empty;
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = string.Empty;
            string LastName = string.Empty;
            DateTime DateOfBirth = DateTime.Now;
            int Gender = -1;
            string Address = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int NationalityCountryID = -1;
            string ImagePath = string.Empty;

            PeopleData.FindPerson(PersonID, ref NationalNo, ref FirstName,
                                     ref SecondName, ref ThirdName, ref LastName,
                                     ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                     ref Email, ref NationalityCountryID, ref ImagePath);

            if (NationalNo == string.Empty)
                return null;

            return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                                     DateOfBirth, (enGender)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        public static clsPerson FindPerson(string NationalNo)
        {

            int PersonID = -1;
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = string.Empty;
            string LastName = string.Empty;
            DateTime DateOfBirth = DateTime.Now;
            int Gender = -1;
            string Address = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int NationalityCountryID = -1;
            string ImagePath = string.Empty;

            PeopleData.FindPerson(NationalNo, ref PersonID, ref FirstName,
                                     ref SecondName, ref ThirdName, ref LastName,
                                     ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                     ref Email, ref NationalityCountryID, ref ImagePath);

            if (PersonID == -1)
                return null;

            return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                              DateOfBirth, (enGender)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        private bool Add()
        {

            if (PeopleData.DoesNationalNoExist(NationalNo))
                return false;

            int PersonID = this.PersonID;

            bool succeeded = PeopleData.AddPerson(ref PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                        DateOfBirth, (int)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

            this.PersonID = PersonID;

            return succeeded;

        }

        private bool Update()
        {

            if (PeopleData.DoesNationalNoExist(NationalNo, PersonID))
                return false;

            return PeopleData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                           DateOfBirth, (int)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        public bool Save()
        {

            bool succeeded = false;

            switch(Mode)
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

        public static bool DeletePerson(int PersonID)
        {

            if (!DoesPersonExist(PersonID))
                return false;

            return PeopleData.DeletePerson(PersonID);

        }

        public static bool DoesPersonExist(int PersonID)
        {

            return PeopleData.DoesPersonExist(PersonID);

        }

        public static bool DoesNationalNoExist(string NationalNo)
        {

            return PeopleData.DoesNationalNoExist(NationalNo);

        }

        static public DataTable GetPeople()
        { 

            return PeopleData.GetPeople();

        }

        static public DataTable GetPeopleMainInfo()
        {

            return PeopleData.GetPeopleMainInfo();

        }

    }

}

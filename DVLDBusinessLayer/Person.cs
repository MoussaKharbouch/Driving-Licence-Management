using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class Person
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

        public Person()
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

        public Person(int PersonID, string NationalNo, string FirstName, string SecondName,
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

            return (FirstName + " " + SecondName + " " + ThirdName + " " + LastName);

        } 
        public static Person FindPerson(int PersonID)
        {

            Person person = new Person();

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

            PeopleData.GetPerson(PersonID, ref NationalNo, ref FirstName,
                                     ref SecondName, ref ThirdName, ref LastName,
                                     ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                     ref Email, ref NationalityCountryID, ref ImagePath);

            if (NationalNo == string.Empty)
                return null;

            return new Person(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                                     DateOfBirth, (enGender)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        public static Person FindPerson(string NationalNo)
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

            PeopleData.GetPerson(NationalNo, ref PersonID, ref FirstName,
                                     ref SecondName, ref ThirdName, ref LastName,
                                     ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                     ref Email, ref NationalityCountryID, ref ImagePath);

            if (PersonID == -1)
                return null;

            return new Person(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                              DateOfBirth, (enGender)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        private bool Add()
        {

            return PeopleData.AddPerson(NationalNo, FirstName, SecondName, ThirdName, LastName,
                                        DateOfBirth, (int)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        private bool Update()
        {

            return PeopleData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                           DateOfBirth, (int)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        public bool Save()
        {

            bool successed = false;

            switch(Mode)
            {

                case enMode.Add:
                    successed = Add();
                    Mode = enMode.Update;
                    break;

                case enMode.Update:
                    successed = Update();
                    break;

                default:
                    break;

            }

            return successed;

        }

        public static bool DeletePerson(int PersonID)
        {

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

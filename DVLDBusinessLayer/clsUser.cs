using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsUser
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public clsUser()
        {

            UserID = -1;
            PersonID = -1;

            Username = string.Empty;
            Password = string.Empty;
            IsActive = false;

            Mode = enMode.Add;

        }

        public clsUser(int UserID, int PersonID, string Username, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;

            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;

        }
        
        public static clsUser FindUser(string Username, string Password)
        {

            clsUser User = new clsUser();

            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            UsersData.FindUser(Username, Password, ref UserID, ref PersonID, ref IsActive);

            if (UserID == -1)
                return null;

            return new clsUser(UserID, PersonID, Username, Password, IsActive);

        }

        public static clsUser FindUser(int UserID)
        {

            clsUser User = new clsUser();

            int PersonID = -1;
            string Username = string.Empty;
            string Password = string.Empty;

            bool IsActive = false;

            UsersData.FindUser(UserID, ref PersonID, ref Username, ref Password, ref IsActive);

            if (PersonID == -1)
                return null;

            return new clsUser(UserID, PersonID, Username, Password, IsActive);

        }

        private bool Add()
        {

            int UserID = this.UserID;

            bool succeeded = UsersData.AddUser(ref UserID, PersonID, Username, Password, IsActive);

            this.UserID = UserID;

            return succeeded;

        }

        private bool Update()
        {

            return UsersData.UpdateUser(UserID, PersonID, Username, Password, IsActive);

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

        public static bool DeleteUser(int UserID)
        {

            return UsersData.DeleteUser(UserID);

        }

        public static bool DoesUserExist(string Username, string Password)
        {

            return UsersData.DoesUserExist(Username, Password);

        }

        public static bool DoesPersonUse(int PersonID)
        {

            return UsersData.DoesPersonUse(PersonID);

        }

        public static bool DoesUsernameExist(string Username)
        {

            return UsersData.DoesUsernameExist(Username);

        }

        static public DataTable GetUsers()
        {

            return UsersData.GetUsers();

        }

        static public DataTable GetUsersMainInfo()
        {

            return UsersData.GetUsersMainInfo();

        }

    }

}

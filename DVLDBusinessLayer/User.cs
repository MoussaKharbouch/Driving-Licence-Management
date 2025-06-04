using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class User
    {

        enum enMode { Add, Update }
        enMode Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public User()
        {

            UserID = -1;
            PersonID = -1;

            Username = string.Empty;
            Password = string.Empty;
            IsActive = false;

            Mode = enMode.Add;

        }

        public User(int UserID, int PersonID, string Username, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;

            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;

        }
        
        public static User FindUser(string Username, string Password)
        {

            User User = new User();

            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            UsersData.GetUser(Username, Password, ref UserID, ref PersonID, ref IsActive);

            if (UserID == -1)
                return null;

            return new User(UserID, PersonID, Username, Password, IsActive);

        }

        public static User FindUser(int UserID)
        {

            User User = new User();

            int PersonID = -1;
            string Username = string.Empty;
            string Password = string.Empty;

            bool IsActive = false;

            UsersData.GetUser(UserID, ref PersonID, ref Username, ref Password, ref IsActive);

            if (UserID == -1)
                return null;

            return new User(UserID, PersonID, Username, Password, IsActive);

        }

        private bool Add()
        {

            int UserID = this.UserID;

            bool succedded = UsersData.AddUser(ref UserID, PersonID, Username, Password, IsActive);

            this.UserID = UserID;

            return succedded;

        }

        private bool Update()
        {

            return UsersData.UpdateUser(UserID, PersonID, Username, Password, IsActive);

        }

        public bool Save()
        {

            bool succedded = false;

            switch(Mode)
            {

                case enMode.Add:
                    succedded = Add();
                    Mode = enMode.Update;
                    break;

                case enMode.Update:
                    succedded = Update();
                    break;

                default:
                    break;

            }

            return succedded;

        }

        public static bool DeleteUser(int UserID)
        {

            return UsersData.DeleteUser(UserID);

        }

        public static bool DoesUserExist(string Username, string Password)
        {

            return UsersData.DoesUserExist(Username, Password);

        }

        public static bool DoesUsernameExist(string Username)
        {

            return UsersData.DoesUsernameExist(Username);

        }

        static public DataTable GetUsers()
        {

            return UsersData.GetUsers();

        }

        static public DataTable GetUsersWithFullName()
        {

            return UsersData.GetUsersWithFullName();

        }

    }

}

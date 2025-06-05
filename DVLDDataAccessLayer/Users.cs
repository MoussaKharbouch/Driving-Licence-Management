using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class UsersData
    {

        public static void GetUser(string Username, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM USERS WHERE Username = @Username AND Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

        public static void GetUser(int UserID, ref int PersonID, ref string Username, ref string Password, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM USERS WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    Username = reader["PersonID"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

        public static bool DoesUserExist(string Username, string Password)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM Users WHERE Username = @Username AND Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool DoesUsernameExist(string Username)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM Users WHERE Username = @Username";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", Username);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool AddUser(ref int UserID, int PersonID, string Username, string Password, bool IsActive)
        {

            UserID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Users]
                                ([PersonID]
                                ,[Username]
                                ,[Password]
                                ,[IsActive])
                            VALUES
                                (@PersonID
                                ,@Username
                                ,@Password
                                ,@IsActive);
                            SELECT SCOPE_IDENTITY() as UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                int.TryParse(result.ToString(), out UserID);

            }
            finally
            {
                connection.Close();
            }

            return (UserID != -1);

        }

        public static bool UpdateUser(int UserID, int PersonID, string Username, string Password,  bool IsActive)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Users]
                             SET [PersonID] = @PersonID
                                 ,[Username] = @Username
                                 ,[Password] = @Password
                                 ,[IsActive] = @IsActive
                              WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[Users]
                             WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static DataTable GetUsers()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Users = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Users.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Users;

        }

        public static DataTable GetUsersWithFullName()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Users.UserID, Users.PersonID,
                             LTRIM(RTRIM(
                                CONCAT(
                                    People.FirstName + ' ',
                                    ISNULL(NULLIF(People.SecondName, ''), '') + ' ',
                                    ISNULL(NULLIF(People.ThirdName, ''), '') + ' ',
                                    People.LastName
                                )
                              )) AS FullName, Users.Username, Users.Password, Users.IsActive
                              FROM Users INNER JOIN People ON Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Users = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Users.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Users;

        }

    }

}

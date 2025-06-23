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
    public static class PeopleData
    {

        public static void FindPerson(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
                                     ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gender,
                                     ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = (reader["SecondName"] != DBNull.Value ? reader["SecondName"].ToString() : string.Empty);
                    ThirdName = (reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : string.Empty);
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = int.Parse(reader["Gender"].ToString());
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    NationalityCountryID = int.Parse(reader["NationalityCountryID"].ToString());
                    ImagePath = (reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty);

                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

        public static void FindPerson(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
                                     ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gender,
                                     ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = int.Parse(reader["PersonID"].ToString());
                    FirstName = reader["FirstName"].ToString();
                    SecondName = (reader["SecondName"] != DBNull.Value ? reader["SecondName"].ToString() : string.Empty);
                    ThirdName = (reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : string.Empty);
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = int.Parse(reader["Gender"].ToString());
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    NationalityCountryID = int.Parse(reader["NationalityCountryID"].ToString());
                    ImagePath = (reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty);

                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

        public static bool DoesPersonExist(int PersonID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool DoesNationalNoExist(string NationalNo)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static bool DoesNationalNoExist(string NationalNo, int ExcludedPersonID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM People WHERE NationalNo = @NationalNo AND PersonID != @ExcludedPersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@ExcludedPersonID", ExcludedPersonID);

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


        public static bool AddPerson(ref int PersonID, string NationalNo, string FirstName, string SecondName,
                                     string ThirdName, string LastName, DateTime DateOfBirth, int Gender,
                                     string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {

            PersonID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[People]
                                ([NationalNo]
                                ,[FirstName]
                                ,[SecondName]
                                ,[ThirdName]
                                ,[LastName]
                                ,[DateOfBirth]
                                ,[Gender]
                                ,[Address]
                                ,[Phone]
                                ,[Email]
                                ,[NationalityCountryID]
                                ,[ImagePath])
                            VALUES
                                (@NationalNo
                                 ,@FirstName
                                 ,@SecondName
                                 ,@ThirdName
                                 ,@LastName
                                 ,@DateOfBirth
                                 ,@Gender
                                 ,@Address
                                 ,@Phone
                                 ,@Email
                                 ,@NationalityCountryID
                                 ,@ImagePath);
                            SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", (SecondName != string.Empty ? (object)SecondName : DBNull.Value));
            command.Parameters.AddWithValue("@ThirdName", (ThirdName != string.Empty ? (object)ThirdName : DBNull.Value));
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", (ImagePath != string.Empty ? (object)ImagePath : DBNull.Value));

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    int.TryParse(result.ToString(), out PersonID);

            }
            finally
            {
                connection.Close();
            }

            return (PersonID != -1);

        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName,
                                        string ThirdName, string LastName, DateTime DateOfBirth, int Gender,
                                        string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[People]
                             SET [NationalNo] = @NationalNo
                                 ,[FirstName] = @FirstName
                                 ,[SecondName] = @SecondName
                                 ,[ThirdName] = @ThirdName
                                 ,[LastName] = @LastName
                                 ,[DateOfBirth] = @DateOfBirth
                                 ,[Gender] = @Gender
                                 ,[Address] = @Address
                                 ,[Phone] = @Phone
                                 ,[Email] = @Email
                                 ,[NationalityCountryID] = @NationalityCountryID
                                 ,[ImagePath] = @ImagePath
                              WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", (SecondName != string.Empty ? (object)SecondName : DBNull.Value));
            command.Parameters.AddWithValue("@ThirdName", (ThirdName != string.Empty ? (object)ThirdName : DBNull.Value));
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", (ImagePath != string.Empty ? (object)ImagePath : DBNull.Value));

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

        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[People]
                             WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetPeople()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable People = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    People.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return People;

        }

        public static DataTable GetPeopleMainInfo()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                                    People.ThirdName, People.LastName, People.DateOfBirth,
                                    case People.Gender when 0 then 'Male' when 1 then 'Female' else 'Elien' end as Gender,
                                    Countries.CountryName as Nationality, People.Phone, People.Email
                                    FROM People join Countries on People.NationalityCountryID = Countries.CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable People = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    People.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return People;

        }

    }

}

using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class InternationalLicensesData
    {

        public static bool FindLicense(int LicenseID, ref int ApplicationID, ref int DriverID,
                                       ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
                                       ref bool IsActive, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    ApplicationID = int.Parse(reader["ApplicationID"].ToString());
                    DriverID = int.Parse(reader["DriverID"].ToString());
                    IssuedUsingLocalLicenseID = int.Parse(reader["IssuedUsingLocalLicenseID"].ToString());
                    IssueDate = DateTime.Parse(reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(reader["ExpirationDate"].ToString());
                    IsActive = bool.Parse(reader["IsActive"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());

                    isFound = true;

                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool FindLicenseByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID,
                                                      ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
                                                      ref bool IsActive, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool isFound = false;

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    LicenseID = int.Parse(reader["LicenseID"].ToString());
                    DriverID = int.Parse(reader["DriverID"].ToString());
                    IssuedUsingLocalLicenseID = int.Parse(reader["IssuedUsingLocalLicenseID"].ToString());
                    IssueDate = DateTime.Parse(reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(reader["ExpirationDate"].ToString());
                    IsActive = bool.Parse(reader["IsActive"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());

                    isFound = true;

                    reader.Close();
                }

            }
            finally
            {

                connection.Close();

            }

            return isFound;

        }

        public static bool DoesLicenseExist(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM InternationalLicenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool AddLicense(ref int LicenseID, int ApplicationID, int DriverID,
                                      int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
                                      bool IsActive, int CreatedByUserID)
        {

            LicenseID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[InternationalLicenses]
                            ([ApplicationID]
                            ,[DriverID]
                            ,[IssuedUsingLocalLicenseID]
                            ,[IssueDate]
                            ,[ExpirationDate]
                            ,[IsActive]
                            ,[CreatedByUserID])
                            VALUES
                            (@ApplicationID
                            ,@DriverID
                            ,@IssuedUsingLocalLicenseID
                            ,@IssueDate
                            ,@ExpirationDate
                            ,@IsActive
                            ,@CreatedByUserID);
                            SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    int.TryParse(result.ToString(), out LicenseID);

            }
            finally
            {
                connection.Close();
            }

            return (LicenseID != -1);

        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID,
                                         int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
                                         bool IsActive, int CreatedByUserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[InternationalLicenses]
                             SET [ApplicationID] = @ApplicationID
                                ,[DriverID] = @DriverID
                                ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
                                ,[IssueDate] = @IssueDate
                                ,[ExpirationDate] = @ExpirationDate
                                ,[IsActive] = @IsActive
                                ,[CreatedByUserID] = @CreatedByUserID
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool DeleteLicense(int LicenseID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[InternationalLicenses]
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static DataTable GetLicenses()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Licenses = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Licenses.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Licenses;

        }

        public static DataTable GetDriverInternationalLicensesHistory(int DriverID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            DataTable Licenses = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Licenses.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Licenses;

        }

        public static DataTable GetLicensesMainInfo()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenseID
                                    ,ApplicationID
                                    ,DriverID
                                    ,IssuedUsingLocalLicenseID
                                    ,IssueDate
                                    ,ExpirationDate
                                    ,CASE WHEN IsActive = 1 THEN 'True' ELSE 'False' END AS IsActive
                             FROM InternationalLicenses";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Licenses = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Licenses.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Licenses;

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class LicensesData
    {

        public static bool FindLicense(int LicenseID, ref int ApplicationID, ref int DriverID,
                                       ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate,
                                       ref string Notes, ref decimal PaidFees, ref bool IsActive,
                                       ref short IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

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
                    LicenseClass = int.Parse(reader["LicenseClass"].ToString());
                    IssueDate = DateTime.Parse(reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(reader["ExpirationDate"].ToString());
                    Notes = (reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty);
                    PaidFees = decimal.Parse(reader["PaidFees"].ToString());
                    IsActive = bool.Parse(reader["IsActive"].ToString());
                    IssueReason = short.Parse(reader["IssueReason"].ToString());
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

            string query = "SELECT 1 AS FOUND FROM Licenses WHERE LicenseID = @LicenseID";

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

        public static bool HasLicenseInSameLicenseClass(int DriverID, int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT 1 as Found FROM Licenses
                             WHERE DriverID = @DriverID AND LicenseClass = @LicenseClass";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

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
                                      int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
                                      string Notes, decimal PaidFees, bool IsActive,
                                      short IssueReason, int CreatedByUserID)
        {

            LicenseID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Licenses]
                            ([ApplicationID]
                            ,[DriverID]
                            ,[LicenseClass]
                            ,[IssueDate]
                            ,[ExpirationDate]
                            ,[Notes]
                            ,[PaidFees]
                            ,[IsActive]
                            ,[IssueReason]
                            ,[CreatedByUserID])
                            VALUES
                            (@ApplicationID
                            ,@DriverID
                            ,@LicenseClass
                            ,@IssueDate
                            ,@ExpirationDate
                            ,@Notes
                            ,@PaidFees
                            ,@IsActive
                            ,@IssueReason
                            ,@CreatedByUserID);
                            SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty ? (object)Notes : DBNull.Value));
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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
                                         int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
                                         string Notes, decimal PaidFees, bool IsActive,
                                         short IssueReason, int CreatedByUserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Licenses]
                             SET [ApplicationID] = @ApplicationID
                                ,[DriverID] = @DriverID
                                ,[LicenseClass] = @LicenseClass
                                ,[IssueDate] = @IssueDate
                                ,[ExpirationDate] = @ExpirationDate
                                ,[Notes] = @Notes
                                ,[PaidFees] = @PaidFees
                                ,[IsActive] = @IsActive
                                ,[IssueReason] = @IssueReason
                                ,[CreatedByUserID] = @CreatedByUserID
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty ? (object)Notes : DBNull.Value));
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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

            string query = @"DELETE FROM [dbo].[Licenses]
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

            string query = "SELECT * FROM Licenses";

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

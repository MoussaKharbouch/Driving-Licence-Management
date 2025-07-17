using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class DetainedLicensesData
    {

        public static bool FindDetainedLicense(int DetainID, ref int LicenseID, ref DateTime DetainDate,
                                               ref decimal FineFees, ref int CreatedByUserID,
                                               ref bool IsReleased, ref DateTime ReleaseDate,
                                               ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    LicenseID = int.Parse(reader["LicenseID"].ToString());
                    DetainDate = DateTime.Parse(reader["DetainDate"].ToString());
                    FineFees = decimal.Parse(reader["FineFees"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());
                    IsReleased = bool.Parse(reader["IsReleased"].ToString());
                    ReleaseDate = (reader["ReleaseDate"] != DBNull.Value ? DateTime.Parse(reader["ReleaseDate"].ToString()) : DateTime.MinValue);
                    ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value ? int.Parse(reader["ReleasedByUserID"].ToString()) : -1);
                    ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value ? int.Parse(reader["ReleaseApplicationID"].ToString()) : -1);

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

        public static bool FindDetainedLicenseByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,
                                                          ref decimal FineFees, ref int CreatedByUserID,
                                                          ref bool IsReleased, ref DateTime ReleaseDate,
                                                          ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT TOP 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    DetainID = int.Parse(reader["DetainID"].ToString());
                    DetainDate = DateTime.Parse(reader["DetainDate"].ToString());
                    FineFees = decimal.Parse(reader["FineFees"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());
                    IsReleased = bool.Parse(reader["IsReleased"].ToString());
                    ReleaseDate = (reader["ReleaseDate"] != DBNull.Value ? DateTime.Parse(reader["ReleaseDate"].ToString()) : DateTime.MinValue);
                    ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value ? int.Parse(reader["ReleasedByUserID"].ToString()) : -1);
                    ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value ? int.Parse(reader["ReleaseApplicationID"].ToString()) : -1);

                    isFound = true;
                }

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool AddDetainedLicense(ref int DetainID, int LicenseID, DateTime DetainDate,
                                              decimal FineFees, int CreatedByUserID,
                                              bool IsReleased, DateTime ReleaseDate,
                                              int ReleasedByUserID, int ReleaseApplicationID)
        {

            DetainID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses
                            (LicenseID, DetainDate, FineFees, CreatedByUserID,
                             IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
                            VALUES
                            (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID,
                             @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", (ReleaseDate != DateTime.MinValue ? (object)ReleaseDate : DBNull.Value));
            command.Parameters.AddWithValue("@ReleasedByUserID", (ReleasedByUserID != -1 ? (object)ReleasedByUserID : DBNull.Value));
            command.Parameters.AddWithValue("@ReleaseApplicationID", (ReleaseApplicationID != -1 ? (object)ReleaseApplicationID : DBNull.Value));

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    int.TryParse(result.ToString(), out DetainID);

            }
            finally
            {
                connection.Close();
            }

            return (DetainID != -1);

        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
                                                 decimal FineFees, int CreatedByUserID,
                                                 bool IsReleased, DateTime ReleaseDate,
                                                 int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                             SET LicenseID = @LicenseID,
                                 DetainDate = @DetainDate,
                                 FineFees = @FineFees,
                                 CreatedByUserID = @CreatedByUserID,
                                 IsReleased = @IsReleased,
                                 ReleaseDate = @ReleaseDate,
                                 ReleasedByUserID = @ReleasedByUserID,
                                 ReleaseApplicationID = @ReleaseApplicationID
                             WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", (ReleaseDate != DateTime.MinValue ? (object)ReleaseDate : DBNull.Value));
            command.Parameters.AddWithValue("@ReleasedByUserID", (ReleasedByUserID != -1 ? (object)ReleasedByUserID : DBNull.Value));
            command.Parameters.AddWithValue("@ReleaseApplicationID", (ReleaseApplicationID != -1 ? (object)ReleaseApplicationID : DBNull.Value));

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

        public static bool IsDetained(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

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


        public static bool DeleteDetainedLicense(int DetainID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "DELETE FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

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

        public static bool DoesDetainedLicenseExist(int DetainID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

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

        public static DataTable GetAllDetainedLicenses()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable DetainedLicenses = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    DetainedLicenses.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return DetainedLicenses;

        }

        public static DataTable GetDetainedLicensesMainInfo()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT DetainedLicenses.DetainID as [Detain ID], 
                             DetainedLicenses.LicenseID as [License ID], 
                             CASE 
                                 WHEN DetainedLicenses.IsReleased = 1 THEN 'Yes' 
                                 ELSE 'No' 
                             END as [Is Released], 
                             DetainedLicenses.FineFees, 
                             DetainedLicenses.ReleaseDate, 
                             People.NationalNo as [National No], 
                             LTRIM(RTRIM(
                                 People.FirstName + 
                                 ISNULL(' ' + People.SecondName, '') + 
                                 ISNULL(' ' + People.ThirdName, '') + 
                                 ISNULL(' ' + People.LastName, '')
                             )) AS [Full Name], 
                             DetainedLicenses.ReleaseApplicationID
                             FROM DetainedLicenses
                             JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID
                             JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                             JOIN People ON Drivers.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            DataTable DetainedLicenses = new DataTable();

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    DetainedLicenses.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return DetainedLicenses;

        }

    }

}

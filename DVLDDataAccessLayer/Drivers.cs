using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class DriversData
    {

        public static bool FindDriver(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = int.Parse(reader["PersonID"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());
                    CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());

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

        public static bool FindDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            bool isFound = false;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = int.Parse(reader["PersonID"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());
                    CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());

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

        public static bool DoesDriverExist(int DriverID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static bool AddDriver(ref int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            DriverID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Drivers]
                             ([PersonID]
                             ,[CreatedByUserID]
                             ,[CreatedDate])
                             VALUES
                             (@PersonID
                             ,@CreatedByUserID
                             ,@CreatedDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    int.TryParse(result.ToString(), out DriverID);

            }
            finally
            {
                connection.Close();
            }

            return (DriverID != -1);

        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Drivers]
                             SET [PersonID] = @PersonID
                                ,[CreatedByUserID] = @CreatedByUserID
                                ,[CreatedDate] = @CreatedDate
                             WHERE [DriverID] = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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

        public static bool DeleteDriver(int DriverID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[Drivers]
                             WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static DataTable GetDrivers()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Drivers = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Drivers.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Drivers;

        }

    }

}
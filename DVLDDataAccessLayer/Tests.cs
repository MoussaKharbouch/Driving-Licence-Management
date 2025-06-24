using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class TestsData
    {

        public static void FindTest(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : string.Empty);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

        public static bool DoesTestExist(int TestID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT 1 AS FOUND FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

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

        public static bool AddTest(ref int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            TestID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Tests]
                             ([TestAppointmentID]
                             ,[TestResult]
                             ,[Notes]
                             ,[CreatedByUserID])
                             VALUES
                             (@TestAppointmentID
                              ,@TestResult
                              ,@Notes
                              ,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty ? (object)Notes : DBNull.Value));
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    int.TryParse(result.ToString(), out TestID);

            }
            finally
            {
                connection.Close();
            }

            return (TestID != -1);

        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Tests]
                             SET [TestAppointmentID] = @TestAppointmentID
                                ,[TestResult] = @TestResult
                                ,[Notes] = @Notes
                                ,[CreatedByUserID] = @CreatedByUserID
                              WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty ? (object)Notes : DBNull.Value));
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

        public static bool DeleteTest(int TestID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM [dbo].[Tests]
                             WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

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

        public static DataTable GetTests()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Tests = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Tests.Load(reader);

                reader.Close();

            }
            finally
            {
                connection.Close();
            }

            return Tests;

        }

    }

}

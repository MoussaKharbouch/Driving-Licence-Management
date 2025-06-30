using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

	public static class TestAppointmentsData
	{

		public static void FindTestAppointment(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,
											   ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{

					TestTypeID = (int)reader["TestTypeID"];
					LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
					AppointmentDate = (DateTime)reader["AppointmentDate"];
					PaidFees = (decimal)reader["PaidFees"];
					CreatedByUserID = (int)reader["CreatedByUserID"];
					IsLocked = (bool)reader["IsLocked"];
					RetakeTestApplicationID = (reader["RetakeTestApplicationID"] != DBNull.Value ? (int)reader["RetakeTestApplicationID"] : -1);

					reader.Close();

				}

			}
			finally
			{
				connection.Close();
			}

		}

		public static bool DoesTestAppointmentExist(int TestAppointmentID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT 1 AS FOUND FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

		public static bool HasActiveAppointmentInTestType(int ApplicantPersonID, int TestTypeID, string DrivingClassName)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"SELECT 1 FROM TestAppointments JOIN LocalDrivingLicenseApplications
							 ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
							 Join Applications
							 On LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
							 Join LicenseClasses
							 On LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
							 WHERE Applications.ApplicantPersonID = @ApplicantPersonID
							 AND TestAppointments.IsLocked = 0
							 AND TestAppointments.TestTypeID = @TestTypeID
							 And LicenseClasses.ClassName = @ClassName";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
			command.Parameters.AddWithValue("@ClassName", DrivingClassName);

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

		public static bool AddTestAppointment(ref int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
											  decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
		{

			TestAppointmentID = -1;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"INSERT INTO [dbo].[TestAppointments]
							 ([TestTypeID]
							 ,[LocalDrivingLicenseApplicationID]
							 ,[AppointmentDate]
							 ,[PaidFees]
							 ,[CreatedByUserID]
							 ,[IsLocked]
							 ,[RetakeTestApplicationID])
							 VALUES
							 (@TestTypeID
							  ,@LocalDrivingLicenseApplicationID
							  ,@AppointmentDate
							  ,@PaidFees
							  ,@CreatedByUserID
							  ,@IsLocked
							  ,@RetakeTestApplicationID);
							 SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
			command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
			command.Parameters.AddWithValue("@PaidFees", PaidFees);
			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			command.Parameters.AddWithValue("@IsLocked", IsLocked);
			command.Parameters.AddWithValue("@RetakeTestApplicationID", (RetakeTestApplicationID != -1 ? (object)RetakeTestApplicationID : DBNull.Value));

			try
			{

				connection.Open();

				object result = command.ExecuteScalar();

				if (result != null)
					int.TryParse(result.ToString(), out TestAppointmentID);

			}
			finally
			{
				connection.Close();
			}

			return (TestAppointmentID != -1);

		}

		public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
												 decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
		{

			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"UPDATE [dbo].[TestAppointments]
							 SET [TestTypeID] = @TestTypeID
								,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID
								,[AppointmentDate] = @AppointmentDate
								,[PaidFees] = @PaidFees
								,[CreatedByUserID] = @CreatedByUserID
								,[IsLocked] = @IsLocked
								,[RetakeTestApplicationID] = @RetakeTestApplicationID
							  WHERE TestAppointmentID = @TestAppointmentID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
			command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
			command.Parameters.AddWithValue("@PaidFees", PaidFees);
			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			command.Parameters.AddWithValue("@IsLocked", IsLocked);
			command.Parameters.AddWithValue("@RetakeTestApplicationID", (RetakeTestApplicationID != -1 ? (object)RetakeTestApplicationID : DBNull.Value));

			command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

		public static bool DeleteTestAppointment(int TestAppointmentID)
		{

			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"DELETE FROM [dbo].[TestAppointments]
							 WHERE TestAppointmentID = @TestAppointmentID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

		public static DataTable GetTestAppointments()
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT * FROM TestAppointments";

			SqlCommand command = new SqlCommand(query, connection);

			DataTable TestAppointments = new DataTable();

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
					TestAppointments.Load(reader);

				reader.Close();

			}
			finally
			{
				connection.Close();
			}

			return TestAppointments;

		}

		public static DataTable GetTestAppointmentsMainInfo()
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked
							 FROM TestAppointments";

			SqlCommand command = new SqlCommand(query, connection);

			DataTable TestAppointments = new DataTable();

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
					TestAppointments.Load(reader);

				reader.Close();

			}
			finally
			{
				connection.Close();
			}

			return TestAppointments;

		}

		public static DataTable GetTestAppointmentsMainInfoForPersonTestType(int ApplicantPersonID, int TestTypeID, string DrivingClassName)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"SELECT TestAppointmentID, AppointmentDate, TestAppointments.PaidFees, TestAppointments.IsLocked
							 FROM TestAppointments
							 JOIN LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
							 JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
							 Join LicenseClasses On LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
							 WHERE ApplicantPersonID = @ApplicantPersonID
							 AND TestTypeID = @TestTypeID
							 And LicenseClasses.ClassName = @ClassName";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
			command.Parameters.AddWithValue("@ClassName", DrivingClassName);

			DataTable TestAppointments = new DataTable();

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
					TestAppointments.Load(reader);

				reader.Close();

			}
			finally
			{
				connection.Close();
			}

			return TestAppointments;

		}

	}

}

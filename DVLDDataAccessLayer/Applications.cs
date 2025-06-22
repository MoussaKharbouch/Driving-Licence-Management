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

	public static class ApplicationsData
	{

		public static void FindApplication(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
										   ref short ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{

					ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
					ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
					ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
					ApplicationStatus = Convert.ToInt16(reader["ApplicationStatus"]);
					LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
					PaidFees = Convert.ToDecimal(reader["PaidFees"]);
					CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

					reader.Close();

				}

			}
			finally
			{
				connection.Close();
			}

		}

		public static bool DoesApplicationExist(int ApplicationID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT 1 AS FOUND FROM Applications WHERE ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

		public static bool AddApplication(ref int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
										  short ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
		{

			ApplicationID = -1;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"INSERT INTO [dbo].[Applications]
							 ([ApplicantPersonID]
							 ,[ApplicationDate]
							 ,[ApplicationTypeID]
							 ,[ApplicationStatus]
							 ,[LastStatusDate]
							 ,[PaidFees]
							 ,[CreatedByUserID])
							 VALUES
							 (@ApplicantPersonID
							 ,@ApplicationDate
							 ,@ApplicationTypeID
							 ,@ApplicationStatus
							 ,@LastStatusDate
							 ,@PaidFees
							 ,@CreatedByUserID);
							 SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
			command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
			command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
			command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
			command.Parameters.AddWithValue("@PaidFees", PaidFees);
			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

			try
			{

				connection.Open();

				object result = command.ExecuteScalar();

				if (result != null)
					int.TryParse(result.ToString(), out ApplicationID);

			}
			finally
			{
				connection.Close();
			}

			return (ApplicationID != -1);

		}

		public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
											 short ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
		{

			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"UPDATE [dbo].[Applications]
							 SET [ApplicantPersonID] = @ApplicantPersonID,
								 [ApplicationDate] = @ApplicationDate,
								 [ApplicationTypeID] = @ApplicationTypeID,
								 [ApplicationStatus] = @ApplicationStatus,
								 [LastStatusDate] = @LastStatusDate,
								 [PaidFees] = @PaidFees,
								 [CreatedByUserID] = @CreatedByUserID
							WHERE ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
			command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
			command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
			command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
			command.Parameters.AddWithValue("@PaidFees", PaidFees);
			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

		public static bool DeleteApplication(int ApplicationID)
		{

			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"DELETE FROM [dbo].[Applications]
							 WHERE ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

		public static DataTable GetApplications()
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT * FROM Applications";

			SqlCommand command = new SqlCommand(query, connection);

			DataTable Applications = new DataTable();

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
					Applications.Load(reader);

				reader.Close();

			}
			finally
			{
				connection.Close();
			}

			return Applications;

		}

	}

}

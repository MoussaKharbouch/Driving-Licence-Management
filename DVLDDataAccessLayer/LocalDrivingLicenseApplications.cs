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

	public class LocalDrivingLicenseApplicationsData
	{

		public static void FindLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{

					ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
					LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);

					reader.Close();

				}

			}
			finally
			{
				connection.Close();
			}

		}

		public static bool DoesLocalDrivingLicenseApplicationExist(int LocalDrivingLicenseApplicationID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT 1 AS FOUND FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

		public static bool DoesPersonHaveActiveLocalLicenseInSameClass(int ApplicantPersonID, int LicenseClassID, int ApplicationTypeID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"SELECT 1 AS FOUND FROM LocalDrivingLicenseApplications join Applications
							 ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
							 WHERE Applications.ApplicantPersonID = @ApplicantPersonID
							 AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
							 AND ApplicationTypeID = @ApplicationTypeID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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

		public static bool AddLocalDrivingLicenseApplication(ref int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
							 ([ApplicationID]
							 ,[LicenseClassID])
							 VALUES
							 (@ApplicationID
							 ,@LicenseClassID)
							 SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

			try
			{
				connection.Open();

				object result = command.ExecuteScalar();

				if (result != null)
					int.TryParse(result.ToString(), out LocalDrivingLicenseApplicationID);

			}
			finally
			{
				connection.Close();
			}

			return (LocalDrivingLicenseApplicationID != -1);

		}

		public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
		{

			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"UPDATE [dbo].[LocalDrivingLicenseApplications]
							 SET [ApplicationID] = @ApplicationID,
							 [LicenseClassID] = @LicenseClassID
						   WHERE [LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

		public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
		{

			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"DELETE FROM [dbo].[LocalDrivingLicenseApplications]
							 WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

		public static DataTable GetLocalDrivingLicenseApplications()
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = "SELECT * FROM LocalDrivingLicenseApplications";

			SqlCommand command = new SqlCommand(query, connection);

			DataTable LocalDrivingLicenseApplications = new DataTable();

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
					LocalDrivingLicenseApplications.Load(reader);

				reader.Close();

			}
			finally
			{
				connection.Close();
			}

			return LocalDrivingLicenseApplications;

		}

		public static DataTable GetFullInfo()
		{

			SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

			string query = @"SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as ""LDLAppID"", LicenseClasses.ClassName as ""Driving Class"", People.NationalNo,
							 LTRIM(RTRIM(
							 CONCAT(
							 People.FirstName, ' ',
							 People.SecondName, ' ',
							 People.ThirdName, ' ',
							 People.LastName
							 )
							 )) AS ""FullName"", Applications.ApplicationDate, Count(Tests.TestID) as ""Passed Tests"", CASE WHEN Applications.ApplicationStatus = 1 THEN 'New'
							 WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' 
							 WHEN Applications.ApplicationStatus = 3 THEN 'Completed' END AS Status
							 FROM Applications INNER JOIN
							 People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN
							 LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
							 LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID left JOIN
							 Tests on LicenseClasses.LicenseClassID = TestID
							 GROUP BY 
							 LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,
							 LicenseClasses.ClassName,
							 People.NationalNo,
							 People.FirstName,
							 People.SecondName,
							 People.ThirdName,
							 People.LastName,
							 Applications.ApplicationDate,
							 Applications.ApplicationStatus";

			SqlCommand command = new SqlCommand(query, connection);

			DataTable LocalDrivingLicenseApplications = new DataTable();

			try
			{

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
					LocalDrivingLicenseApplications.Load(reader);

				reader.Close();

			}
			finally
			{
				connection.Close();
			}

			return LocalDrivingLicenseApplications;

		}

	}

}

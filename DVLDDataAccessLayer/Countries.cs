using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{

    public static class CountriesData
    {

        public static DataTable GetCountries()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Countries = new DataTable();

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Countries.Load(reader);

            }
            finally
            {
                connection.Close();
            }

            return Countries;

        }

        public static void GetCountry(int CountryID, ref string CountryName)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT CountryName FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    CountryName = reader["CountryName"].ToString();
                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

        public static void GetCountry(string CountryName, ref int CountryID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT CountryID FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    CountryName = reader["CountryName"].ToString();
                    reader.Close();

                }

            }
            finally
            {
                connection.Close();
            }

        }

    }

}

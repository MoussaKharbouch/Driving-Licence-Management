using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class Country
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        Country(int CountryID, string CountryName)
        {

            this.CountryID = CountryID;
            this.CountryName = CountryName;

        }

        public static DataTable GetCountriesNames()
        {

            return CountriesData.GetCountriesNames();

        }

        public static Country FindCountry(int CountryID)
        {

            string CountryName = string.Empty; 

            CountriesData.GetCountry(CountryID, ref CountryName);

            if (CountryName == null)
                return null;

            return new Country(CountryID, CountryName);

        }

        public static Country FindCountry(string CountryName)
        {

            int CountryID = -1;

            CountriesData.GetCountry(CountryName, ref CountryID);

            if (CountryID == null)
                return null;

            return new Country(CountryID, CountryName);

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{

    public class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        clsCountry(int CountryID, string CountryName)
        {

            this.CountryID = CountryID;
            this.CountryName = CountryName;

        }

        public static DataTable GetCountries()
        {

            return CountriesData.GetCountries();

        }

        public static clsCountry FindCountry(int CountryID)
        {

            string CountryName = string.Empty; 

            CountriesData.GetCountry(CountryID, ref CountryName);

            if (CountryName == null)
                return null;

            return new clsCountry(CountryID, CountryName);

        }

        public static clsCountry FindCountry(string CountryName)
        {

            int CountryID = -1;

            CountriesData.GetCountry(CountryName, ref CountryID);

            if (CountryID == -1)
                return null;

            return new clsCountry(CountryID, CountryName);

        }

    }

}

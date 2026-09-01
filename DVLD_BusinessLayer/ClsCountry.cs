using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccessLayer;
using System.Runtime.CompilerServices;


namespace DVLD_BusinessLayer
{
    public class ClsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public ClsCountry()
        {
                
        }
        private ClsCountry(int ID,string Name)
        {
            this.CountryID = ID; 
            this.CountryName = Name;
        }

        public static DataTable GetCountries()
        {
            return ClsCountryData.SelectCountreisFoDB();
        }
        public static ClsCountry GetCountryByName(string countryName)
        {
            int CountryID = -1;
            if(ClsCountryData.GetCountryIDByName(countryName,ref CountryID))
            {
                return new ClsCountry(CountryID, countryName);
            }
            else
            {
                return null;
            }
        }
        public static ClsCountry GetCountryByID(int countryID)
        {
            string CountryName = "";
            if (ClsCountryData.GetCountryNameByID(countryID, ref CountryName))
            {
                return new ClsCountry(countryID, CountryName);
            }
            else
            {
                return null;
            }
        }
    }
}

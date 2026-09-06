using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public static class ClsCountryData
    {

        public static DataTable SelectCountreisFoDB()
        {
            DataTable Countries = new DataTable();

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select * from Countries";

            SqlCommand Command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();


                if (Reader.HasRows)
                {

                    Countries.Load(Reader);
                }
                

                Reader.Close();



            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git All Countries " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                connection.Close();
            }

            return Countries;
        }

        public static bool GetCountryNameByID(int CountryID,ref string CoutryName)
        {
            bool isAffeced = false;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    CoutryName = (string)Reader["CountryName"];
                    isAffeced = true;
                }
                else
                {
                    isAffeced = false;
                }
                Reader.Close(); 



            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git Country by ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                connection.Close();
            }

            return isAffeced;
        }

        public static bool GetCountryIDByName(string CountryName,ref int CountryID)
        {
            bool isAffeced = false;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read()) 
                {
                    CountryID = (int)Reader["CountryID"];
                    isAffeced = true;
                }
                else
                {
                    isAffeced = false;
                }


                Reader.Close();


            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git Country By Name " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                connection.Close();
            }

            return isAffeced;
        }


    }
}

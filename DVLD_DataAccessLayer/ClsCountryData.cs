using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_DataAccessLayer
{
    public static class ClsCountryData
    {

        public static DataTable SelectCountreisFoDB()
        {
            DataTable Countries = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"select * from Countries";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {

                        connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {

                                Countries.Load(Reader);
                            }
                            Reader.Close();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git All Countries " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            

            return Countries;
        }

        public static bool GetCountryNameByID(int CountryID,ref string CoutryName)
        {
            bool isAffeced = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {
                        Command.Parameters.AddWithValue("@CountryID", CountryID);

                        connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                CoutryName = (string)Reader["CountryName"];
                                isAffeced = true;
                            }
                            else
                            {
                                isAffeced = false;
                            }
                            
                        }
                    }



                    
                }
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git Country by ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           

            return isAffeced;
        }

        public static bool GetCountryIDByName(string CountryName,ref int CountryID)
        {
            bool isAffeced = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {
                        Command.Parameters.AddWithValue("@CountryName", CountryName);

                        connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                CountryID = (int)Reader["CountryID"];
                                isAffeced = true;
                            }
                            else
                            {
                                isAffeced = false;
                            }
                        }


                    }


                    
                }
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git Country By Name " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           

            return isAffeced;
        }


    }
}

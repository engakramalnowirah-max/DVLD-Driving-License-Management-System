using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    public static class ClsLicenseClassData
    {
        public static DataTable SelectLicenseClassesOfDB()
        {
            DataTable Countries = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"select * from LicenseClasses";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {

                        connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                Countries.Load(Reader);
                            }
                        }
                    }
                }


                
            }
            catch (Exception)
            {

                throw;
            }

            return Countries;
        }

        public static bool GetLicenseClassByID(int LicenseClassID, ref string ClassName, ref string Description,ref short MinimumAllowedAge,ref short DefaultValidityLength, ref float ClassFees)
        {
            bool isAffeced = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {
                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                ClassName = Reader["ClassName"].ToString();
                                Description = Reader["ClassDescription"].ToString();
                                MinimumAllowedAge = Convert.ToInt16(Reader["MinimumAllowedAge"]);
                                DefaultValidityLength = Convert.ToInt16(Reader["DefaultValidityLength"]);
                                ClassFees = Convert.ToSingle(Reader["ClassFees"]);
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
            catch (Exception)
            {

                throw;
            }
            

            return isAffeced;
        }

        public static bool GetLicenseClasseByName(string Name, ref int LicenseClassID, ref string Description, ref short MinimumAllowedAge, ref short DefaultValidityLength, ref float ClassFees)
        {
            bool isAffeced = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = "SELECT * FROM LicenseClasses WHERE ClassName = @Name";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {
                        Command.Parameters.AddWithValue("@Name", Name);

                        connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                LicenseClassID = (int)Reader["LicenseClassID"];
                                Description = Reader["ClassDescription"].ToString();
                                MinimumAllowedAge = Convert.ToInt16(Reader["MinimumAllowedAge"]);
                                DefaultValidityLength = Convert.ToInt16(Reader["DefaultValidityLength"]);
                                ClassFees = Convert.ToSingle(Reader["ClassFees"]);
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
            catch (Exception)
            {

                throw;
            }
            

            return isAffeced;
        }

    }
}

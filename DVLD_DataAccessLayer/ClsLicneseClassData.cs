using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public static class ClsLicenseClassData
    {
        public static DataTable SelectLicenseClassesOfDB()
        {
            DataTable Countries = new DataTable();

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select * from LicenseClasses";

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
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return Countries;
        }

        public static bool GetLicenseClassByID(int LicenseClassID, ref string ClassName, ref string Description,ref short MinimumAllowedAge,ref short DefaultValidityLength, ref float ClassFees)
        {
            bool isAffeced = false;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    ClassName = Reader["ClassName"].ToString();
                    Description = Reader["ClassDescription"].ToString();
                    MinimumAllowedAge =Convert.ToInt16(Reader["MinimumAllowedAge"]);
                    DefaultValidityLength =Convert.ToInt16(Reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                    isAffeced = true;
                }
                else
                {
                    isAffeced = false;
                }
                Reader.Close();



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return isAffeced;
        }

        public static bool GetLicenseClasseByName(string Name, ref int LicenseClassID, ref string Description, ref short MinimumAllowedAge, ref short DefaultValidityLength, ref float ClassFees)
        {
            bool isAffeced = false;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT * FROM LicenseClasses WHERE ClassName = @Name";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@Name", Name);
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
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


                Reader.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return isAffeced;
        }

    }
}

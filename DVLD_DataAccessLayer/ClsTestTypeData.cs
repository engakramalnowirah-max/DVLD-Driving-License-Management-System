using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    public static class ClsTestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM TestTypes";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {


                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                dt.Load(Reader);
                            }

                          
                        }
                    }
                    
                }
            }
            catch (Exception)
            {


            }
            
            return dt;
        }
        public static bool UpdataTestType(int ID, string Title,string Description, float Fees)
        {
            short isAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE TestTypes
                            SET TestTypeTitle = @Title,
                                TestTypeDescription = @Description,
                                TestTypeFees = @Fees
                           WHERE TestTypeID = @ID";
                    using (SqlCommand Command = new SqlCommand(@Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@ID", ID);
                        Command.Parameters.AddWithValue("@Title", Title);
                        Command.Parameters.AddWithValue("@Description", Description);
                        Command.Parameters.AddWithValue("@Fees", Fees);
        
                            Connection.Open();
                            isAffected = (short)Command.ExecuteNonQuery();
                        
                    }
                }
            }
            catch (Exception)
            {


            }
            
            return (isAffected != 0);
        }

        public static bool GetTestTypeByID(int ID, ref string Title,ref string Description, ref float Fees)
        {
            bool isAffectied = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM TestTypes WHERE TestTypeID = @ID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ID", ID);
                        
                            Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isAffectied = true;
                                Title = Reader["TestTypeTitle"].ToString();
                                Description = Reader["TestTypeDescription"].ToString();
                                Fees = Convert.ToSingle(Reader["TestTypeFees"]);
                            }
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                isAffectied = false;

            }
            
            return isAffectied;
        }

        public static int InsertTestType(string Title,string Description, float Fees)
        {
            int isAffectied = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"INSERT INTO TestTypes
                             (TestTypeTitle
                             ,TestTypeDescription
                             ,TestTypeFees)
                              VALUES
                             (@Title
                             ,@Description
                             ,Fees)SELECT SCOPE_IDENTITY()";
                    using (SqlCommand Comand = new SqlCommand(Query, Connection))
                    {
                        Comand.Parameters.AddWithValue("@Title", Title);
                        Comand.Parameters.AddWithValue("@Description", Description);
                        Comand.Parameters.AddWithValue("@Fees", Fees);

                        Connection.Open();
                        object obj = Comand.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int Reselte))
                        {
                            isAffectied = Reselte;
                        }


                    }
                }
            }
            catch (Exception)
            {


            }
            
            return isAffectied;

        }
    }
}

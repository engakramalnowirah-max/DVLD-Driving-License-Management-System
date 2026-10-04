using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    public static class ClsApplicationTypeData
    {
        public static DataTable SelectAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM ApplicationTypes";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                dt.Load(Reader);
                            }
                            Reader.Close();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git All Base Applicaion Type " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           

            return dt;
        }

        public static bool UpdataApplicationType(int ID,string Title, float Fees)
        {
            short isAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE ApplicationTypes
                            SET ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                           WHERE ApplicationTypeID = @ID";
                    using (SqlCommand Command = new SqlCommand(@Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ID", ID);
                        Command.Parameters.AddWithValue("@Title", Title);
                        Command.Parameters.AddWithValue("@Fees", Fees);


                        Connection.Open();
                        isAffected = (short)Command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Update Applicaion Type " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           
            return (isAffected != 0);
        }

        public static bool GetAllicaionByID(int ID,ref string Title , ref float Fees)
        {
            bool isAffectied = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ID", ID);

                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isAffectied = true;
                                Title = Reader["ApplicationTypeTitle"].ToString();
                                Fees = Convert.ToSingle(Reader["ApplicationFees"]);
                            }
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git  Applicaion Type by ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           
            return isAffectied;
        }

        public static int InsertApplicationType(string Title, float  Fees)
        {
            int isAffectied = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"INSERT INTO ApplicationTypes
                             (ApplicationTypeTitle
                             ,ApplicationFees)
                              VALUES
                             (@Title
                             ,@Fees)SELECT SCOPE_IDENTITY()";
                    using (SqlCommand Comand = new SqlCommand(Query, Connection))
                    {
                        Comand.Parameters.AddWithValue("@Title", Title);
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
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in insert new  Applicaion Type " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
           
           
            }
               
            return isAffectied;

        }
    }
}

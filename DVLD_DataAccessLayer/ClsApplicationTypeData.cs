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


namespace DVLD_DataAccessLayer
{
    public static class ClsApplicationTypeData
    {
        public static DataTable SelectAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM ApplicationTypes";
            SqlCommand Command = new SqlCommand(Query, Connection);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                Connection.Close(); 
            }

            return dt;
        }

        public static bool UpdataApplicationType(int ID,string Title, float Fees)
        {
            short isAffected = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"UPDATE ApplicationTypes
                            SET ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                           WHERE ApplicationTypeID = @ID";
            SqlCommand Command = new SqlCommand(@Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                Connection.Open() ;
                isAffected = (short)Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                Connection.Close();
            }
            return (isAffected != 0);
        }

        public static bool GetAllicaionByID(int ID,ref string Title , ref float Fees)
        {
            bool isAffectied = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID",ID);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read()) 
                {
                    isAffectied = true;
                    Title = Reader["ApplicationTypeTitle"].ToString();
                    Fees =Convert.ToSingle(Reader["ApplicationFees"]);
                }
            }
            catch (Exception)
            {
                isAffectied = false;
                
            }
            finally
            {
                Connection.Close();
            }
            return isAffectied;
        }

        public static int InsertApplicationType(string Title, float  Fees)
        {
            int isAffectied = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"INSERT INTO ApplicationTypes
           (ApplicationTypeTitle
           ,ApplicationFees)
     VALUES
           (@Title
           ,@Fees)SELECT SCOPE_IDENTITY()";
            SqlCommand Comand = new SqlCommand(Query, Connection);
            Comand.Parameters.AddWithValue("@Title",Title);
            Comand.Parameters.AddWithValue("@Fees",Fees);

            try
            {
                Connection.Open();
                object obj = Comand.ExecuteScalar();
                if(obj != null &&int.TryParse(obj.ToString(),out int  Reselte))
                {
                    isAffectied = Reselte;
                }


            }
            catch (Exception)
            {

                
            }
            finally
            {
                Connection.Close();
            }
            return isAffectied;

        }
    }
}

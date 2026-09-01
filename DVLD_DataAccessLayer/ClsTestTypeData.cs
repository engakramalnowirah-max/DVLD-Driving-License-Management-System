using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public static class ClsTestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM TestTypes";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if(Reader.HasRows)
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
        public static bool UpdataTestType(int ID, string Title,string Description, float Fees)
        {
            short isAffected = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"UPDATE TestTypes
                            SET TestTypeTitle = @Title,
                                TestTypeDescription = @Description,
                                TestTypeFees = @Fees
                           WHERE TestTypeID = @ID";
            SqlCommand Command = new SqlCommand(@Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Description", Description);
            Command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                Connection.Open();
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

        public static bool GetTestTypeByID(int ID, ref string Title,ref string Description, ref float Fees)
        {
            bool isAffectied = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM TestTypes WHERE TestTypeID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    isAffectied = true;
                    Title = Reader["TestTypeTitle"].ToString();
                    Description = Reader["TestTypeDescription"].ToString();
                    Fees = Convert.ToSingle(Reader["TestTypeFees"]);
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

        public static int InsertTestType(string Title,string Description, float Fees)
        {
            int isAffectied = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"INSERT INTO TestTypes
           (TestTypeTitle
           ,TestTypeDescription
           ,TestTypeFees)
     VALUES
           (@Title
           ,@Description
           ,Fees)SELECT SCOPE_IDENTITY()";
            SqlCommand Comand = new SqlCommand(Query, Connection);
            Comand.Parameters.AddWithValue("@Title", Title);
            Comand.Parameters.AddWithValue("@Description", Description);
            Comand.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                Connection.Open();
                object obj = Comand.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Reselte))
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

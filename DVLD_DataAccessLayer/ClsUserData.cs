using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;

namespace DVLD_DataAccessLayer
{
    public static class ClsUserData
    {
        public static DataTable SelectAllUsers()
        {
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT Users.UserID, Users.PersonID,  People.FirstName +' '+People.SecondName +' '+ People.ThirdName +' '+ People.LastName  as Name, Users.UserName, Users.IsActive
                             FROM     Users INNER JOIN
                           People ON Users.PersonID = People.PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            DataTable dt = new DataTable();

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
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

        public static int InsertNewUser(int PersonID,string UserName,string Password,short IsActive)
        {
            int UserID = -1;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"INSERT INTO Users
           (PersonID
           ,UserName
           ,Password
           ,IsActive)
     VALUES
           (@PersonID
           ,@UserName
           ,@Password
           ,@IsActive)SELECT SCOPE_IDENTITY()";

            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@PersonID",PersonID);
            Command.Parameters.AddWithValue("@UserName",UserName);
            Command.Parameters.AddWithValue("@Password",Password);
            Command.Parameters.AddWithValue("@IsActive",IsActive);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(),out int num))
                {
                    UserID = num;
                }

            }
            catch (Exception)
            {

                
            }
            finally
            {
                Connection.Close();
            }

            return UserID;


        }
        public static bool UpdateUserToDB(int UserID,int PersonID ,string UserName,string Password,short IsActive)
        {
            int isAiffected = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"UPDATE [dbo].[Users]
                    SET PersonID = @PersonID
                       ,UserName = @UserName
                        ,Password = @Password
                            ,IsActive = @IsActive
                     WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                Connection.Open();
                 
                isAiffected = Command.ExecuteNonQuery();



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Connection.Close ();
            }
            return (isAiffected != 0);

        }
        public static bool DeleteUserToDB(int UserID)
        {
            int isAiffected = 0;

            SqlConnection Connection  = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"Delete FROM [dbo].[Users]
                             WHER UserID = @UserID";
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();
                isAiffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                Connection.Close ();
            }
            return (isAiffected != 0);
        }


        public static bool GetUserByUserID(int UserID,ref int PersonID,ref string UserName,ref string Password,ref short IsActive)
        {
            bool isAiffective = false;
            SqlConnection Connection = new SqlConnection (ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query,Connection);
            command.Parameters.AddWithValue("@UserID",UserID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if(Reader.Read())
                {
                    PersonID = int.Parse(Reader["PersonID"].ToString());
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = (short)((bool)Reader["IsActive"] ? 1 : 0);
                    isAiffective = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Connection.Close();
            }
            return (isAiffective);
        }


        public static bool IsUserExistByPersonID(int PersonID)
        {
            int isAiffective = -1;

            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@PersonID",PersonID);

            try
            {
                Connection.Open();
                isAiffective = Command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                
            }
            finally
            {
                Connection.Close();
            }

            return (isAiffective != -1);
        }

        public static bool GetUserByUserNameAndPassword(string UserName ,string Password, ref int UserID, ref int PersonID,  ref short IsActive)
        {
            bool isAiffective = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM Users WHERE  Password = @Password and  UserName = @UserName  ;";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    UserID = int.Parse(Reader["UserID"].ToString());
                    PersonID = int.Parse(Reader["PersonID"].ToString());
                    IsActive = (short)((bool)Reader["IsActive"] ? 1 : 0);
                    isAiffective = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Connection.Close();
            }
            return (isAiffective);
        }
    }

    
}

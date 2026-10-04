using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_DataAccessLayer
{
    public static class ClsUserData
    {
        public static DataTable SelectAllUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT Users.UserID, Users.PersonID,  People.FirstName +' '+People.SecondName +' '+ People.ThirdName +' '+ People.LastName  as Name, Users.UserName, Users.IsActive
                             FROM     Users INNER JOIN
                           People ON Users.PersonID = People.PersonID";

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
            catch (Exception ex)
            {
                throw;
            }
            
            return dt;
        }

        public static int InsertNewUser(int PersonID,string UserName,string Password,short IsActive)
        {
            int UserID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

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

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        Command.Parameters.AddWithValue("@UserName", UserName);
                        Command.Parameters.AddWithValue("@Password", Password);
                        Command.Parameters.AddWithValue("@IsActive", IsActive);


                        Connection.Open();
                        object obj = Command.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int num))
                        {
                            UserID = num;
                        }
                    }

                    
                }
            }
            catch (Exception)
            {


            }
            

            return UserID;


        }
        public static bool UpdateUserToDB(int UserID,int PersonID ,string UserName,string Password,short IsActive)
        {
            int isAiffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE [dbo].[Users]
                    SET PersonID = @PersonID
                       ,UserName = @UserName
                        ,Password = @Password
                            ,IsActive = @IsActive
                     WHERE UserID = @UserID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@UserID", UserID);
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        Command.Parameters.AddWithValue("@UserName", UserName);
                        Command.Parameters.AddWithValue("@Password", Password);
                        Command.Parameters.AddWithValue("@IsActive", IsActive);


                        Connection.Open();

                        isAiffected = Command.ExecuteNonQuery();
                    }
                }
               
            }
            catch (Exception)
            {

                throw;
            }
            
            return (isAiffected != 0);

        }
        public static bool DeleteUserToDB(int UserID)
        {
            int isAiffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"Delete FROM [dbo].[Users]
                             WHER UserID = @UserID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@UserID", UserID);


                        Connection.Open();
                        isAiffected = Command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception)
            {


            }
            
            return (isAiffected != 0);
        }


        public static bool GetUserByUserID(int UserID,ref int PersonID,ref string UserName,ref string Password,ref short IsActive)
        {
            bool isAiffective = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM Users WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(Query, Connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);


                        Connection.Open();
                        using (SqlDataReader Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                PersonID = int.Parse(Reader["PersonID"].ToString());
                                UserName = (string)Reader["UserName"];
                                Password = (string)Reader["Password"];
                                IsActive = (short)((bool)Reader["IsActive"] ? 1 : 0);
                                isAiffective = true;
                            }
                        }
                    }

                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return (isAiffective);
        }


        public static bool IsUserExistByPersonID(int PersonID)
        {
            int isAiffective = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@PersonID", PersonID);


                        Connection.Open();
                        isAiffective = Command.ExecuteNonQuery();
                    }
                }

                
            }
            catch (Exception)
            {


            }
            

            return (isAiffective != -1);
        }

        public static bool GetUserByUserNameAndPassword(string UserName ,string Password, ref int UserID, ref int PersonID,  ref short IsActive)
        {
            bool isAiffective = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM Users WHERE  Password = @Password and  UserName = @UserName  ;";

                    using (SqlCommand command = new SqlCommand(Query, Connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);


                        Connection.Open();
                        using (SqlDataReader Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                UserID = int.Parse(Reader["UserID"].ToString());
                                PersonID = int.Parse(Reader["PersonID"].ToString());
                                IsActive = (short)((bool)Reader["IsActive"] ? 1 : 0);
                                isAiffective = true;
                            }
                        }
                    }
                }

               
            }
            catch (Exception)
            {

                throw;
            }
           
            return (isAiffective);
        }
    }

    
}

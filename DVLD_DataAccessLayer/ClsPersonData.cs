using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_DataAccessLayer
{
    static public class ClsPersonData
    {
        public static DataTable SelectAllPeopleFoDB()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT People.PersonID , People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, 
                                     Case
                                    when People.Gendor = 0 then  'Male'
                            	 else  'FeMale'

                        end as GendorCaption, People.DateOfBirth, Countries.CountryName, People.Phone, People.Email
                                FROM     Countries INNER JOIN
                            People ON Countries.CountryID = People.NationalityCountryID";

            SqlCommand Command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows) 
                {
                    dt.Load(Reader);
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

            return dt;
        }

        public static int InsertNewPersoneInDB(string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,bool Gendor,string Address,string phone,string Email,int CountryID,string ImagePath)
        {
            int ID = 0;
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"INSERT INTO People (NationalNo,FirstName,SecondName ,ThirdName,LastName,DateOfBirth,Gendor ,Address,Phone ,Email,NationalityCountryID,ImagePath)    
                           VALUES (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Address,@phone,@Email,@CountryID,@ImagePath)SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if(ThirdName != string.Empty)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@phone", phone);
            if (Email != string.Empty)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != string.Empty)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(),out int number))
                {
                    ID = number;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            { 
                connection.Close();
            }
            return ID;
        }
        public static bool UpdutePersonInDB(int ID,string NationalNo, string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,bool Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
        {
            int IsAffict = -1;
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"UPDATE People SET NationalNo = @NationalNo,
                           FirstName = @FirstName,
                           SecondName = @SecondName, 
                           ThirdName = @ThirdName,
                           LastName = @LastName,
                           DateOfBirth = @DateOfBirth,
                           Gendor = @Gendor, 
                           Address = @Address,
                           Phone = @Phone, 
                           Email = @Email, 
                          NationalityCountryID = @NationalityCountryID, 
                          ImagePath = @ImagePath  WHERE PersonID = @ID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != string.Empty)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != string.Empty)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != string.Empty)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                IsAffict = command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
            return (IsAffict != 0);

        }
        public static bool DeleteOnePersonOfDB(int PersonID)
        {
            int IsAfficted = 0;
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"Delete FROM People   WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                IsAfficted = command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                connection.Close();
            }
            return (IsAfficted != 0);
        }



        public static bool GetPersonByPersonID(int PersonID,ref string NotionalNo,ref string FirstName,ref string SecondName,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth,ref bool Gendor,ref string Address,ref string Phone,ref string Email,ref int NationalityCountryID,ref string ImagePath)
        {
           

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            bool isAffeced = false;
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    NotionalNo = Reader["NationalNo"].ToString();
                    FirstName = Reader["FirstName"].ToString();
                    SecondName = Reader["SecondName"].ToString() ;
   
                    LastName = Reader["LastName"].ToString();
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor =Convert.ToBoolean( Reader["Gendor"]);
                    Address = Reader["Address"].ToString();
                    Phone = Reader["Phone"].ToString();
                    Email = Reader["Email"].ToString();
                    NationalityCountryID =int.Parse(Reader["NationalityCountryID"].ToString());
                    ImagePath = Reader["ImagePath"].ToString();

                    ThirdName = Reader["ThirdName"] != DBNull.Value ? Reader["ThirdName"].ToString() : "";
                    Email = Reader["Email"] != DBNull.Value ? Reader["Email"].ToString() : "";
                    ImagePath = Reader["ImagePath"] != DBNull.Value ? Reader["ImagePath"].ToString() : "";
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
                isAffeced = false;
                
            }
            finally
            {
                connection.Close();
            }

            return isAffeced;
        }
        public static bool GetPersonByNationalID(string NationalNo, ref int PersonID ,ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref bool Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            bool isAffeced = false;
            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    PersonID = int.Parse( Reader["PersonID"].ToString());
                    FirstName = Reader["FirstName"].ToString();
                    SecondName = Reader["SecondName"].ToString();

                    LastName = Reader["LastName"].ToString();
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor =Convert.ToBoolean(Reader["Gendor"]);
                    Address = Reader["Address"].ToString();
                    Phone = Reader["Phone"].ToString();
                    
                    NationalityCountryID = int.Parse(Reader["NationalityCountryID"].ToString());
                    

                    ThirdName = Reader["ThirdName"] != DBNull.Value ? Reader["ThirdName"].ToString() : "";
                    Email = Reader["Email"] != DBNull.Value ? Reader["Email"].ToString() : "";
                    ImagePath = Reader["ImagePath"] != DBNull.Value ? Reader["ImagePath"].ToString() : "";
                    isAffeced = true;

                }

                Reader.Close();



            }
            catch (Exception)
            {
                isAffeced = false;
                throw;
            }
            finally
            {
                connection.Close();
            }

            return isAffeced;
        }



        public static bool IsPersonExist(int PersonID)
        {
              
        
            int IsAfficted = -1;
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT Founde=1  FROM People  WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                IsAfficted = command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                connection.Close();
            }
            return (IsAfficted != 0);
        
        }

        public static bool IsPersonExist(string NationalNo)
        {


            int IsAfficted = -1;
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT Founde=1  FROM People  WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                IsAfficted = command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
            return (IsAfficted != 0);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccessLayer
{
    public static class ClsTestAppointmentData
    {
        public static DataTable SELECTAllTestAppointmentes()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"
                              select * from TestAppointments_View
                               order by AppointmentDate Desc;";
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

                throw;
            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select TestAppointmentID,AppointmentDate,PaidFees,IsLocked from TestAppointments 
                             where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID;";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

                throw;
            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

        public static int InsertTestAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,float PaidFees,int CreatedByUserID, bool IsLocked,int RetakeTestApplicationID)
        {
            int AppointmentID = -1;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"INSERT INTO TestAppointments
           (TestTypeID
           ,LocalDrivingLicenseApplicationID
           ,AppointmentDate
           ,PaidFees
           ,CreatedByUserID
           ,IsLocked
           ,RetakeTestApplicationID)
     VALUES
           (@TestTypeID
           ,@LocalDrivingLicenseApplicationID
           ,@AppointmentDate
           ,@PaidFees
           ,@CreatedByUserID
           ,@IsLocked
           ,@RetakeTestApplicationID)SElECT SCOPE_IDENTITY();";
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if( RetakeTestApplicationID != -1 )
            {
                Command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }
            else
            {
                Command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);

            }


            try
            {
                    Connection.Open();
                    object obj = Command.ExecuteScalar();
                    if (obj != null && int.TryParse(obj.ToString(), out int Resalut))
                    {
                        AppointmentID = Resalut;
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
            return AppointmentID;
        }
        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int isAffected = 0;
          
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"
UPDATE TestAppointments
   SET TestTypeID = @TestTypeID
      ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
      ,AppointmentDate = @AppointmentDate
      ,PaidFees = @PaidFees
      ,CreatedByUserID = @CreatedByUserID
      ,IsLocked = @IsLocked
      ,RetakeTestApplicationID = @RetakeTestApplicationID
 WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand Command = new SqlCommand (Query,Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsLocked", IsLocked);
            Command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            try
            {
                Connection.Open();
                isAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close (); }

            return (isAffected != 0);
        }
        public static bool GetTestAppointmentByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreateByUserID,ref bool IsLocked,ref int RetakTestApplicationID)
        {
            bool isAffected = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select * from TestAppointments where TestAppointmentID = @TestAppointmentID";
            SqlCommand Command = new SqlCommand (Query,Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreateByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);
                    if (reader["RetakeTestApplicationID"] != System.DBNull.Value)
                      RetakTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                    {
                        RetakTestApplicationID = -1;
                    }
                    isAffected = true;
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close (); }

            return isAffected;
        }


        public static bool GetLastTestAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID, ref int TestAppointmentID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreateByUserID, ref short IsLocked, ref int RetakTestApplicationID)
        {
            bool isAffected = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select top 1 * 
                             from TestAppointments
                             where (TestTypeID = @TestTypeID)
                             And (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                             order by TestAppointmentID Desc;";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreateByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = Convert.ToInt16(reader["IsLocked"]);
                    if (reader["RetakeTestApplicationID"] != System.DBNull.Value)
                        RetakTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                    {
                        RetakTestApplicationID = -1;
                    }
                    isAffected = true;
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close(); }

            return isAffected;
        }


        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }


        
    }
}

 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_DataAccessLayer
{
    public static class ClsLocalDrivingLicenseApplicationsData
    {
        public static DataTable SelectAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select * from LocalDrivingLicenseApplications_View";
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
        public static int InsertNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"INSERT INTO LocalDrivingLicenseApplications
           (ApplicationID
           ,LicenseClassID)
     VALUES
           (@ApplicationID
           ,@LicenseClassID)SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
           

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Number))
                {
                    LocalDrivingLicenseApplicationID = Number;
                }

            }
            catch (Exception)
            {


            }
            finally
            {
                Connection.Close();
            }

            return LocalDrivingLicenseApplicationID;


        }
        public static bool UpdateLocalDrivingLicenseApplicationToDB(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int isAiffected = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"UPDATE LocalDrivingLicenseApplications
                    SET ApplicationID = @ApplicationID
                       ,LicenseClassID = @LicenseClassID
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


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
                Connection.Close();
            }
            return (isAiffected != 0);

        }

        public static bool GetLocalDrivingLicenseApplicationByID(int ID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isAffectied = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    isAffectied = true;
                    ApplicationID = (int)Reader["ApplicationID"];
                    LicenseClassID =(int)Reader["LicenseClassID"];
                    
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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivinglicenseAppID)
        {
            int isAffected = -1;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"Delete  from LocalDrivingLicenseApplications
                             where LocalDrivingLicenseApplicationID = @LocalDrivinglicenseAppID";
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@LocalDrivinglicenseAppID", LocalDrivinglicenseAppID);
            try
            {
                Connection.Open();
                isAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

               
            }
            finally
            {
                Connection.Close();
            }

            return (isAffected != -1);
        }
        public static bool GetLocalDrivingLicenseApplicationByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplication, ref int LicenseClassID)
        {
            bool isAffectied = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    isAffectied = true;
                    LocalDrivingLicenseApplication = (int)Reader["LocalDrivingLicenseApplication"];
                    LicenseClassID = (int)Reader["LicenseClassID"];

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

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                Connection.Open();
                object Reslte = Command.ExecuteScalar();
                if (Reslte != null)
                {
                    isFound = true;
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

            return isFound;
        }

        
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID,int TestTypeID )
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
                }

            }

            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool IsApplicationHasSuccessTestForTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int isFound = -1;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT Found = 1
            FROM LocalDrivingLicenseApplications 
            INNER JOIN TestAppointments 
                ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
            INNER JOIN TestTypes 
                ON TestAppointments.TestTypeID = TestTypes.TestTypeID 
            INNER JOIN Tests 
                ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID

            WHERE 
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                AND TestAppointments.TestTypeID = @TestTypeID
                AND Tests.TestResult = 1";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Result))
                {
                    isFound = Result;
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
            return (isFound != -1);
        }



        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTest = Trials;
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                connection.Close();
            }

            return TotalTrialsPerTest;

        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                connection.Close();
            }

            return Result;

        }

        public static bool IsLicenseIssued(int ApplicationID)
        {
            int RowAffected = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT Licenses.LicenseID
                  FROM     Licenses INNER JOIN
                  Applications ON Licenses.ApplicationID = Applications.ApplicationID
                  WHERE  (Licenses.ApplicationID = @ApplicationID)";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Result))
                {
                    RowAffected = Result;
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

            return (RowAffected > 0);
        }
    }
}

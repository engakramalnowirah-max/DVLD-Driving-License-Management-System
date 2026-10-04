 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace DVLD_DataAccessLayer
{
    public static class ClsLocalDrivingLicenseApplicationsData
    {
        public static DataTable SelectAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"select * from LocalDrivingLicenseApplications_View";
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
        public static int InsertNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"INSERT INTO LocalDrivingLicenseApplications
                                    (ApplicationID
                                    ,LicenseClassID)
                                     VALUES
                                    (@ApplicationID
                                    ,@LicenseClassID)SELECT SCOPE_IDENTITY();";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



                        Connection.Open();
                        object obj = Command.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int Number))
                        {
                            LocalDrivingLicenseApplicationID = Number;
                        }
                    }
                }

                
            }
            catch (Exception)
            {


            }


            return LocalDrivingLicenseApplicationID;


        }
        public static bool UpdateLocalDrivingLicenseApplicationToDB(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int isAiffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE LocalDrivingLicenseApplications
                    SET ApplicationID = @ApplicationID
                       ,LicenseClassID = @LicenseClassID
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



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

        public static bool GetLocalDrivingLicenseApplicationByID(int ID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isAffectied = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ID", ID);

                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isAffectied = true;
                                ApplicationID = (int)Reader["ApplicationID"];
                                LicenseClassID = (int)Reader["LicenseClassID"];

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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivinglicenseAppID)
        {
            int isAffected = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"Delete  from LocalDrivingLicenseApplications
                             where LocalDrivingLicenseApplicationID = @LocalDrivinglicenseAppID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@LocalDrivinglicenseAppID", LocalDrivinglicenseAppID);
                        
                            Connection.Open();
                            isAffected = Command.ExecuteNonQuery();
                    }
                        
                }
            }
            catch (Exception)
            {


            }
            

            return (isAffected != -1);
        }
        public static bool GetLocalDrivingLicenseApplicationByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplication, ref int LicenseClassID)
        {
            bool isAffectied = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isAffectied = true;
                                LocalDrivingLicenseApplication = (int)Reader["LocalDrivingLicenseApplication"];
                                LicenseClassID = (int)Reader["LicenseClassID"];

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

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                        Connection.Open();
                        object Reslte = Command.ExecuteScalar();
                        if (Reslte != null)
                        {
                            isFound = true;
                        }
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
           

            return isFound;
        }

        
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID,int TestTypeID )
        {
            bool Result = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                        connection.Open();

                        object result = command.ExecuteScalar();


                        if (result != null)
                        {
                            Result = true;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

            

            return Result;
        }

        public static bool IsApplicationHasSuccessTestForTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int isFound = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
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
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                        Connection.Open();
                        object obj = Command.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int Result))
                        {
                            isFound = Result;
                        }
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
           
            return (isFound != -1);
        }



        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                        {
                            TotalTrialsPerTest = Trials;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }  

            return TotalTrialsPerTest;

        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool Result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                        {
                            Result = returnedResult;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }



            return Result;

        }

        public static bool IsLicenseIssued(int ApplicationID)
        {
            int RowAffected = -1;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT Licenses.LicenseID
                               FROM     Licenses INNER JOIN
                               Applications ON Licenses.ApplicationID = Applications.ApplicationID
                               WHERE  (Licenses.ApplicationID = @ApplicationID)";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);



                        Connection.Open();

                        object result = Command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int Result))
                        {
                            RowAffected = Result;
                        }
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
          

            return (RowAffected > 0);
        }
    }
}

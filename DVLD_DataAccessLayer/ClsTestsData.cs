using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccessLayer
{
    public static class ClsTestsData
    {
        public static int InsertTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = 0;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"INSERT INTO Tests
           (TestAppointmentID
           ,TestResult
           ,Notes
           ,CreatedByUserID)
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);


            update TestAppointments
            set IsLocked = 1
            where TestAppointmentID = @TestAppointmentID

               SElECT SCOPE_IDENTITY();";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            
            


            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Result))
                {
                    TestID = Result;
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
            return TestID;
        }
        public static bool EditTest(int TestID,int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int isAffected = 0;

            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"UPDATE Tests
                             SET TestAppointmentID = @TestAppointmentID
                                ,TestResult = @TestResult
                                ,Notes = @Notes
                                ,CreatedByUserID = @CreatedByUserID
                          WHERE TestID = @TestID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            
            try
            {
                Connection.Open();
                isAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close(); }

            return (isAffected != 0);
        }
        public static bool GetTestByTestID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isAffected = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select * from Tests where TestID = @TestID ; ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    if (reader["Notes"] != System.DBNull.Value)
                        Notes = reader["Notes"].ToString();
                    else
                        Notes = "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    
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


        public static bool GetTestByTestAppointmentID(int TestAppointmentID,ref int TestID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isAffected = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"select * from Tests where TestAppointmentID = @TestAppointmentID ; ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    if (reader["Notes"] != System.DBNull.Value)
                        Notes = reader["Notes"].ToString();
                    else
                        Notes = "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static bool GetLastTestPerPersoneAndLicenseClassAndTestType(int ApplicantPersonID, int LicenseClassID,int TestTypeID,ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isAffected = false;
            SqlConnection Connection = new SqlConnection(ClsConnectionSettings.ConnectionString);
            string Query = @"SELECT TOP (1)
                                Tests.TestID,
                                Tests.TestAppointmentID,
                                Tests.TestResult,
                                Tests.Notes,
                                Tests.CreatedByUserID
                            FROM Tests
                            INNER JOIN TestAppointments
                                ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            INNER JOIN LocalDrivingLicenseApplications
                                ON TestAppointments.LocalDrivingLicenseApplicationID =
                                   LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            INNER JOIN Applications
                                ON LocalDrivingLicenseApplications.ApplicationID =
                                   Applications.ApplicationID
                            WHERE Applications.ApplicantPersonID = @ApplicantPersonID
                              AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                              AND TestAppointments.TestTypeID = @TestTypeID
                            ORDER BY Tests.TestAppointmentID DESC; ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult =Convert.ToBoolean( reader["TestResult"]);
                    if (reader["Notes"] != System.DBNull.Value)
                        Notes = reader["Notes"].ToString();
                    else
                        Notes = "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

    }
}

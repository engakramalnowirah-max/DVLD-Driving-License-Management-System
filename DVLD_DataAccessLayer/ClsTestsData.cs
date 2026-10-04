using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    public static class ClsTestsData
    {
        public static int InsertTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
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
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestResult", TestResult);
                        Command.Parameters.AddWithValue("@Notes", Notes);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




                        Connection.Open();
                        object obj = Command.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int Result))
                        {
                            TestID = Result;
                        }
                    }

                    
                }
            }
            catch (Exception)
            {

                throw;
            }
           
            return TestID;
        }
        public static bool EditTest(int TestID,int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int isAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE Tests
                             SET TestAppointmentID = @TestAppointmentID
                                ,TestResult = @TestResult
                                ,Notes = @Notes
                                ,CreatedByUserID = @CreatedByUserID
                                WHERE TestID = @TestID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TestID", TestID);
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        Command.Parameters.AddWithValue("@TestResult", TestResult);
                        Command.Parameters.AddWithValue("@Notes", Notes);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


                        Connection.Open();
                        isAffected = Command.ExecuteNonQuery();
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }

            return (isAffected != 0);
        }
        public static bool GetTestByTestID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isAffected = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"select * from Tests where TestID = @TestID ; ";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TestID", TestID);

                       
                            Connection.Open();
                        using (SqlDataReader reader = Command.ExecuteReader())
                        {
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
                        }
                            
                        
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return isAffected;
        }


        public static bool GetTestByTestAppointmentID(int TestAppointmentID,ref int TestID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isAffected = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"select * from Tests where TestAppointmentID = @TestAppointmentID ; ";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


                        Connection.Open();
                        using (SqlDataReader reader = Command.ExecuteReader())
                        {
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
                        }
                        
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
       
            return isAffected;
        }

        public static bool GetLastTestPerPersoneAndLicenseClassAndTestType(int ApplicantPersonID, int LicenseClassID,int TestTypeID,ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isAffected = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
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
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                           Connection.Open();
                            using (SqlDataReader reader = Command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    TestID = (int)reader["TestID"];
                                    TestAppointmentID = (int)reader["TestAppointmentID"];
                                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                                    if (reader["Notes"] != System.DBNull.Value)
                                        Notes = reader["Notes"].ToString();
                                    else
                                        Notes = "";

                                    CreatedByUserID = (int)reader["CreatedByUserID"];

                                    isAffected = true;
                                }
                               
                            }
                        
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           

            return isAffected;
        }

    }
}

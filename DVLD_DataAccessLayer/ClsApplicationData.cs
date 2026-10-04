using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    public static class ClsApplicationData
    {
      

        public static DataTable SelectAllApplications()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
                {
                    string Query = @"select * from Applications order by ApplicationTypeID Desc;";
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
                clsEventViewer.SendEventLogApplication("Erorr in Git All Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            

            return dt;
        }

        public static int InsertApplication(int ApplicantPersonID, DateTime ApplicationDate,int ApplicationTypeID,short ApplicationStatus,DateTime LastStatusDate,float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"INSERT INTO Applications
                                 (ApplicantPersonID
                                 ,ApplicationDate
                                 ,ApplicationTypeID
                                 ,ApplicationStatus
                                 ,LastStatusDate
                                 ,PaidFees
                                 ,CreatedByUserID)
                                  VALUES
                                 (@ApplicantPersonID
                                 ,@ApplicationDate
                                 ,@ApplicationTypeID
                                 ,@ApplicationStatus
                                 ,@LastStatusDate
                                 ,@PaidFees
                                 ,@CreatedByUserID)SELECT SCOPE_IDENTITY()";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ApplicantPersonID", @ApplicantPersonID);
                        Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                        Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();

                        Connection.Open();
                        object obj = Command.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int Result))
                        {
                            ApplicationID = Result;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
           
            return ApplicationID;
        }

        public static bool UpdateApplication(int ID,int PersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float ParidFees, int CreatedByUser)
        {
            int isAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE Applications
                             SET ApplicantPersonID = @PersonID
                            ,ApplicationDate = @ApplicationDate
                            ,ApplicationTypeID = @ApplicationTypeID
                            ,ApplicationStatus = @ApplicationStatus
                            ,LastStatusDate = @LastStatusDate
                            ,PaidFees = @ParidFees
                            ,CreatedByUserID = @CreatedByUser
                             WHERE ApplicationID = @ID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ID", ID);
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                        Command.Parameters.AddWithValue("@ParidFees", ParidFees);
                        Command.Parameters.AddWithValue("@CreatedByUser", CreatedByUser);


                        Connection.Open();
                        isAffected = Command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Update Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           
            return (isAffected != 0);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int isAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"Delete FROM Applications
                             WHERE ApplicationID = @ApplicationID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        Connection.Open();
                        isAffected = Command.ExecuteNonQuery();
                    }

                }
                
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Delete Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            
            return (isAffected != 0);
        }

        public static bool GetApplicationByID(int ID,ref int PersonID,ref DateTime ApplicationDate,ref int ApplicationTypeID,ref short ApplicationStatus,ref DateTime LastStatusDate,ref float PaidFees, ref int CreatedByUserID)
        {
            bool isAffected = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT * FROM Applications WHERE ApplicationID= @ID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ID", ID);

                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                PersonID = (int)Reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)Reader["ApplicationDate"];
                                ApplicationTypeID = (int)Reader["ApplicationTypeID"];
                                ApplicationStatus = Convert.ToInt16(Reader["ApplicationStatus"]);
                                LastStatusDate = (DateTime)Reader["LastStatusDate"];
                                PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                isAffected = true;
                            }
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git Base Applicaion By ID" + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }

            return isAffected;
        }



        public static int GetActiveApplicationIDforLicenseClass(int PersonID, int ApplicationTypeID, int licenseClassID)
        {
            int ApplicationID = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"SELECT ActiveApplicationID = Applications.ApplicationID
                              FROM     Applications INNER JOIN
                              LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
				              where Applications.ApplicantPersonID = @PersonID  and Applications.ApplicationTypeID = @ApplicationTypeID and LocalDrivingLicenseApplications.LicenseClassID = @licenseClassID and Applications.ApplicationStatus = 1;";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        Command.Parameters.AddWithValue("@licenseClassID", licenseClassID);


                        Connection.Open();

                        object obj = Command.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int Result))
                        {
                            ApplicationID = Result;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Git Acitve Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           

            return ApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID,int NewStatus)
        {
             int isAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    string Query = @"UPDATE Applications
                    SET 
                     ApplicationStatus = @NewStatus
                     ,LastStatusDate = @LastDate

                     WHERE ApplicationID = @ApplicationID";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        Command.Parameters.AddWithValue("@NewStatus", NewStatus);
                        Command.Parameters.AddWithValue("@LastDate", DateTime.Now);

                        Connection.Open();
                        isAffected = Command.ExecuteNonQuery();
                    }
                    
                }
            }
            catch (Exception ex)
            {

                clsEventViewer.SendEventLogApplication("Erorr in Base Applicaion Update Status " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           
            return (isAffected != 0);

        }
    }
}

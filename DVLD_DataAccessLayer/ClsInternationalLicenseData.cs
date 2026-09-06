using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccessLayer
{
    public static class ClsInternationalLicenseData
    {

        public static int InsertInternationalLicense(
            int ApplicationID,
            int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            bool IsActive,
            int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"Update InternationalLicenses
                             set IsActive=0
                             where DriverID=@DriverID;


                  



                 INSERT INTO InternationalLicenses
                (ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                 IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                VALUES
                (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID,
                 @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    InternationalLicenseID = ID;
                }
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in insert new International License " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseID;
        }

 
        public static bool GetInternationalLicenseByInternationalLicenseID(
            int InternationalLicenseID,
            ref int ApplicationID,
            ref int DriverID,
            ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate,
            ref DateTime ExpirationDate,
            ref bool IsActive,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM InternationalLicenses
                             WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)Reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    IsActive = (bool)Reader["IsActive"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git International Licnese By ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }



        //public static bool GetInternationalLicenseByIssuedUsingLocalLicenseID(
        //    int IssuedUsingLocalLicenseID,
        //    ref int InternationalLicenseID,
        //    ref int DriverID,
        //    ref int ApplicationID,
        //    ref DateTime IssueDate,
        //    ref DateTime ExpirationDate,
        //    ref bool IsActive,
        //    ref int CreatedByUserID)
        //{
        //    bool IsFound = false;

        //    SqlConnection Connection =
        //        new SqlConnection(ClsConnectionSettings.ConnectionString);

        //    string Query = @"SELECT * FROM InternationalLicenses
        //                     WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";

        //    SqlCommand Command = new SqlCommand(Query, Connection);

        //    Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

        //    try
        //    {
        //        Connection.Open();

        //        SqlDataReader Reader = Command.ExecuteReader();

        //        if (Reader.Read())
        //        {
        //            IsFound = true;

        //            InternationalLicenseID = (int)Reader["InternationalLicenseID"];
        //            DriverID = (int)Reader["DriverID"];
        //            ApplicationID = (int)Reader["ApplicationID"];
        //            IssueDate = (DateTime)Reader["IssueDate"];
        //            ExpirationDate = (DateTime)Reader["ExpirationDate"];
        //            IsActive = (bool)Reader["IsActive"];
        //            CreatedByUserID = (int)Reader["CreatedByUserID"];
        //        }

        //        Reader.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }

        //    return IsFound;
        //}

        public static bool UpdateInternationalLicense(
            int InternationalLicenseID,
            int ApplicationID,
            int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            bool IsActive,
            int CreatedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"UPDATE InternationalLicenses
                             SET ApplicationID = @ApplicationID,
                                 DriverID = @DriverID,
                                 IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                                 IssueDate = @IssueDate,
                                 ExpirationDate = @ExpirationDate,
                                 IsActive = @IsActive,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Update International License " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }


        //public static bool DeleteInternationalLicense(int InternationalLicenseID)
        //{
        //    int RowsAffected = 0;

        //    SqlConnection Connection =
        //        new SqlConnection(ClsConnectionSettings.ConnectionString);

        //    string Query = @"DELETE FROM InternationalLicenses
        //                     WHERE InternationalLicenseID = @InternationalLicenseID";

        //    SqlCommand Command = new SqlCommand(Query, Connection);

        //    Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

        //    try
        //    {
        //        Connection.Open();
        //        RowsAffected = Command.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }

        //    return (RowsAffected > 0);
        //}


        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM InternationalLicenses ORDER BY IsActive,ExpirationDate desc";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dt.Load(Reader);

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git International Licneses " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }


        //public static bool IsInternationalLicenseExistByIssueLicenseID(int IssuedUsingLocalLicenseID)
        //{
        //    int isFounde = -1;

        //    SqlConnection Connection =
        //        new SqlConnection(ClsConnectionSettings.ConnectionString);

        //    string Query = @"SELECT Founde = 1
        //                     FROM     InternationalLicenses 
        //                     WHERE  (IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID);";

        //    SqlCommand Command = new SqlCommand(Query, Connection);

        //    Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

        //    try
        //    {
        //        Connection.Open();

        //        object obj = Command.ExecuteScalar();
        //        if (obj != null && int.TryParse(obj.ToString(), out int Result))
        //        {
        //            isFounde = Result;
        //        }
                 

                
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }

        //    return (isFounde != -1);
        //}

        


        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int ID = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT TOP 1 InternationalLicenseID
                             FROM InternationalLicenses
                             WHERE DriverID = @DriverID and GETDATE() >= IssueDate AND GETDATE() <= ExpirationDate
                             ORDER BY ExpirationDate DESC";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int Val))
                    ID = Val;
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git international License ID By Driver ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return ID;
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string query = @"
            SELECT    InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DriverID=@DriverID
                order by ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git Driver International Licenses " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public static class ClsLicenseData
    {

        public static bool GetLicenseByLicenseID(
    int LicenseID,
    ref int ApplicationID,
    ref int DriverID,
    ref int LicenseClass,
    ref DateTime IssueDate,
    ref DateTime ExpirationDate,
    ref string Notes,
    ref float PaidFees,
    ref bool IsActive,
    ref short IssueReason,
    ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM Licenses
                     WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClass = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];

                    if (Reader["Notes"] != DBNull.Value)
                        Notes = Reader["Notes"].ToString();
                    else
                        Notes = "";

                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                    IssueReason = Convert.ToInt16(Reader["IssueReason"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
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

            return IsFound;
        }

        public static bool DeleteLicense(int LicenseID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"DELETE FROM Licenses
                     WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            int RowAffected = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT Found = 1
                     FROM Licenses
                     WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                RowAffected = Command.ExecuteNonQuery();
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

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM Licenses";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dt.Load(Reader);

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

        public static bool IsLicenseExistByApplicationID(int ApplicationID)
        {
            int RowAffected = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT Found =1 FROM Licenses 
                     WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                RowAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return (RowAffected != -1);
        }


        //public static bool IsLicenseExistByDriverID(int DriverID)
        //{
        //    int RowAffected = -1;

        //    SqlConnection Connection =
        //        new SqlConnection(ClsConnectionSettings.ConnectionString);

        //    string Query = @"SELECT Found =1 FROM Licenses 
        //             WHERE DriverID = @DriverID";

        //    SqlCommand Command = new SqlCommand(Query, Connection);

        //    Command.Parameters.AddWithValue("@DriverID", DriverID);

        //    try
        //    {
        //        Connection.Open();

        //        RowAffected = Command.ExecuteNonQuery();

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }

        //    return (RowAffected > 0);
        //}


        //public static DataTable GetLicensesByDriverID(int DriverID)
        //{
        //    DataTable dt = new DataTable();

        //    SqlConnection Connection =
        //        new SqlConnection(ClsConnectionSettings.ConnectionString);

        //    string Query = @"SELECT * FROM Licenses
        //             WHERE DriverID = @DriverID
        //             ORDER BY IssueDate DESC";

        //    SqlCommand Command = new SqlCommand(Query, Connection);

        //    Command.Parameters.AddWithValue("@DriverID", DriverID);

        //    try
        //    {
        //        Connection.Open();

        //        SqlDataReader Reader = Command.ExecuteReader();

        //        if (Reader.HasRows)
        //            dt.Load(Reader);

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

        //    return dt;
        //}

        //public static int GetActiveLicenseIDByDriverID(int DriverID)
        //{
        //    int LicenseID = -1;

        //    SqlConnection Connection =
        //        new SqlConnection(ClsConnectionSettings.ConnectionString);

        //    string Query = @"SELECT TOP 1 LicenseID 
        //             FROM Licenses
        //             WHERE DriverID = @DriverID AND IsActive = 1
        //             ORDER BY IssueDate DESC";

        //    SqlCommand Command = new SqlCommand(Query, Connection);

        //    Command.Parameters.AddWithValue("@DriverID", DriverID);

        //    try
        //    {
        //        Connection.Open();

        //        object Result = Command.ExecuteScalar();

        //        if (Result != null && int.TryParse(Result.ToString(), out int ID))
        //        {
        //            LicenseID = ID;
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

        //    return LicenseID;
        //}
        public static int InsertLicense(
    int ApplicationID,
    int DriverID,
    int LicenseClass,
    DateTime IssueDate,
    DateTime ExpirationDate,
    string Notes,
    float PaidFees,
    bool IsActive,
    short IssueReason,
    int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"INSERT INTO Licenses
        (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate,
         Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
        VALUES
        (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate,
         @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
        SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    LicenseID = ID;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return LicenseID;
        }


        public static bool UpdateLicense(
    int LicenseID,
    int ApplicationID,
    int DriverID,
    int LicenseClass,
    DateTime IssueDate,
    DateTime ExpirationDate,
    string Notes,
    float PaidFees,
    bool IsActive,
    short IssueReason,
    int CreatedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"UPDATE Licenses SET
        ApplicationID = @ApplicationID,
        DriverID = @DriverID,
        LicenseClass = @LicenseClass,
        IssueDate = @IssueDate,
        ExpirationDate = @ExpirationDate,
        Notes = @Notes,
        PaidFees = @PaidFees,
        IsActive = @IsActive,
        IssueReason = @IssueReason,
        CreatedByUserID = @CreatedByUserID
        WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool GetLicenseByApplicationID(
    int ApplicationID,
    ref int LicenseID,
    ref int DriverID,
    ref int LicenseClass,
    ref DateTime IssueDate,
    ref DateTime ExpirationDate,
    ref string Notes,
    ref float PaidFees,
    ref bool IsActive,
    ref short IssueReason,
    ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    LicenseID = (int)Reader["LicenseID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClass = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];

                    Notes = Reader["Notes"] != DBNull.Value ? Reader["Notes"].ToString() : "";
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                    IssueReason = Convert.ToInt16(Reader["IssueReason"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
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

            return IsFound;
        }

        public static bool SetLicenseActiveStatus(int LicenseID, short IsActive)
        {
            int RowsAffected = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"UPDATE Licenses
                     SET IsActive = @IsActive
                     WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static int IsLicenseActive(int PersonID,int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT Licenses.LicenseID
                  FROM     Licenses INNER JOIN
                  Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN
                  LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                  WHERE  (Applications.ApplicantPersonID = @PersonID) AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID) AND (Licenses.IsActive = 1)";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();

                object obj = Command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(),out int ID))
                {
                    LicenseID = ID;
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

            return LicenseID;
        }

        public static bool DeactivatetLiccense(int LicenseID)
        {
            
            int RowsAffected = -1;
            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"UPDATE Licenses SET

                             IsActive = 0

                             WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);



            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ClsConnectionSettings.ConnectionString);

            string query = @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

    }
}

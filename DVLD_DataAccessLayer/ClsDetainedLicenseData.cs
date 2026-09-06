using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccessLayer
{
    public static class ClsDetainedLicenseData
    {
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM DetainedLicenses_View";

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
                clsEventViewer.SendEventLogApplication("Erorr in Get All Detained Licenses " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static int InsertDetainedLicense(
            int LicenseID,
            DateTime DetainDate,
            float FineFees,
            int CreatedByUserID,
            short IsReleased,
            DateTime? ReleaseDate,
            int? ReleasedByUserID,
            int? ReleaseApplicationID)
        {
            int DetainID = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"INSERT INTO DetainedLicenses
            (LicenseID, DetainDate, FineFees, CreatedByUserID,
             IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
            VALUES
            (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID,
             @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID);
            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsReleased", IsReleased);

            Command.Parameters.AddWithValue("@ReleaseDate",
                ReleaseDate.HasValue ? (object)ReleaseDate.Value : DBNull.Value);

            Command.Parameters.AddWithValue("@ReleasedByUserID",
                ReleasedByUserID.HasValue ? (object)ReleasedByUserID.Value : DBNull.Value);

            Command.Parameters.AddWithValue("@ReleaseApplicationID",
                ReleaseApplicationID.HasValue ? (object)ReleaseApplicationID.Value : DBNull.Value);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    DetainID = ID;
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Insert Detained License " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return DetainID;
        }

        public static bool UpdateDetainedLicense(
            int DetainID,
            int LicenseID,
            DateTime DetainDate,
            float FineFees,
            int CreatedByUserID,
            short IsReleased,
            DateTime? ReleaseDate,
            int? ReleasedByUserID,
            int? ReleaseApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"UPDATE DetainedLicenses
            SET LicenseID = @LicenseID,
                DetainDate = @DetainDate,
                FineFees = @FineFees,
                CreatedByUserID = @CreatedByUserID,
                IsReleased = @IsReleased,
                ReleaseDate = @ReleaseDate,
                ReleasedByUserID = @ReleasedByUserID,
                ReleaseApplicationID = @ReleaseApplicationID
            WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsReleased", IsReleased);

            Command.Parameters.AddWithValue("@ReleaseDate",
                ReleaseDate.HasValue ? (object)ReleaseDate.Value : DBNull.Value);

            Command.Parameters.AddWithValue("@ReleasedByUserID",
                ReleasedByUserID.HasValue ? (object)ReleasedByUserID.Value : DBNull.Value);

            Command.Parameters.AddWithValue("@ReleaseApplicationID",
                ReleaseApplicationID.HasValue ? (object)ReleaseApplicationID.Value : DBNull.Value);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Update Detained License " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }


        public static bool GetDetainedLicenseByLicenseID(
            int LicenseID,
            ref int DetainID,
            ref DateTime DetainDate,
            ref float FineFees,
            ref int CreatedByUserID,
            ref short IsReleased,
            ref DateTime? ReleaseDate,
            ref int? ReleasedByUserID,
            ref int? ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM DetainedLicenses
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

                    DetainID = (int)Reader["DetainID"];
                    DetainDate = (DateTime)Reader["DetainDate"];
                    FineFees = Convert.ToSingle(Reader["FineFees"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    IsReleased = Convert.ToInt16(Reader["IsReleased"]);

                    ReleaseDate = Reader["ReleaseDate"] == DBNull.Value
                        ? (DateTime?)null
                        : (DateTime)Reader["ReleaseDate"];

                    ReleasedByUserID = Reader["ReleasedByUserID"] == DBNull.Value
                        ? (int?)null
                        : (int)Reader["ReleasedByUserID"];

                    ReleaseApplicationID = Reader["ReleaseApplicationID"] == DBNull.Value
                        ? (int?)null
                        : (int)Reader["ReleaseApplicationID"];
                }

                Reader.Close();
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool GetDetainedLicenseByDetainedID(
            int DetainID,
            ref int LicenseID,
            ref DateTime DetainDate,
            ref float FineFees,
            ref int CreatedByUserID,
            ref short IsReleased,
            ref DateTime? ReleaseDate,
            ref int? ReleasedByUserID,
            ref int? ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM DetainedLicenses
                             WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    LicenseID = (int)Reader["LicenseID"];
                    DetainDate = (DateTime)Reader["DetainDate"];
                    FineFees = Convert.ToSingle(Reader["FineFees"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    IsReleased = Convert.ToInt16(Reader["IsReleased"]);

                    ReleaseDate = Reader["ReleaseDate"] == DBNull.Value
                        ? (DateTime?)null
                        : (DateTime)Reader["ReleaseDate"];

                    ReleasedByUserID = Reader["ReleasedByUserID"] == DBNull.Value
                        ? (int?)null
                        : (int)Reader["ReleasedByUserID"];

                    ReleaseApplicationID = Reader["ReleaseApplicationID"] == DBNull.Value
                        ? (int?)null
                        : (int)Reader["ReleaseApplicationID"];
                }

                Reader.Close();
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool DeleteDetainedLicense(int DetainID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"DELETE FROM DetainedLicenses
                             WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT 1 FROM DetainedLicenses
                             WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                IsFound = (Result != null);
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
        public static bool ReleaseLicense(int DetainID, int ReleasedByUserID,int ReleaseApplicationID)
        {

            int RowAffected = -1;
            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"update DetainedLicenses
                             set 
                             IsReleased = 1,
                             ReleaseDate = @ReleaseDate,
                             ReleasedByUserID = @ReleasedByUserID,
                             ReleaseApplicationID = @ReleaseApplicationID
                             where DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();


                RowAffected = Command.ExecuteNonQuery();
               
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in Release License " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            finally
            {
                Connection.Close();
            }

            return (RowAffected > -1);
        }
    }
}

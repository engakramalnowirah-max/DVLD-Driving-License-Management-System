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
    public static class ClsDetainedLicenseData
    {
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT * FROM DetainedLicenses_View";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {


                        Connection.Open();

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.HasRows)
                                dt.Load(Reader);
                        }

                       
                    }
                    
                }
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Get All Detained Licenses " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

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
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"INSERT INTO DetainedLicenses
                                    (LicenseID, DetainDate, FineFees, CreatedByUserID,
                                     IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
                                    VALUES
                                    (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID,
                                     @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID);
                                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

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


                        Connection.Open();

                        object Result = Command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int ID))
                            DetainID = ID;
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Insert Detained License " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

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
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

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

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

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


                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Update Detained License " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

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
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT * FROM DetainedLicenses
                             WHERE LicenseID = @LicenseID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);


                        Connection.Open();

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

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
                        }
                    }

                }
                
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
          

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
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                { 

                    string Query = @"SELECT * FROM DetainedLicenses
                             WHERE DetainID = @DetainID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    { 

                        Command.Parameters.AddWithValue("@DetainID", DetainID);

                        
                            Connection.Open();

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

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
                        }

                            
                        
                    }
                }
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
           

            return IsFound;
        }

        public static bool DeleteDetainedLicense(int DetainID)
        {
            int RowsAffected = 0;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"DELETE FROM DetainedLicenses
                             WHERE DetainID = @DetainID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@DetainID", DetainID);


                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
           

            return (RowsAffected > 0);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT 1 FROM DetainedLicenses
                             WHERE LicenseID = @LicenseID AND IsReleased = 0";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@LicenseID", LicenseID);


                        Connection.Open();

                        object Result = Command.ExecuteScalar();

                        IsFound = (Result != null);
                    }
                    
                }
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in insert new Base Applicaion " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
           

            return IsFound;
        }
        public static bool ReleaseLicense(int DetainID, int ReleasedByUserID,int ReleaseApplicationID)
        {

            int RowAffected = -1;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"update DetainedLicenses
                             set 
                             IsReleased = 1,
                             ReleaseDate = @ReleaseDate,
                             ReleasedByUserID = @ReleasedByUserID,
                             ReleaseApplicationID = @ReleaseApplicationID
                             where DetainID = @DetainID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                        Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                        Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                        Command.Parameters.AddWithValue("@DetainID", DetainID);


                        Connection.Open();


                        RowAffected = Command.ExecuteNonQuery();
                    }

                    
                }
            }
            catch (Exception ex) { clsEventViewer.SendEventLogApplication("Erorr in Release License " + ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            

            return (RowAffected > -1);
        }
    }
}

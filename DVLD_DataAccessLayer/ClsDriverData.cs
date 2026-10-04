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
    public static class ClsDriverData
    {
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"INSERT INTO Drivers
                            (PersonID, CreatedByUserID, CreatedDate)
                            VALUES
                            (@PersonID, @CreatedByUserID, @CreatedDate);
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                        Connection.Open();

                        object Result = Command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int ID))
                        {
                            DriverID = ID;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in insert new Driver " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            

            return DriverID;
        }

        public static bool GetDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT * FROM Drivers WHERE DriverID = @DriverID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@DriverID", DriverID);

                        Connection.Open();

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.Read())
                            {
                                IsFound = true;

                                PersonID = (int)Reader["PersonID"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                CreatedDate = (DateTime)Reader["CreatedDate"];
                            }

                           
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git Driver By ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
          

            return IsFound;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int RowsAffected = 0;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"UPDATE Drivers
                             SET PersonID = @PersonID,
                                 CreatedByUserID = @CreatedByUserID,
                                 CreatedDate = @CreatedDate
                             WHERE DriverID = @DriverID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@DriverID", DriverID);
                        Command.Parameters.AddWithValue("@PersonID", PersonID);
                        Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Update " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
           

            return (RowsAffected > 0);
        }

        public static bool DeleteDriver(int DriverID)
        {
            int RowsAffected = 0;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"DELETE FROM Drivers WHERE DriverID = @DriverID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@DriverID", DriverID);


                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();

                    }
                    
                }
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Delete Driver " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            

            return (RowsAffected > 0);
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT * FROM Drivers_View ";

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
                clsEventViewer.SendEventLogApplication("Erorr in Git All Drivers " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
           

            return dt;
        }

        public static bool IsDriverExist(int DriverID)
        {
            int RowAffected = -1;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT Fuond = 1 FROM Drivers WHERE DriverID = @DriverID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@DriverID", DriverID);


                        Connection.Open();


                        RowAffected = Command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in is Driver Exist " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
           

            return (RowAffected > 0);
        }

        public static bool GetDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection =
                    new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string Query = @"SELECT * FROM Drivers WHERE PersonID = @PersonID";

                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {

                        Command.Parameters.AddWithValue("@PersonID", PersonID);


                        Connection.Open();

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.Read())
                            {
                                IsFound = true;

                                DriverID = (int)Reader["DriverID"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                CreatedDate = (DateTime)Reader["CreatedDate"];
                            }

                           
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git Driver By Person ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
         

            return IsFound;
        }
    }
}
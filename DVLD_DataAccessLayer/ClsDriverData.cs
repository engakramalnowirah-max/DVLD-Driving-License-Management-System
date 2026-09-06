using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccessLayer
{
    public static class ClsDriverData
    {
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"INSERT INTO Drivers
                            (PersonID, CreatedByUserID, CreatedDate)
                            VALUES
                            (@PersonID, @CreatedByUserID, @CreatedDate);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    DriverID = ID;
                }
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in insert new Driver " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return DriverID;
        }

        public static bool GetDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)Reader["PersonID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)Reader["CreatedDate"];
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git Driver By ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"UPDATE Drivers
                             SET PersonID = @PersonID,
                                 CreatedByUserID = @CreatedByUserID,
                                 CreatedDate = @CreatedDate
                             WHERE DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Update " + ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteDriver(int DriverID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"DELETE FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Delete Driver " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM Drivers_View ";

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
                clsEventViewer.SendEventLogApplication("Erorr in Git All Drivers " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static bool IsDriverExist(int DriverID)
        {
            int RowAffected = -1;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT Fuond = 1 FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();


                RowAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in is Driver Exist " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return (RowAffected > 0);
        }

        public static bool GetDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(ClsConnectionSettings.ConnectionString);

            string Query = @"SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    DriverID = (int)Reader["DriverID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)Reader["CreatedDate"];
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventViewer.SendEventLogApplication("Erorr in Git Driver By Person ID " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
    }
}
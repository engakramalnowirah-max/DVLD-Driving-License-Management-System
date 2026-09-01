using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class ClsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public ClsPresone PersoneInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }


        public ClsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.PersoneInfo = null;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }


        private ClsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersoneInfo = ClsPresone.Find(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;

            Mode = enMode.Update;
        }


        private bool _AddNewDriver()
        {
            this.DriverID = ClsDriverData.AddNewDriver(
                this.PersonID,
                this.CreatedByUserID,
                this.CreatedDate
            );

            return (this.DriverID != -1);
        }


        private bool _UpdateDriver()
        {
            return ClsDriverData.UpdateDriver(
                this.DriverID,
                this.PersonID,
                this.CreatedByUserID,
                this.CreatedDate
            );
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDriver();

                default:
                    return false;
            }
        }

        public static ClsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            bool IsFound = ClsDriverData.GetDriverByID(
                DriverID,
                ref PersonID,
                ref CreatedByUserID,
                ref CreatedDate
            );

            if (IsFound)
            {
                return new ClsDriver(
                    DriverID,
                    PersonID,
                    CreatedByUserID,
                    CreatedDate
                );
            }
            else
            {
                return null;
            }
        }

        public static ClsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            bool IsFound = ClsDriverData.GetDriverByPersonID(
                PersonID,
                ref DriverID,
                ref CreatedByUserID,
                ref CreatedDate
            );

            if (IsFound)
            {
                return new ClsDriver(
                    DriverID,
                    PersonID,
                    CreatedByUserID,
                    CreatedDate
                );
            }
            else
            {
                return null;
            }
        }
        public static bool Delete(int DriverID)
        {
            return ClsDriverData.DeleteDriver(DriverID);
        }

        public static bool IsDriverExist(int DriverID)
        {
            return ClsDriverData.IsDriverExist(DriverID);
        }

        public static DataTable GetAllDrivers()
        {
            return ClsDriverData.GetAllDrivers();
        }


        public static DataTable GetLicenses(int DriverID)
        {
            return ClsLicense.GetLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return ClsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }
    }
}

using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class ClsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public ClsUser CreatedByUserInfo { get; set; }
        public short IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public int? ReleaseApplicationID { get; set; }

        public ClsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.CreatedByUserInfo = null;
            this.IsReleased = 0;
            this.ReleaseDate = null;
            this.ReleasedByUserID = null;
            this.ReleaseApplicationID = null;

            Mode = enMode.AddNew;
        }

        private ClsDetainedLicense(int DetainID,int LicenseID,DateTime DetainDate,float FineFees,int CreatedByUserID,short IsReleased,DateTime? ReleaseDate,int? ReleasedByUserID,int? ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = ClsUser.Find(CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            this.DetainID = ClsDetainedLicenseData.InsertDetainedLicense(this.LicenseID,this.DetainDate,this.FineFees,this.CreatedByUserID,this.IsReleased,this.ReleaseDate,this.ReleasedByUserID,this.ReleaseApplicationID
            );

            return (this.DetainID != -1);
        }

        private bool _Update()
        {
            return ClsDetainedLicenseData.UpdateDetainedLicense(
                this.DetainID,
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID,
                this.IsReleased,
                this.ReleaseDate,
                this.ReleasedByUserID,
                this.ReleaseApplicationID
            );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }


        public static ClsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = -1;
            short IsReleased = 0;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = -1;
            int? ReleaseApplicationID = -1;

            bool IsFound = ClsDetainedLicenseData.GetDetainedLicenseByLicenseID(
                LicenseID,
                ref DetainID,
                ref DetainDate,
                ref FineFees,
                ref CreatedByUserID,
                ref IsReleased,
                ref ReleaseDate,
                ref ReleasedByUserID,
                ref ReleaseApplicationID
            );

            if (IsFound)
            {
                return new ClsDetainedLicense(
                    DetainID,
                    LicenseID,
                    DetainDate,
                    FineFees,
                    CreatedByUserID,
                    IsReleased,
                    ReleaseDate,
                    ReleasedByUserID,
                    ReleaseApplicationID
                );
            }

            return null;
        }

        public static ClsDetainedLicense FindByDetainedID(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = -1;
            short IsReleased = 0;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = -1;
            int? ReleaseApplicationID = -1;

            bool IsFound = ClsDetainedLicenseData.GetDetainedLicenseByDetainedID(
                DetainID,
                ref LicenseID,
                ref DetainDate,
                ref FineFees,
                ref CreatedByUserID,
                ref IsReleased,
                ref ReleaseDate,
                ref ReleasedByUserID,
                ref ReleaseApplicationID
            );

            if (IsFound)
            {
                return new ClsDetainedLicense(
                    DetainID,
                    LicenseID,
                    DetainDate,
                    FineFees,
                    CreatedByUserID,
                    IsReleased,
                    ReleaseDate,
                    ReleasedByUserID,
                    ReleaseApplicationID
                );
            }

            return null;
        }

        public static bool Delete(int DetainID)
        {
            return ClsDetainedLicenseData.DeleteDetainedLicense(DetainID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return ClsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return ClsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public bool ReleaseDetaindLicense(int ReleaseByUserID,int ReleaseApplicationID)
        {
            return ClsDetainedLicenseData.ReleaseLicense(this.DetainID, ReleaseByUserID, ReleaseApplicationID);
        }
    }
}

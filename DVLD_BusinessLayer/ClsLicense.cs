using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DVLD_BusinessLayer.ClsLicense;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class ClsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
         enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew =2, ReplacementForDamaged = 3,  ReplacementForLost = 4}

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public ClsApplication ApplicationInfo {get;set;}
        public int DriverID { get; set; }
        public ClsDriver DriverInfo { get; set; }
        public int LicenseClassID { get; set; }
        public ClsLicenseClass LicenseClassInfo {get;set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public bool IsDetaind { get { return ClsDetainedLicense.IsLicenseDetained(this.LicenseID); } }
        public ClsDetainedLicense DetainedInfo { get; set; }

        public ClsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.ApplicationInfo = new ClsApplication();
            this.DriverID = -1;
            this.DriverInfo = null;
            this.LicenseClassID = -1;
            this.LicenseClassInfo = new ClsLicenseClass();
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            this.DetainedInfo = new ClsDetainedLicense();
            Mode = enMode.AddNew;
        }


        private ClsLicense(
            int LicenseID,
            int ApplicationID,
            int DriverID,
            int LicenseClassID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            string Notes,
            float PaidFees,
            bool IsActive,
            enIssueReason IssueReason,
            int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = ClsApplication.FindBaseApplicationByID(ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = ClsDriver.FindByDriverID(DriverID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = ClsLicenseClass.Find(LicenseClassID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.DetainedInfo = ClsDetainedLicense.FindByLicenseID(this.LicenseID);
            Mode = enMode.Update;
        }


        private bool _AddNewLicense()
        {
            this.LicenseID = ClsLicenseData.InsertLicense(
                this.ApplicationID,
                this.DriverID,
                this.LicenseClassID,
                this.IssueDate,
                this.ExpirationDate,
                this.Notes,
                this.PaidFees,
                this.IsActive,
                (short)this.IssueReason,
                this.CreatedByUserID);

            return (this.LicenseID != -1);
        }


        private bool _UpdateLicense()
        {
            return ClsLicenseData.UpdateLicense(
                this.LicenseID,
                this.ApplicationID,
                this.DriverID,
                this.LicenseClassID,
                this.IssueDate,
                this.ExpirationDate,
                this.Notes,
                this.PaidFees,
                this.IsActive,
                (short)this.IssueReason,
                this.CreatedByUserID);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();

                default:
                    return false;
            }
        }


        public static ClsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            short IssueReason = 0;
            int CreatedByUserID = -1;

            bool IsFound = ClsLicenseData.GetLicenseByLicenseID(
                LicenseID,
                ref ApplicationID,
                ref DriverID,
                ref LicenseClassID,
                ref IssueDate,
                ref ExpirationDate,
                ref Notes,
                ref PaidFees,
                ref IsActive,
                ref IssueReason,
                ref CreatedByUserID);

            if (IsFound)
            {
                return new ClsLicense(
                    LicenseID,
                    ApplicationID,
                    DriverID,
                    LicenseClassID,
                    IssueDate,
                    ExpirationDate,
                    Notes,
                    PaidFees,
                    IsActive,
                    (enIssueReason)IssueReason,
                    CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static ClsLicense FindByApplicationID(int ApplicationID)
        {
            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            short IssueReason = 0;
            int CreatedByUserID = -1;

            bool IsFound = ClsLicenseData.GetLicenseByApplicationID(
                ApplicationID,
                ref LicenseID,
                ref DriverID,
                ref LicenseClassID,
                ref IssueDate,
                ref ExpirationDate,
                ref Notes,
                ref PaidFees,
                ref IsActive,
                ref IssueReason,
                ref CreatedByUserID);

            if (IsFound)
            {
                return new ClsLicense(
                    LicenseID,
                    ApplicationID,
                    DriverID,
                    LicenseClassID,
                    IssueDate,
                    ExpirationDate,
                    Notes,
                    PaidFees,
                    IsActive,
                    (enIssueReason)IssueReason,
                    CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public static bool Delete(int LicenseID)
        {
            return ClsLicenseData.DeleteLicense(LicenseID);
        }

        public static int IsLicenseActive(int PersonID,int LicenseClassID)
        {
            return ClsLicenseData.IsLicenseActive(PersonID, LicenseClassID);
        }

       
        public static bool IsLicenseExist(int LicenseID)
        {
            return ClsLicenseData.IsLicenseExist(LicenseID);
        }

        public static bool IsLicenseExistByApplicationID(int ApplicationID )
        {
            return ClsLicenseData.IsLicenseExistByApplicationID(ApplicationID);
        }

        public bool DeactivateCurrentLiccense()
        {
           return ClsLicenseData.DeactivatetLiccense(this.LicenseID);
        }
        public ClsLicense RenewLicense(string Notes,int CreateByUserID)
        {
            

            ClsApplication Application = new ClsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = ClsApplication.enApplicationStatus.Completed;
            Application.ApplicationTypeID = ClsApplicationType.Find((int)ClsApplication.enApplicationType.RenewDrivingLicenseService).ID;
            Application.LastStatusDate = DateTime.Now;
            Application.CreatedByUserID = CreateByUserID;
            Application.PaidFees = ClsApplicationType.Find((int)ClsApplication.enApplicationType.RenewDrivingLicenseService).Fees;

            if (!Application.Save())
            {
                return null;
            }
   
            ClsLicense _RenewLicense = new ClsLicense ();

            int DefaultValidityLength = this.LicenseClassInfo.DefaultValidityLength;

            _RenewLicense.ApplicationID = Application.ApplicationID; ;
            _RenewLicense.CreatedByUserID = CreateByUserID;
            _RenewLicense.IssueReason = ClsLicense.enIssueReason.Renew;
            _RenewLicense.DriverID = this.DriverID;
            _RenewLicense.LicenseClassID = this.LicenseClassID;
            _RenewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            _RenewLicense.Notes = Notes;
            _RenewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            _RenewLicense.IsActive = true;

            if(!_RenewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLiccense();

            return _RenewLicense;

        }


        public ClsLicense Replacmente(enIssueReason IssueReason, int CreateByUserID)
        {


            ClsApplication Application = new ClsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = ClsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.CreatedByUserID = CreateByUserID;
            if (IssueReason == ClsLicense.enIssueReason.ReplacementForDamaged)
            {
                Application.ApplicationTypeID = (int)ClsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense;
                Application.PaidFees = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense).Fees;
            }
            else
            {
                Application.ApplicationTypeID = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReplacementforaLostDrivingLicense).ID;
                Application.PaidFees = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReplacementforaLostDrivingLicense).Fees;
            }

            if (!Application.Save())
            {
                return null;
            }

            ClsLicense ReplacLicense = new ClsLicense();

            int DefaultValidityLength = this.LicenseClassInfo.DefaultValidityLength;

            ReplacLicense.ApplicationID = Application.ApplicationID; ;
            ReplacLicense.CreatedByUserID = CreateByUserID;
            ReplacLicense.IssueReason = IssueReason;
            ReplacLicense.DriverID = this.DriverID;
            ReplacLicense.LicenseClassID = this.LicenseClassID;
            ReplacLicense.PaidFees = 0;
            ReplacLicense.Notes = Notes;
            ReplacLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            ReplacLicense.IsActive = true;

            if (!ReplacLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLiccense();

            return ReplacLicense;

        }

        public Boolean IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }

        public bool ReleaseDetainedLicense(int ReleaseByUserID,ref int ReleaseApplicatonID)
        {


            ClsApplication Application = new ClsApplication();
            Application.ApplicationStatus = ClsApplication.enApplicationStatus.Completed;
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.CreatedByUserID = ReleaseByUserID;
            Application.LastStatusDate = DateTime.Now;
            Application.ApplicationTypeID = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ID;
            Application.PaidFees = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees;

            if (!Application.Save())
            {
                return false;
            }

            ReleaseApplicatonID = Application.ApplicationID;
            bool isRelease = DetainedInfo.ReleaseDetaindLicense(ReleaseByUserID, Application.ApplicationID);




            return isRelease;

        }

        public static DataTable GetLicenses(int DriverID)
        {
            return ClsLicenseData.GetDriverLicenses(DriverID);
        }

        public int DetainedLicense(float FindFees, int CreateByUserID)
        {
             ClsDetainedLicense _DetainedLicense = new ClsDetainedLicense();


            _DetainedLicense.LicenseID = this.LicenseID;
            _DetainedLicense.DetainDate = DateTime.Now;
            _DetainedLicense.FineFees = FindFees;
            _DetainedLicense.IsReleased = 0;
            _DetainedLicense.CreatedByUserID = CreateByUserID;
            _DetainedLicense.ReleaseApplicationID = null;
            _DetainedLicense.ReleaseDate = null;
            _DetainedLicense.ReleasedByUserID = null;

            if (!_DetainedLicense.Save())
            {
                return -1;
            }

            return _DetainedLicense.DetainID;

        }
    }
}
    


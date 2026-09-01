using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class ClsInternationalLicense:ClsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public ClsDriver DriverInfo { get; set; }


        public ClsInternationalLicense()
        {




            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.DriverInfo = new ClsDriver();
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;

            Mode = enMode.AddNew;
        }


        private ClsInternationalLicense(
            int AppllicationID, 
            int ApplicantPersonID, 
            DateTime ApplicationDate, 
            int ApplicationTypeID, 
            enApplicationStatus ApplicationStatus, 
            DateTime LastStatusDate, float 
            PaidFees, 
            int CreatedByUserID,
            int InternationalLicenseID,
            int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            bool IsActive
            )
        {
            //this is for base Class
            this.ApplicationID = AppllicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = ClsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = ClsUser.Find(CreatedByUserID);

            //this is for sub Class
            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.DriverInfo = ClsDriver.FindByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }


        private bool _AddNew()
        {
            this.InternationalLicenseID =
                ClsInternationalLicenseData.InsertInternationalLicense(
                    this.ApplicationID,
                    this.DriverID,
                    this.IssuedUsingLocalLicenseID,
                    this.IssueDate,
                    this.ExpirationDate,
                    this.IsActive,
                    this.CreatedByUserID
                );

            return (this.InternationalLicenseID != -1);
        }


        private bool _Update()
        {
            return ClsInternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID,
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID
            );
        }

        public bool Save()
        {
            base.Mode = (ClsApplication.enMode)Mode;
            if (!base.Save())
                return false;


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

        public static ClsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = ClsInternationalLicenseData.GetInternationalLicenseByInternationalLicenseID(
                InternationalLicenseID,
                ref ApplicationID,
                ref DriverID,
                ref IssuedUsingLocalLicenseID,
                ref IssueDate,
                ref ExpirationDate,
                ref IsActive,
                ref CreatedByUserID
            );

            if (IsFound)
            {
                ClsApplication Application = ClsApplication.FindBaseApplicationByID( ApplicationID );
                return new ClsInternationalLicense(
                                  ApplicationID,
                                  Application.ApplicantPersonID,
                                  Application.ApplicationDate,
                                  Application.ApplicationTypeID,
                                  Application.ApplicationStatus,
                                  Application.LastStatusDate,
                                  Application.PaidFees,
                                  Application.CreatedByUserID,
                                  InternationalLicenseID,
                                  DriverID,
                                  IssuedUsingLocalLicenseID,
                                  IssueDate,
                                  ExpirationDate,
                                  IsActive
                );
            }
            else
            {
                return null;
            }
        }

        //public static ClsInternationalLicense FindByIssuedUsingLocalLicenseID(int IssuedUsingLocalLicenseID)
        //{
        //    int InternationalLicenseID = -1;
        //    int DriverID = -1;
        //    int ApplicationID = -1;
        //    DateTime IssueDate = DateTime.Now;
        //    DateTime ExpirationDate = DateTime.Now;
        //    bool IsActive = false;
        //    int CreatedByUserID = -1;

        //    bool IsFound = ClsInternationalLicenseData.GetInternationalLicenseByIssuedUsingLocalLicenseID(
        //        IssuedUsingLocalLicenseID,
        //        ref InternationalLicenseID,
        //        ref DriverID,
        //        ref ApplicationID,
        //        ref IssueDate,
        //        ref ExpirationDate,
        //        ref IsActive,
        //        ref CreatedByUserID
        //    );

        //    if (IsFound)
        //    {
        //        return new ClsInternationalLicense(
        //            InternationalLicenseID,
        //            ApplicationID,
        //            DriverID,
        //            IssuedUsingLocalLicenseID,
        //            IssueDate,
        //            ExpirationDate,
        //            IsActive,
        //            CreatedByUserID
        //        );
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}







        public static DataTable GetAll()
        {
            return ClsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return ClsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return ClsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccessLayer;
using System.Runtime.CompilerServices;
using System.Deployment.Internal;

namespace DVLD_BusinessLayer
{
    public class ClsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType { NewLocalDrivingLicenseService =1, RenewDrivingLicenseService =2, ReplacementforaLostDrivingLicense = 3, ReplacementforaDamagedDrivingLicense  =4,
                                       ReleaseDetainedDrivingLicsense  =5, NewInternationalLicense =6, RetakeTest =7};

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        

        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public ClsApplicationType ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get 
            { 
               switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "unKnown";

               }
                    
            }
            

        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public ClsUser UserInfo;
       



        public ClsApplication()
        {
            this.ApplicationID = 0;
            this.ApplicantPersonID = 0;
            
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 0;
            this.ApplicationTypeInfo = new ClsApplicationType();
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = 0;
            this.UserInfo = new ClsUser();
            this.Mode = enMode.AddNew;

        }
        private ClsApplication(int AppllicationID, int ApplicantPersonID, DateTime ApplicationDate,int ApplicationTypeID ,enApplicationStatus ApplicationStatus,DateTime LastStatusDate,float PaidFees,int CreatedByUserID)
        {
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
            
            
            this.Mode = enMode.Update;
        }
        private bool _EditAppliction()
        {
            return ClsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (short)this.ApplicationStatus, this.LastStatusDate,this.PaidFees, this.CreatedByUserID);
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = ClsApplicationData.InsertApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (short)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != 0);
        }
        public bool Save()
        {
            switch(this.Mode)
            {
                case enMode.Update:
                    return _EditAppliction();
                case enMode.AddNew:
                    if(_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        public static DataTable GetAllApplications()
        {
            return ClsApplicationData.SelectAllApplications();
        }
        public static ClsApplication FindBaseApplicationByID(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            float PaidFees = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            short ApplicationStatus = -1;

            if(ClsApplicationData.GetApplicationByID(ApplicationID,ref ApplicantPersonID,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate,ref PaidFees,ref CreatedByUserID))
            {
                return new ClsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }

        public bool Cancelled()
        {
            return ClsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }
        public bool SetCompleted()
        {
            return ClsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }
        public static bool DeletebaseApplication(int ApplicationID )
        {
            return ClsApplicationData.DeleteApplication(ApplicationID);
        }

        public static int GetActiveActicationIDForLicensClass(int PersonID, int ApplicationTypeID, int licenseClassID)
        {
            return ClsApplicationData.GetActiveApplicationIDforLicenseClass(PersonID, ApplicationTypeID, licenseClassID);
        }


    }
}

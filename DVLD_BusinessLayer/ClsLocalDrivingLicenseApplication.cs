 using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class ClsLocalDrivingLicenseApplication : ClsApplication
    {
        public enum enMode { AddNew = 0 , Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }
        
        public int LicenseClassID { get; set; }
        public ClsLicenseClass LicenseClassInfo;
        public ClsPresone PersonInfo;
        public string PersonFullName
        {
            get
            {
                return PersonInfo.FirstName + " " + PersonInfo.SecondName + " " + PersonInfo.ThirdName + " " + PersonInfo.LastName;
            }
        }

        public ClsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            
            this.LicenseClassID = -1;
            LicenseClassInfo = new ClsLicenseClass();
            this.PersonInfo = new ClsPresone();
            Mode = enMode.AddNew;

        }

        private ClsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplication,int ApplicationID,int LicenseClassID
                                                  ,int ApplicatPersonID,DateTime ApplicationDate,int ApplicationTypeID
                                                  ,enApplicationStatus ApplicationStatus,DateTime LastStatusDate,float PaidFees
                                                  ,int CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = ClsLicenseClass.Find(LicenseClassID);
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicatPersonID;
            this.PersonInfo = ClsPresone.Find(ApplicatPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }
        
        private bool _EditLocalDrivingLicenseApplication()
        {
            return ClsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplicationToDB(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }
        private bool _AddNewLocalDrivinglicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = ClsLocalDrivingLicenseApplicationsData.InsertNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != 0);
        }
        public bool Save()
        {
            base.Mode = (ClsApplication.enMode)Mode;

            if(!base.Save())
                return false;

            switch(Mode)
            {
                case enMode.Update:
                    return _EditLocalDrivingLicenseApplication();
                case enMode.AddNew:
                    if (_AddNewLocalDrivinglicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                        default:
                    return false;
                    
            }
        }

        public static ClsLocalDrivingLicenseApplication Find(int localDrivinglicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            bool IsFound = ClsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationByID(localDrivinglicenseApplicationID, ref ApplicationID, ref LicenseClassID);
            
            

            if(IsFound)
            {
                ClsApplication Application = ClsApplication.FindBaseApplicationByID(ApplicationID);
              
                return new ClsLocalDrivingLicenseApplication(localDrivinglicenseApplicationID, Application.ApplicationID, LicenseClassID, Application.ApplicantPersonID
                            , Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static ClsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int localDrivinglicenseApplication = -1, LicenseClassID = -1;
            bool IsFound = ClsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationByApplicationID(ApplicationID, ref localDrivinglicenseApplication, ref LicenseClassID);



            if (IsFound)
            {
                ClsApplication Application = ClsApplication.FindBaseApplicationByID(ApplicationID);

                return new ClsLocalDrivingLicenseApplication(localDrivinglicenseApplication, Application.ApplicationID, LicenseClassID, Application.ApplicantPersonID
                            , Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public  bool Delete(int LoclDrivingLicenseApplicationID)
        {
            return ClsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(LoclDrivingLicenseApplicationID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return ClsLocalDrivingLicenseApplicationsData.SelectAllLocalDrivingLicenseApplications();
        }

        public bool DoesAttendTestType(ClsTestType.enTestType TestType)
        {
            return ClsLocalDrivingLicenseApplicationsData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID,(int) TestType);
        }

        public bool DoesPassTestType(ClsTestType.enTestType TestTypeID)
        {
            return ClsLocalDrivingLicenseApplicationsData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }




        public bool IsThereAnActiveScheduledTest(ClsTestType.enTestType TestTypeID)
        {
            return ClsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, ClsTestType.enTestType TestTypeID)
        {
            return ClsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public ClsTest GetLastTestPerTestType(ClsTestType.enTestType TestTypeID)
        {
            return ClsTest.FindTestPerPersoneAndLicenseClassAndTestType(this.ApplicantPersonID,LicenseClassID,TestTypeID);
        }

        public bool IsLicenseIssued()
        {
            return ClsLocalDrivingLicenseApplicationsData.IsLicenseIssued(this.ApplicationID);
        }


        public byte TotalTrialsPerTest(ClsTestType.enTestType TestTypeID)
        {
            return ClsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, ClsTestType.enTestType TestTypeID)
        {
            return ClsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public int IssueLicenseForTheFirtTime(string Notes,int CreatedByUserID)
        {
            int DriverID = -1;

            ClsDriver Driver = ClsDriver.FindByPersonID(this.ApplicantPersonID);
            if (Driver == null)
            {
                Driver = new ClsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedDate = DateTime.Now;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
                
            }

            ClsLicense License = new ClsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = Driver.DriverID; 
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = ClsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;
            if(License.Save())
            {
                this.SetCompleted();
                return License.LicenseID;
            }

            return -1;
        }

        public static int GetLicenseIsExist(int ApplicntPersonID,int LicenseClassID)
        {
            return ClsLicense.IsLicenseActive(ApplicntPersonID, LicenseClassID);
        }

        public  int GetLicenseIsExist()
        {
            return ClsLicense.IsLicenseActive(this.ApplicantPersonID, this.LicenseClassID);
        }

        public bool PassedAllTests()
        {
            return (DoesPassTestType(ClsTestType.enTestType.VisionTest) && DoesPassTestType(ClsTestType.enTestType.WrittenTest) && DoesPassTestType(ClsTestType.enTestType.StreetTest));
        }
    }
}

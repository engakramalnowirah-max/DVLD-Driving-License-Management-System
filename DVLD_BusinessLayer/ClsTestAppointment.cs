using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class ClsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public ClsTestType TestTypeInfo { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public ClsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreateByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public ClsApplication RetakeTestApplicationInfo { get; set; }

        public int TestID
        {
            get { return _GetTestID(); }

        }

        public ClsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.TestTypeInfo = new ClsTestType();
            this.LocalDrivingLicenseApplicationID = -1;
            LocalDrivingLicenseApplicationInfo = new ClsLocalDrivingLicenseApplication();
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreateByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            this.RetakeTestApplicationInfo = new ClsApplication();
            this.Mode = enMode.AddNew;

        }


        private ClsTestAppointment(int TestAppointmentID, int TestTypeID, int LoclDrivingLicenseApp, DateTime AppointmentDate, float PaidFees, int CreateByUserID, bool IsLocked, int RetakeTestApplicationID )
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.TestTypeInfo = ClsTestType.Find((ClsTestType.enTestType)TestTypeID);
            this.LocalDrivingLicenseApplicationID = LoclDrivingLicenseApp;
            this.LocalDrivingLicenseApplicationInfo = ClsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreateByUserID = CreateByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = ClsApplication.FindBaseApplicationByID(RetakeTestApplicationID);
            this.Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = ClsTestAppointmentData.InsertTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreateByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != 0);
        }
        private bool _UpdateTestAppointment()
        {
            return ClsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreateByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestAppointment())
                    {
                        Mode  = enMode.Update; 
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateTestAppointment();
            }

            return false;
        }

        public static ClsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = 0, LocalDrivingLicenseApplication = 0, CreateByUserID = 0, RetakTestApplicationID = 0;
            DateTime AppointmentDate = DateTime.Now;
            bool IsLocked = false;
            float PaidFees = 0;
            bool isFound = ClsTestAppointmentData.GetTestAppointmentByID(TestAppointmentID,ref TestTypeID,ref LocalDrivingLicenseApplication,ref AppointmentDate,ref PaidFees,ref CreateByUserID,ref IsLocked,ref RetakTestApplicationID);

            if (isFound)
            {
                return new ClsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplication, AppointmentDate, PaidFees, CreateByUserID, IsLocked, RetakTestApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseAppID,ClsTestType.enTestType TestType )
        {
            return ClsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseAppID, (int)TestType);
        }

        public static DataTable GetAllTestAppointmentes(int LocalDrivingLicenseAppID, ClsTestType.enTestType TestType)
        {
            return ClsTestAppointmentData.SELECTAllTestAppointmentes();
        }

        

        

        private int _GetTestID()
        {
            return ClsTestAppointmentData.GetTestID(this.TestAppointmentID);
        }
    }
}

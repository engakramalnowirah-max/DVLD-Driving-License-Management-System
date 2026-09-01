using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class ClsTest
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public ClsTestAppointment TestAppointmentInfo;
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreateByUserID { get; set; }

        public ClsTest()
        {
            this.TestID = 0;
            this.TestAppointmentID = 0;
            this.TestAppointmentInfo = new ClsTestAppointment();
            this.TestResult = false;
            this.Notes = "";
            this.CreateByUserID = 0;
            this.Mode = enMode.AddNew;
        }

        private ClsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreateByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID= TestAppointmentID;
            this.TestAppointmentInfo= ClsTestAppointment.Find(TestAppointmentID);
            this.TestResult= TestResult;
            this.Notes = Notes;
            this.CreateByUserID = CreateByUserID;
            this.Mode = enMode.Update;
        }


        private bool _AddNewTest()
        {
            this.TestID = ClsTestsData.InsertTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreateByUserID);

            return (this.TestID != 0);
        }
        private bool _UpdateTest()
        {
            return ClsTestsData.EditTest(this.TestID,TestAppointmentID,this.TestResult,this.Notes,this.CreateByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateTest();
            }

            return false;
        }

        public static ClsTest Find(int TestID)
        {
            int TestAppointmentID = -1, CreateByUserID = -1; 
            bool TestResult = false;
            string Notes = "";
            bool isFound = ClsTestsData.GetTestByTestID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreateByUserID);

            if (isFound)
            {
                return new ClsTest(TestID, TestAppointmentID, TestResult, Notes, CreateByUserID);
            }
            else
            {
                return null;
            }
        }


        public static ClsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1, CreateByUserID = -1;
            bool TestResult = false;
            string Notes = "";
            bool isFound = ClsTestsData.GetTestByTestAppointmentID(TestAppointmentID,ref TestID, ref TestResult, ref Notes, ref CreateByUserID);

            if (isFound)
            {
                return new ClsTest(TestID, TestAppointmentID, TestResult, Notes, CreateByUserID);
            }
            else
            {
                return null;
            }
        }


        public static ClsTest FindTestPerPersoneAndLicenseClassAndTestType(int ApplicntPersoneID,int LicenseClassID,ClsTestType.enTestType TestType)
        {
            int TestID = -1, CreateByUserID = -1, TestAppointmentID =-1;
            bool TestResult = false;
            string Notes = "";
            bool isFound = ClsTestsData.GetLastTestPerPersoneAndLicenseClassAndTestType(ApplicntPersoneID,LicenseClassID,(int)TestType,ref TestID,ref TestAppointmentID, ref TestResult, ref Notes, ref CreateByUserID);

            if (isFound)
            {
                return new ClsTest(TestID, TestAppointmentID, TestResult, Notes, CreateByUserID);
            }
            else
            {
                return null;
            }
        }

       

    }
}

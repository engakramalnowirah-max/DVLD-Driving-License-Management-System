using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Tests.Controls
{
    public partial class strlScheduleTest : UserControl
    {
        
        public strlScheduleTest()
        {
            InitializeComponent();
            
        }

        public enum enMode { AddNew =0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enCreation { FristTimeAppointmnet =0 , RetakeTestApplcation = 1}
        public enCreation Creation = enCreation.FristTimeAppointmnet;

        private ClsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID;
        private ClsTestAppointment _TestAppointment;
        private int _TestAppointmentID;

        private ClsTestType.enTestType _TestType ;

        public ClsTestType.enTestType TestType
        {
            get { return _TestType; }
            set
            {
                _TestType = value;
                if (_TestType == ClsTestType.enTestType.VisionTest)
                {
                    pbTestImage.Image = Resources.Vision_512;
                    groupBox1.Text = "Vision Tes";
                }
                else if (_TestType == ClsTestType.enTestType.WrittenTest)
                {
                    pbTestImage.Image = Resources.Written_Test_512;
                    groupBox1.Text = "Written Test";
                }
                else
                {
                    pbTestImage.Image = Resources.Cars_48;
                    groupBox1.Text = "Street Test";
                }
            }
        }

        private bool _HandleIsActiveTestConstrains()
        {
            if (_LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType)&& Mode == enMode.AddNew)
            {
                MessageBox.Show("Person Alrady have an active appointment for this test, You cannot add new appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            

            switch (TestType)
            {
                case ClsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;

                    return true;

                case ClsTestType.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(ClsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

                case ClsTestType.enTestType.StreetTest:

                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(ClsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = ClsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblTestFees.Text = _TestAppointment.PaidFees.ToString();

            //we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
                groupBox2.Enabled = true;
                lblTestTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;
        }
        private bool _HandleIsTestAnLockedConstrains()
        {
            if(_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Cannot Update Test Appointmente,This an Locked ";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }

            return true;
        }

        private void _HandleIsRetakTestConstrains()
        {
            if(Mode == enMode.AddNew && Creation == enCreation.RetakeTestApplcation)
            {
                ClsApplication Application = new ClsApplication();

                Application.ApplicationDate = DateTime.Now;
                Application.LastStatusDate = DateTime.Now;
                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.PersonInfo.PersonID;
                Application.ApplicationStatus = ClsApplication.enApplicationStatus.Completed;
                Application.CreatedByUserID = ClsGloabl.CurrentUser.UserID;
                Application.ApplicationTypeID = (int)ClsApplication.enApplicationType.RetakeTest;
                Application.PaidFees = ClsApplicationType.Find((int)ClsApplication.enApplicationType.RetakeTest).Fees;

                if (Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
                    return;
                }
                else
                {
                    MessageBox.Show("Erorr: Application Erorr Save ",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        public void LoadInfo(int LocalDrivingLicnestApplicationID, int TestAppointmntID = -1)
        { 
            _TestAppointmentID = TestAppointmntID;

            if(_TestAppointmentID == -1)
            {
                Mode = enMode.AddNew;
            }
            else
            {
                Mode = enMode.Update;
            }

            _LocalDrivingLicenseApplicationID = LocalDrivingLicnestApplicationID;
            _LocalDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestType))
            {
                Creation = enCreation.RetakeTestApplcation;
            }
            else
            {
                Creation = enCreation.FristTimeAppointmnet;
            }

            if (Creation == enCreation.RetakeTestApplcation) 
            {
                groupBox2.Enabled = true;
                lblTestTitle.Text = "Schedule Retake Test";
                lblRetakeFees.Text = ClsApplicationType.Find((int)ClsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lblRetakeTestAppID.Text = "0";
               
            }
            else
            {
                groupBox2.Enabled = false;
                lblTestTitle.Text = "Schedule Test";
                lblRetakeFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
                

            

            lblLDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPersonName.Text = _LocalDrivingLicenseApplication.PersonInfo.FullName;

            lblTrialTest.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestType).ToString();

            if (Mode == enMode.AddNew)
            {
                lblTestFees.Text = ClsTestType.Find(_TestType).Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/M";
                _TestAppointment = new ClsTestAppointment();
            }
            else
            {
                if(!_LoadTestAppointmentData())
                {
                    return;
                }
            }
            
            double TotalFees = Convert.ToDouble(lblTestFees.Text) + Convert.ToDouble(lblRetakeFees.Text);
            lblTotalFees.Text = TotalFees.ToString();

            if (!_HandleIsActiveTestConstrains())
                return;
              

            if(!_HandleIsTestAnLockedConstrains())
                return;

            if (!_HandlePrviousTestConstraint())
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _HandleIsRetakTestConstrains();

            _TestAppointment.TestTypeID = (int)TestType;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.CreateByUserID = ClsGloabl.CurrentUser.UserID;
            _TestAppointment.PaidFees = Convert.ToSingle(lblTestFees.Text);

            if (_TestAppointment.Save())
            {
                MessageBox.Show("Successfuly: Appointment Saved Successfuly ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return;
            }
            else
            {
                MessageBox.Show("Erorr: Appointment Erorr Save ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}

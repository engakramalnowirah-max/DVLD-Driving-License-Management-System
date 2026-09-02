using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_BusinessLayer.ClsTestType;

namespace Project_DVLD_PresentaionLayer.Tests.Controls
{
    public partial class strlScheduledTest : UserControl
    {
        public strlScheduledTest()
        {
            InitializeComponent();
        }
        private ClsTestType.enTestType _TestTypeID;
        private int _TestID = -1;

        private ClsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public ClsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case ClsTestType.enTestType.VisionTest:
                        {
                            groupBox1.Text = "Vision Test";
                            pbTestImage.Image = Resources.Vision_512;
                            break;
                        }

                    case ClsTestType.enTestType.WrittenTest:
                        {
                            groupBox1.Text = "Written Test";
                            pbTestImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case ClsTestType.enTestType.StreetTest:
                        {
                            groupBox1.Text = "Street Test";
                            pbTestImage.Image = Resources.Cars_48;
                            break;


                        }
                }
            }
        }

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private ClsTestAppointment _TestAppointment;

        public void LoadInfo(int TestAppointmentID)
        {

            _TestAppointmentID = TestAppointmentID;


            _TestAppointment = ClsTestAppointment.Find(_TestAppointmentID);

            //incase we did not find any appointment .
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;


            //this will show the trials for this test before 
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();



            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();



        }
    }
}

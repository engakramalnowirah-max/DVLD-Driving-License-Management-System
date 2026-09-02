using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Tests
{
    public partial class frmSchedulTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private ClsTestType.enTestType _TestType = ClsTestType.enTestType.VisionTest;
        private int _TestAppointment = -1;

        public frmSchedulTest(int LocalDrivingLicenseApplicationID, ClsTestType.enTestType TestType, int TestAppointment = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            _TestType = TestType;
            _TestAppointment = TestAppointment;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSchedulTest_Load(object sender, EventArgs e)
        {
            strlScheduleTest1.TestType = _TestType;
            strlScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID,_TestAppointment);
        }
    }
}

using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Project_DVLD_PresentaionLayer.Tests
{
    public partial class frmTestAppointments : Form
    {
        private int _LocalDrivingLicenseApplicationID = 0;
        private ClsTestType.enTestType _TestType  = ClsTestType.enTestType.VisionTest;
        private DataTable _dtTestAppointments;

        public frmTestAppointments(int localDrivingLicenseApplicationID, ClsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _TestType = TestTypeID;
        }

        

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void _LoadTestTypeImageAndTittl()
        {
            if(_TestType == ClsTestType.enTestType.VisionTest)
            {
                lblTestTitle.Text = "Vision Test";
                pbTestImage.Image = Resources.Vision_512;
                this.Text = lblTestTitle.Text;
            }
            else if( _TestType == ClsTestType.enTestType.WrittenTest)
            {
                lblTestTitle.Text = "Written Test";
                pbTestImage.Image = Resources.Written_Test_512;
                this.Text = lblTestTitle.Text;
            }
            else 
            {
                lblTestTitle.Text = "Street Test";
                pbTestImage.Image = Resources.Cars_48;
                this.Text = lblTestTitle.Text;
            }
        }
        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTittl();
            strlDetailsLoclDrivingLicenseApplication1.LoadLoclDrivingLicensApplicationByID(_LocalDrivingLicenseApplicationID);
            _dtTestAppointments =ClsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);


            dgvTestAppointments.DataSource = _dtTestAppointments;
            lblRecords.Text = dgvTestAppointments.Rows.Count.ToString();
            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Test ID";
                dgvTestAppointments.Columns[0].Width = 120;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 180;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 120;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 120;

                ((DataGridViewCheckBoxColumn)dgvTestAppointments.Columns[3]).TrueValue = 1;
                ((DataGridViewCheckBoxColumn)dgvTestAppointments.Columns[3]).FalseValue = 0;
            }
        }

       

        private void dgvTestAppointments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvTestAppointments.ClearSelection();
                dgvTestAppointments.Rows[e.RowIndex].Selected = true;
                dgvTestAppointments.CurrentCell = dgvTestAppointments.Rows[e.RowIndex].Cells[0];
            }
        }

        private void pbSchadulTest_Click(object sender, EventArgs e)
        {
            ClsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an Active appointment for this test, you cannot add new appointment","not allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            ClsTest LastTest = LocalDrivingLicenseApplication.GetLastTestPerTestType(_TestType);
            if(LastTest == null)
            {
                frmSchedulTest SchedulTest = new frmSchedulTest(_LocalDrivingLicenseApplicationID, _TestType);
                SchedulTest.ShowDialog(this);
                frmTestAppointment_Load(null, null);
                return;
            }


            if (LastTest.TestResult == true)
            {
                MessageBox.Show("this Person Already Passed this Test before, you Can retake faild Test ", "not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int TestAppointmentID = LastTest.TestAppointmentID;
            frmSchedulTest SchedulTest1 = new frmSchedulTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType);
            SchedulTest1.ShowDialog(this);
            frmTestAppointment_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmSchedulTest SchedulTest = new frmSchedulTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            SchedulTest.ShowDialog(this);
            frmTestAppointment_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmTakeTest TakeTest = new frmTakeTest(TestAppointmentID,_TestType);
            TakeTest.ShowDialog(this);
            frmTestAppointment_Load(null, null);
        }
    }
}

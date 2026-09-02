using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Applications.Issue_Driving_License_First_Time;
using Project_DVLD_PresentaionLayer.Applications.Local_Driving_License;
using Project_DVLD_PresentaionLayer.Licenses;
using Project_DVLD_PresentaionLayer.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.LocalDrivingApplications
{
    public partial class frmListlocalDrivingLicenseApplications : Form
    {
        public frmListlocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable _dtLocalDrivingLicensesApplications;
        private void frmListlocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtLocalDrivingLicensesApplications = ClsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvApplications.DataSource = _dtLocalDrivingLicensesApplications;
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRows.Text = dgvApplications.Rows.Count.ToString();
            if (dgvApplications.Rows.Count > 0)
            {
                dgvApplications.Columns[0].HeaderText = "LDL AppID";
                dgvApplications.Columns[0].Width = 110;

                dgvApplications.Columns[1].HeaderText = "Driving Class";
                dgvApplications.Columns[1].Width = 250;

                dgvApplications.Columns[2].HeaderText = "National No.";
                dgvApplications.Columns[2].Width = 120;

                dgvApplications.Columns[3].HeaderText = "Full Name";
                dgvApplications.Columns[3].Width = 300;

                dgvApplications.Columns[4].HeaderText = "Application Date";
                dgvApplications.Columns[4].Width = 150;

                dgvApplications.Columns[5].HeaderText = "Passed Tests";
                dgvApplications.Columns[5].Width = 120;

                dgvApplications.Columns[6].HeaderText = "Status";
                dgvApplications.Columns[6].Width = 120;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmNewLocalApplication NewApp = new frmNewLocalApplication();
            NewApp.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null,null);
        }

        private void dgvApplications_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvApplications.ClearSelection();
                dgvApplications.Rows[e.RowIndex].Selected = true;
                dgvApplications.CurrentCell = dgvApplications.Rows[e.RowIndex].Cells[0];
            }
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalApplication EditApplication = new frmNewLocalApplication((int)dgvApplications.CurrentRow.Cells[0].Value);
            EditApplication.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null, null);
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments VisionTestAppointment = new frmTestAppointments((int)dgvApplications.CurrentRow.Cells[0].Value,ClsTestType.enTestType.VisionTest);
            VisionTestAppointment.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null, null);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments WrittenTestAppointment = new frmTestAppointments((int)dgvApplications.CurrentRow.Cells[0].Value, ClsTestType.enTestType.WrittenTest);
            WrittenTestAppointment.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null, null);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments StreetTestAppointment = new frmTestAppointments((int)dgvApplications.CurrentRow.Cells[0].Value, ClsTestType.enTestType.StreetTest);
            StreetTestAppointment.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null, null);
        }

        private void txtValueWithFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;


            }


            if (txtValueWithFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtLocalDrivingLicensesApplications.DefaultView.RowFilter = "";
                lblNumberOfRows.Text = _dtLocalDrivingLicensesApplications.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")

                _dtLocalDrivingLicensesApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtValueWithFilter.Text.Trim());
            else
                _dtLocalDrivingLicensesApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtValueWithFilter.Text.Trim());

            lblNumberOfRows.Text = _dtLocalDrivingLicensesApplications.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "None")
            {
                txtValueWithFilter.Visible = false;
                

                
            }
            else
            {
                txtValueWithFilter.Visible = true;
                txtValueWithFilter.Text = "";
                txtValueWithFilter.Focus();
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLocalDrivingLicenseApplicationInfo LDLAppInfo = new frmShowLocalDrivingLicenseApplicationInfo((int)dgvApplications.CurrentRow.Cells[0].Value);
            LDLAppInfo.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null, null);

        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you suer do want to Delete this Application", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LoclDrivingLicenseApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            ClsLocalDrivingLicenseApplication LoclDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(LoclDrivingLicenseApplicationID);

            if (LoclDrivingLicenseApplication != null)
            {
                if (LoclDrivingLicenseApplication.Delete(LoclDrivingLicenseApplicationID))
                {
                    MessageBox.Show("Application Delete Sucessfully", "Sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListlocalDrivingLicenseApplications_Load(null, null);

                }
                else
                {
                    MessageBox.Show("Application Delete Error", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("are you suer do want to Cancel this Application","Information",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LoclDrivingLicenseApplicationID = (int)dgvApplications.CurrentRow.Cells [0].Value;
            ClsLocalDrivingLicenseApplication LoclDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(LoclDrivingLicenseApplicationID);

            if(LoclDrivingLicenseApplication != null)
            {
                if(LoclDrivingLicenseApplication.Cancelled())
                {
                    MessageBox.Show("Application Canceled Sucessfully", "Sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListlocalDrivingLicenseApplications_Load(null, null);

                }
                else
                {
                    MessageBox.Show("Application Canceled Error", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            ClsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    ClsLocalDrivingLicenseApplication.Find
                                                    (LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == ClsApplication.enApplicationStatus.New);
            ScheduleTestsMenue.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == ClsApplication.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            DeleteLoclDrivingLicenseApplicationToolStripMenuItem.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == ClsApplication.enApplicationStatus.New);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(ClsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(ClsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(ClsTestType.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == ClsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicenseFristTime IssueDrivingLicnese = new frmIssueDrivingLicenseFristTime((int)dgvApplications.CurrentRow.Cells[0].Value);
            IssueDrivingLicnese.ShowDialog(this);
            frmListlocalDrivingLicenseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = ClsLicense.FindByApplicationID(ClsLocalDrivingLicenseApplication.Find((int)dgvApplications.CurrentRow.Cells[0].Value).ApplicationID).LicenseID;
            frmShowLicense Licnese = new frmShowLicense(LicenseID);
            Licnese.ShowDialog(this);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = ClsPresone.Find((string)dgvApplications.CurrentRow.Cells[2].Value).PersonID;
            frmLicenseHistory LicensesHistory = new frmLicenseHistory(PersonID);
            LicensesHistory.ShowDialog(this);
        }
    }
}

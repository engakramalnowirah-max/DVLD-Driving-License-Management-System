using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Applications.Issue_Driving_License_First_Time
{
    public partial class frmIssueDrivingLicenseFristTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private ClsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = new ClsLocalDrivingLicenseApplication();


        public frmIssueDrivingLicenseFristTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDrivingLicenseFristTime_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"Erorr: this Application By ID{_LocalDrivingLicenseApplicationID} Not Exist","Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show($"Person should pass all test first.", "Not All Pass Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicneseID = _LocalDrivingLicenseApplication.GetLicenseIsExist();
            if (LicneseID != -1)
            {
                MessageBox.Show($"Person already has license before with License", "Not Persmison", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }



            strlDetailsLoclDrivingLicenseApplication1.LoadLoclDrivingLicensApplicationByID(_LocalDrivingLicenseApplicationID);

        }





        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicnseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), ClsGloabl.CurrentUser.UserID);
            if (LicnseID != -1)
            {
                MessageBox.Show("Sucssefully: License Save Successfully With ID"+LicnseID.ToString(), "Save Data", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                txtNotes.Enabled = false;
                btnIssue.Enabled = false;
            }
            else
            {
                MessageBox.Show("Erorr: same thing Have Erorr", " Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        
    }
}

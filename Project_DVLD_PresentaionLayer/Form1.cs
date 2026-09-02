using Project_DVLD_PresentaionLayer.Applications.International_Driving_License;
using Project_DVLD_PresentaionLayer.Applications.Release_Detained_Driving_Licsense;
using Project_DVLD_PresentaionLayer.Applications.Renew_Driving_License;
using Project_DVLD_PresentaionLayer.Applications.Replacment_for_Damaged_or_Lost_License;
using Project_DVLD_PresentaionLayer.ApplicationType;
using Project_DVLD_PresentaionLayer.Detain_License;
using Project_DVLD_PresentaionLayer.Drivers;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.LocalDrivingApplications;
using Project_DVLD_PresentaionLayer.Manag_Test_Type;
using Project_DVLD_PresentaionLayer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeople people = new frmPeople();
            people.ShowDialog(this);
        }

        private void replacementForLostOrDamgedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void internatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalDrivingLicense ListInternationalDrivingLicese = new frmListInternationalDrivingLicense();
            ListInternationalDrivingLicese.ShowDialog(this);
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserList ManageUsers = new frmUserList();
            ManageUsers.ShowDialog(this);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes ApplicationTypes = new frmListApplicationTypes();
            ApplicationTypes.ShowDialog(this);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmManagTestType TestType = new frmManagTestType();
            TestType.ShowDialog(this);
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListlocalDrivingLicenseApplications LocalDrivingLicenseApplications = new frmListlocalDrivingLicenseApplications();
            LocalDrivingLicenseApplications.ShowDialog(this);
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalApplication NewLocalDrivingApplication = new frmNewLocalApplication();
            NewLocalDrivingApplication.ShowDialog(this);
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsGloabl.CurrentUser = null;
            this.Hide();
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void currentInfoUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo ShowInfo = new frmShowUserInfo(ClsGloabl.CurrentUser.UserID);
            ShowInfo.ShowDialog(this);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword ChangePassword = new frmChangePassword(ClsGloabl.CurrentUser.UserID);
            ChangePassword.ShowDialog(this);
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicense NewInternationalLicense = new frmAddNewInternationalLicense();
            NewInternationalLicense.ShowDialog(this);
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewDrivingLicense RenewDrivingLicense = new frmRenewDrivingLicense();
            RenewDrivingLicense.ShowDialog(this);
        }

        private void replacementForLostOrDamgedLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmReplacmentforDamagedOrLostLicnese RepalcementFor = new frmReplacmentforDamagedOrLostLicnese();
            RepalcementFor.ShowDialog(this);
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedDrivingLicenese ReleaseDetainedDrivingLicense = new frmReleaseDetainedDrivingLicenese();
            ReleaseDetainedDrivingLicense.ShowDialog(this);

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmListlocalDrivingLicenseApplications LocalDrivingLicenseApplications = new frmListlocalDrivingLicenseApplications();
            LocalDrivingLicenseApplications.ShowDialog(this);
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers ListDrivers = new frmListDrivers();
            ListDrivers.ShowDialog(this);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedDrivingLicenese ReleaseDetainedDrivingLicenese = new frmReleaseDetainedDrivingLicenese();
            ReleaseDetainedDrivingLicenese.ShowDialog(this);
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLManageDetainedDrivingLicense ListDetainedDrivingLicense = new frmLManageDetainedDrivingLicense();
            ListDetainedDrivingLicense.ShowDialog(this);
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainDrivingLicense detainDrivingLicense = new frmDetainDrivingLicense();
            detainDrivingLicense.ShowDialog(this);
        }
    }
}

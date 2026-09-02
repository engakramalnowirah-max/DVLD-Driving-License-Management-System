using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Licenses;
using Project_DVLD_PresentaionLayer.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Applications.Local_Driving_License
{
    public partial class strlDetailsLoclDrivingLicenseApplication : UserControl
    {
        public strlDetailsLoclDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        private ClsLocalDrivingLicenseApplication _LoclDrivingLicenseApplication;
        private int _LoclDrivingLicensApplicationID = -1;
        public int LoclDrivingLicenseApplicationID { get { return _LoclDrivingLicensApplicationID; } }

        private void _FillDataLocaDrivingLicensApplication()
        {
            lblDLAppID.Text= _LoclDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LoclDrivingLicenseApplication.LicenseClassInfo.ClassName;
            strlApplicationBaseInfo1.LoadApplicationInfo(_LoclDrivingLicenseApplication.ApplicationID);

        }
        private void _ResateDefultValue()
        {
            lblDLAppID.Text = "[???]";
            lblLicenseClass.Text = "[???]";
            strlApplicationBaseInfo1.ResatDefultValue();
        }
        public void LoadLoclDrivingLicensApplicationByID(int LoclDrivingLicenseApplicationID)
        {
            _LoclDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(LoclDrivingLicenseApplicationID);
            if(_LoclDrivingLicenseApplication == null)
            {
                MessageBox.Show($"This is a Locl Driving Licens Application By ID {LoclDrivingLicenseApplicationID} is Not Existing", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResateDefultValue();
                return;
            }
            _LoclDrivingLicensApplicationID = LoclDrivingLicenseApplicationID;
            _FillDataLocaDrivingLicensApplication();
        }
        public void LoadLoclDrivingLicensApplicationByApplicationID(int ApplicationID)
        {
            _LoclDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(ApplicationID);
            if (_LoclDrivingLicenseApplication == null)
            {
                MessageBox.Show($"This is a Application By ID {ApplicationID} is Not Existing", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResateDefultValue();
                return;
            }
            _LoclDrivingLicensApplicationID = _LoclDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _FillDataLocaDrivingLicensApplication();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ClsLicense.IsLicenseExistByApplicationID(_LoclDrivingLicenseApplication.ApplicationID))
            {
                frmShowLicense LicenseInfo = new frmShowLicense(_LoclDrivingLicenseApplication.ApplicationID);
                LicenseInfo.ShowDialog(this);
                

            }

        }
    }
}

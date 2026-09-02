using DVLD_BusinessLayer;
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

namespace Project_DVLD_PresentaionLayer.Licenses.International_Licnese.Controls
{
    public partial class strlInternationalLicenseInfo : UserControl
    {
        private ClsInternationalLicense _InternationalLicense = null;
        private int _InternationalLicneseID = -1;
        public strlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public ClsInternationalLicense InternationalLicenseInfo { get { return _InternationalLicense; } }
        public int InternationalLicenseID { get { return _InternationalLicneseID; } } 



        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersoneInfo.Gendor == false)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            if (_InternationalLicense.DriverInfo.PersoneInfo.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _InternationalLicense.DriverInfo.PersoneInfo.ImagePath;
            }
            else
            {
                pbPersonImage.Image = null;
            }
        }
        private void _RefrechDataToControals()
        {
            _InternationalLicneseID = _InternationalLicense.InternationalLicenseID;
            lblName.Text = _InternationalLicense.DriverInfo.PersoneInfo.FullName.ToString();
            lblIntLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersoneInfo.NationalNo;
            lblGendor.Text = (_InternationalLicense.DriverInfo.PersoneInfo.Gendor == false)?"Male" : "FeMale";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = (_InternationalLicense.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersoneInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();
            _LoadPersonImage();


        }

        public void LoadData(int InternationalLicenseID)
        {
            _InternationalLicense = ClsInternationalLicense.Find(InternationalLicenseID);

            if(_InternationalLicense == null)
            {
                MessageBox.Show("Error","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _RefrechDataToControals();

        }

    }
}

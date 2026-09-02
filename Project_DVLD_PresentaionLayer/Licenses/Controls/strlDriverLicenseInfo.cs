using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Licenses.Controls
{
    public partial class strlDriverLicenseInfo : UserControl
    {
        public strlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private int _LicenseID = -1;
        private ClsLicense _License;


        public ClsLicense SelectedLicenseInfo { get { return _License; } }
        public int LicenseID { get { return _LicenseID; } }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersoneInfo.Gendor == false)
            {
                pbPersonImage.Image = Resources.Male_512;

            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            string _ImagePath = _License.DriverInfo.PersoneInfo.ImagePath;
            if (_ImagePath != "")
            {
                if (File.Exists(_ImagePath))
                {
                    pbPersonImage.Load(_ImagePath);

                }
                else
                {
                    MessageBox.Show("could not fine this image: = "+_ImagePath,"not founde",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        //public void LoadDataByApplicationID(int ApplicationID)
        //{
        //    _License = ClsLicense.FindByApplicationID(ApplicationID);
        //    if(_License == null)
        //    {
        //        MessageBox.Show("Erorr: License is not Exist By Application ID" + ApplicationID.ToString(), "Error Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }


        //    lblClass.Text = _License.LicenseClassInfo.ClassName;
        //    lblName.Text = _License.DriverInfo.PersoneInfo.FullName;
        //    lblLicenseID.Text = _License.LicenseID.ToString();
        //    lblNationalNo.Text = _License.DriverInfo.PersoneInfo.NationalNo;
        //    lblGendor.Text = (_License.DriverInfo.PersoneInfo.Gendor == false) ? "Male" : "FeMale";
        //    lblIssueDate.Text = _License.IssueDate.ToShortDateString();
        //    lblIssueReason.Text = _License.IssueReason.ToString();
        //    lblNotes.Text = _License.Notes.ToString();
        //    lblisActive.Text = (_License.IsActive == true) ? "Yes" : "No";
        //    lblDateOfBirth.Text = _License.DriverInfo.PersoneInfo.DateOfBirth.ToShortDateString();
        //    lblDriverID.Text = _License.DriverID.ToString();
        //    lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
        //    lblIsDetained.Text = (_License.IsDetaind == true) ? "Yes" : "No";
        //    _LicenseID = _License.LicenseID;
        //    _LoadPersonImage();
        //}

        public void LoadDataByLicenseID(int LicenseID)
        {
            _License = ClsLicense.Find(LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Erorr: License is not Exist By License ID" + LicenseID.ToString(), "Error Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblName.Text = _License.DriverInfo.PersoneInfo.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersoneInfo.NationalNo;
            lblGendor.Text = (_License.DriverInfo.PersoneInfo.Gendor == false) ? "Male" : "FeMale";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblNotes.Text = _License.Notes.ToString();
            lblisActive.Text = (_License.IsActive == true) ? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersoneInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = (_License.IsDetaind == true) ? "Yes" : "No";
            _LicenseID = _License.LicenseID;
            _LoadPersonImage();
        }

    }
}

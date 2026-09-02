using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_BusinessLayer.ClsLicense;

namespace Project_DVLD_PresentaionLayer.Applications.Release_Detained_Driving_Licsense
{
    public partial class frmReleaseDetainedDrivingLicenese : Form
    {
        public frmReleaseDetainedDrivingLicenese()
        {
            InitializeComponent();

        }

        public frmReleaseDetainedDrivingLicenese(int LicenseID,int DetainedID = -1)
        {
            InitializeComponent();
            SelectedLicenseID = LicenseID;
            strlFilterDrivingLicense1.LoadLicenseInfo(SelectedLicenseID);
            strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo = ClsDetainedLicense.FindByDetainedID(DetainedID);
            strlFilterDrivingLicense1.FilterEnabled = false;


        }
        
        private int SelectedLicenseID = -1;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure to Save This Information", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            int ApplicationID = -1;

            bool isReleased = strlFilterDrivingLicense1.SelectedLicenseInfo.ReleaseDetainedLicense(ClsGloabl.CurrentUser.UserID, ref ApplicationID);
            if(!isReleased)
            {
                MessageBox.Show("Releas License is Faild -:(", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = ApplicationID.ToString();
            MessageBox.Show("Released License Successfully", "Reply Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnRelease.Enabled = false;
            strlFilterDrivingLicense1.FilterEnabled = false;

        }

        //private void strlFilterDrivingLicense1_OnLicenseSelected(int obj)
        //{
        //    SelectedLicenseID = obj;
        //    lblLicenseID.Text = SelectedLicenseID.ToString();
        //    lklShowLicenseHistory.Enabled = (SelectedLicenseID != -1);


        //    if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsDetaind)
        //    {
        //        MessageBox.Show("Selected License is Not Detained, choose anther one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        btnRelease.Enabled = false;
        //    }

        //    lklShowLicensInfo.Enabled = true;

        //    lblDetainID.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
        //    lblDetainDate.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo.DetainDate.ToString();
        //    lblApplicationFees.Text = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
        //    lblLicenseID.Text = SelectedLicenseID.ToString();
        //    lblCreatedBy.Text = ClsGloabl.CurrentUser.UserName;
        //    lblFineFees.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
        //    lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
        //    lklShowLicensInfo.Enabled = true;

        //}



        private void lklShowLicensInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense License = new frmShowLicense(SelectedLicenseID);
            License.ShowDialog(this);
        }

        private void lklShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ClsLicense.Find(SelectedLicenseID).DriverInfo.PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog(this);
        }

        private void strlFilterDrivingLicense1_OnLicenseSelected(object sender, Licenses.Controls.strlFilterDrivingLicense.LicenseSelectedEventArgs e)
        {
            SelectedLicenseID = e.LicenseID;
            lblLicenseID.Text = SelectedLicenseID.ToString();
            lklShowLicenseHistory.Enabled = (SelectedLicenseID != -1);


            if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsDetaind)
            {
                MessageBox.Show("Selected License is Not Detained, choose anther one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            lklShowLicensInfo.Enabled = true;

            lblDetainID.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo.DetainDate.ToString();
            lblApplicationFees.Text = ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblLicenseID.Text = SelectedLicenseID.ToString();
            lblCreatedBy.Text = ClsGloabl.CurrentUser.UserName;
            lblFineFees.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
            lklShowLicensInfo.Enabled = true;
        }
    }
}

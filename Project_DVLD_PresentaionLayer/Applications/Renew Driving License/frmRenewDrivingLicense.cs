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

namespace Project_DVLD_PresentaionLayer.Applications.Renew_Driving_License
{
    public partial class frmRenewDrivingLicense : Form
    {
        int NewLicenseID = -1;
        
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }

        //private void strlFilterDrivingLicense1_OnLicenseSelected(int obj)
        //{
        //    int LicenseID = obj;

        //    lblOldLicenseID.Text = NewLicenseID.ToString();

        //    if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsLicenseExpired()) 
        //    {
        //        MessageBox.Show("Selected License is not yet espiared, it well expire on:", "Not allowed" +
        //            "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        btnIssue.Enabled = false;
        //        return;
        //    }


        //     if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsActive)
        //     {
        //        MessageBox.Show("Selected License is not Active, Please choose Another one.", "Not allowed" +
        //            "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        btnIssue.Enabled = false;
        //        return;
        //     }


        //    int DefaultValidityLength = strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
        //    lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToShortDateString();
        //    lblLicenseFees.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
        //    lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text)+ Convert.ToSingle(lblLicenseFees.Text)).ToString();
        //    txtNotes.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.Notes;
        //    btnIssue.Enabled = true;

        //}

        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationFees.Text = ClsApplicationType.Find((int)ClsApplication.enApplicationType.RenewDrivingLicenseService).Fees.ToString();
            lbIssueDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = ClsGloabl.CurrentUser.UserID.ToString();
            lklShowNewLicenseInfo.Enabled = false;
            strlFilterDrivingLicense1.txtLicenseIDFocus();


        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are your sour to Renew this License", "Question", MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            ClsLicense RenewLicense = strlFilterDrivingLicense1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), ClsGloabl.CurrentUser.UserID);
            if(RenewLicense == null)
            {
                MessageBox.Show("Erorr: same thing is wrong.", "Erorr",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewLicenseID = RenewLicense.LicenseID;
            lblRenewedLicenseID.Text = NewLicenseID.ToString();
            lblApplicationID.Text = RenewLicense.ApplicationID.ToString();
            MessageBox.Show("Renew License Sucssfult By ID."+ NewLicenseID.ToString(), "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lklShowNewLicenseInfo.Enabled = true;
            strlFilterDrivingLicense1.FilterEnabled = false;
            btnIssue.Enabled = false;
            
        }

        private void lklShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ClsLicense.Find(NewLicenseID).DriverInfo.PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog(this);
        }

        private void strlFilterDrivingLicense1_OnLicenseSelected(object sender, Licenses.Controls.strlFilterDrivingLicense.LicenseSelectedEventArgs e)
        {
            int LicenseID = e.LicenseID;

            lblOldLicenseID.Text = NewLicenseID.ToString();

            if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet espiared, it well expire on:", "Not allowed" +
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }


            if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active, Please choose Another one.", "Not allowed" +
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }


            int DefaultValidityLength = strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToShortDateString();
            lblLicenseFees.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = strlFilterDrivingLicense1.SelectedLicenseInfo.Notes;
            btnIssue.Enabled = true;
        }
    }
}

using DVLD_BusinessLayer;
using Guna.UI2.WinForms;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.Licenses;
using Project_DVLD_PresentaionLayer.Licenses.International_Licnese;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Project_DVLD_PresentaionLayer.Applications.International_Driving_License
{
    public partial class frmAddNewInternationalLicense : Form
    {
        public frmAddNewInternationalLicense()
        {
            InitializeComponent();
        }


        int _InternationalLicenseID = -1;
             

        private void frmAddNewInternationalLicense_Load(object sender, EventArgs e)
        {
            strlFilterDrivingLicense1.FilterEnabled = true;
            strlFilterDrivingLicense1.txtLicenseIDFocus();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = ClsApplicationType.Find((int)ClsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedBy.Text = ClsGloabl.CurrentUser.UserID.ToString();
            lklShowILLicenseInfo.Enabled = false;
            lklLicensesHistory.Enabled = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillApplicationData()
        {
            
        }

        private void _FillInternationalLicenseData()
        {
            

        }

        //private void strlFilterDrivingLicense1_OnLicenseSelected(int obj)
        //{
        //    int LocalLicenseID = obj;
            
        //    lblLocalLicenseID.Text = LocalLicenseID.ToString();
        //    lklLicensesHistory.Enabled = (LocalLicenseID != -1);
        //    if(LocalLicenseID == -1)
        //    {
        //        return;
        //    }

        //    if(strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseClassID != 3 )
        //    {
        //        MessageBox.Show("Erorr: This Licnese Class is Not Of Class 3", "Not Allowe", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        btnIssue.Enabled = false;
        //        return;
        //    }

        //    int ActiveInternationalLiceseID = ClsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(strlFilterDrivingLicense1.SelectedLicenseInfo.DriverID);
        //    if (ActiveInternationalLiceseID != -1)
        //    {
        //        MessageBox.Show("Erorr: This is Person has Aleardy Active License with ID="+ActiveInternationalLiceseID.ToString(), "Not Allowe", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        btnIssue.Enabled = false;
        //        return;
        //    }
                
        //    btnIssue.Enabled = true;
            
        //}

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure to Save This Information","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) != DialogResult.OK) 
            {
                return;
            }


            ClsInternationalLicense InternationalLicense = new ClsInternationalLicense();

            InternationalLicense.ApplicationStatus = ClsApplication.enApplicationStatus.Completed;
            InternationalLicense.ApplicantPersonID = strlFilterDrivingLicense1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationTypeID = ClsApplicationType.Find((int)ClsApplication.enApplicationType.NewInternationalLicense).ID;
            InternationalLicense.CreatedByUserID = ClsGloabl.CurrentUser.UserID;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = ClsApplicationType.Find((int)ClsApplication.enApplicationType.NewInternationalLicense).Fees;

            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.DriverID = strlFilterDrivingLicense1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IsActive = true;
            InternationalLicense.IssuedUsingLocalLicenseID = strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseID;


            if(!InternationalLicense.Save())
            {
                MessageBox.Show("Erorr: Data Save Failed", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lklShowILLicenseInfo.Enabled = true;
            strlFilterDrivingLicense1.FilterEnabled = false;
            btnIssue.Enabled = false;
            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;

        }

        private void lklShowILLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicense InternationalLicense = new frmShowInternationalLicense(_InternationalLicenseID);
            InternationalLicense.ShowDialog(this);
        }

        private void lklLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory LicnesesHistory = new frmLicenseHistory(strlFilterDrivingLicense1.SelectedLicenseInfo.DriverInfo.PersonID);
            LicnesesHistory.ShowDialog(this);
        }

        private void strlFilterDrivingLicense1_OnLicenseSelected(object sender, Licenses.Controls.strlFilterDrivingLicense.LicenseSelectedEventArgs e)
        {
            int LocalLicenseID = e.LicenseID;

            lblLocalLicenseID.Text = LocalLicenseID.ToString();
            lklLicensesHistory.Enabled = (LocalLicenseID != -1);
            if (LocalLicenseID == -1)
            {
                return;
            }

            if (strlFilterDrivingLicense1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Erorr: This Licnese Class is Not Of Class 3", "Not Allowe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            int ActiveInternationalLiceseID = ClsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(strlFilterDrivingLicense1.SelectedLicenseInfo.DriverID);
            if (ActiveInternationalLiceseID != -1)
            {
                MessageBox.Show("Erorr: This is Person has Aleardy Active License with ID=" + ActiveInternationalLiceseID.ToString(), "Not Allowe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }
    }
}

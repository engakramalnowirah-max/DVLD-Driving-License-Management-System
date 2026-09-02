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
using static System.Net.Mime.MediaTypeNames;

namespace Project_DVLD_PresentaionLayer.Applications.Replacment_for_Damaged_or_Lost_License
{
    public partial class frmReplacmentforDamagedOrLostLicnese : Form
    {
        int ReplacmentLicneseID = -1;

        public frmReplacmentforDamagedOrLostLicnese()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void rdDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacment For Damaged Licnese";
            lblTitle.Text = "Replacment For Damaged Licnese";
 
            lblApplicationFees.Text = _ApplicationFees().ToString();

        }

        private float _ApplicationFees()
        {
           
            if(rdDamagedLicense.Checked)
            {
                return ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense).Fees;
            }
            else
            {
                return ClsApplicationType.Find((int)ClsApplication.enApplicationType.ReplacementforaLostDrivingLicense).Fees; ;
            }
        }
        private ClsLicense.enIssueReason _IssueReason()
        {

            if (rdDamagedLicense.Checked)
            {
                return ClsLicense.enIssueReason.ReplacementForDamaged;
            }
            else
            {
                return ClsLicense.enIssueReason.ReplacementForLost;
            }
        }
        private void rdLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacment For Lost Licnese";
            lblTitle.Text = "Replacment For Lost Licnese";

            lblApplicationFees.Text = _ApplicationFees().ToString();
        }

        private void frmReplacmentforDamagedOrLostLicnese_Load(object sender, EventArgs e)
        {

            strlFilterDrivingLicense1.txtLicenseIDFocus();
            lklShowLicenseHistory.Enabled = false;
            rdDamagedLicense.Checked = true;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
        }

        private void lklShowNewLicensInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowLicense LicenseInfo = new frmShowLicense(ReplacmentLicneseID);
            LicenseInfo.ShowDialog(this);
        }

        

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure to Save This Information", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            ClsLicense ReplacLicense = strlFilterDrivingLicense1.SelectedLicenseInfo.Replacmente(_IssueReason(), ClsGloabl.CurrentUser.UserID);
            lblReplacedLicenseID.Text = ReplacLicense.LicenseID.ToString();
            ReplacmentLicneseID = ReplacLicense.LicenseID;
            lblApplicationID.Text = ReplacLicense.ApplicationID.ToString();
            MessageBox.Show("Replacmente License Sucssfult New ID." + ReplacLicense.LicenseID.ToString(), "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssueReplacement.Enabled = false;
            strlFilterDrivingLicense1.FilterEnabled = false;
        }

        private void lklShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ClsLicense.Find(ReplacmentLicneseID).DriverInfo.PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog(this);
        }

        private void strlFilterDrivingLicense1_OnLicenseSelected(object sender, Licenses.Controls.strlFilterDrivingLicense.LicenseSelectedEventArgs e)
        {
            int LicenseID = e.LicenseID;

            lblOldLicenseID.Text = LicenseID.ToString();

            lklShowLicenseHistory.Enabled = (LicenseID != -1);


            if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active, Please choose Another one.", "Not allowed" +
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
            }
            lblCreatedBy.Text = ClsGloabl.CurrentUser.UserID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
        }


    }
}

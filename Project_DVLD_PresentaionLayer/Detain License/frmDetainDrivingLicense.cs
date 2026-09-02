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

namespace Project_DVLD_PresentaionLayer.Detain_License
{
    public partial class frmDetainDrivingLicense : Form
    {
        public frmDetainDrivingLicense()
        {
            InitializeComponent();
            lklShowLicensInfo.Enabled = false;
        }


        private int _SelectedLicenseID = -1;



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("are you sure for detained this license.", "Question", MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int DetainedID = strlFilterDrivingLicense1.SelectedLicenseInfo.DetainedLicense(Convert.ToSingle(txtFineFees.Text.Trim()), ClsGloabl.CurrentUser.UserID);
            if(DetainedID == -1)
            {
                MessageBox.Show("Detained License is Error some thing is faild.","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text = DetainedID.ToString();
            strlFilterDrivingLicense1.FilterEnabled = false;
            MessageBox.Show("Detained License Successfully.", "Reply Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetain.Enabled = false;
        }

        private void lklShowLicensInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
                frmShowLicense License = new frmShowLicense(_SelectedLicenseID);
                License.ShowDialog(this);
            

        }

        //private void strlFilterDrivingLicense1_OnLicenseSelected(int obj)
        //{
        //    _SelectedLicenseID = obj;
        //    lblLicenseID.Text = _SelectedLicenseID.ToString();
        //    lklShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

        //    if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsActive)
        //    {
        //        MessageBox.Show("Selected License is Not Active, choose anther one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        btnDetain.Enabled = false;
        //    }

        //    if (strlFilterDrivingLicense1.SelectedLicenseInfo.IsDetaind)
        //    {
        //        MessageBox.Show("Selected License i aleady Detained, choose anther one.","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
        //        btnDetain.Enabled = false;
        //    }
        //    txtFineFees.Focus();
        //    lklShowLicensInfo.Enabled = true;
        //}



        private void frmDetainDrivingLicense_Load(object sender, EventArgs e)
        {
            strlFilterDrivingLicense1.txtLicenseIDFocus();
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = ClsGloabl.CurrentUser.UserID.ToString();
        }

        private void lklShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID   = ClsLicense.Find(_SelectedLicenseID).DriverInfo.PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog(this);
        }

        private void strlFilterDrivingLicense1_OnLicenseSelected(object sender, Licenses.Controls.strlFilterDrivingLicense.LicenseSelectedEventArgs e)
        {
            _SelectedLicenseID = e.LicenseID;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            lklShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (!strlFilterDrivingLicense1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, choose anther one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }

            if (strlFilterDrivingLicense1.SelectedLicenseInfo.IsDetaind)
            {
                MessageBox.Show("Selected License i aleady Detained, choose anther one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }
            txtFineFees.Focus();
            lklShowLicensInfo.Enabled = true;
        }
    }
}

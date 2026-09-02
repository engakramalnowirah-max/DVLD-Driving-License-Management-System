using DVLD_BusinessLayer;
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

namespace Project_DVLD_PresentaionLayer.Applications.Control
{
    public partial class strlApplicationBaseInfo : UserControl
    {
        public strlApplicationBaseInfo()
        {
            InitializeComponent();
        }
        private ClsApplication _Application;
        private int _ApplicationID = -1;
        public int ApplicationID { get { return _ApplicationID; } }

        private void _FillDataApplication()
        {
            lblAppID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblFees.Text = _Application.PaidFees.ToString();
            lblType.Text = ClsApplicationType.Find(_Application.ApplicationTypeID).Title;
            lblFullName.Text = ClsPresone.Find(_Application.ApplicantPersonID).FullName;
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblCreatedByUser.Text = ClsUser.Find(_Application.CreatedByUserID).UserName;
        }
        public void ResatDefultValue()
        {
            lblAppID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[$$$]";
            lblType.Text = "[???]";
            lblFullName.Text = "[???]";
            lblDate.Text = "[??/??/????]";
            lblStatusDate.Text = "[??/??/????]";
            lblCreatedByUser.Text = "[???]"; 
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = ClsApplication.FindBaseApplicationByID(ApplicationID);
            if (_Application == null)
            {
                MessageBox.Show($"This is a Application By ID {ApplicationID} is Not Existing", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResatDefultValue();
                return;
            }
            _ApplicationID = ApplicationID;
            _FillDataApplication();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo PersonInfo = new frmShowPersonInfo(_Application.ApplicantPersonID);
            PersonInfo.ShowDialog(this);
        }
    }
}

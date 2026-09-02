using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Licenses.Controls
{
    public partial class strlFilterDrivingLicense : UserControl
    {
        ////Defined a custom event handler delegate with parameters
        //public event Action<int> OnLicenseSelected;
        //// Create a protected method to raise the event with a parameter
        //protected virtual void LicenseSelected(int LicenseID)
        //{
        //    Action<int> handler = OnLicenseSelected;// Raise the event with the parameter
        //    if (handler != null)
        //    {
        //        handler(LicenseID);
        //    }
        //}


        public class LicenseSelectedEventArgs : EventArgs
        {
            public int LicenseID { get; }
            public int AplicationID { get; }
            public int DriverID {  get; }

            public LicenseSelectedEventArgs(int licenseID, int aplicationID, int driverID)
            {
                LicenseID = licenseID;
                AplicationID = aplicationID;
                DriverID = driverID;
            }
        }

        public event EventHandler<LicenseSelectedEventArgs> OnLicenseSelected;

        private void RaiseOnLicenseSelected(int LicenseID,int ApplicationID,int DriverID)
        {
            RaiseOnLicenseSelected(new LicenseSelectedEventArgs(LicenseID, ApplicationID, DriverID));
        }
        protected virtual void RaiseOnLicenseSelected(LicenseSelectedEventArgs e)
        {
            OnLicenseSelected?.Invoke(this, e);
        }

        private int _LicenseID = -1;

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        public strlFilterDrivingLicense()
        {
            InitializeComponent();
        }

        public ClsLicense SelectedLicenseInfo { get { return strlDriverLicenseInfo1.SelectedLicenseInfo; } }
        public int LicenseID { get { return _LicenseID; } }

        public void LoadLicenseInfo(int LicenseID)
        {

            txtLicenseID.Text = LicenseID.ToString();
            strlDriverLicenseInfo1.LoadDataByLicenseID(LicenseID);
            _LicenseID = strlDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                RaiseOnLicenseSelected(strlDriverLicenseInfo1.SelectedLicenseInfo.LicenseID, strlDriverLicenseInfo1.SelectedLicenseInfo.ApplicationID, strlDriverLicenseInfo1.SelectedLicenseInfo.DriverID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadLicenseInfo(_LicenseID);
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                button1.PerformClick();
            }
        }
    }
}

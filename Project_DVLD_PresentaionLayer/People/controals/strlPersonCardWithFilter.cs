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

namespace Project_DVLD_PresentaionLayer.People
{
    public partial class strlPersonCardWithFilter : UserControl
    {
        //Defined a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;// Raise the event with the parameter
            if (handler != null)
            {
                handler(PersonID);
            }
        }

        public strlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _ShowAddPerson = true;
        public bool AddNowPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Enabled = _ShowAddPerson;
            }

        }

        private bool _FillterEnabled = true;
        public bool FillterEnabled
        {
            get { return _FillterEnabled; }
            set
            {
                _FillterEnabled = value;
                gbFilter.Enabled = _FillterEnabled;
            }
        }

        
        public int PersonID
        {
            get
            {
                return _PersonID;
            }
        }

        public ClsPresone SelectedPersonInfo
        {
            get
            {
                return strlDetails1.SelectedPersonInfo;
            }
        }

        private int _PersonID = -1;


        private void FindBythisIDNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    strlDetails1.LoadPersonInfo(int.Parse(txtValueToFilter.Text));
                    break;

                case "National No":
                    strlDetails1.LoadPersonInfo(txtValueToFilter.Text);
                    break;

                default:
                    break;
            }
            _PersonID = strlDetails1.PersonID;
            if (OnPersonSelected != null && gbFilter.Enabled)
            {
                OnPersonSelected(strlDetails1.PersonID);
            }
        }

        public void LoadPersonInfo(int Person)
        {
            cbFilterBy.SelectedIndex = 0;
          
            txtValueToFilter.Text = Person.ToString();
            FindBythisIDNow();
        }

        private void txtValueToFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnFindLoclDrivingLicenseApplicationBythisID.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersoncs AddNewPerson = new frmAddEditPersoncs();
            AddNewPerson.HandBack += DataBackEvent;
            AddNewPerson.ShowDialog(this);

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValueToFilter.Text = "";
            txtValueToFilter.Focus();
        }

        private void strlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            cbFilterBy.Focus();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtValueToFilter.Text = PersonID.ToString();
            strlDetails1.LoadPersonInfo(PersonID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("");
                return;
            }

            FindBythisIDNow();
        }

        public void FilterFoucus()
        {
            txtValueToFilter.Focus();
        }

        private void txtValueToFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValueToFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtValueToFilter, "This field is required!");
            }
            else
            {

                errorProvider1.SetError(txtValueToFilter, null);

            }
        }
    }
}

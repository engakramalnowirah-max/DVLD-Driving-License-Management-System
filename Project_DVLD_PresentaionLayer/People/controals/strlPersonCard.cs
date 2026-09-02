using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.ManagePeople
{
    public partial class strlDetails : UserControl
    {

        private ClsPresone _Person = new ClsPresone();

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }

        }


        public strlDetails()
        {
            InitializeComponent();
            
        }
        public ClsPresone SelectedPersonInfo
        {
            get { return  this._Person; }
        }
      



        public void LoadPersonInfo(int PersonID)
        {
          
            _PersonID = PersonID;

              _Person = ClsPresone.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Erorr: This Person By ID:"+ _PersonID + " is Not Exist","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            _FillPersonInfoInCountrols();



        }
        public void LoadPersonInfo(string NationalNo)
        {


            _Person = ClsPresone.Find(NationalNo);
            if (_Person == null)
            {
                MessageBox.Show("Erorr: This Person By National Number:" + NationalNo + " is Not Exist", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _FillPersonInfoInCountrols();



        }
        private void _LoadPersonImage()
        {
            if(_Person.Gendor == false)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                pbPersonImage.Image = null;
            }
        }
        private void _FillPersonInfoInCountrols()
        {
            lblID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblGendor.Text = _Person.Gendor == false ? "Male" : "FeMale";
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            _LoadPersonImage();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person.PersonID == -1)
                return;
            frmAddEditPersoncs EditPerson = new frmAddEditPersoncs(_Person.PersonID);
            EditPerson.ShowDialog(this);
            LoadPersonInfo(_Person.PersonID);
            
        }

      
    
    }
}

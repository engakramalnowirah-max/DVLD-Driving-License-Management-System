using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_DVLD_PresentaionLayer
{
    public partial class frmAddEditPersoncs : Form
    {
        public delegate void frmAddEditPersoncsEventHandBack(object instens, int PersonID);
        public event frmAddEditPersoncsEventHandBack HandBack;
       
        enum enMode { Update =0 , AddNew =1 }
        enum enGendor {  Male= 0, FeMale =1 }
        enMode _Mode;
        ClsPresone _Person;
        int _PersonID = 0;

        public frmAddEditPersoncs(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
           

        }
        public frmAddEditPersoncs()
        {
            InitializeComponent();
            _Mode= enMode.AddNew;

         
        }

        private void _FillCountriesToCombobox()
        {
            DataTable dt = ClsCountry.GetCountries();
            foreach (DataRow dr in dt.Rows)
            {
                cbCountreis.Items.Add(dr["CountryName"].ToString());
            }
        }
     
        private void _LoadData()
        {
            _Person = ClsPresone.Find(_PersonID);

            if(_Person == null)
            {
                MessageBox.Show("This a Person By ID :"+_PersonID+" is Not Existing","Not Founde ",MessageBoxButtons.OKCancel);
                this.Close();
                return;
            }

            lblPersonID.Text = _PersonID.ToString();

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            if (_Person.ThirdName != "")
                txtThirdName.Text = _Person.ThirdName;
            else
                txtThirdName.Text = "";

            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            if(_Person.Gendor == false)
            {
                rdbMale.Checked = true;
            }
            else
            {
                rdbFeMale.Checked = true;
            }
            if (_Person.Email != "")
            {
                txtEmail.Text = _Person.Email;
            }
            else
            {
                txtEmail.Text = "";
            }
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            txtPhone.Text = _Person.Phone;

            cbCountreis.SelectedIndex = cbCountreis.FindString(_Person.CountryInfo.CountryName);

            txtAddress.Text = _Person.Address;

            if(_Person.ImagePath != null)
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
                
            }
            else
            {
                pbPersonImage.ImageLocation = null;
            }

            llRemove.Visible = (pbPersonImage.ImageLocation != null);



        }
        private void _ResetDefualtValues()
        {
            _FillCountriesToCombobox();

            if (_Mode == enMode.Update)
            {
                lblTitil.Text = "Update Person";
               
            }
            else
            {
                lblTitil.Text = "Add New Person";
                lblPersonID.Text = "N/A";
                _Person = new ClsPresone();


            }

            rdbMale.Checked = true;

            if (rdbMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountreis.SelectedIndex = cbCountreis.FindString("Yemen");

            

            llRemove.Visible = (pbPersonImage.ImageLocation != null);
           
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";



        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Erorr: You Has Same Validation Erorr :-(","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            if(!_HandelPersonImage())
            {
                return;
            }
            int  NationalityCountryID = ClsCountry.GetCountryByName(cbCountreis.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim(); 
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            _Person.NationalityCountryID = NationalityCountryID;
            if (rdbMale.Checked )
            {
                _Person.Gendor = false;
            }
            else
            {
                _Person.Gendor = true;
            }
            if(pbPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }



            if (_Person.Save())
            {
                lblTitil.Text = "Update Person";
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                MessageBox.Show("Person: Information Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HandBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Erorr: Data is Not Save Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    

        private void frmAddEditPersoncs_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void rdbFeMale_Click(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == "")
            {
                pbPersonImage.Image = Resources.Female_512;
            }
        }

        private void rdbMale_Click(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == "")
            {
                pbPersonImage.Image = Resources.Male_512;
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()) )
            {
                errorProvider1.SetError(txtNationalNo, "This Field is Required !");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, "");
            }


            if (!(txtNationalNo.Text.Trim() != _Person.NationalNo && ClsPresone.isPersonExist(txtNationalNo.Text.Trim())))
            {
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, "");
            }
        }
        private void TextBoxesValidatinos(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, "This Field is Required!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txt, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (!ClsValidation.ValidationEmail(email))
            {
                errorProvider1.SetError(txtEmail, "Invalid Email Format!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private bool _HandelPersonImage()
        {
            if(_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                        throw;
                    }
                    
                }
                if(pbPersonImage.ImageLocation != null)
                {
                    string SouresImagePath = pbPersonImage.ImageLocation.ToString();
                    if(ClsUitl.CopyImageToProjectFolder(ref SouresImagePath))
                    {
                        pbPersonImage.ImageLocation = SouresImagePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Erorr: Copying Image Fill","Erorr",MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFillPath = openFileDialog1.FileName;
                pbPersonImage.ImageLocation = SelectedFillPath;
                llRemove.Visible = true;
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            



            if (rdbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemove.Visible = false;  
        }
    }
}

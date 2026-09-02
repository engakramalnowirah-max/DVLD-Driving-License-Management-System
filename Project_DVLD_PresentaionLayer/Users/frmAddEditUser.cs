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
using static System.Net.Mime.MediaTypeNames;

namespace Project_DVLD_PresentaionLayer.Users
{
    public partial class frmAddEditUser : Form
    {
        private enum enMode { AddNew =1,Update = 2};
        
        private enMode _Mode;

        private int _UserID = 0;

        ClsUser _User;
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            _Mode = enMode.Update;
        }

        private void _ResetDefultValue()
        {
            if(_Mode == enMode.Update)
            {
                lblTitil.Text = "Update User Info";
                this.Text = "Update User";
                tpLoingInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                lblTitil.Text = "Add New User";
                this.Text = "Add New User";
                lblUserID.Text = "[???]";
                tpLoingInfo.Enabled = false;
                _User = new ClsUser();
            }
            
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfrmPass.Text = "";
            chbIsActive.Checked =  true;
        }

        private void _LoadData()
        {
            _User = ClsUser.Find(_UserID);

            if(_User == null )
            {
                MessageBox.Show("Erorr: This User Not Exist By ID:"+_UserID.ToString(),"Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            strlPersonCardWithFilter1.FillterEnabled = false;

            strlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName.ToString();
            txtPassword.Text = _User.Password.ToString();
            txtConfrmPass.Text = _User.Password.ToString();
            chbIsActive.Checked = (_User.IsActive == 0)?false:true;


        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();

            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "UserName Cannot be Blank !");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");
            }
        }


        private void _PasswordValidating(object sender, CancelEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtConfrmPass.Text))
            {
                errorProvider1.SetError(txtConfrmPass, "Password Cannot be Blank!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfrmPass, "");
            }


            

            if (txtConfrmPass.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfrmPass, "Password Confrmation does not match Password !");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfrmPass, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoingInfo.Enabled = true;
                tapcontrol.SelectedIndex = 1;
                return;
            }


           


           if (strlPersonCardWithFilter1.SelectedPersonInfo.PersonID != -1)
           {
                if(ClsUser.isUserExistForPersonID(strlPersonCardWithFilter1.SelectedPersonInfo.PersonID))
                {
                    MessageBox.Show("Selected PersonID already has a user, chose another one!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoingInfo.Enabled = true;
                    tapcontrol.SelectedIndex = 1;
                }
                
           }
           else
           {
                MessageBox.Show("Please Select a Person!",
                                         "selec a Person",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
           }
                


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Erorr: You Has Same Validation Erorr :-(", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            short isActive = 0;
            if (chbIsActive.Checked)
            {
                isActive = 1;
            }
            else
            {
                isActive = 0;
            }

            _User.PersonID = strlPersonCardWithFilter1.SelectedPersonInfo.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = isActive;

            if(_User.Save())
            {
                strlPersonCardWithFilter1.FillterEnabled = false;

                lblTitil.Text = "Update User Info";
                this.Text = "Update User";
                    lblUserID.Text = _User.UserID.ToString();
                    _Mode = enMode.Update;
                    MessageBox.Show("User: Information Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erorr: Data is Not Save Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password Cannot be Blank!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtPassword, "");
            }
        }
    }
}

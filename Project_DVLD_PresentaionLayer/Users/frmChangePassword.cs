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

namespace Project_DVLD_PresentaionLayer.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID = 0;
        ClsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valide!, put the mouse of ","Validation Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _User.Password = txtNewPass.Text.Trim();

            if(_User.Save())
            {
                MessageBox.Show("User: Password Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualValues();
                return;
            }
            else
            {
                MessageBox.Show("Erorr: Password is Not Save Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void NewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPass.Text))
            {
                errorProvider1.SetError(txtNewPass, "New Password Cannot be Blank!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNewPass, "");
            }
        }


        private void _ConfermPassValidating(object sender, CancelEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtConfermPass.Text))
            {
                errorProvider1.SetError(txtConfermPass, "Password Confrmation Cannot be Blank!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfermPass, "");
            }




            if (txtConfermPass.Text.Trim() != txtNewPass.Text.Trim())
            {
                errorProvider1.SetError(txtConfermPass, "Password Confrmation does not match Password !");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfermPass, "");
            }
        }

        private void txtCourrentPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourrentPass.Text))
            {
                errorProvider1.SetError(txtCourrentPass, "Courrent Password Cannot be Blank!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtCourrentPass, "");
            }


            if (txtCourrentPass.Text.Trim() != _User.Password)
            {
                errorProvider1.SetError(txtCourrentPass, "Courrent password is wrong!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtCourrentPass, "");
            }
        }
        private void _ResetDefualValues()
        {
            txtCourrentPass.Text = "";
            txtNewPass.Text = "";
            txtConfermPass.Text = "";
            txtCourrentPass.Focus();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualValues();

            _User = ClsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("Could not Find User By ID = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            strlUserCard1.LoadUserinfo(_UserID);

        }
    }
}

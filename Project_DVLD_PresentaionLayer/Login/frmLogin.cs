using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            string UserName = "", Password = "";
            if(ClsGloabl.GetStoredCreadential(ref UserName,ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chRememberMe.Checked = true;
            }
            else
            {
                chRememberMe.Checked = false;
            }
          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ClsUser User = ClsUser.Find(txtUserName.Text.Trim(),txtPassword.Text.Trim());
            if(User != null)
            {
                if(chRememberMe.Checked )
                {
                    ClsGloabl.RememberUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    ClsGloabl.RememberUserNameAndPassword("","");
                }

                if(User.IsActive == 0)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your account is not active Contact to Your Admin", "Athouizatione error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClsGloabl.CurrentUser = User;
                this.Hide();
                frmMain Main = new frmMain();
                Main.Show();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("invaild UserName/Password.","wrong Login",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
            label1.BackColor = Color.Red;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

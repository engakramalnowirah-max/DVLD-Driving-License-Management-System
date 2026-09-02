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

namespace Project_DVLD_PresentaionLayer.Users.Controls
{
    public partial class strlUserCard : UserControl
    {
        public strlUserCard()
        {
            InitializeComponent();
        }
        private int _UserID = 0;
        public int UserID {
            get { return _UserID; }}

        private ClsUser _User;
    


        private void _FillDataToLable()
        {
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = (_User.IsActive == 0) ? "No" : "Yes";
            strlDetails1.LoadPersonInfo(_User.PersonID);
        }
        public void LoadUserinfo(int UserID)
        {
            _UserID = UserID;

            _User =  ClsUser.Find(UserID);
            if( _User == null )
            {
                MessageBox.Show("Erorr: User Is Not Exist By ID:"+_UserID,"Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _FillDataToLable();
        }
    }
}

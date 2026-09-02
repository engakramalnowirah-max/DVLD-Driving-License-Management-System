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

namespace Project_DVLD_PresentaionLayer.ApplicationType
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _ID = 0;
        private ClsApplicationType _ApplicationType;

        public frmUpdateApplicationType(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = ClsApplicationType.Find(_ID);

            if (_ApplicationType != null)
            {
                lblID.Text = _ApplicationType.ID.ToString();
                txtTitle.Text = _ApplicationType.Title.ToString();
                txtFees.Text = _ApplicationType.Fees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Same Fileds are not valide!, put the mouse over the Red Icon(s)", "Validattion Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _ApplicationType.Title = txtTitle.Text.Trim();
            _ApplicationType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if(_ApplicationType.Save())
            {
                MessageBox.Show("Application Type: Information Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Application Type: Data is Not Save Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Title Cannot be Blank");
                e.Cancel = true;
                return;
            }
           


            if (txtTitle.Text.Any(char.IsDigit))
            {
                errorProvider1.SetError(txtTitle, "Title cannot contain numbers");
                e.Cancel = true;
                return;
            }
            

                errorProvider1.SetError(txtTitle, "");
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text))
            {
                errorProvider1.SetError(txtFees, "Fees Cannot be Blank");
                e.Cancel = true;
                return;
            }

            if(!decimal.TryParse(txtFees.Text,out _))
            {
                errorProvider1.SetError(txtFees, "Fees must be a valid number");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(txtFees, "");
        }
    }
}

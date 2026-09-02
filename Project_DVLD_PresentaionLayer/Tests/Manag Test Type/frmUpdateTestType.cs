using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Manag_Test_Type
{
    public partial class frmUpdateTestType : Form
    {
        private ClsTestType.enTestType TestTypeID = ClsTestType.enTestType.VisionTest;
        ClsTestType _TestType;

        public frmUpdateTestType(int ID)
        {
            InitializeComponent();
            TestTypeID = (ClsTestType.enTestType)ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Same Fileds are not valide!, put the mouse over the Red Icon(s)", "Validattion Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            _TestType.Title = txtTitle.Text.Trim();
            _TestType.Description = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
            {
                MessageBox.Show("Test Type: Information Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Test Type: Data is Not Save Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {

            _TestType = ClsTestType.Find(TestTypeID);

            if (_TestType == null)
            {
                MessageBox.Show("Erorr: Same thing is wrong :-(", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblID.Text = _TestType.ID.ToString();
            txtTitle.Text = _TestType.Title.ToString();
            txtDescription.Text = _TestType.Description.ToString();
            txtFees.Text = _TestType.Fees.ToString();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "This Record Cannot be Blank");
                e.Cancel = true;
                return;
            }



            if (txtTitle.Text.Any(char.IsDigit))
            {
                errorProvider1.SetError(txtTitle, "This Record cannot contain numbers");
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

            if (!decimal.TryParse(txtFees.Text, out _))
            {
                errorProvider1.SetError(txtFees, "Fees must be a valid number");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(txtFees, "");
        }
    }
}

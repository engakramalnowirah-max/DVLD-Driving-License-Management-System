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

namespace Project_DVLD_PresentaionLayer.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private ClsTestType.enTestType _TestType = ClsTestType.enTestType.VisionTest;

        private ClsTest Test ;

        public frmTakeTest(int TestAppointmentID,ClsTestType.enTestType TestType)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestType;
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            strlScheduledTest1.TestTypeID = _TestType;
            strlScheduledTest1.LoadInfo(_TestAppointmentID);

            if(strlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            lblMessageUser.Visible = false;

            int TestID = strlScheduledTest1.TestID;
            if (TestID != -1)
            {
                Test = ClsTest.Find(TestID);

                if(Test.TestResult == true)
                {
                    rdPass.Checked = true;
                }
                else
                    rdFaill.Checked = true;

                txtNotes.Text = Test.Notes;
                lblMessageUser.Visible = true;
                btnSave.Enabled = false;
                rdFaill.Enabled = false;
                rdPass.Enabled = false;
            }
            else
            {

                Test = new ClsTest();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            Test.TestAppointmentID = _TestAppointmentID;
            Test.TestResult = rdPass.Checked;
            Test.Notes = txtNotes.Text.Trim();
            Test.CreateByUserID = ClsGloabl.CurrentUser.UserID;

            if(MessageBox.Show("are you suer do want to Save this Result", "Information",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)== DialogResult.Yes)
            {
                if (Test.Save())
                {
                    
                    MessageBox.Show("Data  Test Save Sucessfully", "Sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    return;
                }
                else
                {
                    MessageBox.Show("Data Save Erorr", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
        
        }

       
    }
}

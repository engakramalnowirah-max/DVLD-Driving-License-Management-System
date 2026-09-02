using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using Project_DVLD_PresentaionLayer.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.LocalDrivingApplications
{
    public partial class frmNewLocalApplication : Form
    {
        private enum enMode { AddNew =0,Update =1 }
        private enMode _Mode;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _PersonIDOnSelecte = -1;
        private ClsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmNewLocalApplication(int ID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = ID;
            _Mode = enMode.Update;
        }

        public frmNewLocalApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        private void _FilllicenseClassesToComboBox()
        {
            DataTable dt = ClsLicenseClass.GetAllLicensesClasses();
            foreach (DataRow dr in dt.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"]);
            }
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefrechDefaultValue()
        {
            _FilllicenseClassesToComboBox();

            

            if (_Mode == enMode.AddNew)
            {
                lblTitil.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                lblID.Text = "[???]";
                tabPage2.Enabled = false;
                cbLicenseClass.SelectedIndex = 2;
                _LocalDrivingLicenseApplication = new ClsLocalDrivingLicenseApplication();
                lblAppDate.Text = DateTime.Now.ToShortDateString();
                lblAppFees.Text = ClsApplicationType.Find((int)ClsApplication.enApplicationType.NewLocalDrivingLicenseService).Fees.ToString();
                lblByUser.Text = ClsGloabl.CurrentUser.UserName;


            }
            else
            {
                lblTitil.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                tabPage2.Enabled = true;
                btnSave.Enabled = true;
            }
            

        }

        private void _LoadData()
        {
            _LocalDrivingLicenseApplication = ClsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Erorr: This Application Not Exist By ID:" + _LocalDrivingLicenseApplicationID.ToString(), "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            strlPersonCardWithFilter2.FillterEnabled = false;
            strlPersonCardWithFilter2.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);

            lblID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblAppFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(ClsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblByUser.Text = ClsUser.Find(_LocalDrivingLicenseApplication.CreatedByUserID).UserName.ToString();
            
        }

        private void frmNewLocalApplication_Load(object sender, EventArgs e)
        {
            _RefrechDefaultValue();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tabPage2.Enabled = true;
                tabControl1.SelectedIndex = 1;
                return;
            }

            if (strlPersonCardWithFilter2.SelectedPersonInfo.PersonID != -1)
            {
               
                
                    btnSave.Enabled = true;
                    tabPage2.Enabled = true;
                    tabControl1.SelectedIndex = 1;
                

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
            int licenseClassID = ClsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            int ActiveApplicationID = ClsApplication.GetActiveActicationIDForLicensClass(strlPersonCardWithFilter2.PersonID, 1, licenseClassID);
            if (ActiveApplicationID != 0)
            {
                MessageBox.Show("Chose another Licens Class, the selected Person Already having this Type", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = strlPersonCardWithFilter2.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = ClsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblAppFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = ClsGloabl.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = licenseClassID;

            if (_LocalDrivingLicenseApplication.Save())
            {
               
                    strlPersonCardWithFilter2.FillterEnabled = false;
                    lblTitil.Text = "Update Local Driving License Application";
                    this.Text = "Update Local Driving License Application";
                    lblID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                    _Mode = enMode.Update;
                    MessageBox.Show("Application: Information Save Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erorr: Data is Not Save Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmNewLocalApplication_Activated(object sender, EventArgs e)
        {
           // strlPersonCardWithFilter2.FilterFoucus();
        }

        private void strlPersonCardWithFilter2_OnPersonSelected(int obj)
        {
            _PersonIDOnSelecte = obj;
        }
    }
}

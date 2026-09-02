using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Licenses.International_Licnese;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Licenses.Controls
{
    public partial class strlDriverLicenses : UserControl
    {
        public strlDriverLicenses()
        {
            InitializeComponent();
        }

        private int _DriverID;
        private ClsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;


        private void _LoadLocalLicenseInfo()
        {

            _dtDriverLocalLicensesHistory = ClsDriver.GetLicenses(_DriverID);


            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            }
        }


        private void _LoadInternationalLicenseInfo()
        {

            _dtDriverInternationalLicensesHistory = ClsDriver.GetInternationalLicenses(_DriverID);


            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = ClsDriver.FindByDriverID(_DriverID);
            if(_Driver == null)
            {
                MessageBox.Show("Erorr Licenses Is not exsiting By Driver ID :"+DriverID.ToString(),"Not Fuonde",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void LoadInfoByPersonID(int PersonID)
        {

            _Driver = ClsDriver.FindByPersonID(PersonID);
            
            if (_Driver == null)
            {
                MessageBox.Show("Erorr Licenses Is not exsiting By Person ID :" + PersonID.ToString(), "Not Fuonde", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        private void showLicneseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicense frm = new frmShowLicense((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog(this);
        }

        private void showLicneseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicense frm = new frmShowInternationalLicense((int)dgvInternationalLicensesHistory.CurrentRow.Cells[1].Value);
            frm.ShowDialog(this);
        }
    }
}

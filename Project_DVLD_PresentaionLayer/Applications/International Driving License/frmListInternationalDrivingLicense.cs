using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Licenses;
using Project_DVLD_PresentaionLayer.Licenses.International_Licnese;
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

namespace Project_DVLD_PresentaionLayer.Applications.International_Driving_License
{
    public partial class frmListInternationalDrivingLicense : Form
    {
        public frmListInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataTable _dtInternationalLicenseApplication = new DataTable();

        private void frmListInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplication = ClsInternationalLicense.GetAll();
            dgvInternationalLicenseApplication.DataSource = _dtInternationalLicenseApplication;
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRows.Text = dgvInternationalLicenseApplication.Rows.Count.ToString();
            if (dgvInternationalLicenseApplication.Rows.Count > 0)
            {
                dgvInternationalLicenseApplication.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseApplication.Columns[0].Width = 110;

                dgvInternationalLicenseApplication.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseApplication.Columns[1].Width = 110;

                dgvInternationalLicenseApplication.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseApplication.Columns[2].Width = 110;

                dgvInternationalLicenseApplication.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseApplication.Columns[3].Width = 110;

                dgvInternationalLicenseApplication.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseApplication.Columns[4].Width = 150;

                dgvInternationalLicenseApplication.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenseApplication.Columns[5].Width = 150;

                dgvInternationalLicenseApplication.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseApplication.Columns[6].Width = 110;
                ((DataGridViewCheckBoxColumn)dgvInternationalLicenseApplication.Columns[6]).TrueValue = 1;
                ((DataGridViewCheckBoxColumn)dgvInternationalLicenseApplication.Columns[6]).FalseValue = 0;


                dgvInternationalLicenseApplication.Columns[7].HeaderText = "Created By User";
                dgvInternationalLicenseApplication.Columns[7].Width = 110;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicense NewInternationalLicense = new frmAddNewInternationalLicense();
            NewInternationalLicense.ShowDialog(this);
            frmListInternationalDrivingLicense_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtValueFilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtValueFilter.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                txtValueFilter.Text = "";
                txtValueFilter.Focus();


            }
        }

        private void txtValueFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Int.License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "L.License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;


            }


            if (txtValueFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplication.DefaultView.RowFilter = "";
                lblNumberOfRows.Text = _dtInternationalLicenseApplication.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "")

                _dtInternationalLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtValueFilter.Text.Trim());
           

            lblNumberOfRows.Text = _dtInternationalLicenseApplication.Rows.Count.ToString();
        }

        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvInternationalLicenseApplication.ClearSelection();
                dgvInternationalLicenseApplication.Rows[e.RowIndex].Selected = true;
                dgvInternationalLicenseApplication.CurrentCell = dgvInternationalLicenseApplication.Rows[e.RowIndex].Cells[0];
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
            {
                _dtInternationalLicenseApplication.DefaultView.RowFilter = "";
            }
            else
            {
                _dtInternationalLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            }
            lblNumberOfRows.Text = _dtInternationalLicenseApplication.Rows.Count.ToString();

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = ClsDriver.FindByDriverID((int)dgvInternationalLicenseApplication.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonId);
            frm.ShowDialog(this);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicense frm = new frmShowInternationalLicense((int)dgvInternationalLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog(this);
        }

        private void showPersonLicnesesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = ClsDriver.FindByDriverID((int)dgvInternationalLicenseApplication.CurrentRow.Cells[2].Value).PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonId);
            frm.ShowDialog(this);
        }
    }
}

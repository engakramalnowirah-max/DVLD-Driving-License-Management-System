using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.Applications.Release_Detained_Driving_Licsense;
using Project_DVLD_PresentaionLayer.Licenses;
using Project_DVLD_PresentaionLayer.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_DVLD_PresentaionLayer.Detain_License
{
    public partial class frmLManageDetainedDrivingLicense : Form
    {
        public frmLManageDetainedDrivingLicense()
        {
            InitializeComponent();
        }

        private void pbDetain_Click(object sender, EventArgs e)
        {
            frmDetainDrivingLicense DetainDrivinglicense = new frmDetainDrivingLicense();
            DetainDrivinglicense.ShowDialog(this);
            frmLManageDetainedDrivingLicense_Load(null,null);
        }

        private void pbRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedDrivingLicenese ReleaseDetainedDrivingLicense = new frmReleaseDetainedDrivingLicenese();
            ReleaseDetainedDrivingLicense.ShowDialog(this);
            frmLManageDetainedDrivingLicense_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DataTable _dtDetainedDrivingLicense = new DataTable();

        private void frmLManageDetainedDrivingLicense_Load(object sender, EventArgs e)
        {
            _dtDetainedDrivingLicense = ClsDetainedLicense.GetAllDetainedLicenses();
            cbFilterBy.SelectedIndex = 0;
            dgvDetainedDrivingLicense.DataSource = _dtDetainedDrivingLicense;
            lblNumberOfRows.Text = dgvDetainedDrivingLicense.Rows.Count.ToString();
            if (dgvDetainedDrivingLicense.Rows.Count > 0)
            {
                dgvDetainedDrivingLicense.Columns[0].HeaderText = "Detain ID";
                dgvDetainedDrivingLicense.Columns[0].Width = 100;

                dgvDetainedDrivingLicense.Columns[1].HeaderText = "License ID";
                dgvDetainedDrivingLicense.Columns[1].Width = 100;

                dgvDetainedDrivingLicense.Columns[2].HeaderText = "Detain Date";
                dgvDetainedDrivingLicense.Columns[2].Width = 130;

                dgvDetainedDrivingLicense.Columns[3].HeaderText = "Is Released";
                dgvDetainedDrivingLicense.Columns[3].Width = 100;

                dgvDetainedDrivingLicense.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedDrivingLicense.Columns[4].Width = 100;

                dgvDetainedDrivingLicense.Columns[5].HeaderText = "Release Date";
                dgvDetainedDrivingLicense.Columns[5].Width = 130;

                dgvDetainedDrivingLicense.Columns[6].HeaderText = "National No";
                dgvDetainedDrivingLicense.Columns[6].Width = 100;

                dgvDetainedDrivingLicense.Columns[7].HeaderText = "Full Name";
                dgvDetainedDrivingLicense.Columns[7].Width = 220;

                dgvDetainedDrivingLicense.Columns[8].HeaderText = "Rlease App ID";
                dgvDetainedDrivingLicense.Columns[8].Width = 90;

                ((DataGridViewCheckBoxColumn)dgvDetainedDrivingLicense.Columns[3]).TrueValue = 1;
                ((DataGridViewCheckBoxColumn)dgvDetainedDrivingLicense.Columns[3]).FalseValue = 0;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtValueWithFilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtValueWithFilter.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                txtValueWithFilter.Text = "";
                txtValueWithFilter.Focus();
            }
        }

        private void txtValueWithFilter_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Detaind ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;


            }

            if (txtValueWithFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedDrivingLicense.DefaultView.RowFilter = "";
                lblNumberOfRows.Text = dgvDetainedDrivingLicense.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "DetainID"|| FilterColumn == "ReleaseApplicationID")

                _dtDetainedDrivingLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtValueWithFilter.Text.Trim());
            else
                _dtDetainedDrivingLicense.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtValueWithFilter.Text.Trim());

            lblNumberOfRows.Text = dgvDetainedDrivingLicense.Rows.Count.ToString();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo Person = new frmShowPersonInfo();
            Person.LoadPersonInfo(dgvDetainedDrivingLicense.CurrentRow.Cells[6].Value.ToString());
            Person.ShowDialog(this);
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicense License = new frmShowLicense((int)dgvDetainedDrivingLicense.CurrentRow.Cells[1].Value);
            License.ShowDialog(this);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = ClsLicense.Find((int)dgvDetainedDrivingLicense.CurrentRow.Cells[1].Value).DriverInfo.PersonID;
            frmLicenseHistory Licenses = new frmLicenseHistory(PersonID);
            Licenses.ShowDialog(this);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedDrivingLicenese releaseDetainedLicense = new frmReleaseDetainedDrivingLicenese((int)dgvDetainedDrivingLicense.CurrentRow.Cells[1].Value, (int)dgvDetainedDrivingLicense.CurrentRow.Cells[0].Value);
            releaseDetainedLicense.ShowDialog(this);
            frmLManageDetainedDrivingLicense_Load(null, null);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
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
                _dtDetainedDrivingLicense.DefaultView.RowFilter = "";
            }
            else
            {
                _dtDetainedDrivingLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            }
            lblNumberOfRows.Text = _dtDetainedDrivingLicense.Rows.Count.ToString();
        }

        private void dgvDetainedDrivingLicense_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDetainedDrivingLicense.ClearSelection();
                dgvDetainedDrivingLicense.Rows[e.RowIndex].Selected = true;
                dgvDetainedDrivingLicense.CurrentCell = dgvDetainedDrivingLicense.Rows[e.RowIndex].Cells[0];
            }
        }
    }
}

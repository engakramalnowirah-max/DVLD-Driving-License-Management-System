using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_DVLD_PresentaionLayer.Gloabl_Classes;
using DVLD_BusinessLayer;

namespace Project_DVLD_PresentaionLayer.Users
{
    public partial class frmUserList : Form
    {
        public frmUserList()
        {
            InitializeComponent();
        }
        private static DataTable dtAllUsers; 

         

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo UserInfo = new frmShowUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            UserInfo.ShowDialog(this);
        }
        private void _RefrechDataToDGV()
        {
            dgvUsers.DataSource = ClsUser.GetAllUsrs().DefaultView.ToTable(false, "UserID", "PersonID", "Name", "UserName", "IsActive");
        }
        private void frmUserList_Load(object sender, EventArgs e)
        {
            dtAllUsers  = ClsUser.GetAllUsrs();
            cbFilterBy.SelectedIndex = 0;
            dgvUsers.DataSource = dtAllUsers;
            lblNumberOfRows.Text = dgvUsers.Rows.Count.ToString();
            if(dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 100;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 100;

                dgvUsers.Columns[2].HeaderText = "Name";
                dgvUsers.Columns[2].Width = 200;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 130;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 100;

                ((DataGridViewCheckBoxColumn)dgvUsers.Columns[4]).TrueValue = 1;
                ((DataGridViewCheckBoxColumn)dgvUsers.Columns[4]).FalseValue = 0;

            }
        }

        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvUsers.ClearSelection();
                dgvUsers.Rows[e.RowIndex].Selected = true;
                dgvUsers.CurrentCell = dgvUsers.Rows[e.RowIndex].Cells[0];
            }
        }

        private void pbAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser AddUser = new frmAddEditUser();
            AddUser.ShowDialog(this);
            _RefrechDataToDGV();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser EditUser = new frmAddEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            EditUser.ShowDialog(this);
            frmUserList_Load(null, null);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sour Are You Want To DeleteLoclDrivingLicenseApplicationing This User By ID :" + dgvUsers.CurrentRow.Cells[0].Value.ToString(), "Q", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ClsUser.Delete((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show($"Delete User By ID {(int)dgvUsers.CurrentRow.Cells[0].Value} Successfully :-) ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _RefrechDataToDGV();
                    return;
                }
                else
                {
                    MessageBox.Show("Erorr For this Processing", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser AddUser = new frmAddEditUser();
            AddUser.ShowDialog(this);
            _RefrechDataToDGV();
        }

        private void txtValueFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "Name";
                    break;
            
                default:
                    FilterColumn = "None";
                    break;


            }


            if (txtValueFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                dtAllUsers.DefaultView.RowFilter = "";
                lblNumberOfRows.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID"|| FilterColumn == "UserID")

                dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtValueFilter.Text.Trim());
            else
                dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtValueFilter.Text.Trim());

            lblNumberOfRows.Text = dgvUsers.Rows.Count.ToString();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword ChangePass = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            ChangePass.ShowDialog(this);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "Is Active")
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

            if(FilterValue == "All")
            {
                dtAllUsers.DefaultView.RowFilter = "";
            }
            else
            {
                dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn,FilterValue);
            }
            lblNumberOfRows.Text = dtAllUsers.Rows.Count.ToString();


                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

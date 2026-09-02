using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.People;

namespace Project_DVLD_PresentaionLayer
{
    public partial class frmPeople : Form
    {

        
        public frmPeople()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static DataTable dtAllPeople = ClsPresone.GetAllPersons();

        DataTable dtPeople = dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
            "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");

     

        private void dgvPeople_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvPeople.ClearSelection();
                dgvPeople.Rows[e.RowIndex].Selected = true;
                dgvPeople.CurrentCell = dgvPeople.Rows[e.RowIndex].Cells[0];
            }
        }
        private void _RefrechDataToDGV()
        {
                  DataTable _dtAllPeople = ClsPresone.GetAllPersons();

              DataTable _dtPeople = dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
                       "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");

                               dgvPeople.DataSource = _dtPeople;
                               
                               lblNumberOfRows.Text = dgvPeople.Rows.Count.ToString(); 
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRows.Text = dgvPeople.Rows.Count.ToString();
            if(dgvPeople.Rows.Count > 0 )
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 120;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 90;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 160;

                dgvPeople.Columns[8].HeaderText = "Country";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 130;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 140;
            }
        }
        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.LightGray;
            pictureBox2.Cursor = Cursors.Hand;
        }






        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmAddEditPersoncs AddPerson= new frmAddEditPersoncs();
            AddPerson.ShowDialog(this);
            _RefrechDataToDGV();
            frmPeople_Load(null, null);


        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo DetailsPerson = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            DetailsPerson.ShowDialog(this);
            _RefrechDataToDGV();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersoncs AddPerson = new frmAddEditPersoncs((int)dgvPeople.CurrentRow.Cells[0].Value);
            AddPerson.ShowDialog(this);
            _RefrechDataToDGV();
            frmPeople_Load(null, null);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersoncs AddPerson = new frmAddEditPersoncs();
            AddPerson.ShowDialog(this);
            _RefrechDataToDGV();
            frmPeople_Load(null, null);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sour Are You Want To Delete This Person By ID :"+ dgvPeople.CurrentRow.Cells[0].Value.ToString(), "Q",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ClsPresone.Delete((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Person Successfully :-) ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _RefrechDataToDGV();
                    frmPeople_Load(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Erorr to this Process", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void FindPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindPerson FindPerson = new frmFindPerson();
            FindPerson.ShowDialog(this);
            _RefrechDataToDGV();
        }

        private void txtValueWithFilter_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                        break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;
                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;
                case "Country":
                    FilterColumn = "CountryName";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn= "None";
                    
                    break; 


            }

            

            if (txtValueWithFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                dtPeople.DefaultView.RowFilter = "";
                lblNumberOfRows.Text = dgvPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")

                dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtValueWithFilter.Text.Trim());
            else
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtValueWithFilter.Text.Trim());

            lblNumberOfRows.Text = dgvPeople.Rows.Count.ToString();
        }

      

        private void txtValueWithFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtValueWithFilter.Visible = false;



            }
            else
            {
                txtValueWithFilter.Visible = true;
                txtValueWithFilter.Text = "";
                txtValueWithFilter.Focus();
            }
        }
    }
}

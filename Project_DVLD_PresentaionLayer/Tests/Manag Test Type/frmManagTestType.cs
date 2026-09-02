using DVLD_BusinessLayer;
using Project_DVLD_PresentaionLayer.ApplicationType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Manag_Test_Type
{
    public partial class frmManagTestType : Form
    {
        public frmManagTestType()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable _dtTestType;


     
        private void frmManagTestType_Load(object sender, EventArgs e)
        {
            _dtTestType = ClsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestType;
            lblNumberOfRows.Text = dgvTestTypes.Rows.Count.ToString();
            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 100;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 130;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 350;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 90;
            }
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType UpdatTest = new frmUpdateTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            UpdatTest.ShowDialog(this);
            frmManagTestType_Load(null,null);
        }

   
        private void dgvTestTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvTestTypes.ClearSelection();
                dgvTestTypes.Rows[e.RowIndex].Selected = true;
                dgvTestTypes.CurrentCell = dgvTestTypes.Rows[e.RowIndex].Cells[0];
            }
        }
    }
}

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

namespace Project_DVLD_PresentaionLayer.ApplicationType
{
    public partial class frmListApplicationTypes : Form
    {
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private DataTable _dtApplicationType ;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplicationType = ClsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationType;
            lblNumberOfRows.Text = dgvApplicationTypes.Rows.Count.ToString();
            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 130;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 350;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 130;
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType UpdatAppliction = new frmUpdateApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            UpdatAppliction.ShowDialog(this);
            frmListApplicationTypes_Load(null,null);
        }

        private void dgvApplicationTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvApplicationTypes.ClearSelection();
                dgvApplicationTypes.Rows[e.RowIndex].Selected = true;
                dgvApplicationTypes.CurrentCell = dgvApplicationTypes.Rows[e.RowIndex].Cells[0];
            }
        }
    }

}

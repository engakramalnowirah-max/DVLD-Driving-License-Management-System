using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.People
{
    public partial class frmFindPerson : Form
    {
        public delegate int HandelDataBackToAther(object obj, int PersonID);
        public event HandelDataBackToAther DataBack;
        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, strlPersonCardWithFilter1.PersonID);
            this.Close();
        }
    }
}

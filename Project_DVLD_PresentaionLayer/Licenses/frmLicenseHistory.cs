using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Licenses
{
    public partial class frmLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            strlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            strlDriverLicenses1.LoadInfoByPersonID(_PersonID);
            strlPersonCardWithFilter1.FillterEnabled = false;
        }
        public frmLicenseHistory()
        {
            InitializeComponent();

        }

        public void LoadDataByPersonID(int PeronID)
        {
            _PersonID = PeronID;
            strlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            strlDriverLicenses1.LoadInfoByPersonID(_PersonID);

            strlPersonCardWithFilter1.FillterEnabled = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

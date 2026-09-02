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

namespace Project_DVLD_PresentaionLayer.Licenses.International_Licnese
{
    public partial class frmShowInternationalLicense : Form
    {
        int _InternationalLicenseID = -1;
        public frmShowInternationalLicense(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicense_Load(object sender, EventArgs e)
        {
            strlInternationalLicenseInfo1.LoadData(_InternationalLicenseID);
        }
    }
}

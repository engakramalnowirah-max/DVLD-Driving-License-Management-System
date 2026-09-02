using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Applications.Local_Driving_License
{
    public partial class frmShowLocalDrivingLicenseApplicationInfo : Form
    {
        public frmShowLocalDrivingLicenseApplicationInfo(int LDLApplicationID)
        {
            InitializeComponent();
            strlDetailsLoclDrivingLicenseApplication1.LoadLoclDrivingLicensApplicationByID(LDLApplicationID);
        }
    }
}

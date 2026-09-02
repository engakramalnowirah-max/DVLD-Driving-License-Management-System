using Project_DVLD_PresentaionLayer.ManagePeople;
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
    public partial class frmShowPersonInfo : Form
    {
        
        public frmShowPersonInfo(int personID)
        {
            InitializeComponent();
            
            strlDetails1.LoadPersonInfo(personID);
        }
        public frmShowPersonInfo()
        {
            InitializeComponent();

        }
        public void LoadPersonInfo(string National)
        {
            strlDetails1.LoadPersonInfo(National);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}

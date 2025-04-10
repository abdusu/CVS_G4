using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVS_G4
{
    public partial class ManageProfile : Form
    {
        public ManageProfile()
        {
            InitializeComponent();
            manageStudentPan.Visible = false;
            manageUserPan.Visible = false;
        }

        private void btnLodfromExcel_Click(object sender, EventArgs e)
        {
            manageStudentPan.Visible = true;
            manageUserPan.Visible = false;
        }

        private void manageStudentPan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddOneStudent_Click(object sender, EventArgs e)
        {
            manageStudentPan.Visible = false;
            manageUserPan.Visible = true;
        }
    }
}

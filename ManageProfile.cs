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
            startManagePan.Visible = true;
            manageStudentPan.Visible = false;
            manageUserPan.Visible = false;
            manageMenuBar.Visible = true; ;
            manageStudentMenu.Visible = false;
            manageUserMenu.Visible = false;
        }

        private void btnLodfromExcel_Click(object sender, EventArgs e)
        {
            manageStudentPan.Visible = true;
            manageUserPan.Visible = false;
            manageMenuBar.Visible = false;
            manageStudentMenu.Visible = true;
            manageUserMenu.Visible = false;
            startManagePan.Visible = false;
        }

        private void manageStudentPan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddOneStudent_Click(object sender, EventArgs e)
        {
            manageStudentPan.Visible = false;
            manageUserPan.Visible = true;
            manageMenuBar.Visible = false;
            manageStudentMenu.Visible = false;
            manageUserMenu.Visible = true;
            startManagePan.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            startManagePan.Visible = true;
            manageStudentPan.Visible = false;
            manageUserPan.Visible = false;
            manageMenuBar.Visible = true; ;
            manageStudentMenu.Visible = false;
            manageUserMenu.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            startManagePan.Visible = true;
            manageStudentPan.Visible = false;
            manageUserPan.Visible = false;
            manageMenuBar.Visible = true; ;
            manageStudentMenu.Visible = false;
            manageUserMenu.Visible = false;
        }
    }
}

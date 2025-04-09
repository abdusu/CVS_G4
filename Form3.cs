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
    public partial class addStudent : Form
    {
        public addStudent()
        {
            InitializeComponent();
            startPan.Visible = true;
            addAllStudentpan.Visible = false;
            AddOneStudentPan.Visible = false;
           
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            startPan.Visible = false;
            addAllStudentpan.Visible = true;
            AddOneStudentPan.Visible = false;
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            addAllStudentpan.Visible = false;
            AddOneStudentPan.Visible = true;
            startPan.Visible = false;

        }
    }
}

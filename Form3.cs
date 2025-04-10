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
            startpan.Visible = true;
            addAllStudentpan.Visible = false;
            AddOneStudentPan.Visible = false;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            addAllStudentpan.Visible = true;
            AddOneStudentPan.Visible = false;
            startpan.Visible = false;

        }

        private void btnAddOneStudent_Click(object sender, EventArgs e)
        {
            startpan.Visible = false;
            addAllStudentpan.Visible = false;
            AddOneStudentPan.Visible = true;
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {

        }

        private void addStudent_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

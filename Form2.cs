﻿using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class Admin : Form
    {
        public Admin(string username, string role, Image photo)
        {
            InitializeComponent();

            label1.Text = username;
            lblUsername.Text = role;
            adminphoto.Image = photo;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            logIn newForm = new logIn();
            newForm.Show();
        }
    }
}

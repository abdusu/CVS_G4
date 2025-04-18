using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace CVS_G4
{
    public partial class addUser : Form
    {

        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        public addUser()
        {
            InitializeComponent();
            Connect();
            AutoId();
            AutouserId();
            addAdminPan.Visible = false;
            addRegiPann.Visible = false;
            startPan.Visible = true;
        }

        private void Connect()
        {
            try
            {
                string connectionString = "Server=localhost;Database=cvs_g4;User Id=root;Password=abdu;";
                con = new MySqlConnection(connectionString);
                con.Open();
                //MessageBox.Show("Database connected successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
            }
        }



        public void AutoId()
        {
            try
            {
                //con.Open();
                string query = "SELECT MAX(ID) FROM admin";
                using (cmd = new MySqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        txtAdminID.Text = "Admin001";
                    }
                    else
                    {
                        string maxAdminId = result.ToString(); // e.g., "CH015"
                        long idNumber = long.Parse(maxAdminId.Substring(5)); // Extract numeric part
                        idNumber++;
                        txtAdminID.Text = "Admin" + idNumber.ToString("D3"); // Format to 3 digits with leading zeros
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed! Please try again.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AutouserId()
        {
            try
            {
                //con.Open();
                string query = "SELECT MAX(ID) FROM user";
                using (cmd = new MySqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        txtUserId.Text = "RE001";
                    }
                    else
                    {
                        string maxAdminId = result.ToString(); // e.g., "CH015"
                        long idNumber = long.Parse(maxAdminId.Substring(2)); // Extract numeric part
                        idNumber++;
                        txtUserId.Text = "RE" + idNumber.ToString("D3"); // Format to 3 digits with leading zeros
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed! Please try again.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an user Image";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    userPhoto.Image = new Bitmap(ofd.FileName);
                }
            }
        }

        private void addUser_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            addAdminPan.Visible = true;
            addRegiPann.Visible = false;
            startPan.Visible = false;
            txtAdminUsername.Clear();
            txtUserName.Clear();
            txtUserPassword.Clear();
            txtUserFullName.Clear();
            txtSchoolName.Clear();
            txtSchoolCode.Clear();
            txtAddress.Clear();
            userPhoto.Image = null;
            schoolLogo.Image = null;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            addAdminPan.Visible = false;
            addRegiPann.Visible = true;
            startPan.Visible = false;
            txtAdminUsername.Clear();
            txtAdminPassword.Clear();
            txtAdminFullName.Clear();
            adminPhto.Image = null;
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an school Logo Image";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    schoolLogo.Image = new Bitmap(ofd.FileName);
                }
            }
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an Admin Photo";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    adminPhto.Image = new Bitmap(ofd.FileName);
                }
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            adminPhto.Image = null;
            txtAdminID.Clear();
            txtAdminUsername.Clear();
            txtAdminPassword.Clear();
            txtAdminFullName.Clear();
            AutoId();
        }

        private void btnAdminSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdminID.Text) || string.IsNullOrWhiteSpace(txtAdminUsername.Text) || string.IsNullOrWhiteSpace(txtAdminPassword.Text) || string.IsNullOrWhiteSpace(txtAdminFullName.Text))
            {
                MessageBox.Show("Please fill in all fields (ID, UserName, Password, FullName).", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (adminPhto.Image == null)
            {
                MessageBox.Show("Please select a photo.");
                return;
            }

            // Convert image to byte[]
            MemoryStream ms = new MemoryStream();
            adminPhto.Image.Save(ms, adminPhto.Image.RawFormat);
            byte[] photoData = ms.ToArray();
            try
            {
                //con.Open();
                string query = "INSERT INTO admin (ID, UserName, Password, FullName, AdminPhoto) VALUES (@ID, @UserName, @Password, @FullName, @Photo)";
                cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", txtAdminID.Text);
                cmd.Parameters.AddWithValue("@UserName", txtAdminUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtAdminPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@FullName", txtAdminFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Photo", photoData);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Admin saved successfully!");
                AutoId();

                // Clear inputs
                txtAdminUsername.Clear();
                txtAdminPassword.Clear();
                txtAdminFullName.Clear();
                adminPhto.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserId.Text) || string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtUserPassword.Text) || string.IsNullOrWhiteSpace(txtUserFullName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtSchoolCode.Text) ||
                string.IsNullOrWhiteSpace(txtSchoolName.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userPhoto.Image == null || schoolLogo.Image == null)
            {
                MessageBox.Show("Please select a photo.");
                return;
            }

            // Convert image to byte[]
            MemoryStream ms = new MemoryStream();
            schoolLogo.Image.Save(ms, schoolLogo.Image.RawFormat);
            byte[] photoData = ms.ToArray();
            MemoryStream ms1 = new MemoryStream();
            userPhoto.Image.Save(ms, userPhoto.Image.RawFormat);
            byte[] photoData1 = ms.ToArray();
            try
            {

                string query = "INSERT INTO user (id, UserName, Password, FullName, user_photo,schol_name," +
                    "school_logo,school_code,adress) VALUES (@id, @UserName, @Password, @FullName, @user_photo," +
                    "@schol_name, @school_logo, @school_code, @adress)";
                cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", txtUserId.Text);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtUserPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@FullName", txtUserFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@user_photo", photoData1);
                cmd.Parameters.AddWithValue("@schol_name", txtSchoolName.Text);
                cmd.Parameters.AddWithValue("@school_logo", photoData);
                cmd.Parameters.AddWithValue("@school_code", txtSchoolCode.Text.Trim());
                cmd.Parameters.AddWithValue("@adress", txtAddress.Text.Trim());



                cmd.ExecuteNonQuery();
                MessageBox.Show("User saved successfully!");
                AutouserId();

                // Clear inputs
                txtAdminUsername.Clear();
                txtUserName.Clear();
                txtUserPassword.Clear();
                txtUserFullName.Clear();
                txtSchoolName.Clear();
                txtSchoolCode.Clear();
                txtAddress.Clear();
                userPhoto.Image = null;
                schoolLogo.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

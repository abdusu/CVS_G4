using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Stimulsoft.Controls.Win.DotNetBar.Controls;
using Stimulsoft.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Stimulsoft.Report.Design.StiActions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CVS_G4
{
    public partial class ManageProfile : Form
    {

        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        public ManageProfile()
        {
            InitializeComponent();
            Connect();
            printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument1_PrintPage);
            startManagePan.Visible = true;
            manageStudentPan.Visible = false;
            manageUserPan.Visible = false;
            manageMenuBar.Visible = true; ;
            manageStudentMenu.Visible = false;
            manageUserMenu.Visible = false;
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
        //image to byte array
        public byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        //byte array to image 
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private void LoadData()
        {
            string query = "SELECT UserName, password, FullName, user_photo, schol_name, school_logo, school_code, adress FROM user";

            cmd = new MySqlCommand(query, con);
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.RowTemplate.Height = 100;
                dataGridView1.DataSource = dt;

                // Format image columns for better display
                if (dataGridView1.Columns.Contains("user_photo"))
                {
                    ((DataGridViewImageColumn)dataGridView1.Columns["user_photo"]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                }

                if (dataGridView1.Columns.Contains("school_logo"))
                {
                    ((DataGridViewImageColumn)dataGridView1.Columns["school_logo"]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                }

                // Show number of rows in a label
                txtNoOfUser.Text = "Total NO Of User: " + dt.Rows.Count.ToString();
            }
        }


        private void LoadStudentCertificateByID(string studentID)
        {
            string query = @"
        SELECT 
            students.ID,
            students.FullName,
            students.SchoolCode,
            students.Collage,
            students.Department,
            students.Batch,
            students.Address,
            certificate.Photo,
            certificate.SchoolName
        FROM 
            students
        LEFT JOIN 
            certificate ON students.ID = certificate.ID
        WHERE 
            students.ID = @id";
            cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", studentID);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtStudentName.Text = reader["FullName"].ToString();
                txtStudentSchoolCode.Text = reader["SchoolCode"].ToString();
                txtStudentColage.Text = reader["Collage"].ToString();
                txtStudentDepartment.Text = reader["Department"].ToString();
                txtStudentBatch.Text = reader["Batch"].ToString();
                txtStudentAddress.Text = reader["Address"].ToString();
                // Certificate Info (can be null)
                textBoxSchoolName.Text = reader["SchoolName"] == DBNull.Value ? "Student SCOHOL CODE: "+ reader["SchoolCode"].ToString() : reader["SchoolName"].ToString();


                if (reader["Photo"] != DBNull.Value)
                {
                    byte[] studentImageBytes = (byte[])reader["Photo"];
                    picStudentEditabl.Image = ByteArrayToImage(studentImageBytes);
                }
                else
                {
                    picStudentEditabl.Image = null;
                }

                panStudentInfo.Visible = true;
                reader.Close();
            }
            else
            {
                MessageBox.Show("No record found.");
                reader.Close();
            }
        }
        private void btnLodfromExcel_Click(object sender, EventArgs e)
        {
            manageStudentPan.Visible = true;
            manageUserPan.Visible = false;
            manageMenuBar.Visible = false;
            manageStudentMenu.Visible = true;
            manageUserMenu.Visible = false;
            startManagePan.Visible = false;
            panEditSTudentInfo.Visible = false;
            panEditStudentCertificate.Visible = false;
            panViewuStudentCertificat.Visible = false;
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
            panEditUserProfile.Visible = false;
            panViewUSerProfilee.Visible = false;
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
            panStudentInfo.Visible = false;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            panEditUserProfile.Visible = true;
            panViewUSerProfilee.Visible = false;
            panEditInfo.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            panEditUserProfile.Visible = false;
            panViewUSerProfilee.Visible = true;
            LoadData();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            string UID = txtEditUserId.Text;
            if (string.IsNullOrWhiteSpace(UID))
            {
                MessageBox.Show("please Enter user id firest");
                return;
            }
            try
            {

                cmd = new MySqlCommand("SELECT * FROM user WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", UID);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEditUserName.Text = reader["UserName"].ToString();
                    txtEditPassword.Text = reader["password"].ToString();
                    txtEditFullName.Text = reader["FullName"].ToString();
                    byte[] userImageBytes = (byte[])reader["user_photo"];
                    picUSER.Image = ByteArrayToImage(userImageBytes);
                    txtEditSchoolName.Text = reader["schol_name"].ToString();
                    byte[] schoolLogoBytes = (byte[])reader["school_logo"];
                    picSCHOOL.Image = ByteArrayToImage(schoolLogoBytes);
                    txtEditSchoolCode.Text = reader["school_code"].ToString();
                    txtEditAddress.Text = reader["adress"].ToString();
                    panEditInfo.Visible = true;
                }
                else
                {
                    MessageBox.Show("User not found.");
                    reader.Close();
                    return;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed. Please try again.\n" + ex.Message);
                reader.Close();
            }
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            string Fname = txtEditFullName.Text;
            string Uname = txtEditUserName.Text;
            string UPass = txtEditPassword.Text;
            string UscholName = txtEditSchoolName.Text;
            string UschoolCode = txtEditSchoolCode.Text;
            string UAddress = txtEditAddress.Text;
            string UID = txtEditUserId.Text;

            if (string.IsNullOrWhiteSpace(UID) || string.IsNullOrWhiteSpace(Fname) ||
                string.IsNullOrWhiteSpace(Uname) || string.IsNullOrWhiteSpace(UPass) ||
                string.IsNullOrWhiteSpace(UscholName) || string.IsNullOrWhiteSpace(UschoolCode) || string.IsNullOrWhiteSpace(UAddress))
            {
                MessageBox.Show("please fill all the fielde first");
                return;
            }
            if (picUSER.Image == null || picSCHOOL.Image == null)
            {
                MessageBox.Show("Please select a photo.");
                return;
            }

            try
            {
                byte[] userImageBytes = ImageToByteArray(picUSER.Image);
                byte[] logoImageBytes = ImageToByteArray(picSCHOOL.Image);

                String query = "UPDATE user SET UserName=@Uname, password=@UPass, FullName=@FName, user_photo=@Uphto, " +
                    "schol_name=@sName, school_logo=@slogo, school_code=@scode, adress=@adress WHERE id=@UID";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Uname", Uname);
                cmd.Parameters.AddWithValue("@UPass", UPass);
                cmd.Parameters.AddWithValue("@FName", Fname);
                cmd.Parameters.AddWithValue("@Uphto", userImageBytes);
                cmd.Parameters.AddWithValue("@sName", UscholName);
                cmd.Parameters.AddWithValue("@slogo", logoImageBytes);
                cmd.Parameters.AddWithValue("@scode", UschoolCode);
                cmd.Parameters.AddWithValue("@adress", UAddress);
                cmd.Parameters.AddWithValue("@UID", UID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("USER UPDATED successfully");
                panEditInfo.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed. Please try again.\n" + ex.Message);
            }
        }

        private void picUSER_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an user Image";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picUSER.Image = new Bitmap(ofd.FileName);
                }
            }
        }

        private void picSCHOOL_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an school logo";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picSCHOOL.Image = new Bitmap(ofd.FileName);
                }
            }

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            //printPreviewDialog1.ShowDialog(); // To preview
            printDocument1.Print(); // Use this to print directly
        }
        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int margin = 20;
            int xStart = margin;
            int y = margin;

            int pageWidth = e.MarginBounds.Width;

            // 1. Print Column Headers
            int x = xStart;
            int headerHeight = 40;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                Rectangle headerRect = new Rectangle(x, y, col.Width, headerHeight);
                e.Graphics.FillRectangle(Brushes.LightGray, headerRect);
                e.Graphics.DrawRectangle(Pens.Black, headerRect);

                e.Graphics.DrawString(col.HeaderText, dataGridView1.Font, Brushes.Black, headerRect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });

                x += col.Width;
            }

            y += headerHeight;

            // 2. Print Data Rows
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Skip new row (the empty last one)
                if (row.IsNewRow) continue;

                x = xStart;
                int rowHeight = row.Height;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    Rectangle cellRect = new Rectangle(x, y, cell.Size.Width, rowHeight);
                    e.Graphics.FillRectangle(new SolidBrush(cell.Style.BackColor.IsEmpty ? Color.White : cell.Style.BackColor), cellRect);
                    e.Graphics.DrawRectangle(Pens.Black, cellRect);

                    if (cell.Value is byte[] imageBytes)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            Image img = Image.FromStream(ms);
                            e.Graphics.DrawImage(img, new Rectangle(x + 2, y + 2, cellRect.Width - 4, cellRect.Height - 4));
                        }
                    }
                    else
                    {
                        string text = cell.Value?.ToString() ?? "";
                        System.Drawing.Font font = cell.Style.Font ?? dataGridView1.Font;
                        Color foreColor = cell.Style.ForeColor.IsEmpty ? Color.Black : cell.Style.ForeColor;

                        e.Graphics.DrawString(text, font, new SolidBrush(foreColor), cellRect, new StringFormat
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        });
                    }

                    x += cell.Size.Width;
                }

                y += rowHeight;
            }
        }

        private void manageUserAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editUserProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panEditUserProfile.Visible = true;
            panViewUSerProfilee.Visible = false;
            panEditInfo.Visible = false;
        }

        private void viewuUserAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panEditUserProfile.Visible = false;
            panViewUSerProfilee.Visible = true;
            LoadData();
        }

        private void manageUserAccountToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            manageStudentPan.Visible = false;
            manageUserPan.Visible = true;
            manageMenuBar.Visible = false;
            manageStudentMenu.Visible = false;
            manageUserMenu.Visible = true;
            startManagePan.Visible = false;
            panEditUserProfile.Visible = false;
            panViewUSerProfilee.Visible = false;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {

        }

        private void editStudentProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnEditStudentInfo_Click(object sender, EventArgs e)
        {
            panEditSTudentInfo.Visible = true;
            panViewuStudentCertificat.Visible = false;
            panEditStudentCertificate.Visible = false;
            panStudentInfo.Visible = false;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            panEditSTudentInfo.Visible = false;
            panViewuStudentCertificat.Visible = false;
            panEditStudentCertificate.Visible = true;
            panStudentInfo.Visible = false;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            panEditSTudentInfo.Visible = false;
            panViewuStudentCertificat.Visible = true;
            panEditStudentCertificate.Visible = false;
            panStudentInfo.Visible = false;
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            string SID = txtSerchStudentID.Text;
            if (string.IsNullOrWhiteSpace(SID))
            {
                MessageBox.Show("please Enter user id firest");
                return;
            }
            LoadStudentCertificateByID(SID);
        }

        
    }
}

using MySql.Data.MySqlClient;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.VisualBasic;
using Stimulsoft.Database;
using static System.Data.Odbc.ODBC32;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Export;

namespace CVS_G4
{
    public partial class Registrare : Form
    {
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;

        String UserName;
        String RFullName;
        String SchoolName;
        String SchoolCode;
        String Address;
        double GPA;
        String Date = DateTime.Now.ToString("MMMM dd, yyyy");  // e.g., April 19, 2025

        public Registrare(String userName, String fullName, String schoolName, String schoolCode, String address, Image UImage, Image Slogo)
        {
            InitializeComponent();
            Connect();
            UserName = userName;
            RFullName = fullName;
            SchoolName = schoolName;
            SchoolCode = schoolCode;
            Address = address;
            Userphoto.Image = UImage;
            schoollog.Image = Slogo;
            lblFullName.Text = RFullName;
            schollNameee.Text = SchoolName;
            lblUsername.Text = UserName;

            panStart.Visible = true;
            panGeneratCertificate.Visible = false;
            panVerifayCertificate.Visible = false;
            panViewCertificate.Visible = false;
            panSubmitFeedBack.Visible = false;
            panViewFeedBack.Visible = false;
            panvrifayed.Visible = false;
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
        private void GetGPA()
        {
            while (!double.TryParse(Interaction.InputBox("PLEASE Enter GPA (1.8 to 4):"), out GPA) || GPA < 1.8 || GPA > 4)
            {
                MessageBox.Show("Please enter a valid GPA between 1.8 and 4.");
            }

            //MessageBox.Show("Your GPA is: " + GPA);
        }

        public byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void LoadData()
        {
        
            string query = "SELECT * FROM certificate";
            cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            // Clear existing columns
            dataGridView1.Columns.Clear();
            // Add the rest of the columns
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("FullName", "FullName");
            dataGridView1.Columns.Add("Collage", "Collage");
            dataGridView1.Columns.Add("Department", "Department");
            dataGridView1.Columns.Add("QRCode", "QRCode");
            dataGridView1.Columns.Add("GPA", "GPA");
            dataGridView1.Columns.Add("Batch", "Batch");
            dataGridView1.Columns.Add("Date", "Date");
            dataGridView1.Columns.Add("CertificateCode", "CertificateCode");
            dataGridView1.Columns.Add("SchoolName", "SchoolName");
            // Add image column with correct layout
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.HeaderText = "Photo";
            imgCol.Name = "Photo";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch; // Prevent image stretch
            dataGridView1.Columns.Add(imgCol);
            // Add rows manually
            foreach (DataRow row in dt.Rows)
            {
                byte[] imgBytes = row["Photo"] as byte[];
                Image img = null;
                if (imgBytes != null && imgBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        img = Image.FromStream(ms);
                    }
                }
                dataGridView1.Rows.Add(row["ID"], row["FullName"], row["Collage"], row["Department"], row["QRCode"], row["GPA"], row["Batch"], row["Date"], 
                    row["CertificateCode"], row["SchoolName"], img);
            }
            dataGridView1.RowTemplate.Height = 100; // Adjust row height for image visibility
           
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            panStart.Visible = false;
            panGeneratCertificate.Visible = false;
            panVerifayCertificate.Visible = false;
            panViewCertificate.Visible = false;
            panSubmitFeedBack.Visible = false;
            panViewFeedBack.Visible = true;
            //        User: Submits feedback(question, complaint, suggestion, etc.)

            //Admin: Views feedback, writes a response.

            //User: Later sees admin's response.
            //ChatGPT said:

            //It seems like you're working on a feedback system with the functionality for both users and admins to interact. Do you need any help with specific parts of the implementation, like saving feedback, displaying it to admins, or showing responses to users?
            //You said:
            //please give me all from scratch
            //ChatGPT said:

            //Here’s a step - by - step guide to building a feedback system in a C# WinForms application. The system will allow users to submit feedback (question, complaint, suggestion, etc.), admins to view feedback, write responses, and users to later see the admin's response. We'll use MySQL as the database to store the feedback and responses.
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            panStart.Visible = false;
            panGeneratCertificate.Visible = true;
            panVerifayCertificate.Visible = false;
            panViewCertificate.Visible = false;
            panSubmitFeedBack.Visible = false;
            panViewFeedBack.Visible = false;
            panvrifayed.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            panvrifayed.Visible = false;
            panStart.Visible = false;
            panGeneratCertificate.Visible = false;
            panVerifayCertificate.Visible = true;
            panViewCertificate.Visible = false;
            panSubmitFeedBack.Visible = false;
            panViewFeedBack.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            panStart.Visible = false;
            panGeneratCertificate.Visible = false;
            panVerifayCertificate.Visible = false;
            panViewCertificate.Visible = true;
            panSubmitFeedBack.Visible = false;
            panViewFeedBack.Visible = false;
            LoadData();
            //string query = "SELECT * FROM certificate";
            //MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //dataGridView1.DataSource = table;
            //foreach (DataRow row in table.Rows)
            //{
            //    if (row["Photo"] != DBNull.Value)
            //    {
            //        byte[] imgBytes = (byte[])row["Photo"];
            //        using (MemoryStream ms = new MemoryStream(imgBytes))
            //        {
            //           row["Photo"] = Image.FromStream(ms);
            //        }
            //    }
            //}
            //dataGridView1.DataSource = table;
            //DataGridViewImageColumn imgColumn = (DataGridViewImageColumn)dataGridView1.Columns["photo"];
            //imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch; // Stretch image to fit
            //dataGridView1.RowTemplate.Height = 100; // Set row height
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            panStart.Visible = false;
            panGeneratCertificate.Visible = false;
            panVerifayCertificate.Visible = false;
            panViewCertificate.Visible = false;
            panSubmitFeedBack.Visible = true;
            panViewFeedBack.Visible = false;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            String studentid = txtId.Text;
            if (string.IsNullOrEmpty(studentid) || studentImage.Image == null)
            {
                MessageBox.Show("Please upload an image  And ID before.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            StiReport report = new StiReport();
            report.Load("certificte.mrt"); // Make sure the file is in your output directory (bin\Debug)

            // Set the value of variables from the form
            try
            {
                cmd = new MySqlCommand("SELECT * FROM certificate WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", studentid);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string SName = reader["FullName"].ToString();
                    string ACollage = reader["Collage"].ToString();
                    string ADepartment = reader["Department"].ToString();
                    string AQRCode = reader["QRCode"].ToString();
                    string AGPA = reader["GPA"].ToString();
                    string ABatch = reader["Batch"].ToString();
                    byte[] AimageBytes = (byte[])reader["Photo"];
                    using (MemoryStream ms = new MemoryStream(AimageBytes))
                    {
                        studentImage.Image = Image.FromStream(ms);
                    }
                    string ADate = reader["Date"].ToString();
                    DialogResult result = MessageBox.Show("Allready have certificate to print clik yes", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        report.Dictionary.Variables["FullName"].Value = SName;
                        report.Dictionary.Variables["SchoolName"].Value = SchoolName;
                        report.Dictionary.Variables["Collage"].Value = ACollage;
                        report.Dictionary.Variables["Department"].Value = ADepartment;
                        report.Dictionary.Variables["RegistrarName"].Value = RFullName;
                        report.Dictionary.Variables["QRCODE"].Value = AQRCode;
                        report.Dictionary.Variables["GPA"].Value = AGPA;
                        report.Dictionary.Variables["Datee"].Value = ADate;
                        report.Dictionary.Variables["Batch"].Value = ABatch;
                        report.Dictionary.Variables["photo"].ValueObject = new Bitmap(studentImage.Image);
                        report.Dictionary.Variables["Logo"].ValueObject = new Bitmap(schoollog.Image);
                        report.Show();
                        reader.Close();
                    }
                    else
                    {
                        // 👉 Return or skip if No is clicked
                        MessageBox.Show("You chose NO. Action cancelled.");
                        reader.Close();
                        return; // Stops execution of the current method
                    }
                    //report.Show();
                    //reader.Close();

                }
                else
                {
                    reader.Close();
                    try
                    {
                        cmd = new MySqlCommand("SELECT * FROM students WHERE ID = @id AND SchoolCode = @schollcode", con);
                        cmd.Parameters.AddWithValue("@id", studentid);
                        cmd.Parameters.AddWithValue("@schollcode", SchoolCode);
                        reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string SFullName = reader["FullName"].ToString();
                            string Collage = reader["Collage"].ToString();
                            string Department = reader["Department"].ToString();
                            string Batch = reader["Batch"].ToString();
                            GetGPA();
                            string sudentname = SFullName; // Or get from your TextBox
                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string certificateCode = $"CERT-{sudentname.Substring(0, 3).ToUpper()}-{timestamp}";
                            MessageBox.Show("certificatw code: " + certificateCode);
                            string qrCode = " Student Nmae: " + SFullName + "\n Colage: " + Collage + "\n Batchler Degre With  Department: " + Department +
                            "\n Gratued From: " + SchoolName + " " + Batch + " Batch " + "\n Address: " + Address + "\n comulative GPA: " + GPA.ToString() +
                            "\n certificate Code: " + certificateCode + "\n\n Date: " + Date;

                            report.Dictionary.Variables["FullName"].Value = SFullName;
                            report.Dictionary.Variables["SchoolName"].Value = SchoolName;
                            report.Dictionary.Variables["Collage"].Value = Collage;
                            report.Dictionary.Variables["Department"].Value = Department;
                            report.Dictionary.Variables["RegistrarName"].Value = RFullName;
                            report.Dictionary.Variables["QRCODE"].Value = qrCode;
                            report.Dictionary.Variables["GPA"].Value = GPA.ToString();
                            report.Dictionary.Variables["Datee"].Value = Date;
                            report.Dictionary.Variables["Batch"].Value = Batch;
                            report.Dictionary.Variables["photo"].ValueObject = new Bitmap(studentImage.Image);
                            report.Dictionary.Variables["Logo"].ValueObject = new Bitmap(schoollog.Image);
                            reader.Close();

                            byte[] ususerImageBytes = ImageToByteArray(studentImage.Image);
                            string Cquery = "INSERT INTO certificate (ID, FullName, Collage, Department, QRCode, GPA, Batch, Photo, Date, CertificateCode,SchoolName) " +
                                "VALUES (@ID, @FullName, @Collage, @Department, @QRCode, @GPA, @Batch, @Photo, @Date, @CertificateCode, @SchoolName)";
                            cmd = new MySqlCommand(Cquery, con);
                            cmd.Parameters.AddWithValue("@ID", studentid);
                            cmd.Parameters.AddWithValue("@FullName", SFullName);
                            cmd.Parameters.AddWithValue("@Collage", Collage);
                            cmd.Parameters.AddWithValue("@Department", Department);
                            cmd.Parameters.AddWithValue("@QRCode", qrCode);
                            cmd.Parameters.AddWithValue("@GPA", GPA.ToString());
                            cmd.Parameters.AddWithValue("@Batch", Batch);
                            cmd.Parameters.AddWithValue("@Photo", ususerImageBytes);
                            cmd.Parameters.AddWithValue("@Date", Date);
                            cmd.Parameters.AddWithValue("@CertificateCode", certificateCode);
                            cmd.Parameters.AddWithValue("@SchoolName", SchoolName);

                            cmd.ExecuteNonQuery();
                            report.Show();
                        }
                        else
                        {
                            MessageBox.Show("the student information is not found! Please try again.");
                            reader.Close();
                            return;
                        }
                        //report.Show();
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("failed! Please try again. Error: " + ex.Message);
                        reader.Close();
                        return;
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please try again." + ex.Message);
                reader.Close();
                return;
            }
            //report.Dictionary.Variables["Name"].Value = txtName.Text;

            //if (schoollog.Image != null)
            //{
            //    // Pass image to report using ValueObject
            //    report.Dictionary.Variables["Photo"].ValueObject = new Bitmap(schoollog.Image);
            //}
            //Bitmap image = new Bitmap(schoollog.Image);
            //var watermark = report.Pages[0].Watermark;
            //watermark.Enabled = true;
            //watermark.Image = image;
            //watermark.ImageTransparency = 80; // Optional: 0 = solid, 100 = invisible
            //watermark.ImageAlignment = ContentAlignment.MiddleCenter; // Adjust as needed
            // Show the report

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an Student Image";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    studentImage.Image = new Bitmap(ofd.FileName);
                }
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            String certificatecodeResult = txtCertificateCodeResult.Text;
            try
            {
                cmd = new MySqlCommand("SELECT * FROM certificate WHERE CertificateCode = @CertificateCode", con);
                cmd.Parameters.AddWithValue("@CertificateCode", certificatecodeResult);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    panvrifayed.Visible = true;
                    lblHolderName.Text = reader["FullName"].ToString();
                    lblHolderDepartment.Text = reader["Department"].ToString();
                    lblHolderGpa.Text = reader["GPA"].ToString();
                    string CBatch = reader["Batch"].ToString();
                    lblCSchoolName.Text = reader["SchoolName"].ToString();
                    byte[] CimageBytes = (byte[])reader["Photo"];
                    using (MemoryStream ms = new MemoryStream(CimageBytes))
                    {
                        CHolderPhoto.Image = Image.FromStream(ms);
                    }
                    lblCertificateHDate.Text = reader["Date"].ToString();
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("The certificate code is not found! Please try again.");
                    reader.Close();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("failed! please try again. Error: " + ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            logIn newForm = new logIn();
            newForm.Show();
        }
    }
}

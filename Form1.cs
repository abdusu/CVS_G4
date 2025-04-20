using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace CVS_G4
{
    public partial class logIn : Form
    {
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;


        public logIn()
        {
            InitializeComponent();
            Connect();
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

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = txtName.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string utype = comUser.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(utype))
            {
                MessageBox.Show("Please enter all the required fields.");
                txtName.Clear();
                txtPassword.Clear();
                comUser.SelectedIndex = -1;
                txtName.Focus();
                return;

            }
            if (utype == "Admin")
            {
                try
                {
                    cmd = new MySqlCommand("SELECT * FROM admin WHERE UserName = @uname AND Password = @password", con);
                    cmd.Parameters.AddWithValue("@uname", uname);
                    cmd.Parameters.AddWithValue("@password", pass);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string userId = reader["id"].ToString();
                        byte[] imageBytes = (byte[])reader["AdminPhoto"];
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                             AdminImage.Image = Image.FromStream(ms);
                        }
                        
                        
                        this.Hide();
                        Admin newForm = new Admin(userId, uname, AdminImage.Image);
                        newForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username and password do not match! Please try again.");
                        txtName.Clear();
                        txtPassword.Clear();
                        comUser.SelectedIndex = -1;
                        txtName.Focus();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login failed! Please try again. Error: " + ex.Message);
                }

            }


            else if (utype == "Registrars")
            {
                try
                {
                    cmd = new MySqlCommand("SELECT * FROM user WHERE UserName = @uname AND password = @password", con);
                    cmd.Parameters.AddWithValue("@uname", uname);
                    cmd.Parameters.AddWithValue("@password", pass);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string userId = reader["id"].ToString();
                        string FullName = reader["FullName"].ToString();
                       
                        string SchoolName = reader["schol_name"].ToString();
                        byte[] logoimageBytes = (byte[])reader["school_logo"];
                        using (MemoryStream ms = new MemoryStream(logoimageBytes))
                        {
                            logoImage.Image = Image.FromStream(ms);
                        }
                        byte[] UserimageBytes = (byte[])reader["user_photo"];
                        using (MemoryStream ms1 = new MemoryStream(UserimageBytes))
                        {
                            userImage.Image = Image.FromStream(ms1);
                        }
                        string SchoolCode = reader["school_code"].ToString();
                        string Adrees = reader["adress"].ToString();

                        this.Hide();
                        Registrare newForm = new Registrare(uname, FullName, SchoolName, SchoolCode, Adrees, userImage.Image, logoImage.Image);
                        newForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username and password do not match! Please try again.");
                        txtName.Clear();
                        txtPassword.Clear();
                        comUser.SelectedIndex = -1;
                        txtName.Focus();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login failed! Please try again. Error: " + ex.Message);
                }

            }








        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}


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
            //Connect();
        }

        private void Connect()
        {
            try
            {
                string connectionString = "Server=localhost;Database=cvs_g4;User Id=root;Password=abdu;";
                con = new MySqlConnection(connectionString);
                con.Open();
                MessageBox.Show("Database connected successfully!");
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
                return;
            }

            try
            {
                cmd = new MySqlCommand("SELECT * FROM user WHERE uname = @uname AND password = @password AND utype = @utype", con);
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@password", pass);
                cmd.Parameters.AddWithValue("@utype", utype);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string userId = reader["id"].ToString();
                    this.Hide();
                    //Form2 newForm = new Form2(userId, uname, utype);
                    //newForm.Show();
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
}


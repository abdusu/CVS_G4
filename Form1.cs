namespace CVS_G4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            Form2 newForm = new Form2();  // Create an instance of the new form
            newForm.Show();  // Show the new form
            this.Hide();
        }
    }
}

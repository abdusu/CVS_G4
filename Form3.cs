using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace CVS_G4
{
    public partial class addStudent : Form
    {
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        public addStudent()
        {
            InitializeComponent();
            Connect();
            AutoId();
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

        private void GetGPA()
        {
            double GPA;

            while (!double.TryParse(Interaction.InputBox("PLEASE Enter GPA (1.8 to 4):"), out GPA) || GPA < 1.8 || GPA > 4)
            {
                MessageBox.Show("Please enter a valid GPA between 1.8 and 4.");
            }

            MessageBox.Show("Your GPA is: " + GPA);
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
        public string AutoId()
        {
            try
            {

                string query = "SELECT MAX(ID) FROM students";
                using (cmd = new MySqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        return "STUD001";
                    }
                    else
                    {
                        string maxAdminId = result.ToString(); // e.g., "CH015"
                        long idNumber = long.Parse(maxAdminId.Substring(4)); // Extract numeric part
                        idNumber++;
                        return "STUD" + idNumber.ToString("D3"); // Format to 3 digits with leading zeros
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed! Please try again.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        private void btnAddOneStudent_Click(object sender, EventArgs e)
        {
            startpan.Visible = false;
            addAllStudentpan.Visible = false;
            AddOneStudentPan.Visible = true;
            dataGridView1.DataSource = null;
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

        private void guna2GradientButton4_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            openFileDialog1.Title = "Select an Excel File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                txtFilePath.Text = filePath;
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        // Read original table from Excel
                        DataTable originalTable = result.Tables[0];

                        // Create new table with ID column
                        DataTable newTable = new DataTable();

                        // Add auto-generated ID column
                        newTable.Columns.Add("ID", typeof(string));

                        // Copy Excel columns


                        for (int col = 0; col < originalTable.Columns.Count; col++)
                        {
                            string columnName = originalTable.Columns[col].ColumnName;
                            newTable.Columns.Add(columnName, originalTable.Columns[col].DataType);
                        }
                        string autoId = ""; // define once outside the loop

                        for (int i = 0; i < originalTable.Rows.Count; i++)
                        {
                            DataRow row = originalTable.Rows[i];

                            if (i == 0)
                            {
                                autoId = AutoId(); // First row: generate initial ID, And Cheack the datat base if the data exist or not if the data is exis get max id not exist initial id STUD001 
                            }
                            else
                            {
                                long idNumber = long.Parse(autoId.Substring(4)); // Get numeric part
                                idNumber++; // Increment by 1
                                autoId = "STUD" + idNumber.ToString("D3"); // Rebuild ID
                            }

                            DataRow newRow = newTable.NewRow();
                            newRow["ID"] = autoId;

                            for (int j = 0; j < originalTable.Columns.Count; j++)
                            {
                                string colName = originalTable.Columns[j].ColumnName;
                                newRow[colName] = row[colName];
                            }

                            newTable.Rows.Add(newRow);
                        }

                        // Bind to DataGridView
                        dataGridView1.DataSource = newTable;
                    }
                }
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // Step 1: Check if DataGridView is empty
            if (dataGridView1.Rows.Count == 0 || dataGridView1.Rows[0].IsNewRow)
            {
                MessageBox.Show("⚠️ No data to export. Please import data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Step 2: Convert DataGridView to DataTable
            DataTable table = (DataTable)dataGridView1.DataSource;


            int errorinserting = 0;
            List<string> ErrordataList = new List<string>();
            List<string> dataList = new List<string>();
            string allData;
            string allDatas;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                //string query = "INSERT INTO students (ID, name, age, city) VALUES (@ID, @name, @age, @city)";
                string query = "INSERT INTO students (ID, FullName, SchoolCode, Collage, Department, Batch, Address)" +
                    " VALUES (@ID, @FullName, @SchoolCode, @Collage, @Department, @Batch, @Address)";
                using (cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", row["ID"]);
                    cmd.Parameters.AddWithValue("@FullName", row["FullName"]);
                    cmd.Parameters.AddWithValue("@SchoolCode", row["SchoolCode"]);
                    cmd.Parameters.AddWithValue("@Collage", row["Collage"]);
                    cmd.Parameters.AddWithValue("@Department", row["Department"]);
                    cmd.Parameters.AddWithValue("@Batch", row["Batch"]);
                    cmd.Parameters.AddWithValue("@Address", row["Address"]);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        errorinserting++;
                        ErrordataList.Add(row["FullName"].ToString());
                        //MessageBox.Show( ex.Message);
                    }
                }

            }
            allData = string.Join("\n", ErrordataList);
            allDatas = string.Join("\n", dataList);
            if (errorinserting > 0)
            {
                MessageBox.Show("   Error data not exported Data to MySQL ↓\n" + errorinserting + " \n" + allData +
                    "\n\n  ✅ successfully  exported Data to MySQL ↓\n" + allDatas, "❌ Error inserting row Name ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("✅ ALL Data exported to MySQL successfully.");
            }

        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }
    }
}

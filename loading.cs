//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace CVS_G4
//{
//   // using Guna.UI2.WinForms;
//    //public partial class loading : Form
//    //{
//    //    public loading()
//    //    {
//    //        InitializeComponent();
//    //    }

//    //    private void loading_Load(object sender, EventArgs e)
//    //    {

//    //    }
//    //}
//    using System;
//    using System.Windows.Forms;

//    public partial class loading : Form
//    {
//        private Timer progressUpdateTimer;
//        private Timer transitionTimer;
//        private int elapsedTime = 0; // To track elapsed time for progress
//        private string[] processMessages = { "Starting process...", "Processing...", "Almost done...", "Finishing..." };

//        public loading()
//        {
//            InitializeComponent();

//            // Initialize the Timer for updating progress every 15 seconds
//            progressUpdateTimer = new Timer();
//            progressUpdateTimer.Interval = 15000; // 15 seconds
//            progressUpdateTimer.Tick += ProgressUpdateTimer_Tick;

//            // Initialize the Timer for transitioning after 50 seconds
//            transitionTimer = new Timer();
//            transitionTimer.Interval = 50000; // 50 seconds
//            transitionTimer.Tick += TransitionTimer_Tick;

//            // Start both timers
//            progressUpdateTimer.Start();
//            transitionTimer.Start();

//            // Start the progress indicators
//            guna2WinProgressIndicator1.Start();
//            guna2TaskBarProgress1.Value = 0;  // Set initial value to 0
//        }

//        private void ProgressUpdateTimer_Tick(object sender, EventArgs e)
//        {
//            // Increment elapsed time by 15 seconds
//            elapsedTime += 15;

//            // Calculate the percentage of the task (max 50 seconds)
//            int percentage = (elapsedTime * 100) / 50;

//            // Update the TaskBar progress (set the Value property directly)
//            guna2TaskBarProgress1.Value = percentage;

//            // Show progress message
//            int messageIndex = (elapsedTime / 15) - 1;  // Show message every 15 seconds
//            if (messageIndex >= 0 && messageIndex < processMessages.Length)
//            {
//                lblStatus.Text = processMessages[messageIndex];  // Assuming lblStatus is a Label control
//            }

//            // Optionally, update a label for percentage display
//            lblPercentage.Text = $"{percentage}%";  // Assuming lblPercentage is a Label control

//            // Optionally, you can log the progress in the output or console for debugging:
//            Console.WriteLine($"Progress: {percentage}% - {processMessages[messageIndex]}");
//        }

//        private void TransitionTimer_Tick(object sender, EventArgs e)
//        {
//            // Stop the progress indicator (you don't need to call Stop())
//            guna2WinProgressIndicator1.Stop();

//            // Open Form2
//            logIn form2 = new logIn();
//            form2.Show();

//            // Optionally, close Form1
//            this.Hide();  // or this.Close();
//        }
//    }




//}
using System;
using System.Threading;
using System.Windows.Forms;

namespace CVS_G4
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        // This event fires when the form is loaded
        private void loading_Load(object sender, EventArgs e)
        {

            // Initialize progress bar and make it visible
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            // Initialize label to show percentage
            lblInfo.Text = "0%";
            lblInfo.Text = "";  // Initially, leave the info label empty

            // Run the progress update on a background thread
            Thread progressThread = new Thread(UpdateProgressBar);
            progressThread.Start();
        }

        // This method will update the progress bar, the percentage label, and the info label
        private void UpdateProgressBar()
        {
            for (int i = 0; i <= 100; i++)
            {
                // Update the ProgressBar value
                this.Invoke(new Action(() =>
                {
                    progressBar1.Value = i;
                    lblInfo.Text = $"{i}%";  // Update the label to show percentage
                }));

                // Show the status messages at specific progress points
                if (i == 3)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "READING MODULES.."));
                }
                if (i == 20)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "TURN ON MODULES.."));
                }
                if (i == 40)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "RUNNING MODULES.."));
                }
                if (i == 60)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "STARTING MODULE.."));
                }
                if (i == 80)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "LAUNCHING APPLICATION.."));
                }

                // Simulate some work with a delay
                Thread.Sleep(50);
            }

            // Once done, show a completion message
            this.Invoke(new Action(() =>
            {
                //MessageBox.Show("Task Complete!");
                this.Hide();
                logIn newForm = new logIn();
                newForm.Show();
            }));
        }

        
    }
}


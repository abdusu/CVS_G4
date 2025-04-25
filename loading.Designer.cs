namespace CVS_G4
{
    partial class loading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2WinProgressIndicator1 = new Guna.UI2.WinForms.Guna2WinProgressIndicator();
            guna2TaskBarProgress1 = new Guna.UI2.WinForms.Guna2TaskBarProgress(components);
            lblStatus = new Label();
            lblInfo = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            progressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.TargetControl = this;
            // 
            // guna2WinProgressIndicator1
            // 
            guna2WinProgressIndicator1.AutoStart = true;
            guna2WinProgressIndicator1.BackColor = Color.Transparent;
            guna2WinProgressIndicator1.Location = new Point(109, 93);
            guna2WinProgressIndicator1.Name = "guna2WinProgressIndicator1";
            guna2WinProgressIndicator1.NumberOfCircles = 10;
            guna2WinProgressIndicator1.ShadowDecoration.CustomizableEdges = customizableEdges3;
            guna2WinProgressIndicator1.Size = new Size(171, 163);
            guna2WinProgressIndicator1.TabIndex = 3;
            // 
            // guna2TaskBarProgress1
            // 
            guna2TaskBarProgress1.TargetForm = this;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe Print", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(94, 148, 255);
            lblStatus.Location = new Point(12, 276);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(51, 43);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "inf";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblInfo.ForeColor = Color.FromArgb(94, 148, 255);
            lblInfo.Location = new Point(162, 147);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(40, 37);
            lblInfo.TabIndex = 5;
            lblInfo.Text = "%";
            // 
            // progressBar1
            // 
            progressBar1.CustomizableEdges = customizableEdges1;
            progressBar1.Location = new Point(-1, 309);
            progressBar1.Name = "progressBar1";
            progressBar1.ProgressColor = Color.FromArgb(94, 148, 255);
            progressBar1.ProgressColor2 = Color.Yellow;
            progressBar1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressBar1.Size = new Size(454, 10);
            progressBar1.TabIndex = 6;
            progressBar1.Text = "guna2ProgressBar1";
            progressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // loading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 47, 66);
            ClientSize = new Size(454, 319);
            Controls.Add(progressBar1);
            Controls.Add(lblInfo);
            Controls.Add(guna2WinProgressIndicator1);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.None;
            Name = "loading";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "loading";
            Load += loading_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2WinProgressIndicator guna2WinProgressIndicator1;
        private Guna.UI2.WinForms.Guna2TaskBarProgress guna2TaskBarProgress1;
        private Label lblInfo;
        private Label lblStatus;
        private System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar1;
    }
}
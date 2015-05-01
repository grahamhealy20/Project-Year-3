namespace TestClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SignOutButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.URLAnchorLabel = new System.Windows.Forms.LinkLabel();
            this.ProjectNameLabel = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TempHeaderLabel = new System.Windows.Forms.Label();
            this.TempLabel = new System.Windows.Forms.Label();
            this.TextBoxPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LoggingBox = new System.Windows.Forms.RichTextBox();
            this.RealTimeLoggingLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.TextBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // SignOutButton
            // 
            this.SignOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SignOutButton.Location = new System.Drawing.Point(499, 10);
            this.SignOutButton.Name = "SignOutButton";
            this.SignOutButton.Size = new System.Drawing.Size(75, 23);
            this.SignOutButton.TabIndex = 5;
            this.SignOutButton.Text = "Sign Out";
            this.SignOutButton.UseVisualStyleBackColor = true;
            this.SignOutButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.URLAnchorLabel);
            this.panel1.Controls.Add(this.ProjectNameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 63);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 60);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // URLAnchorLabel
            // 
            this.URLAnchorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.URLAnchorLabel.AutoSize = true;
            this.URLAnchorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.URLAnchorLabel.Location = new System.Drawing.Point(441, 18);
            this.URLAnchorLabel.Name = "URLAnchorLabel";
            this.URLAnchorLabel.Size = new System.Drawing.Size(140, 18);
            this.URLAnchorLabel.TabIndex = 1;
            this.URLAnchorLabel.TabStop = true;
            this.URLAnchorLabel.Text = "BabyGuard Website";
            // 
            // ProjectNameLabel
            // 
            this.ProjectNameLabel.AutoSize = true;
            this.ProjectNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ProjectNameLabel.Location = new System.Drawing.Point(3, 0);
            this.ProjectNameLabel.Name = "ProjectNameLabel";
            this.ProjectNameLabel.Size = new System.Drawing.Size(111, 25);
            this.ProjectNameLabel.TabIndex = 0;
            this.ProjectNameLabel.Text = "BabyGuard";
            this.ProjectNameLabel.Visible = false;
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Location = new System.Drawing.Point(100, 15);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(14, 13);
            this.UserName.TabIndex = 7;
            this.UserName.Text = "T";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.SignOutButton);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.UserName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 44);
            this.panel2.TabIndex = 8;
            // 
            // TempHeaderLabel
            // 
            this.TempHeaderLabel.AutoSize = true;
            this.TempHeaderLabel.Location = new System.Drawing.Point(6, 6);
            this.TempHeaderLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.TempHeaderLabel.Name = "TempHeaderLabel";
            this.TempHeaderLabel.Size = new System.Drawing.Size(70, 13);
            this.TempHeaderLabel.TabIndex = 0;
            this.TempHeaderLabel.Text = "Temperature:";
            // 
            // TempLabel
            // 
            this.TempLabel.AutoSize = true;
            this.TempLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.TempLabel.Location = new System.Drawing.Point(6, 19);
            this.TempLabel.MaximumSize = new System.Drawing.Size(100, 25);
            this.TempLabel.MinimumSize = new System.Drawing.Size(100, 0);
            this.TempLabel.Name = "TempLabel";
            this.TempLabel.Size = new System.Drawing.Size(100, 25);
            this.TempLabel.TabIndex = 8;
            this.TempLabel.Text = "T";
            // 
            // TextBoxPanel
            // 
            this.TextBoxPanel.Controls.Add(this.splitContainer1);
            this.TextBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxPanel.Location = new System.Drawing.Point(0, 63);
            this.TextBoxPanel.Name = "TextBoxPanel";
            this.TextBoxPanel.Size = new System.Drawing.Size(584, 354);
            this.TextBoxPanel.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TempHeaderLabel);
            this.splitContainer1.Panel1.Controls.Add(this.TempLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.LoggingBox);
            this.splitContainer1.Panel2.Controls.Add(this.RealTimeLoggingLabel);
            this.splitContainer1.Size = new System.Drawing.Size(584, 354);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 11;
            // 
            // LoggingBox
            // 
            this.LoggingBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoggingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoggingBox.Location = new System.Drawing.Point(0, 16);
            this.LoggingBox.Name = "LoggingBox";
            this.LoggingBox.Size = new System.Drawing.Size(387, 338);
            this.LoggingBox.TabIndex = 2;
            this.LoggingBox.Text = "";
            // 
            // RealTimeLoggingLabel
            // 
            this.RealTimeLoggingLabel.AutoSize = true;
            this.RealTimeLoggingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RealTimeLoggingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealTimeLoggingLabel.Location = new System.Drawing.Point(0, 0);
            this.RealTimeLoggingLabel.Name = "RealTimeLoggingLabel";
            this.RealTimeLoggingLabel.Size = new System.Drawing.Size(88, 16);
            this.RealTimeLoggingLabel.TabIndex = 1;
            this.RealTimeLoggingLabel.Text = "Real-time log";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.TextBoxPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "MainForm";
            this.Text = "Baby Guard Baby Monitor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.TextBoxPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button SignOutButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ProjectNameLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel URLAnchorLabel;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label TempHeaderLabel;
        private System.Windows.Forms.Label TempLabel;
        private System.Windows.Forms.Panel TextBoxPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label RealTimeLoggingLabel;
        private System.Windows.Forms.RichTextBox LoggingBox;
    }
}


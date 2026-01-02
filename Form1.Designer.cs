namespace VirusAntivirus
{
    partial class Form1
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
            this.btnSelectDirectory = new System.Windows.Forms.Button();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.btnStopScan = new System.Windows.Forms.Button();
            this.btnCreateTestVirus = new System.Windows.Forms.Button();
            this.lstScanLog = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblSelectedDirectory = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectDirectory
            // 
            this.btnSelectDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnSelectDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectDirectory.ForeColor = System.Drawing.Color.White;
            this.btnSelectDirectory.Location = new System.Drawing.Point(12, 12);
            this.btnSelectDirectory.Name = "btnSelectDirectory";
            this.btnSelectDirectory.Size = new System.Drawing.Size(150, 35);
            this.btnSelectDirectory.TabIndex = 0;
            this.btnSelectDirectory.Text = "Select Directory";
            this.btnSelectDirectory.UseVisualStyleBackColor = false;
            this.btnSelectDirectory.Click += new System.EventHandler(this.btnSelectDirectory_Click);
            // 
            // btnStartScan
            // 
            this.btnStartScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnStartScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartScan.ForeColor = System.Drawing.Color.White;
            this.btnStartScan.Location = new System.Drawing.Point(168, 12);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(120, 35);
            this.btnStartScan.TabIndex = 1;
            this.btnStartScan.Text = "Start Scan";
            this.btnStartScan.UseVisualStyleBackColor = false;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // btnStopScan
            // 
            this.btnStopScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(43)))), ((int)(((byte)(28)))));
            this.btnStopScan.Enabled = false;
            this.btnStopScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopScan.ForeColor = System.Drawing.Color.White;
            this.btnStopScan.Location = new System.Drawing.Point(294, 12);
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(120, 35);
            this.btnStopScan.TabIndex = 2;
            this.btnStopScan.Text = "Stop Scan";
            this.btnStopScan.UseVisualStyleBackColor = false;
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnCreateTestVirus
            // 
            this.btnCreateTestVirus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnCreateTestVirus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateTestVirus.ForeColor = System.Drawing.Color.White;
            this.btnCreateTestVirus.Location = new System.Drawing.Point(420, 12);
            this.btnCreateTestVirus.Name = "btnCreateTestVirus";
            this.btnCreateTestVirus.Size = new System.Drawing.Size(150, 35);
            this.btnCreateTestVirus.TabIndex = 3;
            this.btnCreateTestVirus.Text = "Create Test Virus";
            this.btnCreateTestVirus.UseVisualStyleBackColor = false;
            this.btnCreateTestVirus.Click += new System.EventHandler(this.btnCreateTestVirus_Click);
            // 
            // lstScanLog
            // 
            this.lstScanLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lstScanLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstScanLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lstScanLog.FormattingEnabled = true;
            this.lstScanLog.HorizontalScrollbar = true;
            this.lstScanLog.ItemHeight = 16;
            this.lstScanLog.Location = new System.Drawing.Point(12, 90);
            this.lstScanLog.Name = "lstScanLog";
            this.lstScanLog.Size = new System.Drawing.Size(776, 338);
            this.lstScanLog.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 53);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 5;
            // 
            // lblSelectedDirectory
            // 
            this.lblSelectedDirectory.AutoSize = true;
            this.lblSelectedDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblSelectedDirectory.Location = new System.Drawing.Point(12, 440);
            this.lblSelectedDirectory.Name = "lblSelectedDirectory";
            this.lblSelectedDirectory.Size = new System.Drawing.Size(120, 16);
            this.lblSelectedDirectory.TabIndex = 6;
            this.lblSelectedDirectory.Text = "No directory selected";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip.Location = new System.Drawing.Point(0, 468);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusLabel1.Text = "Total Files Scanned: 0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabel2.Text = "Threats Found: 0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(800, 490);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblSelectedDirectory);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lstScanLog);
            this.Controls.Add(this.btnCreateTestVirus);
            this.Controls.Add(this.btnStopScan);
            this.Controls.Add(this.btnStartScan);
            this.Controls.Add(this.btnSelectDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Virus Antivirus Scanner";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectDirectory;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.Button btnStopScan;
        private System.Windows.Forms.Button btnCreateTestVirus;
        private System.Windows.Forms.ListBox lstScanLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblSelectedDirectory;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}


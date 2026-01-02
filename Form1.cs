using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirusAntivirus
{
    public partial class Form1 : Form
    {
        // Virus Database - Contains MD5 hashes of known "infected" files
        private readonly Dictionary<string, string> VirusDatabase = new Dictionary<string, string>
        {
            // These are dummy MD5 hashes for demonstration
            // In a real antivirus, these would be actual virus signatures
            { "5d41402abc4b2a76b9719d911017c592", "Test Virus 1" }, // MD5 of "hello"
            { "098f6bcd4621d373cade4e832627b4f6", "Test Virus 2" }, // MD5 of "test"
            { "827ccb0eea8a706c4c34a16891f84e7b", "Test Virus 3" }  // MD5 of "12345"
        };

        private string selectedDirectory = string.Empty;
        private BackgroundWorker scanWorker;
        private CancellationTokenSource cancellationTokenSource;
        private int totalFilesScanned = 0;
        private int threatsFound = 0;
        private List<string> infectedFiles = new List<string>();
        private readonly string quarantineFolder = Path.Combine(Application.StartupPath, "Quarantine");

        public Form1()
        {
            InitializeComponent();
            InitializeScanWorker();
            CreateQuarantineFolder();
        }

        /// <summary>
        /// Initializes the BackgroundWorker for file scanning
        /// </summary>
        private void InitializeScanWorker()
        {
            scanWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            scanWorker.DoWork += ScanWorker_DoWork;
            scanWorker.ProgressChanged += ScanWorker_ProgressChanged;
            scanWorker.RunWorkerCompleted += ScanWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Creates the Quarantine folder if it doesn't exist
        /// </summary>
        private void CreateQuarantineFolder()
        {
            try
            {
                if (!Directory.Exists(quarantineFolder))
                {
                    Directory.CreateDirectory(quarantineFolder);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error creating quarantine folder: {ex.Message}");
            }
        }

        /// <summary>
        /// Button click handler for selecting a directory to scan
        /// </summary>
        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a directory to scan for viruses";
                folderDialog.ShowNewFolderButton = false;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedDirectory = folderDialog.SelectedPath;
                    lblSelectedDirectory.Text = $"Selected: {selectedDirectory}";
                    LogMessage($"Directory selected: {selectedDirectory}");
                }
            }
        }

        /// <summary>
        /// Button click handler for starting the scan
        /// </summary>
        private void btnStartScan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedDirectory))
            {
                MessageBox.Show("Please select a directory first.", "No Directory Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(selectedDirectory))
            {
                MessageBox.Show("The selected directory does not exist.", "Invalid Directory", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Reset counters and UI
            totalFilesScanned = 0;
            threatsFound = 0;
            infectedFiles.Clear();
            lstScanLog.Items.Clear();
            progressBar.Value = 0;
            UpdateStatusStrip();

            // Enable/Disable buttons
            btnStartScan.Enabled = false;
            btnStopScan.Enabled = true;
            btnSelectDirectory.Enabled = false;

            // Start the scan
            cancellationTokenSource = new CancellationTokenSource();
            LogMessage("=== Scan Started ===");
            scanWorker.RunWorkerAsync(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Button click handler for stopping the scan
        /// </summary>
        private void btnStopScan_Click(object sender, EventArgs e)
        {
            if (scanWorker.IsBusy)
            {
                cancellationTokenSource?.Cancel();
                LogMessage("=== Scan Cancelled by User ===");
            }
        }

        /// <summary>
        /// Button click handler for creating a test virus file
        /// </summary>
        private void btnCreateTestVirus_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a test file with specific content that will generate a known MD5 hash
                // This content is designed to match one of our test virus hashes
                string testVirusPath = Path.Combine(Application.StartupPath, "fake_virus.txt");
                
                // Content that will produce a predictable hash for testing
                string testContent = "hello"; // This will generate MD5: 5d41402abc4b2a76b9719d911017c592
                
                File.WriteAllText(testVirusPath, testContent);
                
                MessageBox.Show($"Test virus file created at:\n{testVirusPath}\n\n" +
                    $"This file contains content that matches a virus signature in the database.",
                    "Test Virus Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LogMessage($"Test virus file created: {testVirusPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating test virus file: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogMessage($"Error creating test virus: {ex.Message}");
            }
        }

        /// <summary>
        /// Background worker method that performs the actual file scanning
        /// </summary>
        private void ScanWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CancellationToken cancellationToken = (CancellationToken)e.Argument;
            string directory = selectedDirectory;

            try
            {
                // Get all files recursively
                List<string> allFiles = new List<string>();
                GetAllFiles(directory, allFiles, cancellationToken);

                int totalFiles = allFiles.Count;
                int currentFile = 0;

                foreach (string filePath in allFiles)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        // Calculate MD5 hash
                        string md5Hash = CalculateMD5Hash(filePath);
                        
                        // Check against virus database
                        bool isInfected = VirusDatabase.ContainsKey(md5Hash);
                        
                        string fileName = Path.GetFileName(filePath);
                        string status = isInfected ? "INFECTED" : "Clean";
                        
                        if (isInfected)
                        {
                            threatsFound++;
                            infectedFiles.Add(filePath);
                            string virusName = VirusDatabase[md5Hash];
                            
                            // Report progress with infected file info
                            scanWorker.ReportProgress(
                                (int)((currentFile + 1) * 100.0 / totalFiles),
                                $"Scanning: {fileName} - {status} [{virusName}]"
                            );
                        }
                        else
                        {
                            // Report progress with clean file
                            scanWorker.ReportProgress(
                                (int)((currentFile + 1) * 100.0 / totalFiles),
                                $"Scanning: {fileName} - {status}"
                            );
                        }

                        totalFilesScanned++;
                        currentFile++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Skip files we don't have access to
                        scanWorker.ReportProgress(
                            (int)((currentFile + 1) * 100.0 / totalFiles),
                            $"Access Denied: {Path.GetFileName(filePath)}"
                        );
                        currentFile++;
                    }
                    catch (Exception ex)
                    {
                        // Log other errors but continue scanning
                        scanWorker.ReportProgress(
                            (int)((currentFile + 1) * 100.0 / totalFiles),
                            $"Error scanning {Path.GetFileName(filePath)}: {ex.Message}"
                        );
                        currentFile++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Critical error during scan: {ex.Message}", "Scan Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Recursively gets all files from a directory
        /// </summary>
        private void GetAllFiles(string directory, List<string> fileList, CancellationToken cancellationToken)
        {
            try
            {
                // Add files in current directory
                string[] files = Directory.GetFiles(directory);
                fileList.AddRange(files);

                // Recursively process subdirectories
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    GetAllFiles(subdirectory, fileList, cancellationToken);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip directories we don't have access to
            }
            catch (Exception)
            {
                // Continue with other directories
            }
        }

        /// <summary>
        /// Calculates the MD5 hash of a file
        /// </summary>
        private string CalculateMD5Hash(string filePath)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Progress changed event handler - updates UI during scanning
        /// </summary>
        private void ScanWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            
            if (e.UserState != null)
            {
                LogMessage(e.UserState.ToString());
            }

            UpdateStatusStrip();
        }

        /// <summary>
        /// Run worker completed event handler - called when scan finishes
        /// </summary>
        private void ScanWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStartScan.Enabled = true;
            btnStopScan.Enabled = false;
            btnSelectDirectory.Enabled = true;
            progressBar.Value = 100;

            if (e.Cancelled)
            {
                LogMessage("=== Scan Cancelled ===");
                MessageBox.Show("Scan was cancelled.", "Scan Cancelled", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LogMessage("=== Scan Completed ===");
                
                if (threatsFound > 0)
                {
                    string message = $"Scan completed!\n\n" +
                        $"Total Files Scanned: {totalFilesScanned}\n" +
                        $"Threats Found: {threatsFound}\n\n" +
                        "Would you like to handle the infected files?";
                    
                    DialogResult result = MessageBox.Show(message, "Threats Detected", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.Yes)
                    {
                        HandleInfectedFiles();
                    }
                }
                else
                {
                    MessageBox.Show($"Scan completed successfully!\n\n" +
                        $"Total Files Scanned: {totalFilesScanned}\n" +
                        $"Threats Found: 0", 
                        "Scan Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            UpdateStatusStrip();
        }

        /// <summary>
        /// Handles infected files - gives option to delete or quarantine
        /// </summary>
        private void HandleInfectedFiles()
        {
            if (infectedFiles.Count == 0)
                return;

            Form quarantineForm = new Form
            {
                Text = "Handle Infected Files",
                Size = new System.Drawing.Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = System.Drawing.Color.FromArgb(37, 37, 38),
                ForeColor = System.Drawing.Color.White,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            ListBox listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(30, 30, 30),
                ForeColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            foreach (string file in infectedFiles)
            {
                listBox.Items.Add(file);
            }

            Button btnQuarantine = new Button
            {
                Text = "Quarantine All",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                FlatStyle = FlatStyle.Flat,
                ForeColor = System.Drawing.Color.White
            };

            Button btnDelete = new Button
            {
                Text = "Delete All",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = System.Drawing.Color.FromArgb(196, 43, 28),
                FlatStyle = FlatStyle.Flat,
                ForeColor = System.Drawing.Color.White
            };

            Button btnCancel = new Button
            {
                Text = "Cancel",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = System.Drawing.Color.FromArgb(45, 45, 48),
                FlatStyle = FlatStyle.Flat,
                ForeColor = System.Drawing.Color.White
            };

            btnQuarantine.Click += (s, e) =>
            {
                QuarantineFiles(infectedFiles);
                quarantineForm.Close();
            };

            btnDelete.Click += (s, e) =>
            {
                if (MessageBox.Show("Are you sure you want to delete all infected files? This action cannot be undone.", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteFiles(infectedFiles);
                    quarantineForm.Close();
                }
            };

            btnCancel.Click += (s, e) => quarantineForm.Close();

            quarantineForm.Controls.Add(listBox);
            quarantineForm.Controls.Add(btnQuarantine);
            quarantineForm.Controls.Add(btnDelete);
            quarantineForm.Controls.Add(btnCancel);

            quarantineForm.ShowDialog(this);
        }

        /// <summary>
        /// Moves infected files to the quarantine folder
        /// </summary>
        private void QuarantineFiles(List<string> files)
        {
            int quarantined = 0;
            foreach (string filePath in files)
            {
                try
                {
                    string fileName = Path.GetFileName(filePath);
                    string quarantinePath = Path.Combine(quarantineFolder, fileName);
                    
                    // If file already exists in quarantine, add a number
                    int counter = 1;
                    while (File.Exists(quarantinePath))
                    {
                        string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);
                        quarantinePath = Path.Combine(quarantineFolder, $"{nameWithoutExt}_{counter}{extension}");
                        counter++;
                    }

                    File.Move(filePath, quarantinePath);
                    quarantined++;
                    LogMessage($"Quarantined: {fileName}");
                }
                catch (Exception ex)
                {
                    LogMessage($"Error quarantining {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            MessageBox.Show($"{quarantined} file(s) moved to quarantine.", "Quarantine Complete", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Deletes infected files
        /// </summary>
        private void DeleteFiles(List<string> files)
        {
            int deleted = 0;
            foreach (string filePath in files)
            {
                try
                {
                    File.Delete(filePath);
                    deleted++;
                    LogMessage($"Deleted: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    LogMessage($"Error deleting {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            MessageBox.Show($"{deleted} file(s) deleted.", "Delete Complete", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Logs a message to the ListBox
        /// </summary>
        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogMessage), message);
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            lstScanLog.Items.Add($"[{timestamp}] {message}");
            lstScanLog.TopIndex = lstScanLog.Items.Count - 1; // Auto-scroll to bottom
        }

        /// <summary>
        /// Updates the status strip with current scan statistics
        /// </summary>
        private void UpdateStatusStrip()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateStatusStrip));
                return;
            }

            toolStripStatusLabel1.Text = $"Total Files Scanned: {totalFilesScanned}";
            toolStripStatusLabel2.Text = $"Threats Found: {threatsFound}";
        }
    }
}


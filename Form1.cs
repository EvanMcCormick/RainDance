using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Raindance
{
    public partial class Form1 : Form
    {
        private Configuration config;
        public ILogger Logger { get; set; }

        public Form1()
        {
            InitializeComponent();
            LoadConfiguration(); // Call the configuration loading method in the constructor
        }

        private void LoadConfiguration()
        {
            string configFile = "raindance.json";
            if (File.Exists(configFile))
            {
                string json = File.ReadAllText(configFile);
                config = JsonConvert.DeserializeObject<Configuration>(json);
            }
            else
            {
                config = new Configuration(); // Create new configuration if file doesn't exist
            }

            // Update form controls with loaded configuration
            txt_IhubRepoPath.Text = config.IhubRepoPath;
            // Add more controls as needed
        }

        private void SaveConfiguration()
        {
            string json = JsonConvert.SerializeObject(config);
            Debug.WriteLine("Serialized JSON: " + json); // Check if JSON serialization is successful
            try
            {
                File.WriteAllText("raindance.json", json);
                Debug.WriteLine("Configuration saved successfully."); // Check if saving the file is successful
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error saving configuration: " + ex.Message); // Output any exceptions
            }
        }

        private void txt_IhubRepoPath_TextChanged(object sender, EventArgs e)
        {
            config.IhubRepoPath = txt_IhubRepoPath.Text;
            SaveConfiguration();
        }

        // Define the Configuration class here
        public class Configuration
        {
            public string IhubRepoPath { get; set; }
            public bool KillVSC { get; set; }
            // Add more properties as needed
        }

        private void clb_stop_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btn_selectall_Click(object sender, EventArgs e)
        {
            SetCheckStateForAllItems(clb_stop, true);
            SetCheckStateForAllItems(clb_delete, true);
            SetCheckStateForAllItems(clb_run, true);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCheckStateForAllItems(clb_stop, false);
            SetCheckStateForAllItems(clb_delete, false);
            SetCheckStateForAllItems(clb_run, false);
        }

        private void SetCheckStateForAllItems(CheckedListBox listBox, bool checkState)
        {
            // Set the checked state for all items in the list
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                listBox.SetItemChecked(i, checkState);
            }
        }

        private void KillSelectedProcesses()
        {
            foreach (string processName in clb_stop.CheckedItems)
            {
                try
                {
                    // Retrieve all processes matching the name
                    Process[] processes = Process.GetProcessesByName(processName);
                    int terminationCount = 0;

                    // Terminate all instances and count the number of terminations
                    foreach (Process process in processes)
                    {
                        process.Kill();
                        terminationCount++;
                    }

                    // Report summary based on the count of terminations
                    if (terminationCount > 0)
                    {
                        Logger.LogInformation(
                            $"Terminated {terminationCount} instance(s) of: {processName}"
                        );
                    }
                    else
                    {
                        Logger.LogInformation($"No processes found for: {processName}");
                    }
                }
                catch (Exception ex)
                {
                    // Log errors while attempting to terminate processes
                    Logger.LogError(ex, $"Error terminating {processName}");
                }
            }
        }

        private void RunCommandsInRepositoryPath()
        {
            // Get the repository path from the configuration
            string repositoryPath = config.IhubRepoPath;
            // Check if the path is valid
            if (Directory.Exists(repositoryPath))
            {
                // Get the list of commands to run
                foreach (string command in clb_run.CheckedItems)
                {
                    try
                    {
                        // Log the command that is being run
                        Logger.LogInformation($"Command: {command}");

                        // Create a new process to run the command
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c {command}",
                            WorkingDirectory = repositoryPath,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true,
                            RedirectStandardError = true,
                            StandardErrorEncoding = System.Text.Encoding.UTF8,
                            StandardOutputEncoding = System.Text.Encoding.UTF8
                        };
                        Process process = new Process { StartInfo = startInfo };
                        process.ErrorDataReceived += (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                Logger.LogError(args.Data);
                            }
                        };
                        process.OutputDataReceived += (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                Logger.LogInformation(args.Data);
                            }
                        };
                        process.Start();
                        process.BeginOutputReadLine();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        // Log the error message
                        Logger.LogError(ex, $"Error running command {command}");
                    }
                }
            }
            else
            {
                // Log an error if the repository path is invalid
                Logger.LogError("Invalid repository path.");
            }
        }

        private async void btn_raindance_Click(object sender, EventArgs e)
        {
            // clear the terminal
            rtxt_terminal.Clear();

            // if nothing is checked, log a warning and return
            if (clb_stop.CheckedItems.Count == 0 && clb_delete.CheckedItems.Count == 0 && clb_run.CheckedItems.Count == 0)
            {
                Logger.LogWarning("No actions selected. Please select actions to perform.");
                return;
            }

            await Task.Run(() =>
            {
                // Perform the selected actions
                KillSelectedProcesses();
                RunCommandsInRepositoryPath();
            });
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            var result = fbdlg_RepoPath.ShowDialog();

            if (result == DialogResult.OK)
            {
                config.IhubRepoPath = fbdlg_RepoPath.SelectedPath;
                txt_IhubRepoPath.Text = config.IhubRepoPath;
                SaveConfiguration();
            }
        }
    }
}

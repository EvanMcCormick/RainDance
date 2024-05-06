using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Raindance
{
    public partial class Form1 : Form
    {
        private Configuration config;

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

        private void clb_stop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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
            System.Text.StringBuilder terminationResults = new System.Text.StringBuilder();

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
                        terminationResults.AppendLine($"Terminated {terminationCount} instance(s) of: {processName}");
                    }
                    else
                    {
                        terminationResults.AppendLine($"No processes found for: {processName}");
                    }
                }
                catch (Exception ex)
                {
                    // Log errors while attempting to terminate processes
                    terminationResults.AppendLine($"Error terminating {processName}: {ex.Message}");
                }
            }

            // Display the accumulated results in the terminal-like TextBox
            txt_terminal.Text = terminationResults.ToString();
        }


        private void RunCommandsInRepositoryPath(){
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
                        // Create a new process to run the command
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c {command}",
                            WorkingDirectory = repositoryPath,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        };
                        Process process = new Process { StartInfo = startInfo };
                        process.Start();
                        // Read the output of the process
                        string output = process.StandardOutput.ReadToEnd();
                        txt_terminal.Text += $"Command: {command}\n{output}\n";
                    }
                    catch (Exception ex)
                    {
                        txt_terminal.Text += $"Error running command {command}: {ex.Message}\n";
                    }
                }
            }
            else
            {
                txt_terminal.Text += "Invalid repository path.\n";
            }
        }

        private void RunCommandsInRepositoryPath(){
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
                        // Create a new process to run the command
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c {command}",
                            WorkingDirectory = repositoryPath,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        };
                        Process process = new Process { StartInfo = startInfo };
                        process.Start();
                        // Read the output of the process
                        string output = process.StandardOutput.ReadToEnd();
                        txt_terminal.Text += $"Command: {command}\n{output}\n";
                    }
                    catch (Exception ex)
                    {
                        txt_terminal.Text += $"Error running command {command}: {ex.Message}\n";
                    }
                }
            }
            else
            {
                txt_terminal.Text += "Invalid repository path.\n";
            }
        }

        private void btn_raindance_Click(object sender, EventArgs e)
        {
            KillSelectedProcesses();
            RunCommandsInRepositoryPath();
        }
    }

}

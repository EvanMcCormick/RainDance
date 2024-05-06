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

    }
 
}

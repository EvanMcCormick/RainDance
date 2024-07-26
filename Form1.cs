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
        private ProcessManager processManager;
        private bool isInitializing = true;

        public Form1()
        {
            InitializeComponent();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                object value = builder.AddConsole();
            });
            Logger = loggerFactory.CreateLogger<Form1>();

            LoadConfiguration(); 

            processManager = new ProcessManager(config, Logger, clb_stop, clb_delete, clb_run);
            clb_stop.ItemCheck += OnItemCheck;
            clb_delete.ItemCheck += OnItemCheck;
            clb_run.ItemCheck += OnItemCheck;
            txt_IhubRepoPath.TextChanged += Txt_IhubRepoPath_TextChanged;

            isInitializing = false; 
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
                config = new Configuration();
            }

            txt_IhubRepoPath.Text = config.IhubRepoPath;
            LoadCheckedListBox(clb_stop, config.StopItems);
            LoadCheckedListBox(clb_delete, config.DeleteItems);
            LoadCheckedListBox(clb_run, config.RunItems);
        }

        private void LoadCheckedListBox(CheckedListBox listBox, System.Collections.Generic.List<ItemWithCheckState> items)
        {
            listBox.Items.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    int index = listBox.Items.Add(item.Name);
                    listBox.SetItemChecked(index, item.IsChecked);
                }
            }
        }

        private void SaveConfiguration()
        {
            config.StopItems = GetCheckedListBoxItems(clb_stop);
            config.DeleteItems = GetCheckedListBoxItems(clb_delete);
            config.RunItems = GetCheckedListBoxItems(clb_run);
            config.IhubRepoPath = txt_IhubRepoPath.Text;

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
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

        private System.Collections.Generic.List<ItemWithCheckState> GetCheckedListBoxItems(CheckedListBox listBox)
        {
            var items = new System.Collections.Generic.List<ItemWithCheckState>();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var item = new ItemWithCheckState
                {
                    Name = listBox.Items[i].ToString(),
                    IsChecked = listBox.GetItemChecked(i)
                };
                items.Add(item);
            }
            return items;
        }
        private void OnItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isInitializing) return; 
            var listBox = sender as CheckedListBox;
            listBox.BeginInvoke((MethodInvoker)delegate
            {
                UpdateCheckState(listBox, e.Index, e.NewValue == CheckState.Checked);
                SaveConfiguration();
            });
        }

        private void UpdateCheckState(CheckedListBox listBox, int index, bool isChecked)
        {
            if (listBox == clb_stop)
            {
                config.StopItems[index].IsChecked = isChecked;
            }
            else if (listBox == clb_delete)
            {
                config.DeleteItems[index].IsChecked = isChecked;
            }
            else if (listBox == clb_run)
            {
                config.RunItems[index].IsChecked = isChecked;
            }
        }

        private void Txt_IhubRepoPath_TextChanged(object sender, EventArgs e)
        {
            if (isInitializing) return; // Prevent saving during initialization

            config.IhubRepoPath = txt_IhubRepoPath.Text;
            SaveConfiguration();
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
                processManager.KillSelectedProcesses();
                processManager.DeleteSelectedFolders();
                processManager.RunCommandsInRepositoryPath();
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

        private void clb_delete_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}

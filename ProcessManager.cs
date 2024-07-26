using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace Raindance
{
    public class ProcessManager
    {
        private readonly Configuration config;
        private readonly ILogger logger;
        private readonly CheckedListBox clb_stop;
        private readonly CheckedListBox clb_delete;
        private readonly CheckedListBox clb_run;

        public ProcessManager(Configuration config, ILogger logger, CheckedListBox clb_stop, CheckedListBox clb_delete, CheckedListBox clb_run)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.clb_stop = clb_stop ?? throw new ArgumentNullException(nameof(clb_stop));
            this.clb_delete = clb_delete ?? throw new ArgumentNullException(nameof(clb_delete));
            this.clb_run = clb_run ?? throw new ArgumentNullException(nameof(clb_run));
        }

        public void KillSelectedProcesses()
        {
            foreach (string processName in clb_stop.CheckedItems)
            {
                logger.LogInformation($"Looking for instance(s) of: {processName}");
                try
                {
                    Process[] processes = Process.GetProcessesByName(processName);
                    int terminationCount = 0;

                    foreach (Process process in processes)
                    {
                        process.Kill();
                        terminationCount++;
                    }

                    if (terminationCount > 0)
                    {
                        logger.LogInformation($"Terminated {terminationCount} instance(s) of: {processName}");
                    }
                    else
                    {
                        logger.LogInformation($"No processes found for: {processName}");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error terminating {processName}");
                }
            }
        }

        public void DeleteSelectedFolders()
        {
            if (string.IsNullOrEmpty(config.IhubRepoPath) || !Directory.Exists(config.IhubRepoPath))
            {
                logger.LogError("Invalid repository path.");
                return;
            }

            foreach (string folderName in clb_delete.CheckedItems)
            {
                logger.LogInformation($"Looking for the {folderName} folder");
                string folderPath = Path.Combine(config.IhubRepoPath, folderName);
                if (Directory.Exists(folderPath))
                {
                    try
                    {
                        Directory.Delete(folderPath, true);
                        logger.LogInformation($"Deleted folder: {folderName}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error deleting folder: {folderName}");
                    }
                }
                else
                {
                    logger.LogWarning($"Folder not found: {folderName}");
                }
            }
        }

        public void RunCommandsInRepositoryPath()
        {
            string repositoryPath = config.IhubRepoPath;
            if (Directory.Exists(repositoryPath))
            {
                foreach (string command in clb_run.CheckedItems)
                {
                    try
                    {
                        logger.LogInformation($"Command: {command}");

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
                                logger.LogError(args.Data);
                            }
                        };
                        process.OutputDataReceived += (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                logger.LogInformation(args.Data);
                            }
                        };
                        process.Start();
                        process.BeginOutputReadLine();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error running command {command}");
                    }
                }
            }
            else
            {
                logger.LogError("Invalid repository path.");
            }
        }
    }
}

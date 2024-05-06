using Microsoft.Extensions.Logging;
using RainDance.Services.Logging;
using System;
using System.Windows.Forms;

namespace Raindance
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configure logging
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                // You can add other logging configurations here if needed
            });

            // Initialize the main form
            var mainForm = new Form1();
            // Assuming you have a public TextBox named LogTextBox in Form1
            var loggerProvider = new TextBoxLoggerProvider(mainForm.rtxt_terminal);
            loggerFactory.AddProvider(loggerProvider);

            // Set up logger for use within Form1 or elsewhere
            mainForm.Logger = loggerFactory.CreateLogger("MainForm");

            // Run the application
            Application.Run(mainForm);
        }
    }
}
